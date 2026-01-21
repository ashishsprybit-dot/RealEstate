using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility.Security;
using Utility;
using BAL;
using System.Data;

public partial class Login : System.Web.UI.Page
{
    TenantBAL objTenantBAL = new TenantBAL();
    protected string RequestedSchoolURL = "";
    protected string BackgroundImage = "";
    protected string SchoolLogo = "";
    protected int showsuccesspopup = 0;
    protected string PopupMessage = "";
    protected string PopupHeading = "";
    private string CookieName = "Te@c$%^&E@#rJud#%^ge#ment";


    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        // Validate request URL ===============================================================

        if (Request["c1"] != null)
        {
            if (Session["RequestedSchoolURL"] != null)
            {
                RequestedSchoolURL = Convert.ToString(Session["RequestedSchoolURL"]);
                if (RequestedSchoolURL.ToLower() != Convert.ToString(Request["c1"]).ToLower())
                {
                    Session["TenantID"] = null;
                    Session["IsTenantuser"] = null;
                    Session["EmailID"] = null;
                    Session["TenantUserID"] = null;
                    Session["TenantName"] = null;
                    Session["SchoolCurrentYear"] = null;
                    Session["SchoolCurrentSemester"] = null;
                    Session["IsGoogleUser"] = null;
                    Session.Abandon();

                    HttpCookie cookie = HttpContext.Current.Request.Cookies["jud#ge$eMe#$nTTe@chER"];
                    if (cookie != null)
                    {
                        cookie.Expires = DateTime.Now.AddDays(-3);
                        HttpContext.Current.Response.Cookies.Add(cookie);
                    }

                    Response.Redirect(Config.WebSiteUrl + Convert.ToString(Request["c1"]).ToLower() + "/login.aspx", true);
                    //if (GeneralBAL.ValidateSchoolURLRequest(Convert.ToString(Request["c1"])))
                    //{
                    //    RequestedSchoolURL = Convert.ToString(Request["c1"]);
                    //    Session["RequestedSchoolURL"] = RequestedSchoolURL;
                    //}
                    //else
                    //{
                    //    Response.Redirect(Config.PageNotFoundUrl);
                    //}
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
        }
        else
        {
            Response.Redirect(Config.PageNotFoundUrl);
        }

        // Validate request URL ===============================================================

        if (Request.Form.Keys.Count > 0)
        {
            switch (Request["type"])
            {
                case "login":
                    CheckAuthentication(Request["tbxUsername"], Request["tbxPassword"]);
                    break;
                case "signup":
                    SignUP();
                    break;
                case "forgetpwd":
                    GetTeacherPassword(Request["forgotemail"]);
                    break;
                case "userchangepassword":
                    UserChangePassword();
                    break;
            }
        }
        else
        {
            if (Request["type"] == "logout")
            {
                //HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
                //if (cookie != null)
                //{
                //    cookie.Expires = DateTime.Now.AddDays(-300.0);
                //    HttpContext.Current.Response.Cookies.Add(cookie);
                //}

                //AdminAuthentication.LogOut();
                HttpContext.Current.Response.Write("<script>window.location.href='" + Config.VirtualDir + RequestedSchoolURL + "/login.aspx?m=logout';</script>");
                HttpContext.Current.Response.End();
            }
        }
        if (Request["m"] != null)
        {
            if (Convert.ToString(Request["m"]).ToLower().Trim() == "logout")
            {
                SessionClear();
                divMsg.InnerText = "You are logged out successfully.";
                divMsg.Attributes["class"] = "alert-message success";
                divMsg.Style["display"] = string.Empty;
            }
        }
        if (Session["TeacherUserID"] != null)
        {
            Response.Redirect("dashboard.aspx");
        }
        if (Request["gs"] != null)
        {
            showsuccesspopup = 1;
            PopupHeading = "Sign up";
            if (Convert.ToString(Request["gs"]) == "d")
            {
                PopupMessage = "This email is already registered with us, please contact admin in case you need any help.";
            }
            else if (Convert.ToString(Request["gs"]) == "s")
            {
                PopupMessage = "Thank you for signing up with us. Admin will get in touch with you for further steps.";
            }

            //PopupMessage
        }


        if (Request["s"] != null)
        {
            showsuccesspopup = 1;
            PopupHeading = "Sign in";
            if (Convert.ToString(Request["s"]) == "d")
            {
                PopupMessage = "Your email is already registered with us and deactivated by admin. Please contact admin for any further help.";
            }
            else if (Convert.ToString(Request["s"]) == "n")
            {
                PopupMessage = "Your account is under approval process. Please contact admin for more details.";
            }
            else if (Convert.ToString(Request["s"]) == "i")
            {
                PopupMessage = "Invalid login details. Please signup with google.";
            }
        }
        BindSchoolDetails();
    }
    #endregion
    private void UserChangePassword()
    {
        string oldpassword = Utility.Security.EncryptDescrypt.EncryptString(Convert.ToString(Request["tbxOldPassword"]));

        string Password = string.Empty;
        if (!string.IsNullOrEmpty(Request["tbxNewPassword"]))
            Password = Request["tbxNewPassword"];

        if (Password.Length < 6)
        {
            Response.Write("<script>DisplMsg('<%= divMsg.ClientID %>','Password must be at least 6 characters long.','alert-message error');</script>");
            Response.End();
        }

        TeachersBAL objTeachersBAL = new TeachersBAL();
        objTeachersBAL.OldPassword = oldpassword;
        Password = Utility.Security.EncryptDescrypt.EncryptString(Password);
        objTeachersBAL.Password = Password;
        if (Session["TeacherUserID"] != null)
        {
            objTeachersBAL.ID = Convert.ToInt32(Session["TeacherUserID"]);
        }
        else
        {
            objTeachersBAL.ID = 0;
        }
        switch (objTeachersBAL.Changepassword())
        {
            case 0:
                Response.Write("2,Your password has been changed successfully.");
                break;
            default:
                Response.Write("1,Current Password you have entered is invalid.");
                break;
        }
        Response.End();
    }

    private void GetTeacherPassword(string strEmailID)
    {
        string strTempPassword = Convert.ToString(Utility.Common.RandomString(10));
        TeachersBAL objTeachersBAL = new TeachersBAL();
        DataSet ds = new DataSet();
        ds = objTeachersBAL.TeacherDetailsForgotPassword(strEmailID, Utility.Security.EncryptDescrypt.EncryptString(strTempPassword), RequestedSchoolURL);

        int Result = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        if (Result == 1)
        {
            string HTMLMail = "";
            if (strEmailID != string.Empty)
            {
                string Subject = "Your password for Curriculum Level Tracker.";
                HTMLMail = Common.ReadFiles(Server.MapPath(Config.VirtualDir + "EmailTemplate/TeacherForgotPassword.html"));
                HTMLMail = HTMLMail.Replace("[[TEACHERNAME]]", Convert.ToString(ds.Tables[1].Rows[0]["FullName"]));
                HTMLMail = HTMLMail.Replace("[[SCHOOLNAME]]", Convert.ToString(ds.Tables[2].Rows[0]["Name"]));
                HTMLMail = HTMLMail.Replace("[[PASSWORD]]", strTempPassword);
                GeneralSettings.SendEmail(strEmailID, new GeneralSettings().getConfigValue("infoemail").ToString(), Subject, HTMLMail);
            }
            Response.Write("success");
        }
        else if (Result == -1)
        {
            Response.Write("google");
        }
        else if (Result == -2)
        {
            Response.Write("invalid");
        }


        Response.End();
    }

    private void BindSchoolDetails()
    {
        DataTable dt = new DataTable();
        SchoolBAL objSchoolBAL = new SchoolBAL();
        dt = objSchoolBAL.SchoolsDetailsByURL(RequestedSchoolURL);
        if (dt.Rows.Count > 0)
        {
            BackgroundImage = Convert.ToString(dt.Rows[0]["ImageName"]);
            SchoolLogo = Convert.ToString(dt.Rows[0]["SchoolLogo"]);
        }
    }

    #region Login Process
    private void SessionClear()
    {
        Session["TenantID"] = null;
        Session["IsTenantuser"] = null;
        Session["EmailID"] = null;
        Session["TenantUserID"] = null;
        Session["TenantName"] = null;
        Session["SchoolCurrentYear"] = null;
        Session["SchoolCurrentSemester"] = null;
        Session["IsGoogleUser"] = null;

 
        Session.Abandon();
    }
    private void CheckAuthentication(string strUsername, string strPassword)
    {
        string strValidation = string.Empty;
        if (IsValidMemberLoginValidation(out strValidation))
        {
            objTenantBAL.UserName = strUsername;
            objTenantBAL.Password = Utility.Security.EncryptDescrypt.EncryptString(strPassword);
            DataSet ds = new DataSet();
            ds = objTenantBAL.TenantLogin("", RequestedSchoolURL);
            int Status = 0;
            DataTable dt = new DataTable();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Status = Convert.ToInt32(ds.Tables[0].Rows[0]["Status"]);
            }

            if (Status == 1)
            {
                dt = ds.Tables[1];
                if (dt.Rows.Count > 0)
                {
                    Session["TenantID"] = Convert.ToString(dt.Rows[0]["TenantID"]);
                    Session["IsTenantuser"] = Convert.ToString(dt.Rows[0]["IsTenantuser"]);
                    Session["EmailID"] = Convert.ToString(dt.Rows[0]["EmailID"]);
                    Session["TenantUserID"] = Convert.ToString(dt.Rows[0]["ID"]);
                    Session["TenantName"] = Convert.ToString(dt.Rows[0]["FirstName"]) + " " + Convert.ToString(dt.Rows[0]["LastName"]);
                    

                    //// Set Cookie ------------------------------------------------------------
                    JudgementAuthentication.SetTeacherCookieInfo(Convert.ToInt64(dt.Rows[0]["ID"]));

                    //// Set Cookie ------------------------------------------------------------
                    //HttpCookie objCookie = HttpContext.Current.Request.Cookies["Te@c$%^&E@#rJud#%^ge#ment"];
                    //if ((objCookie != null))
                    //{
                    //    objCookie.Expires = DateTime.Now.AddDays(-3);
                    //}
                    //objCookie = new HttpCookie("Te@c$%^&E@#rJud#%^ge#ment", Utility.Security.EncryptDescrypt.EncryptString(Convert.ToString(dt.Rows[0]["ID"])));
                    //objCookie.Expires = DateTime.Now.AddDays(1);
                    //HttpContext.Current.Response.Cookies.Add(objCookie);


                }

                //if (ds.Tables.Count > 1)
                //{
                //    if (ds.Tables[2].Rows.Count > 0)
                //    {
                //        Session["SchoolCurrentYear"] = Convert.ToString(ds.Tables[2].Rows[0]["CurrentYear"]);
                //        Session["SchoolCurrentSemester"] = Convert.ToString(ds.Tables[2].Rows[0]["SemesterNo"]);
                //    }
                //}

                Response.Write("success");
            }
            else if (Status == 0)
            {
                Response.Write("deactivated");
            }
            else if (Status == 2)
            {
                Response.Write("notapproved");
            }
            else if (Status == -1)
            {
                Response.Write("invalid");
            }
        }
        else
        {
            Response.Write("invalid");
        }
        Response.End();
    }

