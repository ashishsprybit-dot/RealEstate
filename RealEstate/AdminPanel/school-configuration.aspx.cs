using BAL;
using Newtonsoft.Json;
using PAL;
using System;
using System.Data;
using System.Globalization;
using Utility;

public partial class school_configuration : System.Web.UI.Page
{
    #region Private Members
    SchoolConfigurationBAL objSchoolConfigurationBAL = new SchoolConfigurationBAL();
    #endregion

    #region Public Members
    public string data = "";
    #endregion

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form.Keys.Count > 0)
        {
            if (Request["savenotificationdays"] != null)
            {
                SaveInfo();
            }
            //if (Request.QueryString.Count == 3 && Request.QueryString[2] == "sc")
            //{
            //    SaveInfo();
            //}
            ////else if (Request.QueryString.Count == 3 && Request.QueryString[2] == "ssc")
            //{
            //    SaveSemester();
            //}
            //else if (!string.IsNullOrEmpty(Request["type"]) && Request["type"] == "remove")
            //{
            //    removeSemester();
            //}
        }
        else
        {
            BindControls();
            BindSemesterList();
        }
    }
    #endregion

    #region Bind Controls

    private void BindControls()
    {

        hdnDays.Value = new BAL.GeneralSettings().getConfigValue("CLTNotificationDays").ToString();


        //DataTable dt = new DataTable();
        //dt = objSchoolConfigurationBAL.GetBySchoolID(Convert.ToInt64(Session["SchoolID"]));
        //if (dt.Rows.Count > 0)
        //{
            
        //    hdnDays.Value = Convert.ToString(dt.Rows[0]["NotificationDays"]).Trim();
        //}
    }
    private void BindSemesterList()
    {
        DataTable dt = new DataTable();
        dt = objSchoolConfigurationBAL.GetSchoolSemesterConfigurationBySchoolID(Convert.ToInt64(Session["SchoolID"]));
        data = JsonConvert.SerializeObject(dt, Formatting.Indented);
    }
    #endregion

    #region Save Information
    private void SaveInfo()
    {
        GeneralBAL objGeneralBAL = new GeneralBAL();
        objGeneralBAL.Save("CLTNotificationDays", Convert.ToString(Request["days"]));

        Response.Write("success");
        Response.End();
        //ShowMessage("Configuration has been saved successfully.", "alert alert-success", divMsg.ClientID);

        //objSchoolConfigurationBAL.SchoolID = Convert.ToInt64(Session["SchoolID"]);
        //objSchoolConfigurationBAL.NextLevelPercentage = float.Parse(Request[tbxMoveStudentstothenextlevelPercentage.UniqueID], CultureInfo.InvariantCulture.NumberFormat);
        //objSchoolConfigurationBAL.NotificationDays = Convert.ToString(Request[hdnDays.UniqueID]);
        //switch (objSchoolConfigurationBAL.Save(Convert.ToInt64(Session["TeacherUserID"])))
        //{
        //    case -1:
        //        ShowMessage("SchoolID invalid.", "alert alert-danger error", divMsg.ClientID);
        //        break;
        //    default:
        //        ShowMessage("School Configuration has been saved successfully.", "alert alert-success", divMsg.ClientID);
        //        break;
        //}
        Response.End();
    }

    private void SaveSemester()
    {
        //SchoolSemesterConfigurationPAL model = new SchoolSemesterConfigurationPAL
        //{
        //    ID = Convert.ToInt64(Request[hdnSemesterID.UniqueID]),
        //    SchoolID = Convert.ToInt64(Session["SchoolID"]),
        //    Sem1StartDate = Convert.ToDateTime(Request[tbxSem1StartDate.UniqueID]),
        //    Sem1EndDate = Convert.ToDateTime(Request[tbxSem1EndDate.UniqueID]),
        //    Sem2StartDate = Convert.ToDateTime(Request[tbxSem2StartDate.UniqueID]),
        //    Sem2EndDate = Convert.ToDateTime(Request[tbxSem2EndDate.UniqueID])
        //};

        //switch (objSchoolConfigurationBAL.SaveSemesterConfiguration(Convert.ToInt64(Session["TeacherUserID"]), model))
        //{
        //    case -1:
        //        ShowMessage("Semester details already exists.", "alert alert-danger error", divMsgSemester.ClientID);
        //        break;
        //    default:
        //        ShowMessage("Semester details has been saved successfully.", "alert alert-success", divMsgSemester.ClientID, true);
        //        DataTable dt = new DataTable();
        //        dt = objSchoolConfigurationBAL.GetSchoolSemesterConfigurationBySchoolID(Convert.ToInt64(Session["SchoolID"]));
        //        data = JsonConvert.SerializeObject(dt, Formatting.Indented);
        //        //Response.Write(Common.ScriptStartTag + "parent.userRefreshedData = " + data + Common.ScriptEndTag);
        //        //Response.Write(Common.ScriptStartTag + "parent.window.reload();" + Common.ScriptEndTag);
        //        Response.Write(Common.ScriptStartTag + "parent.window.setTimeout(\"parent.window.location.href = 'school-configuration.aspx'\",2000);" + Common.ScriptEndTag);
        //        break;
        //}
        Response.End();
    }

    private void removeSemester()
    {
        //Int64 SemesterID = Convert.ToInt64(Request["hdnID"]);
        //switch (objSchoolConfigurationBAL.DeleteSemester(Convert.ToInt64(Session["SchoolID"]), SemesterID))
        //{
        //    case -1:
        //        ShowMessage("Semester details not exists.", "alert alert-danger error", divMsgSemester.ClientID);
        //        break;
        //    default:
        //        ShowMessage("Semester details deleted successfully.", "alert alert-success", divMsgSemester.ClientID, true);
        //        break;
        //}
        //BindSemesterList();
        Response.End();
    }
    #endregion

    #region General
    private void ShowMessage(string strMessage, string strMessageType, string divMessage, Boolean IsClearCall = false)
    {
        Response.Write("<link href='" + Config.VirtualDir + "style/style.css' rel='Stylesheet' type='text/css' />");
        Response.Write("<script type='text/javascript' language='javascript' src='" + Config.VirtualDir + "js/jquery-1.10.2.min.js'></script>");
        Response.Write("<script type='text/javascript' language='javascript' src='" + Config.VirtualDir + "js/general.js'></script>");
        Response.Write("<script type='text/javascript'>var virtualDir = '" + Config.VirtualDir + "';</script>");
        Response.Write("$(document.ready(function {");
        Response.Write(Common.ScriptStartTag + "parent.$('#" + divMessage + "').show();" + Common.ScriptEndTag);
        Response.Write(Common.ScriptStartTag + "parent.$('#" + divMessage + "').html('" + strMessage + "');" + Common.ScriptEndTag);
        Response.Write(Common.ScriptStartTag + "parent.$('#" + divMessage + "').attr('class', '" + strMessageType + "');" + Common.ScriptEndTag);
        if (IsClearCall)
        {
            Response.Write(Common.ScriptStartTag + "parent.$('#" + tbxSem1StartDate.ClientID + "').val('');" + Common.ScriptEndTag);
            Response.Write(Common.ScriptStartTag + "parent.$('#" + tbxSem1EndDate.ClientID + "').val('');" + Common.ScriptEndTag);
            Response.Write(Common.ScriptStartTag + "parent.$('#" + tbxSem2StartDate.ClientID + "').val('');" + Common.ScriptEndTag);
            Response.Write(Common.ScriptStartTag + "parent.$('#" + tbxSem2EndDate.ClientID + "').val('');" + Common.ScriptEndTag);
            Response.Write(Common.ScriptStartTag + "parent.$('#" + hdnSemesterID.ClientID + "').val('0');" + Common.ScriptEndTag);
            Response.Write(Common.ScriptStartTag + "init();" + Common.ScriptEndTag);
        }
        Response.Write(Javascript.ScriptStartTag + "window.setTimeout(\"parent.$('#" + divMessage + "').fadeOut(600);\",5000)" + Javascript.ScriptEndTag);
        Response.Write("});");
    }
    #endregion
}