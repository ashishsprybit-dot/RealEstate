namespace BAL
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Web;
    using System.Web.UI;
    using Utility;
    using Utility.Security;

    public class AdminAuthentication : Page
    {
        private static int _AdminID;
        private static int _AdminType;
        private static string _EmailID;
        private static string _FirstName;
        private static bool _IsAdminLoggedIn;
        private static bool _IsSuperAdmin;
        private static string _LastName;
        private ArrayList _PageRights;
        private static string _PageRightsName;
        private static string _UserName;
        private static string CookieName = "r65n7268ocs";
        public int CurrentPage;
        public int PagingLimit;
        public int RecordPerPage;
        public string SortColumn;
        public string SortType;
        private static char SplitStr = '!';

        public AdminAuthentication()
        {
            this.CurrentPage = 1;
            this.RecordPerPage = 0x19;
            this.PagingLimit = 15;
            this.SortColumn = string.Empty;
            this.SortType = string.Empty;
        }

        public AdminAuthentication(ArrayList AdminPageRights)
        {
            this._PageRights = AdminPageRights;
        }

        public static bool GetAdminInfo()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
            {
                string[] strArray = EncryptDescrypt.DecryptString(cookie.Value).Split(new char[] { SplitStr });
                if (strArray.Length == 7)
                {
                    _AdminID = Convert.ToInt32(strArray[0]);
                    _EmailID = Convert.ToString(strArray[1]);
                    _UserName = Convert.ToString(strArray[2]);
                    _IsAdminLoggedIn = true;
                    _IsSuperAdmin = Convert.ToBoolean(strArray[3]);
                    _FirstName = Convert.ToString(strArray[4]);
                    _LastName = Convert.ToString(strArray[5]);
                    _AdminType = Convert.ToInt32(strArray[6]);
                    return IsAdminLoggedIn;
                }
            }
            LogOut();
            return IsAdminLoggedIn;
        }

        public static void LogOut()
        {
            _IsAdminLoggedIn = false;
            _AdminID = 0;
            _UserName = string.Empty;
            _EmailID = string.Empty;
            _FirstName = string.Empty;
            _LastName = string.Empty;
            _AdminType = 0;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        
        protected virtual void Page_PreInit(object sender, EventArgs e)
        {
            if (!GetAdminInfo())
            {
                Common.RedirectToAdminLoginPage(base.Request.RawUrl);
            }
        }

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
            if ((objCookie != null))
            {
                objCookie.Expires = DateTime.Now.AddDays(-3);
            }
            objCookie = new HttpCookie(CookieName, Utility.Security.EncryptDescrypt.EncryptString(Convert.ToString(AdminID) + SplitStr + EmailID + SplitStr + UserName + SplitStr + IsSuper + SplitStr + FirstName + SplitStr + LastName + SplitStr + AdminType));
            objCookie.Expires = DateTime.Now.AddDays(1);
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }

        public static int AdminID
        {
            get
            {
                return _AdminID;
            }
        }

        public static int AdminType
        {
            get
            {
                return _AdminType;
            }
            set
            {
                _AdminType = value;
            }
        }

        public static string EmailID
        {
            get
            {
                return _EmailID;
            }
        }

        public static string FirstName
        {
            get
            {
                return _FirstName;
            }
        }

        private static bool IsAdminLoggedIn
        {
            get
            {
                return _IsAdminLoggedIn;
            }
        }

        public static bool IsSuperAdmin
        {
            get
            {
                return _IsSuperAdmin;
            }
        }

        public static string LastName
        {
            get
            {
                return _LastName;
            }
        }

        public ArrayList PageRights
        {
            get
            {
                return this._PageRights;
            }
        }

        public static string PageRightsName
        {
            get
            {
                return _PageRightsName;
            }
        }

        public static string UserName
        {
            get
            {
                return _UserName;
            }
        }
    }
}

