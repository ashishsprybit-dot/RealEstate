<%@ Application Language="C#" %>
<%@ Import Namespace="Utility" %>
<%@ Import Namespace="System.IO" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown
    }

    void Application_Error(object sender, EventArgs e)
    {


    }

    void Session_Start(object sender, EventArgs e)
    {
    }

    void Session_End(object sender, EventArgs e)
    {
    }

    void Application_BeginRequest(Object sender, EventArgs e)
    {
        string strUrl = string.Empty;

        if (System.Web.HttpContext.Current.Request.RawUrl.ToLower().IndexOf("/api/judgementapi.asmx/") >= 0)
        {
            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Headers["Content-Type"]))
            {
                if (Convert.ToString(HttpContext.Current.Request.Headers["Content-Type"]).ToLower() != "application/x-www-form-urlencoded")
                {
                    JudgementAPI j = new JudgementAPI();
                    j.BadRequest();
                    HttpContext.Current.Response.End();
                }
            }
            else
            {
                JudgementAPI j = new JudgementAPI();
                j.BadRequest();
                HttpContext.Current.Response.End();
            }
        }


        if (System.Web.HttpContext.Current.Request.RawUrl.ToLower().IndexOf("browserlink") >= 0 || System.Web.HttpContext.Current.Request.RawUrl.ToLower().IndexOf("requestdata") >= 0)
        {
            return;
        }
        string[] strTmpQuery = System.Web.HttpContext.Current.Request.RawUrl.ToLower().Split('?');

        string strQuery = string.Empty;

        if (System.Web.HttpContext.Current.Request.RawUrl.ToLower() == Config.PageNotFoundUrl)
        {
            return;
        }

        if (strTmpQuery.Length == 2)
        {
            strQuery = strTmpQuery[1];

        }

        strUrl = strTmpQuery[0];

        string[] strPath;

        if (Convert.ToBoolean(Config.IsVirtualDirSlash))
        {
            strPath = strUrl.Trim('/').Split('/');
        }
        else
        {
            strPath = strUrl.Replace(Config.VirtualDir.ToLower(), string.Empty).Trim('/').Split('/');
        }



        string strFolderName = strPath[0];

        if (strPath.Length < 2 && strFolderName.IndexOf("eduhubs") < 0 && strFolderName.IndexOf("adminpanel") < 0 && strFolderName.IndexOf("thumb") < 0 && strFolderName.IndexOf("google-login.aspx") < 0 && strFolderName.IndexOf("google-signup.aspx") < 0 && strFolderName.IndexOf("google-signup-admin.aspx") < 0 && strFolderName.IndexOf("google-login-admin.aspx") < 0 && strFolderName.ToLower().IndexOf("syncedufiles.aspx") < 0 && strFolderName.ToLower().IndexOf("sendschoolnotification.aspx") < 0 && strFolderName.ToLower().IndexOf("importlivedata.aspx") < 0 && strFolderName.ToLower().IndexOf("importlivedata-categoryexcel.aspx") < 0 && strFolderName.ToLower().IndexOf("croncheck.aspx") < 0 && strFolderName.ToLower().IndexOf("judgementdbbackup.aspx") < 0 && strFolderName.ToLower().IndexOf("attachment.aspx") < 0 && strFolderName.ToLower().IndexOf("TestMail.aspx") < 0  && strFolderName.ToLower().IndexOf("submit-homegroup-result-job.aspx") < 0 && strFolderName.ToLower().IndexOf("submit-pending-homegroup.aspx") < 0 && strFolderName.ToLower().IndexOf("sync.aspx") < 0)
        {
            if (strPath.Length == 1)
            {
                Response.Redirect(Config.WebSiteUrl + Convert.ToString(strPath[0]).Replace("/", string.Empty) + "/login.aspx");
            }
            else
            {
                Response.Redirect(Config.PageNotFoundUrl);
            }
            return;

        }

        switch (strFolderName)
        {
            case "eduhubs":
            case "adminpanel":
            case "webresource.axd":
            case "style":
            case "js":
            case "include":
            case "ckfinder":
            case "ckeditor":
            case "images":
            case "colorbox":
            case "fonts":
            case "source":
            case "assets":
            case "validation-lib":
            case "thumb.aspx":
            case "google-login.aspx":
            case "google-signup.aspx":
            case "google-signup-admin.aspx":
            case "google-login-admin.aspx":
            case "syncedufiles.aspx":
            case "sendschoolnotification.aspx":
            case "importlivedata.aspx":
            case "importlivedata-categoryexcel.aspx":
            case "croncheck.aspx":
            case "judgementdbbackup.aspx":
            case "attachment.aspx":
            case "TestMail.aspx":
            case "api":
            case "submit-homegroup-result-job.aspx":
            case "submit-pending-homegroup.aspx":
            case "sync.aspx":
                break;
            default:

                if (strPath.Length == 1 && strPath[0].Length == 0)
                {
                    strPath[0] = "default.aspx";
                }
                Common.UrlRewriting(strPath, strQuery);
                break;
        }


    }


</script>

