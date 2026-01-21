using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Utility;

public partial class AdminPanel_SchoolAdmin_Modify : AdminAuthentication
{
    #region Private Members
    private int _ID = 0;
    TeachersBAL objTeachersBAL = new TeachersBAL();
    protected string strPages = string.Empty;
    protected string strPasswordEdit = string.Empty;
    string strImageName = string.Empty;
    protected int GoogleUser = 0;
    #endregion

    #region Public Members
    public new int ID
    {
        get
        {
            return _ID;
        }
    }
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        Int32.TryParse(Request["id"], out _ID);
        objTeachersBAL.ID = ID;

        if (Request.Form.Keys.Count > 0)
        {
            SaveInfo();
        }
        else
        {
            BindSchoool();
            BindPages();
            if (objTeachersBAL.ID != 0)
            {
                spnHeader.InnerHtml = "Edit School Admin";
                // tbPassword.Visible = false;
                tbPassword.Visible = true;
                BindControls();
            }
            else
            {
                tbPassword.Visible = true;
            }
        }
        Master.SelectedSection = AdminPanel_Admin.Section.General;

    }
    #endregion

    #region Bind Controls
    private void BindSchoool()
    {
        SchoolBAL objSchool = new SchoolBAL();
        objSchool.Name = "";
        int intTotalRecord = 0;
        DataTable dtblSchoolList = new DataTable();
        dtblSchoolList = objSchool.GetList(ref CurrentPage, 10000, out intTotalRecord, "Name", "ASC");

        if (dtblSchoolList.Rows.Count > 0)
        {
            ddlSchool.DataSource = dtblSchoolList;
            ddlSchool.DataTextField = "Name";
            ddlSchool.DataValueField = "ID";
            ddlSchool.DataBind();
        }
        ddlSchool.Items.Insert(0, new ListItem("-- Select --", ""));
    }
    private void BindPages()
    {
        TeachersBAL objTeachersBAL = new TeachersBAL();
        DataTable dt = new DataTable();
        dt = objTeachersBAL.PagesList(0);
        if (dt.Rows.Count > 0)
        {
            rptPages.DataSource = dt;
            rptPages.DataBind();
        }
    }

    private void BindControls()
    {
        DataTable dt = new DataTable();
        objTeachersBAL.ID = ID;
        dt = objTeachersBAL.SchoolAdminGetByID();

        if (dt.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ImageName"])))
            {
                hdnImage.Value = Convert.ToString(dt.Rows[0]["ImageName"]);
                imgImage.Src = Config.VirtualDir + "thumb.aspx?path=" + Config.CMSFiles + Convert.ToString(dt.Rows[0]["ImageName"]) + "&width=80";
                imgImage.Visible = true;
            }
            else
            {
                imgImage.Visible = false;
                hdnImage.Value = string.Empty;
            }

            tbxFirstName.Value = Convert.ToString(dt.Rows[0]["FirstName"]).Trim();
            tbxLastName.Value = Convert.ToString(dt.Rows[0]["LastName"]).Trim();
            tbxEmail.Value = Convert.ToString(dt.Rows[0]["EmailID"]).Trim();
            tbxphone.Value = Convert.ToString(dt.Rows[0]["Phone"]).Trim();
            //trConfirmPwd.Visible = false;
            trConfirmPwd.Visible = true;
            divlblPwd.Visible = false;
            //  divPwd.Style.Add("display", "none");

            string strPwd = "";
            if (Convert.ToString(dt.Rows[0]["Password"]) != string.Empty)
            {
                strPwd = Utility.Security.EncryptDescrypt.DecryptString(Convert.ToString(dt.Rows[0]["Password"]));
                tbxPassword.Value = strPwd;
                tbxConfPassword.Value = strPwd;
                hdnpwd.Value = Convert.ToString(dt.Rows[0]["Password"]).Trim();
            }

            strPasswordEdit = strPwd;


            lblPassword.InnerText = strPwd;
            lblPassword.Visible = false;
            //tbxPassword.Attributes.Add("value", strPwd);
            //tbxPassword.Attributes.Add("type", "text");
            ddlSchool.Value = Convert.ToString(dt.Rows[0]["SchoolID"]).Trim();
            strPages = Convert.ToString(dt.Rows[0]["Modules"]).Trim();

            ddlStatus.Value = Convert.ToString(dt.Rows[0]["Status"]).Trim();

            if (Convert.ToString(dt.Rows[0]["GoogleID"]) != string.Empty)
            {
                GoogleUser = 1;
            }

            if (Convert.ToString(dt.Rows[0]["IsAPIAccess"]).ToLower() == "true")
            {
                chkAPIAccess.Checked = true;
            }
        }
    }
    #endregion

    #region Save Information
    private void SaveInfo()
    {
        objTeachersBAL.ID = ID;
        string strPasword = string.Empty;

        if (!string.IsNullOrEmpty(Request.Form[tbxPassword.UniqueID]))
        {
            strPasword = Request.Form[tbxPassword.UniqueID];
        }
        else
        {
            if (Convert.ToString(Request.Form[hdnpwd.UniqueID]) != string.Empty)
                strPasword = Utility.Security.EncryptDescrypt.DecryptString(Request.Form[hdnpwd.UniqueID]);
        }

        if (Request.Files.Count > 0)    // if (Request.Files[fupdImage.UniqueID] != null)
        {
            if (Request.Files[0].ContentLength > 0)
            {
                strImageName = System.IO.Path.GetExtension(Request.Files[0].FileName);

                if (IsImage(Request.Files[0].FileName.ToLower()) == false)
                {
                    Response.Write("<script>DisplMsg('<%= divMsg.ClientID %>','Please upload only .JPG, .JPEG, .PNG, .BMP, .GIF image files.','alert-message error');</script>");
                }
                strImageName = Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Date.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Ticks) + strImageName;
                if (ID != 0)
                {
                    DeleteGalleryFile(Convert.ToString(Request[hdnCategoryFile.UniqueID]));
                }
                Request.Files[0].SaveAs(Request.PhysicalApplicationPath + "/" + Config.CMSFiles + strImageName);
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(Request.Form[hdnImage.UniqueID]))
            {
                strImageName = Request.Form[hdnImage.UniqueID];
            }
            else
            {
                strImageName = string.Empty;
            }
        }

        objTeachersBAL.ImageName = strImageName;
        objTeachersBAL.FirstName = Request.Form[tbxFirstName.UniqueID].Trim();
        objTeachersBAL.LastName = Request.Form[tbxLastName.UniqueID].Trim();
        if (strPasword != string.Empty)
        {
            objTeachersBAL.Password = Utility.Security.EncryptDescrypt.EncryptString(strPasword);
        }
        else {
            objTeachersBAL.Password = "";
        }
        objTeachersBAL.EmailID = Request.Form[tbxEmail.UniqueID].Trim();
        objTeachersBAL.Phone = Request.Form[tbxphone.UniqueID].Trim();
        objTeachersBAL.SchoolID = Convert.ToInt32(Request.Form[ddlSchool.UniqueID]);

        string assignedModules = "";
        int IsAPIAccess = 0;
        if (Request[chkAPIAccess.UniqueID] != null)
        {
            IsAPIAccess = 1;
        }

        assignedModules = Convert.ToString(Request[hdnPages.UniqueID]);
        switch (objTeachersBAL.SchoolAdminSave(Convert.ToInt64(Session["UserID"]), assignedModules, Convert.ToInt32(Request[ddlStatus.UniqueID]), IsAPIAccess))
        {
            case 0:
                ShowMessage("Duplicate Email Address found.", "alert alert-danger error", divMsg.ClientID);
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                break;
            default:
                ShowMessage("School Admin has been saved successfully.", "alert alert-success", divMsg.ClientID);
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                Response.Write(Common.ScriptStartTag + "parent.window.setTimeout(\"parent.window.location.href = 'school-admin-list.aspx'\",2000);" + Common.ScriptEndTag);
                break;
        }
        Response.End();
    }
    #endregion

    #region General

    private void ShowMessage(string strMessage, string strMessageType, string divMessage)
    {
        Response.Write("<link href='" + Config.VirtualDir + "style/style.css' rel='Stylesheet' type='text/css' />");
        Response.Write("<script type='text/javascript' language='javascript' src='" + Config.VirtualDir + "js/jquery-1.10.2.min.js'></script>");
        Response.Write("<script type='text/javascript' language='javascript' src='" + Config.VirtualDir + "js/general.js'></script>");
        Response.Write("<script type='text/javascript'>var virtualDir = '" + Config.VirtualDir + "';</script>");
        Response.Write("$(document.ready(function {");
        Response.Write(Common.ScriptStartTag + "parent.$('#" + divMessage + "').show();" + Common.ScriptEndTag);
        Response.Write(Common.ScriptStartTag + "parent.$('#" + divMessage + "').html('" + strMessage + "');" + Common.ScriptEndTag);
        Response.Write(Common.ScriptStartTag + "parent.$('#" + divMessage + "').attr('class', '" + strMessageType + "');" + Common.ScriptEndTag);
        Response.Write(Javascript.ScriptStartTag + "window.setTimeout(\"parent.$('#" + divMessage + "').fadeOut(600);\",5000)" + Javascript.ScriptEndTag);
        Response.Write("});");
    }
    private void DeleteGalleryFile(string strImage)
    {
        if (!string.IsNullOrEmpty(strImage))
        {
            if (Common.IsFileExist(Config.CMSFiles, strImage))
            {
                Common.FileDelete(Config.CMSFiles, strImage);
            }
        }
    }
    public static bool IsImage(string strImageName)
    {
        bool blnReturnValue = false;
        switch (System.IO.Path.GetExtension(strImageName).ToLower())
        {
            case ".jpg":
            case ".jpeg":
            case ".gif":
            case ".png":
            case ".bmp":
                blnReturnValue = true;
                break;
        }
        return blnReturnValue;
    }

    #endregion
}