using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BAL;
using PAL;
using Utility;

public partial class Include_CMSContent : System.Web.UI.UserControl
{
    private string _PageName = string.Empty;
    private bool _DisplayTitle = false;

    #region Properties
    public string PageName {
        get { return _PageName; }
        set { _PageName = value; } 
    }
    public bool DisplayTitle
    {
        get { return _DisplayTitle; }
        set { _DisplayTitle = value; }
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
        dt = objCMSBAL.GetCMSFrontData();  
        if (dt.Rows.Count > 0)
        {
            ltrlCMSContnet.Text = Convert.ToString(dt.Rows[0]["PageDescription"]).Replace("{%WebSiteUrl%}", Config.WebSiteUrl); ;
            divHeader.InnerHtml = "<h1>" + Convert.ToString(dt.Rows[0]["PageTitle"]) + "<span class='main-title-arrow'></span></h1>";
           
        }
        divHeader.Visible = DisplayTitle;
    } 
    #endregion
}