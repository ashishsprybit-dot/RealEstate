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
using DAL;

public partial class Login : System.Web.UI.Page
{
    AdminBAL objAdminBAL = new AdminBAL();

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            switch (Request["type"])
            {
                case "login":
                    CheckAdminAuthentication(Request["tbxUsername"], Request["tbxPassword"]);
                    break;
                case "otp":
                    ValidateOTP(Request["tbxU"], Request["tbxP"], Request["tbxOTP"]);
                    break;
                case "forgetpwd":
                    GetAdminPassword(Request["forgotemail"]);
                    break;
                case "userChangePassword":
                    UserChangePassword();
                    break;
            }
        }
        else
        {
            if (Request["type"] == "logout")
            {
                AdminAuthentication.LogOut();
                HttpContext.Current.Response.Write("<script>window.location.href='" + Config.VirtualDir + "adminpanel/login.aspx?m=logout';</script>");
                HttpContext.Current.Response.End();
            }
        }
        if (Request["m"] != null)
        {
            if (Convert.ToString(Request["m"]).ToLower().Trim() == "logout")
            {
                LogoutProcess();
                divMsg.InnerText = "You are logged out successfully.";
                divMsg.Attributes["class"] = "alert-message success";
                divMsg.Style["display"] = string.Empty;
            }
        }
    }
    #endregion

    #region Login Process
    private void ValidateOTP(string Username, string Password, string OTP)
    {
        int Result = 0;

        if (OTP == "123" && Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["IsSiteLive"]) == "0")
        {
            Result = 1;
        }
        else
        {

            DbParameter[] dbParam = new DbParameter[]
            {
                new DbParameter("@UserName", DbParameter.DbType.VarChar, 500, Username),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 500, Utility.Security.EncryptDescrypt.EncryptString(Password)),
                new DbParameter("@OTP", DbParameter.DbType.VarChar, 50, OTP),
                new DbParameter("@Type", DbParameter.DbType.VarChar, 50, "VALIDATEOTP"),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output),
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "AdminOTPSave", dbParam);
            Result = Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
        }
        if (Result == 1)
        {
            objAdminBAL.UserName = Username;
            objAdminBAL.Password = Utility.Security.EncryptDescrypt.EncryptString(Password);
            Int16 intResult;
            DataTable dtblAdmin = objAdminBAL.LoginCeck(out intResult);
            if (intResult == 0)
            {
                AdminAuthentication.SetAdminInfo(Convert.ToInt32(dtblAdmin.Rows[0]["ID"]), Convert.ToString(dtblAdmin.Rows[0]["UserName"]), Convert.ToString(dtblAdmin.Rows[0]["EmailID"]), Convert.ToBoolean(dtblAdmin.Rows[0]["IsSupAdmin"]), Convert.ToString(dtblAdmin.Rows[0]["FirstName"]), Convert.ToString(dtblAdmin.Rows[0]["LastName"]), Convert.ToInt32(dtblAdmin.Rows[0]["AdminType"]));
                Session["UserName"] = Convert.ToString(dtblAdmin.Rows[0]["UserName"]);
                Session["FirstName"] = Convert.ToString(dtblAdmin.Rows[0]["FirstName"]);
                Session["UserID"] = Convert.ToString(dtblAdmin.Rows[0]["ID"]);
                Session["AdminType"] = Convert.ToString(dtblAdmin.Rows[0]["AdminType"]);
                Session["EmailID"] = Convert.ToString(dtblAdmin.Rows[0]["EmailID"]);

                // SetIpAddress(Convert.ToInt32(dtblAdmin.Rows[0]["ID"]));
            }
            Response.Write(Utility.Javascript.RedirectToPage(Config.WebSiteUrl + "adminpanel/dashboard.aspx", true));
        }
        else if (Result == -1)
        {
            Response.Write("locked");
        }
        else if (Result == -2)
        {
            Response.Write("wrongotp");
        }
        Response.End();

    }
    private void CheckAdminAuthentication(string strUsername, string strPassword)
    {
        string strValidation = string.Empty;
        if (IsValidMemberLoginValidation(out strValidation))
        {
            objAdminBAL.UserName = strUsername;
            objAdminBAL.Password = Utility.Security.EncryptDescrypt.EncryptString(strPassword);
            short intResult;
            DataTable dtblAdmin = objAdminBAL.LoginCeck(out intResult);

            if (intResult == 0)
            {
                SendOTPToUserName(Convert.ToString(dtblAdmin.Rows[0]["UserName"]), Convert.ToString(dtblAdmin.Rows[0]["EmailID"]), strPassword);
                Response.Write("success");
            }
            else if (intResult == 1)
            {
                Response.Write("invalid");

            }
            else if (intResult == 2)
            {
                Response.Write("invalid");
            }
            /*
            Without OTP Code

            if (intResult == 0 && (Convert.ToString(dtblAdmin.Rows[0]["AdminType"]) != "2") && Convert.ToString(dtblAdmin.Rows[0]["AdminType"]) != "4")
                intResult = 1;

            if (intResult == 0 && (Convert.ToString(dtblAdmin.Rows[0]["AdminType"]) == "2" || Convert.ToString(dtblAdmin.Rows[0]["AdminType"]) == "4"))
            {
                AdminAuthentication.SetAdminInfo(Convert.ToInt32(dtblAdmin.Rows[0]["ID"]), Convert.ToString(dtblAdmin.Rows[0]["UserName"]), Convert.ToString(dtblAdmin.Rows[0]["EmailID"]), Convert.ToBoolean(dtblAdmin.Rows[0]["IsSupAdmin"]), Convert.ToString(dtblAdmin.Rows[0]["FirstName"]), Convert.ToString(dtblAdmin.Rows[0]["LastName"]), Convert.ToInt32(dtblAdmin.Rows[0]["AdminType"]));
                Session["UserName"] = Convert.ToString(dtblAdmin.Rows[0]["UserName"]);
                Session["FirstName"] = Convert.ToString(dtblAdmin.Rows[0]["FirstName"]);
                Session["UserID"] = Convert.ToString(dtblAdmin.Rows[0]["ID"]);
                Session["AdminType"] = Convert.ToString(dtblAdmin.Rows[0]["AdminType"]);
                Session["EmailID"] = Convert.ToString(dtblAdmin.Rows[0]["EmailID"]);
                //Session["IsSuperAdmin"] = Convert.ToString(dtblAdmin.Rows[0]["IsSupAdmin"]);

                AdminBAL objAdmin = new AdminBAL();
                objAdmin.SetIPAddress(Request.UserHostAddress.ToString(), Convert.ToInt32(dtblAdmin.Rows[0]["ID"]), false);

                Response.Write(Utility.Javascript.RedirectToPage(Config.WebSiteUrl + "adminpanel/dashboard.aspx", true));
            }
            else if (intResult == 1)
            {
                Response.Write(Utility.Javascript.DisplayMsgFront(divMsg.ClientID, "Your login credentials are incorrect. Please enter valid login credentials <br />or contact to administrator for more support.<br/>", Javascript.MessageType.Error, true));
                string strScript = Javascript.ScriptStartTag;
                strScript += "$('#tbxPassword').val('');";
                strScript += Javascript.ScriptEndTag;
                Response.Write(strScript);

            }
            else if (intResult == 2)
            {
                Response.Write(Utility.Javascript.DisplayMsgFront(divMsg.ClientID, "Your account is deactivated. Kindly contact administrator for more details.", Javascript.MessageType.Error, true));
            }


            */
        }
        else
        {
            // Response.Write(Javascript.DisplayMsgFront(divMsg.ClientID, strValidation, Javascript.MessageType.Error, true));
        }
        Response.End();
    }
    private void SendOTPToUserName(string strUsername, string Email, string strPassword)
    {
        DataTable dt = new DataTable();
        string strOTP = Utility.Common.RandomString(5);
        int Result = 0;

        DbParameter[] dbParam = new DbParameter[]
        {
            new DbParameter("@UserName", DbParameter.DbType.VarChar, 500, strUsername),
            new DbParameter("@Password", DbParameter.DbType.VarChar, 500, Utility.Security.EncryptDescrypt.EncryptString(strPassword)),
            new DbParameter("@OTP", DbParameter.DbType.VarChar, 50, strOTP),
            new DbParameter("@Type", DbParameter.DbType.VarChar, 50, "UPDATEOTP"),
            new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output),
        };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "AdminOTPSave", dbParam);
        Result = Convert.ToInt32(dbParam[dbParam.Length - 1].Value);

        if (Result > 0)
        {
            string strSubject = "OTP for Curriculum Level Tracker";

            string HTMLMail = "OTP for Curriculum Level Tracker.";
            HTMLMail = Common.ReadFiles(Server.MapPath(Config.VirtualDir + "EmailTemplate/AdminOTP.html"));
            HTMLMail = HTMLMail.Replace("[[ADMINNAME]]", strUsername);
            HTMLMail = HTMLMail.Replace("[[OTP]]", strOTP);
            GeneralSettings.SendEmail(Email.Trim(), new GeneralSettings().getConfigValue("infoemail").ToString(), strSubject, HTMLMail.ToString());
        }

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

    #region Logout Process
    private void LogoutProcess()
    {
        Session["UserName"] = null;
        Session["FirstName"] = null;
        Session["UserID"] = null;
        Session["AdminType"] = null;
        Session["EmailID"] = null;
        if (Request.Cookies["AdminName"] != null)
        {
            Response.Cookies["AdminName"].Expires = DateTime.Now.AddDays(-1);
        }
        if (Request.Cookies["AdminInfo"] != null)
        {
            Response.Cookies["AdminInfo"].Expires = DateTime.Now.AddDays(-1);
        }
        // Utility.Common.DisplayMessage(divMsg, "", Common.ErrorMsgType.Success);        
    }
    #endregion

    #region Admin Change Password
    /// <summary>
    /// Users the change password.
    /// </summary>
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

        AdminBAL objAdminBAL = new AdminBAL();
        objAdminBAL.OldPassword = oldpassword;
        Password = Utility.Security.EncryptDescrypt.EncryptString(Password);
        objAdminBAL.Password = Password;
        if (Session["UserID"] != null)
        {
            objAdminBAL.ID = Convert.ToInt32(Session["UserID"]);
        }
        else
        {
            objAdminBAL.ID = AdminAuthentication.AdminID;
        }
        switch (objAdminBAL.Changepassword())
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
    #endregion

    #region Admin Forget Password
    private void GetAdminPassword(string strEmailID)
    {
        string strValidation = string.Empty;

        objAdminBAL.EmailID = strEmailID;
        DataTable dtblAdminInfo = objAdminBAL.GetPassword();
        if (dtblAdminInfo.Rows.Count > 0)
        {

            string HTMLMail = "";

            if (strEmailID != string.Empty)
            {
                string Subject = "Your password for Curriculum Level Tracker Admin Portal";
                HTMLMail = Common.ReadFiles(Server.MapPath(Config.VirtualDir + "EmailTemplate/TeacherForgotPassword.html"));
                HTMLMail = HTMLMail.Replace("[[TEACHERNAME]]", Convert.ToString(dtblAdminInfo.Rows[0]["FirstName"]).Trim());
                HTMLMail = HTMLMail.Replace("[[SCHOOLNAME]]", "CLT");
                HTMLMail = HTMLMail.Replace("[[PASSWORD]]", Utility.Security.EncryptDescrypt.DecryptString(Convert.ToString(dtblAdminInfo.Rows[0]["Password"]).Trim()));
                HTMLMail = HTMLMail.Replace("temporary ", "");
                GeneralSettings.SendEmail(strEmailID, new GeneralSettings().getConfigValue("infoemail").ToString(), Subject, HTMLMail);
            }
            Response.Write("success");
        }
        else
        {
            Response.Write("invalid");
        }
        Response.End();
    }

    private bool IsValidForgetPwdValidation(out string strErrMsg)
    {
        string[] strValidMsg = new string[1];
        strErrMsg = string.Empty;
        strValidMsg[0] = Validation.RequireFieldAndEmailID(Convert.ToString(Request["tbxEmailAddress"]), "Email Address");
        strErrMsg = Utility.Validation.GenerateErrorMessage(strValidMsg, "<br/> - ");
        return (strErrMsg.Length == 0 ? true : false);
    }
    #endregion


}