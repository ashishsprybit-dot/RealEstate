using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Utility;
using BAL;
using System.Data;

public partial class dashboard : System.Web.UI.Page
{
    protected string SchoolURL = "";
    protected string Teachers = "";
    protected string HomeGroups = "";
    protected string Students = "";
    protected string PendingStudents = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        SchoolURL = this.Master.RequestedSchoolURL;
        BindDashboard();
    }

    private void BindDashboard()
    {
        DataTable dt = new DataTable();
        TeachersBAL objTeachersBAL = new TeachersBAL();
        if (Session["SchoolID"] != null)
        {
            dt = objTeachersBAL.TeacherDashboard(Convert.ToInt32(Session["TeacherUserID"]), Convert.ToInt32(Session["SchoolID"]));
            if (dt.Rows.Count > 0)
            {
                Teachers = Convert.ToString(dt.Rows[0]["Teachers"]);
                HomeGroups = Convert.ToString(dt.Rows[0]["HomeGroups"]);
                Students = Convert.ToString(dt.Rows[0]["Students"]);

                if (Convert.ToString(dt.Rows[0]["AssessementTouched"]) != "")
                {
                    PendingStudents = Convert.ToString(Convert.ToInt32(Students) - Convert.ToInt32(dt.Rows[0]["AssessementTouched"]));
                }

            }
        }

        if (Convert.ToString(Session["IsTeacher"]).ToLower() == "false")
        {
            divAdmin.Visible = true;
        }
        else
        {
            divAdmin.Visible = true;
        }
    }
}