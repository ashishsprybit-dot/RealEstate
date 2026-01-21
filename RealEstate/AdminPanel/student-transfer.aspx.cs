using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using System.Data;
using Utility;
using DAL;

public partial class AdminPanel_Student_Transfer : AdminAuthentication
{

    #region Page Events
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["BindStudents"] != null)
        {
            BindStudents();
        }
        
        if (Request["StudentTransfer"] != null)
        {
            SaveInfo();
        }

        BindSchools();
        Master.SelectedSection = AdminPanel_Admin.Section.General;

    }
    #endregion

    #region Bind Controls
    private void BindSchools()
    {
        SchoolBAL objSchool = new SchoolBAL();
        objSchool.Name = "";
        int intTotalRecord = 0;
        DataTable dt = new DataTable();
        dt = objSchool.GetList(ref CurrentPage, 10000, out intTotalRecord, "Name", "ASC");

        DataView dv = new DataView(dt);
        dv.RowFilter = "Status=1";
        dt = dv.ToTable();
        if (dt.Rows.Count > 0)
        {
            ddlFromSchool.DataSource = dt;
            ddlFromSchool.DataTextField = "Name";
            ddlFromSchool.DataValueField = "ID";
            ddlFromSchool.DataBind();

            ddlToSchool.DataSource = dt;
            ddlToSchool.DataTextField = "Name";
            ddlToSchool.DataValueField = "ID";
            ddlToSchool.DataBind();
        }
        ddlFromSchool.Items.Insert(0, new ListItem("-- Select --", ""));
        ddlToSchool.Items.Insert(0, new ListItem("-- Select --", ""));
        
        ddlStudents.Items.Insert(0, new ListItem("-- Select --", ""));
      
    }

    private void BindStudents()
    {
        DataTable dtStudents = new DataTable();
        dtStudents = StudentListBySchoolID(Convert.ToInt32(Request["SchoolID"]));
        ddlStudents.DataSource = dtStudents;
        ddlStudents.DataTextField = "Name";
        ddlStudents.DataValueField = "StudentID";
        ddlStudents.DataBind();

        ddlStudents.Items.Insert(0, new ListItem("-- Select --", ""));
        Response.Write(Common.RenderControl(divStudents, Common.RenderControlName.HtmlGeneric));
        Response.End();
    }

    
    #endregion

    #region Save Information
    private void SaveInfo()
    {
        int FromSchoolID = Convert.ToInt32(Request["FromSchoolID"]);
        int ToSchoolID = Convert.ToInt32(Request["ToSchoolID"]);
        int StudentID = Convert.ToInt32(Request["StudentID"]);
       

        string result = StudentTransferSave(FromSchoolID, ToSchoolID, StudentID, Convert.ToInt64(Session["UserID"]));
        if (result != "")
        {
            Response.Write(result);
        }
        else
        {
            Response.Write("Success");
        }
        Response.End();
    }
    #endregion

    #region General

    private void ShowMessage(string strMessage, string strMessageType, string divMessage)
    {
        Response.Write("<link href='" + Config.VirtualDir + "style/style.css' rel='Stylesheet' type='text/css' />");
        Response.Write("<script type='text/javascript' language='javascript' src='" + Config.VirtualDir + "js/jquery-1.10.2.min.js'></script>");
        Response.Write("<script type='text/javascript' language='javascript' src='" + Config.VirtualDir + "js/general.js'></script>");
        Response.Write("<script type='text/javascript'>var virtualDir = '" + Config.VirtualDir + "';</script>");
        Response.Write("$(document.ready(function {");
        Response.Write(Common.ScriptStartTag + "parent.$('#" + divMessage + "').show();" + Common.ScriptEndTag);
        Response.Write(Common.ScriptStartTag + "parent.$('#" + divMessage + "').html('" + strMessage + "');" + Common.ScriptEndTag);
        Response.Write(Common.ScriptStartTag + "parent.$('#" + divMessage + "').attr('class', '" + strMessageType + "');" + Common.ScriptEndTag);
        Response.Write(Javascript.ScriptStartTag + "window.setTimeout(\"parent.$('#" + divMessage + "').fadeOut(600);\",5000)" + Javascript.ScriptEndTag);
        Response.Write("});");
    }


    #endregion

    #region Database 
    public DataTable StudentListBySchoolID(int SchoolID)
    {
        DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 20, SchoolID)
            };

        return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "StudentsBySchoolID", dbParam);
    }
    public string StudentTransferSave(int FromSchoolID, int ToSchoolID, long StudentID,   long CreatedBy)
    {
        DbParameter[] dbParam = new DbParameter[] {
            new DbParameter("@FromSchoolID", DbParameter.DbType.Int, 200, FromSchoolID),
            new DbParameter("@ToSchoolID", DbParameter.DbType.Int, 200, ToSchoolID),
            new DbParameter("@StudentID", DbParameter.DbType.Int, 200, StudentID),          
            new DbParameter("@CreatedBy", DbParameter.DbType.Int, 200, CreatedBy),
            new DbParameter("@ReturnVal", DbParameter.DbType.VarChar, 4000, ParameterDirection.Output)
        };

        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "StudentTransfer", dbParam);
        return Convert.ToString(dbParam[4].Value);
    }
    #endregion
}