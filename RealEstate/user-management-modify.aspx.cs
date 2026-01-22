using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Utility;

public partial class Main_Teacher_Modify : System.Web.UI.Page
{
    #region Private Members
    protected string SchoolURL = "";
    private int _ID = 0;
    UserManagementBAL objUserManagementBAL = new UserManagementBAL();
    protected string strPages = string.Empty;
    protected string strModules = string.Empty;
    protected DataTable dtModules = new DataTable();

    string strImageName = string.Empty;
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
        objUserManagementBAL.ID = ID;
        SchoolURL = this.Master.RequestedSchoolURL;
        if (Request.Form.Keys.Count > 0)
        {
            SaveInfo();
        }
        else
        {
            //BindPages();
           // BindModules();
            if (objUserManagementBAL.ID != 0)
            {
                spnHeader.InnerHtml = "Edit Tenant Users";
                tbPassword.Visible = false;
                BindControls();
            }
            else
            {
                tbPassword.Visible = true;
            }
        }
        hdnSchoolURL.Value = this.Master.RequestedSchoolURL;
        //  Master.SelectedSection = AdminPanel_Admin.Section.General;

    }
    #endregion

    #region Bind Controls
    //private void BindModules()
    //{
    //    TeachersBAL objTeachersBAL = new TeachersBAL();
    //    DataTable dt = new DataTable();
    //    dtModules = objTeachersBAL.PagesList(0);
    //    DataView dv = new DataView(dtModules);
    //    dv.Sort = "MainHeading DESC";
    //    dt = dv.ToTable(true, "MainHeading");                    

    //    if (dt.Rows.Count > 0)
    //    {
    //       // rptModules.DataSource = dt;
    //       // rptModules.DataBind();
    //    }
    //}
    protected DataTable GetSubHeading(string MainHeading)
    {
        DataTable dt = new DataTable();
        DataView dv = new DataView(dtModules);
        dv.RowFilter = "MainHeading='" + MainHeading + "'";
        dv.Sort = "SubHeading ASC";
        return dv.ToTable(true, "SubHeading", "MainHeading");
    }
    protected DataTable GetSubModules(string MainHeading, string SubHeading)
    {
        DataTable dt = new DataTable();
        DataView dv = new DataView(dtModules);
        dv.RowFilter = "MainHeading='"+ MainHeading + "' AND SubHeading='" + SubHeading + "'";
        dv.Sort = "RANK ASC";
        return dv.ToTable();
    }
    //private void BindPages()
    //{
    //    int intTotalRecord = 0;
    //    HomeGroupsBAL objHomeGroups = new HomeGroupsBAL();
    //    DataTable dt = new DataTable();
    //    DataSet ds = new DataSet();
    //    int CurrentPage = 1;
    //    ds = objHomeGroups.GetList(ref CurrentPage, 10000, out intTotalRecord, "KGCKEY", "ASC", SchoolURL, Convert.ToInt32(Session["TeacherUserID"]), 0);
    //    dt = ds.Tables[0];

    //    if (dt.Rows.Count > 0)
    //    {
    //        //rptPages.DataSource = dt;
    //        //rptPages.DataBind();
    //    }
    //}

    private void BindControls()
    {
        DataTable dt = new DataTable();
        objUserManagementBAL.ID = ID;
        dt = objUserManagementBAL.GetByID(SchoolURL);

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
            ddlStatus.Value = Convert.ToString(dt.Rows[0]["Status"]).Trim();
            trConfirmPwd.Visible = false;
            divlblPwd.Visible = true;
            divPwd.Style.Add("display", "none");

            if (Convert.ToString(dt.Rows[0]["Password"]) != string.Empty)
            {
                string strPwd = Utility.Security.EncryptDescrypt.DecryptString(Convert.ToString(dt.Rows[0]["Password"]));

                hdnpwd.Value = Convert.ToString(dt.Rows[0]["Password"]).Trim();
                lblPassword.InnerText = strPwd;
                tbxPassword.Attributes.Add("value", strPwd);
                tbxPassword.Attributes.Add("type", "text");
            }
            strPages = Convert.ToString(dt.Rows[0]["Pages"]).Trim();
            strModules = Convert.ToString(dt.Rows[0]["Modules"]).Trim();
        }
    }
    #endregion

    #region Save Information
    private void SaveInfo()
    {
        objUserManagementBAL.ID = ID;
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

        objUserManagementBAL.ImageName = strImageName;
        objUserManagementBAL.FirstName = Request.Form[tbxFirstName.UniqueID].Trim();
        objUserManagementBAL.LastName = Request.Form[tbxLastName.UniqueID].Trim();

        if (strPasword != string.Empty)
        {
            objUserManagementBAL.Password = Utility.Security.EncryptDescrypt.EncryptString(strPasword);
        }
        else
        {
            objUserManagementBAL.Password = "";
        }
        objUserManagementBAL.EmailID = Request.Form[tbxEmail.UniqueID].Trim();
        objUserManagementBAL.Phone = Request.Form[tbxphone.UniqueID].Trim();
        objUserManagementBAL.Status = Convert.ToInt32(Request.Form[ddlStatus.UniqueID]);

        string assignedModules = "";
        assignedModules = Convert.ToString(Request[hdnPages.UniqueID]);

        string assignedPageAccess = "";
        assignedPageAccess = Convert.ToString(Request[hdnModulePages.UniqueID]);



        switch (objUserManagementBAL.Save(Convert.ToInt64(Session["TeacherUserID"]), assignedModules, assignedPageAccess, Convert.ToString(Request[hdnSchoolURL.UniqueID])))
        {
            case 0:
                ShowMessage("Duplicate Email Address found.", "alert alert-danger error", divMsg.ClientID);
                break;
            default:
                ShowMessage("Tenant information has been saved successfully.", "alert alert-success", divMsg.ClientID);
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                Response.Write(Common.ScriptStartTag + "parent.window.setTimeout(\"parent.window.location.href = 'user-management-list.aspx'\",2000);" + Common.ScriptEndTag);
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