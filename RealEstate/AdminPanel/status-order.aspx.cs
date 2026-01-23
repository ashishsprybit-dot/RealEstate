using BAL;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.UI.WebControls;
using Utility;
using DAL;
using System.Data.SqlClient;
using System.Configuration;

public partial class AdminPanel_category_order : System.Web.UI.Page
{
    string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["SaveOrder"] != null)
        {
            SaveOrderChange();
        }

        BindList();
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
                    rptStatusListOrderChange.DataSource = dt;
                    rptStatusListOrderChange.DataBind();
                }
            }
        }
    }
  

    private void SaveOrderChange()
    {
        StatusOrderChange(Convert.ToString(Request["Status"]));
        Response.Write("success");
        Response.End();
    }
    public void StatusOrderChange(string Sequence)
    {
        DbParameter[] dbParam = new DbParameter[] {                
                new DbParameter("@Sequence", DbParameter.DbType.VarChar, 50000,Sequence)
            };
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "StatusOrderChange", dbParam);

    }
}