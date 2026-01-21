using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using PAL;
using Utility;

public partial class Include_CMS : System.Web.UI.UserControl
{
    private string _PageName = string.Empty;   

    #region Properties
    public string PageName {
        get { return _PageName; }
        set { _PageName = value; } 
    }    
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ltrlCMSContnet.Text = "Description not available...";    
            BindPageContent();
        }
    }
    private void BindPageContent()
    {
        CMSBAL objCMSBAL = new CMSBAL();
        DataTable dt = new DataTable();
        objCMSBAL.PageUrl = PageName;
        dt = objCMSBAL.GetCMSData();  
        if (dt.Rows.Count > 0)
        {
            ltrlCMSContnet.Text = Convert.ToString(dt.Rows[0]["PageDescription"]).Replace("{%WebSiteUrl%}", Config.WebSiteUrl); ;
        }        
    } 
    #endregion
}