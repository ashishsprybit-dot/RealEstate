using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdminPanel_StatusMaster : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["Save"] != null)
        {
            Save(Convert.ToInt32(Request["StatusID"]));
        }
        else if (Request["Delete"] != null)
        {
            Delete();
        }
        else
        {
            BindList();
        }
    }

    private void BindList()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SP_GetStatusList", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    rptStatus.DataSource = dt;
                    rptStatus.DataBind();
                }
            }
        }
    }


    private void Save(int id)
    {
        string status = Request["Status"].Trim();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SP_SaveOrUpdateStatus", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatusID", id);
                cmd.Parameters.AddWithValue("@Status", status);

                con.Open();
                string result = (string)cmd.ExecuteScalar();
                Response.Write(result);
                Response.End();
            }
        }
    }

    private void Delete()
    {
        int id = Convert.ToInt32(Request["ID"]);
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("SP_DeleteStatus", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatusID", id);
                con.Open();
                string result = (string)cmd.ExecuteScalar();
                Response.Write(result);
                Response.End();
            }
        }
    }
}