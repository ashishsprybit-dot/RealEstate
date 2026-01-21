using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Utility;

public partial class AdminPanel_Tenant_Modify : AdminAuthentication
{
    #region Private Members
    private int _ID = 0;
    string strImageName = string.Empty;
    string strLogo = string.Empty;
    TenantBAL objTenantBAL = new TenantBAL();
    protected string strTenants = "";
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
        objTenantBAL.ID = ID;

        if (Request.Form.Keys.Count > 0)
        {
            SaveInfo();
        }
        else
        {
            //BindCategory();
            if (objTenantBAL.ID != 0)
            {
                spnHeader.InnerHtml = "Edit Tenant";
                BindControls();
            }
        }

        Master.SelectedSection = AdminPanel_Admin.Section.General;
    }
    #endregion
    private void BindCategory()
    {
        DataTable dt = new DataTable();
        CategoryBAL objCategoryBAL = new CategoryBAL();
        dt = objCategoryBAL.CategoryListALL();
        if (dt.Rows.Count > 0)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = "ParentID = 0";
            dv.Sort = "SequenceNo ASC";
            //rptTenants.DataSource = dv;
            //rptTenants.DataBind();
        }
    }
    #region Bind Controls

    private void BindControls()
    {
        DataTable dt = new DataTable();
        objTenantBAL.ID = ID;
        dt = objTenantBAL.GetByID();

        if (dt.Rows.Count > 0)
        {
            //if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ImageName"])))
            //{
            //    hdnImage.Value = Convert.ToString(dt.Rows[0]["ImageName"]);
            //    imgImage.Src = Config.VirtualDir + "thumb.aspx?path=" + Config.CMSFiles + Convert.ToString(dt.Rows[0]["ImageName"]) + "&width=80";
            //    imgImage.Visible = true;
            //}
            //else
            //{
            //    imgImage.Visible = false;
            //    hdnImage.Value = string.Empty;
            //}

            if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["TenantLogo"])))
            {
                hdnLogo.Value = Convert.ToString(dt.Rows[0]["TenantLogo"]);
                imgLogo.Src = Config.VirtualDir + "thumb.aspx?path=" + Config.CMSFiles + Convert.ToString(dt.Rows[0]["TenantLogo"]) + "&width=80";
                imgLogo.Visible = true;
            }
            else
            {
                imgLogo.Visible = false;
                hdnLogo.Value = string.Empty;
            }
            tbxTenantURL.Value = Convert.ToString(dt.Rows[0]["TenantURL"]).Trim();
            tbxTenantName.Value = Convert.ToString(dt.Rows[0]["Name"]).Trim();
            //tbxCasesCode.Value = Convert.ToString(dt.Rows[0]["CasesCode"]).Trim();
            strTenants = "";// Convert.ToString(dt.Rows[0]["TenantCategory"]).Trim();
        }
    }
    #endregion

    #region Save Information
    private void SaveInfo()
    {
        objTenantBAL.ID = ID;

        //if (Request.Files[fupdImage.UniqueID].ContentLength > 0)
        //{
        //    strImageName = System.IO.Path.GetExtension(Request.Files[fupdImage.UniqueID].FileName);

        //    //if (IsImage(Request.Files[fupdImage.UniqueID].FileName.ToLower()) == false)
        //    //{
        //    //    Response.Write("<script>DisplMsg('<%= divMsg.ClientID %>','Please upload only .JPG, .JPEG, .PNG, .BMP, .GIF image files.','alert-message error');</script>");
        //    //}
        //    strImageName = Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Date.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Ticks) + strImageName;
        //    //if (ID != 0)
        //    //{
        //    //    DeleteGalleryFile(Convert.ToString(Request[hdnCategoryFile.UniqueID]));
        //    //}
        //    Request.Files[fupdImage.UniqueID].SaveAs(Request.PhysicalApplicationPath + "/" + Config.CMSFiles + strImageName);

        //}
        //else
        //{
        //    if (!string.IsNullOrEmpty(Request.Form[hdnImage.UniqueID]))
        //    {
        //        strImageName = Request.Form[hdnImage.UniqueID];
        //    }
        //    else
        //    {
        //        strImageName = string.Empty;
        //    }
        //}

        if (Request.Files[fupdLogo.UniqueID].ContentLength > 0)
        {
            strLogo = System.IO.Path.GetExtension(Request.Files[fupdLogo.UniqueID].FileName);

            //if (IsImage(Request.Files[fupdLogo.UniqueID].FileName.ToLower()) == false)
            //{
            //    Response.Write("<script>DisplMsg('<%= divMsg.ClientID %>','Please upload only .JPG, .JPEG, .PNG, .BMP, .GIF image files.','alert-message error');</script>");
            //}
            strLogo = Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Date.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Ticks) + strLogo;
            //if (ID != 0)
            //{
            //    DeleteGalleryFile(Convert.ToString(Request[hdnCategoryFile.UniqueID]));
            //}
            Request.Files[fupdLogo.UniqueID].SaveAs(Request.PhysicalApplicationPath + "/" + Config.CMSFiles + strLogo);

        }
        else
        {
            if (!string.IsNullOrEmpty(Request.Form[hdnLogo.UniqueID]))
            {
                strLogo = Request.Form[hdnLogo.UniqueID];
            }
            else
            {
                strLogo = string.Empty;
            }
        }


        objTenantBAL.ImageName = strImageName;
        objTenantBAL.Logo = strLogo;
        objTenantBAL.Name = Request.Form[tbxTenantName.UniqueID].Trim();
        // objTenantBAL.CasesCode = Request.Form[tbxCasesCode.UniqueID].Trim();
        string TenantCategory = Convert.ToString(Request[hdnTenants.UniqueID]);
        switch (objTenantBAL.Save(Convert.ToInt64(Session["UserID"]), Convert.ToString(Request[tbxTenantURL.UniqueID]), TenantCategory))
        {
            case -1:
                ShowMessage("Duplicate Name found.", "alert alert-danger error", divMsg.ClientID);
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                break;
            case -2:
                ShowMessage("Duplicate Cases21 Code found.", "alert alert-danger error", divMsg.ClientID);
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                break;
            default:
                ShowMessage("Tenant details has been saved successfully.", "alert alert-success", divMsg.ClientID);
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                Response.Write(Common.ScriptStartTag + "parent.window.setTimeout(\"parent.window.location.href = 'tenant-list.aspx'\",2000);" + Common.ScriptEndTag);
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