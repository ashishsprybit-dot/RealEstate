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
using Newtonsoft.Json;
using DAL;

public partial class JudgementTool_Main : AdminPageSetting
{
    #region Private Members
    public string RequestedSchoolURL = "";
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
    private string CookieName = "Te@c$%^&E@#rJud#%^ge#ment";
    protected string Teachers = "";
    protected string HomeGroups = "";
    protected string Students = "";
    protected int isGoogleUser = 1;

    #endregion

    #region Public Properties
    public string dataForStudentListMaster = "";
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
        //if (Session["UserID"] != null)
        //    intAdminID = Convert.ToInt32(Session["UserID"]);


        //if (Session["IsSuperAdmin"] != null)
        //    IsSuperAdmin = Convert.ToBoolean(Session["IsSuperAdmin"]);



        //if (!IsPostBack)
        //{

        //    BindAdminName();
        //}
        ////  UserName = Convert.ToString(AdminAuthentication.FirstName + " " + AdminAuthentication.LastName);
        if (Session["IsGoogleUser"] != null)
        {
            isGoogleUser = Convert.ToInt32(Session["IsGoogleUser"]);
        }
    }


    #endregion

    #region Bind Admin Name
    private void BindStudents()
    {
        UnlockSemesterBAL objBAL = new UnlockSemesterBAL();
        int LoginUserID = 0;
        if (Session["TeacherUserID"] != null)
        {
            LoginUserID = Convert.ToInt32(Session["TeacherUserID"]);
        }
        DataTable dt = objBAL.getStudentList(Convert.ToInt64(Session["SchoolID"]), LoginUserID);
        dataForStudentListMaster = JsonConvert.SerializeObject(dt, Formatting.Indented);
    }
    private void BindMenu()
    {
        string SURL = Convert.ToString(Request["c1"]);

        DataTable dt = new DataTable();
        TeachersBAL objTeachersBAL = new TeachersBAL();

        if (Session["TeacherUserID"] != null)
            objTeachersBAL.ID = Convert.ToInt32(Session["TeacherUserID"]);
        dt = objTeachersBAL.TeacherPageRights();

        DataView dv = new DataView(dt);
        dv.RowFilter = "ShowinMenu=1 and ParentName='Modules'";

        if (dv.Count > 0)
        {
            rptModules.DataSource = dv;
            rptModules.DataBind();
        }

        dv = new DataView(dt);
        dv.RowFilter = "ShowinMenu=1 and ParentName='Results'";

        if (dv.Count > 0)
        {
            rptResults.DataSource = dv;
            rptResults.DataBind();
            rptResults.Visible = true;
        }
        else
        {
            rptResults.Visible = false;
        }
        dv = new DataView(dt);
        dv.RowFilter = "ShowinMenu=1 and ParentName='Home Groups'";

        if (dv.Count > 0)
        {
            rptHomeGroups.DataSource = dv;
            rptHomeGroups.DataBind();
            rptHomeGroups.Visible = true;
        }
        else
        {
            rptHomeGroups.Visible = false;
        }
        
        //--------------------- Reports --------------------------------

        dv = new DataView(dt);
        dv.RowFilter = "ShowinMenu=1 and ParentName='Reports'";

        if (dv.Count > 0)
        {
            rptReports.DataSource = dv;
            rptReports.DataBind();
            rptReports.Visible = true;
        }
        else
        {
            rptReports.Visible = false;
        }

        //--------------------- Export --------------------------------

        dv = new DataView(dt);
        dv.RowFilter = "ShowinMenu=1 and ParentName='Export'";

        if (dv.Count > 0)
        {
            rptExport.DataSource = dv;
            rptExport.DataBind();
            rptExport.Visible = true;
        }
        else
        {
            rptExport.Visible = false;
        }


        //--------------------- Cohorts --------------------------------

        dv = new DataView(dt);
        dv.RowFilter = "ParentName='Cohorts Reports'";

        if (dv.Count > 0)
        {
            DataTable dtCohorts = new DataTable();
            CohortsBAL objCohortsBAL = new CohortsBAL();
            dtCohorts = objCohortsBAL.GetList(SURL);
            if (dtCohorts.Rows.Count > 0)
            {
                rptCohorts.DataSource = dtCohorts;
                rptCohorts.DataBind();
                rptCohorts.Visible = true;
            }
        }
        else
        {
            rptCohorts.Visible = false;
        }


        if (Session["TeacherUserID"] != null)
        {

            DataView dvPageAccess = new DataView(dt);
            string Pageurl = Request.Url.ToString();
            //Pageurl = Pageurl.Replace(Utility.Config.WebSiteUrl, string.Empty).Replace(RequestedSchoolURL, string.Empty).Replace("/", string.Empty);

            if (Pageurl.IndexOf("school-configuration.aspx") >= 0)
            {
                Pageurl = "school-configuration.aspx";
            }
            else
            {
                Pageurl = Pageurl.Replace(Utility.Config.WebSiteUrl, string.Empty).Replace(RequestedSchoolURL, string.Empty).Replace("/", string.Empty);
            }

            if (Pageurl.IndexOf("?") >= 0)
            {
                Pageurl = Pageurl.Substring(0, Pageurl.IndexOf("?"));
            }
            dvPageAccess.RowFilter = "PageVerify like '%," + Pageurl.ToLower() + "%'";

            if (dvPageAccess.Count == 0 && Pageurl.ToLower() != "dashboard.aspx" && Pageurl.ToLower() != "access-denied.aspx" && Pageurl.ToLower() != "testchart.aspx")
            {
                if (Request["c1"] != null)
                {
                    Response.Redirect(Config.WebSiteUrl + SURL + "/access-denied.aspx");
                }
                else
                {
                    Response.Redirect("access-denied.aspx");
                }

            }

        }
        else
        {
            if (Request["c1"] != null)
            {

                Response.Redirect(Config.WebSiteUrl + SURL + "/login.aspx");
            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }

    }
    private void BindAdminName()
    {
        //AdminBAL objAdmin = new AdminBAL();
        //DataTable dt = new DataTable();
        //if (Session["UserID"] != null)
        //    objAdmin.ID = Convert.ToInt32(Session["UserID"]);
        //dt = objAdmin.AdminByID();
        //if (dt.Rows.Count > 0)
        //{
        //    if (Convert.ToString(dt.Rows[0]["ImageName"]).Trim() != string.Empty)
        //        imgAvtar.Src = Utility.Config.VirtualDir + "thumb.aspx?path=" + Utility.Config.CMSFiles + Convert.ToString(dt.Rows[0]["ImageName"]) + "&width=40";
        //    UserName = Convert.ToString(Convert.ToString(dt.Rows[0]["FirstName"]));// + " " + Convert.ToString(dt.Rows[0]["LastName"]));
        //}
    }
    #endregion

    #region Check Cookie
    protected void Page_Init(object sender, EventArgs e)
    {
        CheckCookieLogin();
        if (Session["RequestedSchoolURL"] != null)
        {
            RequestedSchoolURL = Convert.ToString(Session["RequestedSchoolURL"]);
            if (RequestedSchoolURL.ToLower() != Convert.ToString(Request["c1"]).ToLower())
            {
                RemoveCookieAndSession();
                /*
                if (GeneralBAL.ValidateSchoolURLRequest(Convert.ToString(Request["c1"])))
                {
                    RequestedSchoolURL = Convert.ToString(Request["c1"]);
                    Session["RequestedSchoolURL"] = RequestedSchoolURL;
                }
                else
                {
                    Response.Redirect(Config.PageNotFoundUrl);
                }
                */
            }
        }
        else
        {
            if (GeneralBAL.ValidateSchoolURLRequest(Convert.ToString(Request["c1"])))
            {
                RequestedSchoolURL = Convert.ToString(Request["c1"]);
                Session["RequestedSchoolURL"] = RequestedSchoolURL;
            }
            else
            {
                Response.Redirect(Config.PageNotFoundUrl);
            }
        }
        BindDashboard();
        BindMenu();
        BindStudents();
    }
    private void BindDashboard()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        TeachersBAL objTeachersBAL = new TeachersBAL();
        if (Session["SchoolID"] != null)
        {
            //  dt = objTeachersBAL.TeacherDashboard(Convert.ToInt32(Session["TeacherUserID"]), Convert.ToInt32(Session["SchoolID"]));
            ds = TeacherDashboard(Convert.ToInt32(Session["TeacherUserID"]), Convert.ToInt32(Session["SchoolID"]));
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                Teachers = Convert.ToString(dt.Rows[0]["Teachers"]);
                HomeGroups = Convert.ToString(dt.Rows[0]["HomeGroups"]);
                Students = Convert.ToString(dt.Rows[0]["Students"]);
            }


            if (ds.Tables[1].Rows.Count > 0)
            {
                try
                {
                    int IsCompleted = 0;
                    IsCompleted = Convert.ToInt32(ds.Tables[1].Rows[0]["IsCompleted"]);
                    DateTime StartDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["StartDate"]);
                    DateTime EndDate = Convert.ToDateTime(ds.Tables[1].Rows[0]["EndDate"]);
                    int SemesterNo = Convert.ToInt32(ds.Tables[1].Rows[0]["SemesterNo"]);

                    if (IsCompleted == 0)
                    {
                        int RemainingDays = Convert.ToInt32((EndDate - Convert.ToDateTime(DateTime.Now.ToString("dd/MMM/yyyy"))).TotalDays);
                        if (RemainingDays <= 10)
                        {
                            spnSemesterHeadings.InnerHtml = "Current Semester " + SemesterNo + " - " + StartDate.ToString("yyyy") + ", End Date: " + EndDate.ToString("dd/MMM/yyyy") + ", <span style='color:#ff7777;font-weight: 600;'>" + RemainingDays.ToString() + " days remaining.</span>";
                        }
                        else {
                            spnSemesterHeadings.InnerHtml = "Current Semester " + SemesterNo + " - " + StartDate.ToString("yyyy") + ", End Date: " + EndDate.ToString("dd/MMM/yyyy") + ", <span>" + RemainingDays.ToString() + " days remaining.</span>";
                        }
                    }
                    else
                    {
                        if (SemesterNo == 2)
                        {
                            spnSemesterHeadings.InnerHtml = "Sem 1 is now locked. Sem 2 will commence on " + StartDate.ToString("dd/MMM/yyyy");
                        }
                        else {
                            spnSemesterHeadings.InnerHtml = "Sem 2 is now locked. Sem 1 will commence on " + StartDate.ToString("dd/MMM/yyyy");
                        }
                    }
                }
                catch(Exception ex)
                {

                }

            }
        }
    }
    private void CheckCookieLogin()
    {
        if (Session["TeacherUserID"] == null)
        {
            // HttpCookie cookie = HttpContext.Current.Request.Cookies["jud#ge$eMe#$nTTe@chER"];
            //HttpCookie cookie = HttpContext.Current.Response.Cookies["jud#ge$eMe#$nTTe@chER"];
            HttpCookie cookie = HttpContext.Current.Request.Cookies["jud#ge$eMe#$nTTe@chER"];
            if (cookie != null)
            {
                // if (Convert.ToDateTime(cookie.Expires.Date) >= DateTime.Now)
                if (cookie.Value != "")
                {
                    int UserID = Convert.ToInt32(EncryptDescrypt.DecryptString(cookie.Value));
                    TeachersBAL objTeachersBAL = new TeachersBAL();
                    objTeachersBAL.ID = UserID;

                    DataTable dt = new DataTable();
                    dt = objTeachersBAL.TeacherDetaiolsByID();
                    if (dt.Rows.Count > 0)
                    {
                        Session["SchoolID"] = Convert.ToString(dt.Rows[0]["SchoolID"]);
                        Session["IsTeacher"] = Convert.ToString(dt.Rows[0]["IsTeacher"]);
                        Session["EmailID"] = Convert.ToString(dt.Rows[0]["EmailID"]);
                        Session["TeacherUserID"] = Convert.ToString(dt.Rows[0]["ID"]);
                        Session["TeacherName"] = Convert.ToString(dt.Rows[0]["FirstName"]) + " " + Convert.ToString(dt.Rows[0]["LastName"]);
                        Session["IsGoogleUser"] = Convert.ToString(dt.Rows[0]["IsGoogleUser"]);

                        DataSet dsSchool = new DataSet();
                        SchoolBAL objSchoolBAL = new SchoolBAL();
                        dsSchool = objSchoolBAL.GetCurrentSemester(RequestedSchoolURL);
                        if (dsSchool.Tables[0].Rows.Count > 0)
                        {
                            Session["SchoolCurrentYear"] = Convert.ToString(dsSchool.Tables[0].Rows[0]["CurrentYear"]);
                            Session["SchoolCurrentSemester"] = Convert.ToString(dsSchool.Tables[0].Rows[0]["SemesterNo"]);
                        }

                    }
                    else
                    {
                        if (cookie != null)
                        {
                            RemoveCookieAndSession();
                            //cookie.Expires = DateTime.Now.AddDays(-3);
                            //HttpContext.Current.Response.Cookies.Add(cookie);
                        }
                    }
                }
            }
        }
        if (Session["SchoolCurrentYear"] == null)
        {
            DataSet ds = new DataSet();
            SchoolBAL objSchoolBAL = new SchoolBAL();
            ds = objSchoolBAL.GetCurrentSemester(RequestedSchoolURL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Session["SchoolCurrentYear"] = Convert.ToString(ds.Tables[0].Rows[0]["CurrentYear"]);
                Session["SchoolCurrentSemester"] = Convert.ToString(ds.Tables[0].Rows[0]["SemesterNo"]);
            }
        }
        
        if (!GeneralBAL.ValidateSchoolTeacher(Convert.ToInt32(Session["SchoolID"]), Convert.ToInt32(Session["TeacherUserID"])))
        {
            RemoveCookieAndSession();
            Response.Redirect(Config.WebSiteUrl + Convert.ToString(Request["c1"]).ToLower() + "/login.aspx", true);
        }

        // Response.Redirect(Config.WebSiteUrl + Convert.ToString(Request["c1"]).ToLower() + "/login.aspx", true);

    }

    private void RemoveCookieAndSession()
    {
        Session["SchoolID"] = null;
        Session["IsTeacher"] = null;
        Session["EmailID"] = null;
        Session["TeacherUserID"] = null;
        Session["TeacherName"] = null;
        Session["SchoolCurrentYear"] = null;
        Session["SchoolCurrentSemester"] = null;
        Session["IsGoogleUser"] = null;


        var httpContext = new HttpContextWrapper(HttpContext.Current);
        var _response = httpContext.Response;

        HttpCookie cookie = new HttpCookie("jud#ge$eMe#$nTTe@chER")
        {
            Expires = DateTime.Now.AddDays(-3) 
        };
        _response.Cookies.Set(cookie);


        Session.Abandon();

        //HttpCookie cookie = HttpContext.Current.Request.Cookies["jud#ge$eMe#$nTTe@chER"];
        //if (cookie != null)
        //{
        //    cookie.Expires = DateTime.Now.AddDays(-3);
        //    HttpContext.Current.Response.Cookies.Add(cookie);
        //}

    }
    #endregion

    public DataSet TeacherDashboard(int TeacherID, int SchoolID)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@TeacherID", DbParameter.DbType.Int, 20, TeacherID),
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 20, SchoolID),
            };
        DataSet dt = new DataSet();
        return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "TeacherDashboard", dbParam);
    }
}