    private bool IsValidMemberLoginValidation(out string strErrMsg)
    {
        string[] strValidMsg = new string[2];
        strErrMsg = string.Empty;
        strValidMsg[0] = Validation.RequireField(Request["tbxUsername"], "Username");
        strValidMsg[1] = Validation.RequireField(Request["tbxPassword"], "Password");
        strErrMsg = Utility.Validation.GenerateErrorMessage(strValidMsg, "<br/> - ");
        return (strErrMsg.Length == 0 ? true : false);
    }
    #endregion

    #region Signup Process

    private void SignUP()
    {
        //objTeachersBAL.ID = 0;
        //string strPasword = string.Empty;

        //if (!string.IsNullOrEmpty(Request.Form["tbxTeacherPassword"]))
        //{
        //    strPasword = Request.Form["tbxTeacherPassword"];
        //}
        //objTeachersBAL.ImageName = "";
        //objTeachersBAL.FirstName = Request.Form["tbxTeacherFirstName"].Trim();
        //objTeachersBAL.LastName = Request.Form["tbxTeacherLastName"].Trim();

        //if (strPasword != string.Empty)
        //{
        //    objTeachersBAL.Password = Utility.Security.EncryptDescrypt.EncryptString(strPasword);
        //}
        //else
        //{
        //    objTeachersBAL.Password = "";
        //}
        //objTeachersBAL.EmailID = Request.Form["tbxTeacherEmail"].Trim();
        //objTeachersBAL.Phone = Request.Form["tbxTeacherPhone"].Trim();
        //objTeachersBAL.Status = 2;

        //string assignedModules = "";
        //switch (objTeachersBAL.Save(1, assignedModules, "", RequestedSchoolURL))
        //{
        //    case 0:
        //        Response.Write("duplicate");
        //        break;
        //    default:
        //        //Send Email to User and Admin

        //        DataSet ds = new DataSet();
        //        SchoolBAL objSchoolBAL = new SchoolBAL();
        //        ds = objSchoolBAL.SchoolAndAdminList(Convert.ToString(Session["RequestedSchoolURL"]));
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            SendUserEmail((objTeachersBAL.FirstName + " " + objTeachersBAL.LastName), objTeachersBAL.EmailID, Convert.ToString(ds.Tables[0].Rows[0]["Name"]));
        //        }

        //        if (ds.Tables[1].Rows.Count > 0)
        //        {
        //            SendAdminEmail(ds.Tables[1], Convert.ToString(ds.Tables[0].Rows[0]["Name"]), (objTeachersBAL.FirstName + " " + objTeachersBAL.LastName), objTeachersBAL.EmailID);
        //        }

        //        Response.Write("success");
        //        break;
        //}
        Response.End();
    }

