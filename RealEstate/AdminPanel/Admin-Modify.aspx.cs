using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Utility;

public partial class AdminPanel_Admin_Modify : AdminAuthentication
{
    #region Private Members
    private int _ID = 0;
    AdminBAL objAdminBAL = new AdminBAL();
    protected string strPages = string.Empty;
    string strImageName = string.Empty;
    protected string strPwd = "";
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
        objAdminBAL.ID = ID;

        if (Request.Form.Keys.Count > 0)
        {
            SaveInfo();
        }
        else
        {
            //  BindDropDowns();
            BindPages();
            if (objAdminBAL.ID != 0)
            {
                spnHeader.InnerHtml = "Edit Admin";
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
    private void BindPages()
    {
        DataTable dt = new DataTable();
        GeneralBAL objGeneralBAL = new GeneralBAL();
        dt = objGeneralBAL.PagesList();
        if (dt.Rows.Count > 0)
        {
            rptPages.DataSource = dt;
            rptPages.DataBind();
        }
    }

    private void BindControls()
    {
        DataTable dtblAdminInfo = new DataTable();
        objAdminBAL.ID = ID;
        dtblAdminInfo = objAdminBAL.GetByID();

        if (dtblAdminInfo.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(dtblAdminInfo.Rows[0]["ImageName"])))
            {
                hdnImage.Value = Convert.ToString(dtblAdminInfo.Rows[0]["ImageName"]);
                imgImage.Src = Config.VirtualDir + "thumb.aspx?path=" + Config.CMSFiles + Convert.ToString(dtblAdminInfo.Rows[0]["ImageName"]) + "&width=80";
                imgImage.Visible = true;
            }
            else
            {
                imgImage.Visible = false;
                hdnImage.Value = string.Empty;
            }

            tbxFirstName.Value = Convert.ToString(dtblAdminInfo.Rows[0]["FirstName"]).Trim();
            tbxLastName.Value = Convert.ToString(dtblAdminInfo.Rows[0]["LastName"]).Trim();
            tbxUserName.Value = Convert.ToString(dtblAdminInfo.Rows[0]["UserName"]).Trim();
            tbxEmail.Value = Convert.ToString(dtblAdminInfo.Rows[0]["EmailID"]).Trim();
            tbxphone.Value = Convert.ToString(dtblAdminInfo.Rows[0]["Phone"]).Trim();
            trConfirmPwd.Visible = false;
            divlblPwd.Visible = true;
            divPwd.Style.Add("display", "none");

            strPwd = Utility.Security.EncryptDescrypt.DecryptString(Convert.ToString(dtblAdminInfo.Rows[0]["Password"]));

            hdnpwd.Value = Convert.ToString(dtblAdminInfo.Rows[0]["Password"]).Trim();
            //lblPassword.InnerText = strPwd;
            lblPassword.InnerText = "*****";
            tbxPassword.Attributes.Add("value", strPwd);
            tbxPassword.Attributes.Add("type", "text");
            string strusertype = Convert.ToString(dtblAdminInfo.Rows[0]["AdminType"]).Trim();

            if (strusertype == "Agent")
            {
                ddlMemberType.Value = "3";
            }
            else if (strusertype == "Team Manager")
            {
                ddlMemberType.Value = "2";
            }
            else
            {
                ddlMemberType.Value = "1";
            }

            strPages = Convert.ToString(dtblAdminInfo.Rows[0]["Pages"]).Trim();
        }
    }
    #endregion

    #region Save Information
    private void SaveInfo()
    {
        objAdminBAL.ID = ID;
        string strPasword = string.Empty;

        if (!string.IsNullOrEmpty(Request.Form[tbxPassword.UniqueID]))
        {
            strPasword = Request.Form[tbxPassword.UniqueID];
        }
        else
        {
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
                // objCategoryBAL.ImageName = strImageName;
            }
            //else
            //{
            //    if (!string.IsNullOrEmpty(Request.Form["hdnImage"]))
            //    {
            //        strImageName = Request.Form["hdnImage"];
            //    }
            //    else
            //    {
            //        strImageName = string.Empty;
            //    }
            //}
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

        objAdminBAL.ImageName = strImageName;
        objAdminBAL.FirstName = Request.Form[tbxFirstName.UniqueID].Trim();
        objAdminBAL.LastName = Request.Form[tbxLastName.UniqueID].Trim();
        objAdminBAL.UserName = Request.Form[tbxUserName.UniqueID].Trim();
        objAdminBAL.Password = Utility.Security.EncryptDescrypt.EncryptString(strPasword);
        objAdminBAL.EmailID = Request.Form[tbxEmail.UniqueID].Trim();
        objAdminBAL.Phone = Request.Form[tbxphone.UniqueID].Trim();
        objAdminBAL.AdminType = Convert.ToInt32(Request.Form[ddlMemberType.UniqueID]);

        string assignedModules = "";

        assignedModules = Convert.ToString(Request[hdnPages.UniqueID]);
        switch (objAdminBAL.Save(Convert.ToInt64(Session["UserID"]), assignedModules))
        {
            case 0:
                ShowMessage("Duplicate Email Address or Username found.", "alert alert-danger error", divMsg.ClientID);
                break;
            default:
                ShowMessage("User information has been saved successfully.", "alert alert-success", divMsg.ClientID);
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                Response.Write(Common.ScriptStartTag + "parent.window.setTimeout(\"parent.window.location.href = 'admin-list.aspx'\",2000);" + Common.ScriptEndTag);
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