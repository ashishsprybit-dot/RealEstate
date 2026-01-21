using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using PAL;
 
using Utility;

public partial class General_setting : AdminAuthentication
{
   #region declaration
    GeneralBAL objGeneralBAL = new GeneralBAL();
    GeneralSettings objGeneralSettings = new GeneralSettings();
    #endregion

    #region Private Members
    private Int64 _ID = 0;
    string strImageName = string.Empty;

    #endregion

    #region Public Members
    public new Int64 ID
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
         
        Int64.TryParse(Request["id"], out _ID);
        objGeneralBAL.ID = ID;
        if (Request.Form.Keys.Count > 0)
        {
            if (Convert.ToString(Request[txtSiteUrl.UniqueID]) == "" || Convert.ToString(Request[txtInfoEmail.UniqueID]) == "")
            {
                string ErrMsg = string.Empty;

                ErrMsg = "<b>Please correct the following errors</b>";
                if (Convert.ToString(Request[txtSiteUrl.UniqueID]) == "")
                {
                    ErrMsg += "Site Url<br/>";
                }
                if (Convert.ToString(Request[txtInfoEmail.UniqueID]) == "")
                {
                    ErrMsg += "Info. Email Address<br/>";
                }

                Response.Write("<script>DisplMsg('<%= divMsg.ClientID %>','" + ErrMsg + "','alert-message error');</script>");
            }
            else
            {
                SaveInfo();
            }
            Response.End();
        }
        if (!IsPostBack)
        {
            BindControl();
        }
      
    }

    private void SaveInfo()
    {
        List<GeneralSettingsPAL> obj_List = new List<GeneralSettingsPAL>();
        #region Site Settings
        //site url
        GeneralSettingsPAL obj_SiteUrl = new GeneralSettingsPAL();
        obj_SiteUrl.Keyword = "siteurl";
        obj_SiteUrl.Value = Request[txtSiteUrl.UniqueID].Trim();
        obj_List.Add(obj_SiteUrl);


        //Info Email
        GeneralSettingsPAL obj_InfoEmail = new GeneralSettingsPAL();
        obj_InfoEmail.Keyword = "infoemail";
        obj_InfoEmail.Value = Request[txtInfoEmail.UniqueID].Trim();
        obj_List.Add(obj_InfoEmail);

        GeneralSettingsPAL obj_mailtoemail = new GeneralSettingsPAL();
        obj_mailtoemail.Keyword = "mailtoemail";
        obj_mailtoemail.Value = Request[txtMailto.UniqueID].Trim();
        obj_List.Add(obj_mailtoemail);

        GeneralSettingsPAL objWebsiteName = new GeneralSettingsPAL();
        objWebsiteName.Keyword = "WebsiteName";
        objWebsiteName.Value = Request[txtWebsiteName.UniqueID].Trim();
        obj_List.Add(objWebsiteName);



        #endregion

        #region Email Settings
        //Email Address
        GeneralSettingsPAL obj_AdminMail = new GeneralSettingsPAL();
        obj_AdminMail.Keyword = "emailaddress";
        obj_AdminMail.Value = Request[txtEmailaddress.UniqueID].Trim();
        obj_List.Add(obj_AdminMail);

        // Admin Password
        GeneralSettingsPAL obj_AdminPassword = new GeneralSettingsPAL();
        obj_AdminPassword.Keyword = "emailpassword";
        obj_AdminPassword.Value = Request[txtPassword.UniqueID].Trim();
        obj_List.Add(obj_AdminPassword);

        // hostname
        GeneralSettingsPAL obj_SmtpHost = new GeneralSettingsPAL();
        obj_SmtpHost.Keyword = "hostname";
        obj_SmtpHost.Value = Request[txtHostName.UniqueID].Trim();
        obj_List.Add(obj_SmtpHost);
        #endregion

        #region MetaData Settings

        // title
        GeneralSettingsPAL obj_Title = new GeneralSettingsPAL();
        obj_Title.Keyword = "title";
        obj_Title.Value = Request[txtMetaTitle.UniqueID].Trim();
        obj_List.Add(obj_Title);

        //meta keyword
        GeneralSettingsPAL obj_MetaKeyword = new GeneralSettingsPAL();
        obj_MetaKeyword.Keyword = "keyword";
        obj_MetaKeyword.Value = Request[txtMetaKeywords.UniqueID].Trim();
        obj_List.Add(obj_MetaKeyword);


        // metadescription
        GeneralSettingsPAL obj_MetaDescription = new GeneralSettingsPAL();
        obj_MetaDescription.Keyword = "description";
        obj_MetaDescription.Value = Request[txtMetaDesc.UniqueID].Trim();
        obj_List.Add(obj_MetaDescription);

        // analyaticcode
        GeneralSettingsPAL obj_AnalyticCode = new GeneralSettingsPAL();
        obj_AnalyticCode.Keyword = "analyaticcode";
        obj_AnalyticCode.Value = Request[txtAnalyticCode.UniqueID].Trim();
        obj_List.Add(obj_AnalyticCode);

        #endregion

        #region External Link Settings

        // External Link
        GeneralSettingsPAL objExternalLink = new GeneralSettingsPAL();
        objExternalLink.Keyword = "externallink";
        objExternalLink.Value = Request[txtExternalLink.UniqueID].Trim();
        obj_List.Add(objExternalLink);

        // External Vedio Link
        GeneralSettingsPAL objExternalVedioLink = new GeneralSettingsPAL();
        objExternalVedioLink.Keyword = "externalvedio";
        objExternalVedioLink.Value = Request[txtExternalVedio.UniqueID].Trim();
        obj_List.Add(objExternalVedioLink);
        #endregion

        #region Social Media Settings

        //Facebook
        GeneralSettingsPAL obj_Facebook = new GeneralSettingsPAL();
        obj_Facebook.Keyword = "facebooklink";
        obj_Facebook.Value = Request[txtFacebook.UniqueID].Trim();
        obj_List.Add(obj_Facebook);

        // Twitter
        GeneralSettingsPAL obj_Twitter = new GeneralSettingsPAL();
        obj_Twitter.Keyword = "twitter";
        obj_Twitter.Value = Request[txtTwitter.UniqueID].Trim();
        obj_List.Add(obj_Twitter);

        //Site Link
        GeneralSettingsPAL obj_SiteLink = new GeneralSettingsPAL();
        obj_SiteLink.Keyword = "sitelink";
        obj_SiteLink.Value = Request[txtSite.UniqueID].Trim();
        obj_List.Add(obj_SiteLink);

        //bloglink
        GeneralSettingsPAL obj_PinterestLink = new GeneralSettingsPAL();
        obj_PinterestLink.Keyword = "pinterestlink";
        obj_PinterestLink.Value = Request[txtPinterest.UniqueID].Trim();
        obj_List.Add(obj_PinterestLink);

        //flickerlink
        GeneralSettingsPAL obj_InLink = new GeneralSettingsPAL();
        obj_InLink.Keyword = "inlink";
        obj_InLink.Value = Request[txtin.UniqueID].Trim();
        obj_List.Add(obj_InLink);

        //youtubelink
        GeneralSettingsPAL obj_YouTube = new GeneralSettingsPAL();
        obj_YouTube.Keyword = "youtubelink";
        obj_YouTube.Value = Request[txtYoutube.UniqueID].Trim();
        obj_List.Add(obj_YouTube);

        //Contact Us
        GeneralSettingsPAL obj_ContactUs = new GeneralSettingsPAL();
        obj_ContactUs.Keyword = "contactus";
        obj_ContactUs.Value = Request[txtContactUs.UniqueID].Trim();
        obj_List.Add(obj_ContactUs);

        //Telephone No
        GeneralSettingsPAL obj_telephoneno = new GeneralSettingsPAL();
        obj_telephoneno.Keyword = "TelephoneNo";
        obj_telephoneno.Value = Request[txttelephoneno.UniqueID].Trim();
        obj_List.Add(obj_telephoneno);


        // Tweeter Feed Url
        GeneralSettingsPAL obj_tweeterfeedurl = new GeneralSettingsPAL();
        obj_tweeterfeedurl.Keyword = "tweeterfeedurl";
        obj_tweeterfeedurl.Value = Request[txttwitterfeedurl.UniqueID].Trim();
        obj_List.Add(obj_tweeterfeedurl);

        // Tweeter User Name
        GeneralSettingsPAL obj_tweeterusername = new GeneralSettingsPAL();
        obj_tweeterusername.Keyword = "tweeterusername";
        obj_tweeterusername.Value = Request[txttwittername.UniqueID].Trim();
        obj_List.Add(obj_tweeterusername);
       
       
        #endregion

        #region General Settings
        //image 
        if (Request.Files[fupdImage.UniqueID] != null)
        {
            if (Request.Files[fupdImage.UniqueID].ContentLength > 0)
            {
                strImageName = System.IO.Path.GetExtension(fupdImage.PostedFile.FileName);
                if (IsImage(fupdImage.PostedFile.FileName.ToLower()) == false)
                {
                   Response.Write( Common.ShowMessage("Please upload only JPG, .JPEG, .PNG, .BMP, .GIF image files.", "alert-message error", divMsg.ClientID));
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

        GeneralSettingsPAL obj_img = new GeneralSettingsPAL();
        obj_img.Keyword = "CMSImage";
        obj_img.Value = strImageName;
        obj_List.Add(obj_img);  

        // footertext
        GeneralSettingsPAL obj_FooterText = new GeneralSettingsPAL();
        obj_FooterText.Keyword = "footertext";
        obj_FooterText.Value = Request[txtFooterText.UniqueID].Trim();
        obj_List.Add(obj_FooterText); 

        //Contact Address 1
        GeneralSettingsPAL obj_googlemapaddress1Contact = new GeneralSettingsPAL();
        obj_googlemapaddress1Contact.Keyword = "contactaddress1";
        obj_googlemapaddress1Contact.Value = Request[txtaddressfirstContact.UniqueID].Trim();
        obj_List.Add(obj_googlemapaddress1Contact);


        //Contact Address 2
        GeneralSettingsPAL obj_googlemapaddress2Contact = new GeneralSettingsPAL();
        obj_googlemapaddress2Contact.Keyword = "contactaddress2";
        obj_googlemapaddress2Contact.Value = Request[txtaddresssecondContact.UniqueID].Trim();
        obj_List.Add(obj_googlemapaddress2Contact);

        //Google map Address 1
        GeneralSettingsPAL obj_googlemapaddress1 = new GeneralSettingsPAL();
        obj_googlemapaddress1.Keyword = "googlemapaddress1";
        obj_googlemapaddress1.Value = Request[txtaddressfirst.UniqueID].Trim();
        obj_List.Add(obj_googlemapaddress1);


        //Google map Address 2
        GeneralSettingsPAL obj_googlemapaddress2 = new GeneralSettingsPAL();
        obj_googlemapaddress2.Keyword = "googlemapaddress2";
        obj_googlemapaddress2.Value = Request[txtaddresssecond.UniqueID].Trim();
        obj_List.Add(obj_googlemapaddress2);



        //Trading Hrs
        GeneralSettingsPAL obj_TradingHrs = new GeneralSettingsPAL();
        obj_TradingHrs.Keyword = "tradinghrs";
        obj_TradingHrs.Value = Request[txtTradingHrs.UniqueID].Trim();
        obj_List.Add(obj_TradingHrs);


        //Trading Hrs
        GeneralSettingsPAL obj_copyrightText = new GeneralSettingsPAL();
        obj_copyrightText.Keyword = "copyright";
        obj_copyrightText.Value = Request[txtcopyright.UniqueID].Trim();
        obj_List.Add(obj_copyrightText);

        // ABN NO

        GeneralSettingsPAL obj_abnno = new GeneralSettingsPAL();
        obj_abnno.Keyword = "abnno";
        obj_abnno.Value = Request[txtabnno.UniqueID].Trim();
        obj_List.Add(obj_abnno);

        // SSL
        GeneralSettingsPAL obj_SSL = new GeneralSettingsPAL();
        obj_SSL.Keyword = "SSL";
        if (Request[chkssl.UniqueID] != null)
            obj_SSL.Value = "true";
        else
            obj_SSL.Value = "false";
        obj_List.Add(obj_SSL);


        #endregion


        foreach (var item in obj_List)
        {
            objGeneralBAL.Save(item.Keyword, item.Value);
        }

        
        Response.Write(Common.ShowMessage("General setting details has been saved successfully.", "alert-message success", divMsg.ClientID));
        Response.Write(Common.ScriptStartTag + "parent.ScrollTop();" + Common.ScriptEndTag);
        
        //Response.Write(Common.ScriptStartTag + "parent.window.setTimeout(\"parent.window.location.href = 'general-setting.aspx'\",2000);" + Common.ScriptEndTag);
        Response.End();
      
    }

     
    private void BindControl()
    {
        using (DataTable dt_Info = objGeneralBAL.GetList())
        {

            #region Site Settings
            DataRow[] rowSiteurl = dt_Info.Select("[keyword]='siteurl'");
            if (rowSiteurl.Length > 0)
            {
                txtSiteUrl.Value = Convert.ToString(rowSiteurl[0].ItemArray[2]);
            }
            DataRow[] rowInfoEmail = dt_Info.Select("[keyword]='infoemail'");
            if (rowInfoEmail.Length > 0)
            {
                txtInfoEmail.Value = Convert.ToString(rowInfoEmail[0].ItemArray[2]);
            }
           
            DataRow[] rowmailtoemail = dt_Info.Select("[keyword]='mailtoemail'");
            if (rowmailtoemail.Length > 0)
            {
                txtMailto.Value = Convert.ToString(rowmailtoemail[0].ItemArray[2]);
            }


            DataRow[] RowWebsiteName = dt_Info.Select("[keyword]='WebsiteName'");
            if (RowWebsiteName.Length > 0)
            {
                txtWebsiteName.Value = Convert.ToString(RowWebsiteName[0].ItemArray[2]);
            }

            #endregion

            #region Email Settings
            DataRow[] rowEmail = dt_Info.Select("[keyword]='emailaddress'");
            if (rowEmail.Length > 0)
            {
                txtEmailaddress.Value = Convert.ToString(rowEmail[0].ItemArray[2]);
            }
            DataRow[] rowPassword = dt_Info.Select("[keyword]='emailpassword'");
            if (rowPassword.Length > 0)
            {
                txtPassword.Value = Convert.ToString(rowPassword[0].ItemArray[2]);
            }
            DataRow[] rowHostName = dt_Info.Select("[keyword]='hostname'");
            if (rowHostName.Length > 0)
            {
                txtHostName.Value = Convert.ToString(rowHostName[0].ItemArray[2]);
            }

            #endregion

            #region Metadata Setting

            DataRow[] rowMetaKeyword = dt_Info.Select("[keyword]='keyword'");
            if (rowMetaKeyword.Length > 0)
            {
                txtMetaKeywords.Value = Convert.ToString(rowMetaKeyword[0].ItemArray[2]);
            }
            DataRow[] rowAdminTitle = dt_Info.Select("[keyword]='title'");
            if (rowAdminTitle.Length > 0)
            {
                txtMetaTitle.Value = Convert.ToString(rowAdminTitle[0].ItemArray[2]);
            }
            DataRow[] rowMetaDesc = dt_Info.Select("[keyword]='description'");
            if (rowMetaDesc.Length > 0)
            {
                txtMetaDesc.Value = Convert.ToString(rowMetaDesc[0].ItemArray[2]);
            }
            DataRow[] rowAnalyaticCode = dt_Info.Select("[keyword]='analyaticcode'");
            if (rowAnalyaticCode.Length > 0)
            {
                txtAnalyticCode.Value = Convert.ToString(rowAnalyaticCode[0].ItemArray[2]);
            }
            #endregion

            #region External Link Settings

            DataRow[] rowExternalLink = dt_Info.Select("[keyword]='externallink'");
            if (rowExternalLink.Length > 0)
            {
                txtExternalLink.Value = Convert.ToString(rowExternalLink[0].ItemArray[2]);
            }
            DataRow[] rowExternalVedio = dt_Info.Select("[keyword]='externalvedio'");
            if (rowExternalVedio.Length > 0)
            {
                txtExternalVedio.Value = Convert.ToString(rowExternalVedio[0].ItemArray[2]);
            }
            #endregion

            #region Social Media Settings

            DataRow[] rowFacebbook = dt_Info.Select("[keyword]='facebooklink'");
            if (rowFacebbook.Length > 0)
            {
                txtFacebook.Value = Convert.ToString(rowFacebbook[0].ItemArray[2]);
            }
            DataRow[] rowTwitter = dt_Info.Select("[keyword]='twitter'");
            if (rowTwitter.Length > 0)
            {
                txtTwitter.Value = Convert.ToString(rowTwitter[0].ItemArray[2]);
            }
            DataRow[] rowSiteLink = dt_Info.Select("[keyword]='sitelink'");
            if (rowSiteLink.Length > 0)
            {
                txtSite.Value = Convert.ToString(rowSiteLink[0].ItemArray[2]);
            }
            DataRow[] rowpinterest = dt_Info.Select("[keyword]='pinterestlink'");
            if (rowpinterest.Length > 0)
            {
                txtPinterest.Value = Convert.ToString(rowpinterest[0].ItemArray[2]);
            }
            DataRow[] rowIn = dt_Info.Select("[keyword]='inlink'");
            if (rowIn.Length > 0)
            {
                txtin.Value = Convert.ToString(rowIn[0].ItemArray[2]);
            }
            DataRow[] rowYoutube = dt_Info.Select("[keyword]='youtubelink'");
            if (rowYoutube.Length > 0)
            {
                txtYoutube.Value = Convert.ToString(rowYoutube[0].ItemArray[2]);
            }
            DataRow[] rowContactUs = dt_Info.Select("[keyword]='contactus'");
            if (rowContactUs.Length > 0)
            {
                txtContactUs.Value = Convert.ToString(rowContactUs[0].ItemArray[2]);
            }
            DataRow[] rowTelephoneno = dt_Info.Select("[keyword]='TelephoneNo'");
            if (rowTelephoneno.Length > 0)
            {
                txttelephoneno.Value = Convert.ToString(rowTelephoneno[0].ItemArray[2]);
            }
            DataRow[] rowtweeterusername = dt_Info.Select("[keyword]='tweeterusername'");
            if (rowtweeterusername.Length > 0)
            {
                txttwittername.Value = Convert.ToString(rowtweeterusername[0].ItemArray[2]);
            }
            DataRow[] rowtweeterfeedurl = dt_Info.Select("[keyword]='tweeterfeedurl'");
            if (rowtweeterfeedurl.Length > 0)
            {
                txttwitterfeedurl.Value = Convert.ToString(rowtweeterfeedurl[0].ItemArray[2]);
            }
            #endregion

            #region General Settings
            DataRow[] rowFooterText = dt_Info.Select("[keyword]='footertext'");
            if (rowFooterText.Length > 0)
            {
                txtFooterText.Value = Convert.ToString(rowFooterText[0].ItemArray[2]);
            } 

            DataRow[] rowContactAddress1 = dt_Info.Select("[keyword]='contactaddress1'");
            if (rowContactAddress1.Length > 0)
            {
                txtaddressfirstContact.Value = Convert.ToString(rowContactAddress1[0].ItemArray[2]);
            }
            DataRow[] rowContactAddress2 = dt_Info.Select("[keyword]='contactaddress2'");
            if (rowContactAddress2.Length > 0)
            {
                txtaddresssecondContact.Value = Convert.ToString(rowContactAddress2[0].ItemArray[2]);
            }

            DataRow[] rowGoogleAddress1 = dt_Info.Select("[keyword]='googlemapaddress1'");
            if (rowGoogleAddress1.Length > 0)
            {
                txtaddressfirst.Value = Convert.ToString(rowGoogleAddress1[0].ItemArray[2]);
            }
            DataRow[] rowGoogleAddress2 = dt_Info.Select("[keyword]='googlemapaddress2'");
            if (rowGoogleAddress2.Length > 0)
            {
                txtaddresssecond.Value = Convert.ToString(rowGoogleAddress2[0].ItemArray[2]);
            }

            DataRow[] rowTradingHrs = dt_Info.Select("[keyword]='tradinghrs'");
            if (rowTradingHrs.Length > 0)
            {
                txtTradingHrs.Value = Convert.ToString(rowTradingHrs[0].ItemArray[2]);
            }

            DataRow[] copyrightText = dt_Info.Select("[keyword]='copyright'");
            if (copyrightText.Length > 0)
            {
                txtcopyright.Value = Convert.ToString(copyrightText[0].ItemArray[2]);
            }

            DataRow[] rowabnno = dt_Info.Select("[keyword]='abnno'");
            if (rowabnno.Length > 0)
            {
                txtabnno.Value = Convert.ToString(rowabnno[0].ItemArray[2]);
            }

            DataRow[] rowSSL = dt_Info.Select("[keyword]='SSL'");
            if (rowSSL.Length > 0)
            {
                if (Convert.ToString(rowSSL[0].ItemArray[2]).ToLower() == "true")
                    chkssl.Checked = true;
                else
                    chkssl.Checked = false;
            }

            DataRow[] CMSImage = dt_Info.Select("[keyword]='CMSImage'");
            if (CMSImage.Length > 0)
            {
                hdnImage.Value = Convert.ToString(CMSImage[0].ItemArray[2]);
                //imgImage.Src = Config.VirtualDir + "thumb.aspx?path=" + Config.CMSFiles + Convert.ToString(CMSImage[0].ItemArray[2]) + "&width=152&height=112";
                imgImage.Visible = true;
            }
            else
            {
                imgImage.Visible = false;
                hdnImage.Value = string.Empty;
            }
          
            #endregion
        }

    }
    #endregion

    #region General

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
 