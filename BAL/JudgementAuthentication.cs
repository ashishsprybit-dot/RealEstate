namespace BAL
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Web;
    using System.Web.UI;
    using Utility;
    using Utility.Security;

    public class JudgementAuthentication : Page
    {
        private static string CookieName = "jud#ge$eMe#$nTTe@chER";
        private static char SplitStr = '!';
        private static bool _IsTeacherCookieLoggedIn;
        private static long _TeacherID;        
        public static bool GetTeacherCookieInfo()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
            {
                string[] strArray = EncryptDescrypt.DecryptString(cookie.Value).Split(new char[] { SplitStr });
                if (strArray.Length == 1)
                {
                    _TeacherID = Convert.ToInt32(strArray[0]);
                    _IsTeacherCookieLoggedIn = true;                    
                    return IsTeacherCookieLoggedIn;
                }
            }
            LogOut();
            return IsTeacherCookieLoggedIn;
        }

        public static void LogOut()
        {
            _IsTeacherCookieLoggedIn = false;
            _TeacherID = 0;            
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-300.0);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }

        protected virtual void Page_PreInit(object sender, EventArgs e)
        {
           
        }       
        public static void SetTeacherCookieInfo(long intTeacherID)
        {
            _TeacherID = intTeacherID;
            _IsTeacherCookieLoggedIn = true;
            HttpCookie objCookie = HttpContext.Current.Request.Cookies[CookieName];
            if ((objCookie != null))
            {
                objCookie.Expires = DateTime.Now.AddHours(-3);
            }
            objCookie = new HttpCookie(CookieName, Utility.Security.EncryptDescrypt.EncryptString(Convert.ToString(TeacherID)));           
            objCookie.Expires = DateTime.Now.AddDays(1);            
            HttpContext.Current.Response.Cookies.Add(objCookie);
        }
        public static long TeacherID
        {
            get
            {
                return _TeacherID;
            }
        }  
      
        private static bool IsTeacherCookieLoggedIn
        {
            get
            {
                return _IsTeacherCookieLoggedIn;
            }
        }

       
    }
}

