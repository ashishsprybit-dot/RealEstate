using BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;

public partial class AdminPanel_Emailtemplate_Modify : System.Web.UI.Page
{
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
            SaveInfo();
        }
        else
        {
            if (ID != 0)
            {
                BindControls();
            }
        }

    }
    #endregion

    #region Bind Controls
    private void BindControls()
    {
        DataTable dtblTemplate = new DataTable();
        EmailTemplateBAL objTemplate = new EmailTemplateBAL();

        objTemplate.ID = ID;
        dtblTemplate = objTemplate.GetByID(ID, 0);
        if (dtblTemplate.Rows.Count > 0)
        {
            System.Text.StringBuilder sbTemplate = new System.Text.StringBuilder(Convert.ToString(dtblTemplate.Rows[0]["Template"]));
            sbTemplate.Replace("###siteurl###", Config.WebSiteUrl);
            tbxTemplateName.Value = Convert.ToString(dtblTemplate.Rows[0]["TemplateName"]);
            tbxSubject.Value = Convert.ToString(dtblTemplate.Rows[0]["Subject"]);
            tbxTemplate.Text = sbTemplate.ToString();
        }
         
    }
    #endregion

    #region Save Information
    private void SaveInfo()
    {
        EmailTemplateBAL objTemplate = new EmailTemplateBAL();
        objTemplate.ID = ID;
        objTemplate.TemplateName = Request[tbxTemplateName.UniqueID];
        System.Text.StringBuilder sbTemplate = new System.Text.StringBuilder(Server.HtmlDecode(Request[hdnContent.UniqueID]));
        sbTemplate.Replace(Config.WebSiteUrl, "###siteurl###");
        objTemplate.Template = sbTemplate.ToString();
        objTemplate.Subject = Request[tbxSubject.UniqueID];
        switch (objTemplate.Save())
        {
            case -1:                
                Response.Write(Common.ShowMessage("This EmailTemplate already exists. So please try another EmailTemplate.", "alert-message error", divMsg.ClientID));
                break;
            default:
                Response.Write(Javascript.ScriptStartTag + " window.setTimeout(\"window.location.href='Emailtemplate-List.aspx'\",2000)" + Javascript.ScriptEndTag);                
                Response.Write(Javascript.DisplayMsg(divMsg.ClientID, "EmailTemplate has been saved successfully.", Javascript.MessageType.Success, true));                
                break;
        }
        Response.End();
    }
    #endregion
}