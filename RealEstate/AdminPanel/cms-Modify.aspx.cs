using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

public partial class AdminPanel_Cms_Modify : AdminAuthentication
{
    CMSBAL objCMSBAL = new CMSBAL();

    #region Private Members
    private int _ID = 0;
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
        objCMSBAL.ID = ID;

        if (Request.Form.Keys.Count > 0)
        { SaveInfo(); }
        else
        {
            if (objCMSBAL.ID != 0)
            { BindControls(); }
        }

    }
    #endregion

    #region Bind Controls
    private void BindControls()
    {
        if (ID > 0)
        {
            DataTable dt = new DataTable();
            objCMSBAL.ID = ID;
            dt = objCMSBAL.GetCMSByID();
            if (dt.Rows.Count > 0)
            {
                tbxTitle.Value = Convert.ToString(dt.Rows[0]["PageTitle"]);
                tbxURL.Value = Convert.ToString(dt.Rows[0]["PageUrl"]);
                System.Text.StringBuilder sbTemplate = new System.Text.StringBuilder(Convert.ToString(dt.Rows[0]["PageDescription"]));
                sbTemplate.Replace("{%WebSiteUrl%}", Config.WebSiteUrl);
                txtDescription.Text = sbTemplate.ToString();
                txtMetaTitle.Value = Convert.ToString(dt.Rows[0]["CMSMetaTitle"]);
                txtMetaKeyword.Value = Convert.ToString(dt.Rows[0]["CMSMetaKeyword"]);
                txtMetaDescription.Value = Convert.ToString(dt.Rows[0]["CMSMetaDescription"]);



                txtHeadText.Value = Convert.ToString(dt.Rows[0]["HeadText"]);
                txtBodyFooterText.Value = Convert.ToString(dt.Rows[0]["BodyFooterText"]);
                //chkEnquiryForm.Checked = Convert.ToBoolean(dt.Rows[0]["ShowEnquiryForm"]);

                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ImageName"])))
                {
                    hdnImage.Value = Convert.ToString(dt.Rows[0]["ImageName"]);
                    imgImage.Src = Config.VirtualDir + "thumb.aspx?path=" + Config.CMSFiles + Convert.ToString(dt.Rows[0]["ImageName"]) + "&width=152&height=112";
                    imgImage.Visible = true;
                }
                else
                {
                    imgImage.Visible = false;
                    hdnImage.Value = string.Empty;
                }
            } 
        }
    }
    #endregion

    #region Save Information
    private void SaveInfo()
    {
        if (Convert.ToString(Request[tbxTitle.UniqueID]) == "" || Convert.ToString(Request[hdnContent.UniqueID]) == "")         
        {
            string ErrMsg = string.Empty;

            ErrMsg = "Please correct the following errors<br/>";
            if (Convert.ToString(Request[tbxTitle.UniqueID]) == "")
            {
                ErrMsg += "Page URL<br/>";
            }
            if (Convert.ToString(Request[txtDescription.UniqueID]) == "")
            {
                ErrMsg += "Description";
            }
            Response.Write("<script>DisplMsg('<%= divMsg.ClientID %>','" + ErrMsg + "','alert-message error');</script>");
        }
        else
        {
            if (Request.Files[fupdImage.UniqueID] != null)
            {
                if (Request.Files[fupdImage.UniqueID].ContentLength > 0)
                {
                    strImageName = System.IO.Path.GetExtension(fupdImage.PostedFile.FileName);
                    if (IsImage(fupdImage.PostedFile.FileName.ToLower()) == false)
                    {
                       Response.Write(Common.ShowMessage("Please upload only .JPG, .JPEG, .PNG, .BMP, .GIF image files.", "alert-message error", divMsg.ClientID));
                    }
                    strImageName = Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Date.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Ticks) + strImageName;
                    if (ID != 0)
                    {
                        DeleteCMSFile(Convert.ToString(Request[hdnCMSFile.UniqueID]));
                    }
                    Request.Files[fupdImage.UniqueID].SaveAs(Request.PhysicalApplicationPath + "/" + Config.CMSFiles + strImageName);
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request[hdnImage.UniqueID]))
                    {
                        strImageName = Request[hdnImage.UniqueID];
                    }
                    else
                    {
                        strImageName = string.Empty;
                    }
                }
            } 

            objCMSBAL.ImageName = strImageName;
            objCMSBAL.ID = ID;
            objCMSBAL.PageTitle = Convert.ToString(Request[tbxTitle.UniqueID]).Trim();
            objCMSBAL.PageUrl = Convert.ToString(Request[tbxURL.UniqueID]).Trim();
            System.Text.StringBuilder sbTemplate = new System.Text.StringBuilder(Server.HtmlDecode(Request[hdnContent.UniqueID]));
            sbTemplate.Replace(Config.WebSiteUrl, "{%WebSiteUrl%}");

            //string strHeader = "<div class=\"content\"><div class=\"container\" style=\"padding-top: 0px;\"><div class=\"row\"><div class=\"ol-md-12 main-el\">";
            //string strFooter = "</div></div></div></div> ";

            //if(ID == 0)
            //{
            //    objCMSBAL.PageDescription = strHeader + sbTemplate.ToString() + strFooter;
            //}
            //else
            //{
            //    objCMSBAL.PageDescription = sbTemplate.ToString();
            //}

            objCMSBAL.PageDescription = sbTemplate.ToString();
            objCMSBAL.CMSMetaTitle = Convert.ToString(Request[txtMetaTitle.UniqueID]).Trim();
            objCMSBAL.CMSMetaKeyword = Convert.ToString(Request[txtMetaKeyword.UniqueID]).Trim();
            objCMSBAL.CMSMetaDescription = Convert.ToString(Request[txtMetaDescription.UniqueID]).Trim();

            objCMSBAL.HeadText = Convert.ToString(Request[txtHeadText.UniqueID]).Trim();
            objCMSBAL.BodyFooterText = Convert.ToString(Request[txtBodyFooterText.UniqueID]).Trim();
            //objCMSBAL.ShowEnquiryForm = 0;
            int intResult = objCMSBAL.Save();

            switch (intResult)
            {
                case -1:
                    
                    Response.Write(Common.ShowMessage("This CMS Page already exists. So please try another CMS Page.", "alert-message error", divMsg.ClientID));
                    Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                    break;
                default:
                    Response.Write(Common.ShowMessage("CMS information has been saved successfully.", "alert-message success", divMsg.ClientID));
                    Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                    Response.Write(Common.ScriptStartTag + "parent.window.setTimeout(\"parent.window.location.href = 'cms-list.aspx'\",1000);" + Common.ScriptEndTag);                   
                    break;
            }
            Response.End();
        }
    }
    #endregion

    #region General
    /*
    private void ShowMessage(string strMessage, string strMessageType, string divMessage)
    {
        Response.Write("<link href='" + Config.VirtualDir + "style/style.css' rel='Stylesheet' type='text/css' />");
        Response.Write("<script type='text/javascript' language='javascript' src='" + Config.VirtualDir + "js/jquery-1.4.2.min.js'></script>");
        Response.Write("<script type='text/javascript' language='javascript' src='" + Config.VirtualDir + "js/general.js'></script>");
        Response.Write("<script type='text/javascript'>var virtualDir = '" + Config.VirtualDir + "';</script>");
        Response.Write("$(document.ready(function {");
        Response.Write(Common.ScriptStartTag + "parent.$('#" + divMessage + "').show();" + Common.ScriptEndTag);
        Response.Write(Common.ScriptStartTag + "parent.$('#" + divMessage + "').html('" + strMessage + "');" + Common.ScriptEndTag);
        Response.Write(Common.ScriptStartTag + "parent.$('#" + divMessage + "').attr('class', '" + strMessageType + "');" + Common.ScriptEndTag);
        Response.Write("});");
    }*/

    private void DeleteCMSFile(string strImage)
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