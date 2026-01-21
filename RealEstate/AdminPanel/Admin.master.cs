using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using System.Web.UI.HtmlControls;
using Utility;
using Utility.Security;
public partial class AdminPanel_Admin : AdminPageSetting
{
    #region Private Members
    private Section _selectedTab = Section.Home;
    protected bool IsSuperAdmin = false;
    public string SelectedSectionName = string.Empty;
    protected int FirstMenuID = 0;
    protected string FirstMenuName = string.Empty;
    protected string UserName = string.Empty;
    protected int UserType = 1;
    protected int intAdminID = 0;
    protected int ReportAccess = 0;
    protected int SettingsAccess = 0;
    public int ShowEzMark = 1;
    #endregion

    #region Public Properties
    public enum Section
    {
        Home,
        General,
        None,
        RollManagment,
        Component,
        Dashboard
    }
    public Section SelectedSection
    {
        get { return _selectedTab; }
        set { _selectedTab = value; }
    }

    public string SelSection
    {
        get { return SelectedSectionName; }
        set { SelectedSectionName = value; }
    }

    protected string GetSelectedSection()
    {
        string SelSectionName = string.Empty;
        SelSectionName = SelSection;
        //switch (SelectedSection)
        //{
        //    case Section.Home:
        //        SelSection = "home";
        //        break;
        //    case Section.None:
        //        SelSection = string.Empty;
        //        break;
        //    case Section.General:
        //        SelSection = "generalmanagement";
        //        break;
        //    case Section.RollManagment:
        //        SelSection = "rolemanagement";
        //        break;
        //    case Section.Component:
        //        SelSection = "component";
        //        break;
        //    case Section.Dashboard:
        //        SelSection = "dashboard";
        //        break;
        //}
        return SelSectionName;
    }
    #endregion

    #region Page Events
    protected void Page_Load(Object Source, EventArgs e)
    {
        if (Session["UserID"] != null)
            intAdminID = Convert.ToInt32(Session["UserID"]);
              

        if (Session["IsSuperAdmin"] != null)
            IsSuperAdmin = Convert.ToBoolean(Session["IsSuperAdmin"]);



        if (!IsPostBack)
        {

            BindAdminName();
        }
        //  UserName = Convert.ToString(AdminAuthentication.FirstName + " " + AdminAuthentication.LastName);
    }


    #endregion

    #region Bind Admin Name
    private void BindMenu()
    {
        DataTable dt = new DataTable();
        AdminBAL objAdminBAL = new AdminBAL();
        if (Session["UserID"] != null)
            objAdminBAL.ID = Convert.ToInt32(Session["UserID"]);
        dt = objAdminBAL.AdminPageRights();
        DataView dv = new DataView(dt);
        dv.RowFilter = "ShowinMenu=1 and ParentName='Modules'";
        if (dv.Count > 0)
        {
            rptModules.DataSource = dv;
            rptModules.DataBind();
        }
        if (Session["UserID"] != null)
        {
            if (Convert.ToInt32(Session["UserID"]) != 1)
            {

                DataView dvPageAccess = new DataView(dt);
                string Pageurl = Request.Url.ToString();
                Pageurl = Pageurl.Replace(Utility.Config.WebSiteUrl, string.Empty).Replace("adminpanel/", string.Empty).Replace("AdminPanel/", string.Empty).Replace("Adminpanel/", string.Empty);

                if (Pageurl.IndexOf("?") >= 0)
                {
                    Pageurl = Pageurl.Substring(0, Pageurl.IndexOf("?"));
                }
                dvPageAccess.RowFilter = "PageVerify like '%" + Pageurl.ToLower() + "%'";

                if (dvPageAccess.Count == 0 && Pageurl.ToLower() != "dashboard.aspx" && Pageurl.ToLower() != "access-denied.aspx" && Pageurl.ToLower() != "popupform.aspx" && Pageurl.ToLower() != "business-lat-long-update.aspx")
                {
                    Response.Redirect("access-denied.aspx");
                }
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }

    }
    private void BindAdminName()
    {
        AdminBAL objAdmin = new AdminBAL();
        DataTable dt = new DataTable();
        if (Session["UserID"] != null)
            objAdmin.ID = Convert.ToInt32(Session["UserID"]);
        dt = objAdmin.AdminByID();
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToString(dt.Rows[0]["ImageName"]).Trim() != string.Empty)
                imgAvtar.Src = Utility.Config.VirtualDir + "thumb.aspx?path=" + Utility.Config.CMSFiles + Convert.ToString(dt.Rows[0]["ImageName"]) + "&width=40";
            UserName = Convert.ToString(Convert.ToString(dt.Rows[0]["FirstName"]));// + " " + Convert.ToString(dt.Rows[0]["LastName"]));
        }
    }
    #endregion

    #region Check Cookie
    protected void Page_Init(object sender, EventArgs e)
    {
        CheckCookieLogin();
        BindMenu();
    }
    private void CheckCookieLogin()
    {
        try
        {
            if (Session["UserID"] == null)
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["r65n7268ocs"];
                //if (cookie != null && cookie.Expires > DateTime.Now)
                if (cookie != null)
                {
                    DataTable dt = new DataTable();

                    string[] strArray = EncryptDescrypt.DecryptString(cookie.Value).Split(new char[] { '!' });
                    int UserID = Convert.ToInt32(strArray[0]);


                    AdminBAL objAdminBAL = new AdminBAL();
                    objAdminBAL.ID = UserID;
                    dt = objAdminBAL.AdminDetailsByID();
                    if (dt.Rows.Count > 0)
                    {
                        Session["UserName"] = Convert.ToString(dt.Rows[0]["UserName"]);
                        Session["UserID"] = Convert.ToString(dt.Rows[0]["ID"]);
                        Session["UserType"] = Convert.ToString(dt.Rows[0]["AdminType"]);
                        Session["IsSuperAdmin"] = Convert.ToString(dt.Rows[0]["IsSupAdmin"]);
                        Session["FirstName"] = Convert.ToString(dt.Rows[0]["FirstName"]);
                        Session["LastName"] = Convert.ToString(dt.Rows[0]["LastName"]);
                    }
                    else
                    {
                        if (cookie != null)
                        {
                            cookie.Expires = DateTime.Now.AddDays(-3);
                            HttpContext.Current.Response.Cookies.Add(cookie);
                        }
                    }
                }
            }
        }
        catch(Exception ex)
        {

        }
    }
    #endregion
}
