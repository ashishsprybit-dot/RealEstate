using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Web.SessionState;
using System.Collections;
using System.Web;

namespace BAL
{

    public class AdminAuthentication : System.Web.UI.Page
    {
        public int CurrentPage;
        public int PagingLimit;
        public int RecordPerPage;
        public string SortColumn;
        public string SortType;

        private static int _AdminID;
        private static string _EmailID;
        private static bool _IsAdminLoggedIn;
        private static bool _IsSuperAdmin;
        private static string _UserName;
        private static string _FirstName;
        private static string _LastName;
        private static string CookieName;
        private static char SplitStr;

        private static int _AdminType;
        public static int AdminType
        {
            get { return _AdminType; }
            set { _AdminType = value; }
        }

        ArrayList _PageRights;
        private static string _PageRightsName;
        public static int AdminID
        {
            get
            {
                return AdminAuthentication._AdminID;
            }
        }

        public static string EmailID
        {
            get
            {
                return AdminAuthentication._EmailID;
            }
        }

        private static bool IsAdminLoggedIn
        {
            get
            {
                return AdminAuthentication._IsAdminLoggedIn;
            }
        }
        public static string UserName
        {
            get
            {
                return AdminAuthentication._UserName;
            }
        }
        public static string FirstName
        {
            get
            {
                return AdminAuthentication._FirstName;
            }
        }
        public static string LastName
        {
            get
            {
                return AdminAuthentication._LastName;
            }
        }
        public static bool IsSuperAdmin
        {
            get { return _IsSuperAdmin; }

        }
        public ArrayList PageRights
        {
            get { return _PageRights; }
        }
        public static string PageRightsName
        {
            get
            {
                return AdminAuthentication._PageRightsName;
            }
            //set
            //{
            //    AdminAuthentication._PageRightsName = value;
            //}
        }
        public AdminAuthentication()
        {
            CurrentPage = 1;
            RecordPerPage = 25;
            PagingLimit = 15;
            SortColumn = System.String.Empty;
            SortType = System.String.Empty;
        }

        static AdminAuthentication()
        {
            AdminAuthentication.CookieName = "r65n7268ocs";
            AdminAuthentication.SplitStr = '!';
        }

        public AdminAuthentication(ArrayList AdminPageRights)
        {

            this._PageRights = AdminPageRights;
        }
        

        #region GetAdminInfo
        public static bool GetAdminInfo()
        {
            HttpCookie objCookie = HttpContext.Current.Request.Cookies[CookieName];
            if ((objCookie != null))
            {
                string[] strCookValue = Utility.Security.EncryptDescrypt.DecryptString(objCookie.Value).Split(SplitStr);
                if ((strCookValue.Length == 7))
                {
                    _AdminID = Convert.ToInt32(strCookValue[0]);
                    _EmailID = Convert.ToString(strCookValue[1]);
                    _UserName = Convert.ToString(strCookValue[2]);
                    _IsAdminLoggedIn = true;
                    _IsSuperAdmin = Convert.ToBoolean(strCookValue[3]);
                    _FirstName = Convert.ToString(strCookValue[4]);
                    _LastName = Convert.ToString(strCookValue[5]);
                    _AdminType = Convert.ToInt32(strCookValue[6]);
                    return IsAdminLoggedIn;
                }
            }
            LogOut();
            return IsAdminLoggedIn;
        }
        #endregion

        #region SetAdminInformation
        public static void SetAdminInfo(int intAdminID, string strUserName, string strEmail, bool IsSuper, string strFirstName, string strLastName, int AdminType)
        {
            _AdminID = intAdminID;
            _UserName = strUserName;
            _EmailID = strEmail;
            _IsAdminLoggedIn = true;
            _IsSuperAdmin = IsSuper;
            _FirstName = strFirstName;
            _LastName = strLastName;
            _AdminType = AdminType;

            HttpCookie objCookie = HttpContext.Current.Request.Cookies[CookieName];
            HttpCookie objCookiePageList = HttpContext.Current.Request.Cookies[CookieName];
            objCookiePageList = new HttpCookie(CookieName, "");
            if ((objCookie != null))
            {
                objCookie.Expires = DateTime.Now.AddDays(-3);
            }
            AdminBAL ObjAdminUser = new AdminBAL();
            ObjAdminUser.ID = intAdminID;

            DataSet dtresult;
            dtresult = ObjAdminUser.AdminLoginCheck();
            ArrayList ObjPageList = new ArrayList();
            ArrayList ObjModuleWebsite = new ArrayList();
            for (int i = 0; i < dtresult.Tables[1].Rows.Count; i++)
            {
                ObjPageList.Add(Convert.ToString(dtresult.Tables[1].Rows[i]["varAdminPageName"]));
                objCookiePageList.Values["Value" + i.ToString()] = ObjPageList[i].ToString();

            }
            ObjPageList.Add(Convert.ToString("dashboard.aspx"));
            objCookie = new HttpCookie(CookieName, Utility.Security.EncryptDescrypt.EncryptString(Convert.ToString(AdminID) + SplitStr + EmailID + SplitStr + UserName + SplitStr + IsSuper + SplitStr + FirstName + SplitStr + LastName + SplitStr + AdminType));

            AdminAuthentication ObjAdminInfo = new AdminAuthentication(ObjPageList);
            //HttpContext.Current.Session["AdminInfo"] = ObjAdminInfo; 
            HttpContext.Current.Response.Cookies.Add(objCookiePageList); 
            _PageRightsName = objCookiePageList.Value;

            HttpContext.Current.Response.Cookies.Add(objCookie);

        }
        #endregion

        #region Log out
        public static void LogOut()
        {
            _IsAdminLoggedIn = false;
            _AdminID = 0;
            _UserName = string.Empty;
            _EmailID = string.Empty;
            _FirstName = string.Empty;
            _LastName = string.Empty;
            _AdminType = 0;
            HttpCookie objCookie = HttpContext.Current.Request.Cookies[CookieName];
            if ((objCookie != null))
            {
                objCookie.Expires = DateTime.Now.AddDays(-300);
                //HttpContext.Current.Session["AdminInfo"] = null;
                HttpContext.Current.Response.Cookies.Add(objCookie);
            }

        }
        #endregion

        #region Page Initialization
        protected virtual void Page_PreInit(object sender, System.EventArgs e)
        {
            if (!GetAdminInfo())
            {
                Utility.Common.RedirectToAdminLoginPage(Request.RawUrl);
            }

        }
        #endregion
    }

}