    #endregion

    #region Send Email
    private void SendUserEmail(string Name, string Email, string SchoolName)
    {
        string HTMLMail = "";
        if (Email != string.Empty)
        {
            string Subject = "Thank you for your registration at " + SchoolName;
            HTMLMail = Common.ReadFiles(Server.MapPath(Config.VirtualDir + "EmailTemplate/UserRegistration.html"));
            HTMLMail = HTMLMail.Replace("[[NAME]]", Name);
            HTMLMail = HTMLMail.Replace("[[SCHOOLNAME]]", SchoolName);
            GeneralSettings.SendEmail(Email, new GeneralSettings().getConfigValue("infoemail").ToString(), Subject, HTMLMail);
        }
    }
    private void SendAdminEmail(DataTable dtAdmin, string SchoolName, string TeacherName, string TeacherEmail)
    {
        string HTMLMail = "";
        HTMLMail = Common.ReadFiles(Server.MapPath(Config.VirtualDir + "EmailTemplate/AdminRegistration.html"));
        if (dtAdmin.Rows.Count > 0)
        {
            for (int i = 0; i < dtAdmin.Rows.Count; i++)
            {
                string Email = Convert.ToString(dtAdmin.Rows[i]["EmailID"]);
                string Name = Convert.ToString(dtAdmin.Rows[i]["Name"]);

                if (Email != string.Empty)
                {
                    string Subject = "New teacher registration at " + SchoolName;

                    HTMLMail = HTMLMail.Replace("[[NAME]]", Name);
                    HTMLMail = HTMLMail.Replace("[[SCHOOLNAME]]", SchoolName);

                    HTMLMail = HTMLMail.Replace("[[TEACHERNAME]]", TeacherName);
                    HTMLMail = HTMLMail.Replace("[[TEACHEREMAIL]]", TeacherEmail);
                    GeneralSettings.SendEmail(Email, new GeneralSettings().getConfigValue("infoemail").ToString(), Subject, HTMLMail);
                }
            }
        }
    }
    #endregion
}