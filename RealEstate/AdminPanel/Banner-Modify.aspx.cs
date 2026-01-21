using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

public partial class AdminPanel_Banner_Modify : System.Web.UI.Page
{
    #region variable
    BannerBAL objBannerBAL = new BannerBAL();
    CMSBAL objCMSBAL = new CMSBAL();
    string strBannerFileName = string.Empty;
    string strBannerFile = string.Empty;

    #endregion

    #region Private Members
    private int _ID = 0;
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
        if (Request.Form.Keys.Count > 0)
        {
            CreateModifyBannerDetail();
        }
        else
        {
            BindModule();
            if (ID != 0)
            {
                BindControls();
            }
        }
        if (!IsPostBack)
        {

            if (string.IsNullOrEmpty(Request["id"]))
            {
                rdNone.Checked = true;
            }
        }

    }
    #endregion

    #region Bind Controls
    private void BindModule()
    {
        DataTable dt = new DataTable();
        MenuBAL objMenuBAL = new MenuBAL();
        dt = objMenuBAL.GetList();
        DataView dv = new DataView(dt);
        dv.RowFilter = "status =1";
        if (dt.Rows.Count > 0)
        {
            rptModuleList.DataSource = dv;
            rptModuleList.DataBind();
        }


    }
    private void BindControls()
    {
        DataTable dtblBanner = new DataTable();
        objBannerBAL.ID = ID;
        dtblBanner = objBannerBAL.GetList();
        if (dtblBanner.Rows.Count > 0)
        {

            tbxTitle.Value = Convert.ToString(dtblBanner.Rows[0]["Title"]);
            tbxTitle2.Value = Convert.ToString(dtblBanner.Rows[0]["Title2"]);
            txtCMSLink.Value = Convert.ToString(dtblBanner.Rows[0]["MenuUrl"]);
            hdnMenuID.Value = Convert.ToString(dtblBanner.Rows[0]["MenuID"]);
            txtExternalLink.Value = Convert.ToString(dtblBanner.Rows[0]["ExternalLinkURL"]);

            //System.Text.StringBuilder sbTemplate = new System.Text.StringBuilder(Convert.ToString(dtblBanner.Rows[0]["Details"]));
            //sbTemplate.Replace("{%WebSiteUrl%}", Config.WebSiteUrl);
            txtDesc.Value = Convert.ToString(dtblBanner.Rows[0]["Details"]);

            if (dtblBanner.Rows[0]["LinkType"].ToString() != string.Empty)
            {
                switch (Convert.ToInt32(dtblBanner.Rows[0]["LinkType"].ToString()))
                {
                    case 0:
                        rdNone.Checked = true;
                        break;
                    case 1:
                        rdCMS.Checked = true;
                        break;
                    case 2:
                        rdExternal.Checked = true;
                        break;
                    default:

                        break;
                }
            }
            else
            {
                if (!Convert.ToBoolean(dtblBanner.Rows[0]["ExternalLink"]))
                    rdCMS.Checked = true;
                else
                    rdExternal.Checked = true;

            }
            if (!string.IsNullOrEmpty(Convert.ToString(dtblBanner.Rows[0]["BannerFile"])))
            {
                hdnImage.Value = Convert.ToString(dtblBanner.Rows[0]["BannerFile"]);
                imgImage.Src = Config.VirtualDir + "thumb.aspx?path=" + Config.Banner + Convert.ToString(dtblBanner.Rows[0]["BannerFile"]) + "&width=200&height=125";
                imgImage.Visible = true;
            }
            else
            {
                imgImage.Visible = false;
                hdnImage.Value = string.Empty;
            }
        }
         
    }
    #endregion

    #region create banner
    private void CreateModifyBannerDetail()
    {
        if (Request.Files[fupdImage.UniqueID] != null)
        {
            if (Request.Files[fupdImage.UniqueID].ContentLength > 0)
            {
                strBannerFile = System.IO.Path.GetExtension(fupdImage.PostedFile.FileName);
                if (IsImage(fupdImage.PostedFile.FileName.ToLower()) == false)
                {
                    Response.Write(Common.ShowMessage("Please upload only .JPG, .JPEG, .PNG, .BMP, .GIF image files.", "alert-message error", divMsg.ClientID));
                }
                strBannerFile = Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Date.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second) + Convert.ToString(DateTime.Now.Ticks) + strBannerFile;
                if (ID != 0)
                {
                    DeleteBannerFile(Convert.ToString(Request[hdnBannerFile.UniqueID]));
                }
                Request.Files[fupdImage.UniqueID].SaveAs(Request.PhysicalApplicationPath + "/" + Config.Banner + strBannerFile);
                objBannerBAL.BannerFile = strBannerFile;
            }
            else
            {
                if (!string.IsNullOrEmpty(Request[hdnImage.UniqueID]))
                {
                    strBannerFile = Request[hdnImage.UniqueID];
                }
                else
                {
                    strBannerFile = string.Empty;
                }
            }
        }
        objBannerBAL.ID = ID;
        objBannerBAL.Title = Convert.ToString(Request[tbxTitle.UniqueID]).Trim(); ;
        objBannerBAL.Title2 = Convert.ToString(Request[tbxTitle2.UniqueID]).Trim(); ;

        //System.Text.StringBuilder sbTemplate = new System.Text.StringBuilder(Server.HtmlDecode(Request[hdnContent.UniqueID]));
        //sbTemplate.Replace(Config.WebSiteUrl, "{%WebSiteUrl%}");
        objBannerBAL.Details = Convert.ToString(Request[txtDesc.UniqueID]).Trim();//sbTemplate.ToString().Trim();
        objBannerBAL.LinkType = Convert.ToInt32(Convert.ToInt32(Request[hdnLinkType.UniqueID]));
        objBannerBAL.ExternalLink = Convert.ToBoolean(Convert.ToInt32(Request[hdnExternallink.UniqueID]));

        if (Convert.ToInt32(Request[hdnMenuID.UniqueID]) == -1)
        {
            objBannerBAL.StaticURL = Convert.ToString(Request[txtCMSLink.UniqueID]).Trim();
        }
        else
        {
            objBannerBAL.StaticURL = string.Empty;
        }

        if (Convert.ToInt32(Request[hdnExternallink.UniqueID]) == 0)
        {
            objBannerBAL.MenuID = Convert.ToString(Request[hdnMenuID.UniqueID]).Trim() == string.Empty ? 0 : Convert.ToInt32(Request[hdnMenuID.UniqueID]);
            objBannerBAL.ExternalLinkURL = string.Empty;
        }
        else
        {
            objBannerBAL.ExternalLinkURL = Convert.ToString(Request[txtExternalLink.UniqueID]).Trim();
            objBannerBAL.MenuID = 0;
        }
        objBannerBAL.BannerFile = strBannerFile.Trim();

        int intResult = objBannerBAL.Save();
        switch (intResult)
        {
            default:
                Response.Write(Common.ShowMessage("Banner has been saved successfully.", "alert-message success", divMsg.ClientID));                
                Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
                Response.Write(Common.ScriptStartTag + "parent.window.setTimeout(\"parent.window.location.href = 'banner-list.aspx'\",1000);" + Common.ScriptEndTag);
                break;
        }
        Response.End();
    }
    #endregion

    #region General
    
     
   

    private void DeleteBannerFile(string strImage)
    {
        if (!string.IsNullOrEmpty(strImage))
        {
            if (Common.IsFileExist(Config.Banner, strImage))
            {
                Common.FileDelete(Config.Banner, strImage);
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