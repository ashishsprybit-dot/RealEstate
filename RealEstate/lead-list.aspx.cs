using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lead_list : System.Web.UI.Page
{
    string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["TenantID"] == null)
        //{
        //    Response.Redirect("lead-list.aspx?deleted=1", false);
        //    return;
        //}

        if (!IsPostBack)
        {
            BindStatusDropdown();         

            BindLeads();
        }
    }

    private void BindLeads()
    {
       // if (Session["TenantID"] == null) return;

        int tenantId = Convert.ToInt32(Session["TenantID"]);
        int statusId = Convert.ToInt32(ddlStatus.SelectedValue);

        using (SqlConnection con = new SqlConnection(connStr))
        {
            using (SqlCommand cmd = new SqlCommand("SP_GetLeadsByTenant", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@TenantID", SqlDbType.Int).Value = tenantId;
                cmd.Parameters.Add("@StatusID", SqlDbType.Int).Value = statusId;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    rptLeads.DataSource = dt;
                    rptLeads.DataBind();
                }
            }
        }
    }

    private void BindStatusDropdown()
    {
        using (SqlConnection con = new SqlConnection(connStr))
        {
            SqlCommand cmd = new SqlCommand(@"
            SELECT StatusID, Status 
            FROM StatusMaster 
            WHERE IsDeleted = 0
            ORDER BY Status", con);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            ddlStatus.DataSource = dr;
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "StatusID";
            ddlStatus.DataBind();
        }

        ddlStatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("All Statuses", "0"));

        string selectedStatus = Request.QueryString["status"];
        //if (!string.IsNullOrEmpty(selectedStatus) && ddlStatus.Items.FindByText(selectedStatus) != null)
        //{
        //    ddlStatus.SelectedItem.Text = selectedStatus;
        //}

        if (!string.IsNullOrEmpty(selectedStatus))
        {
            System.Web.UI.WebControls.ListItem item = ddlStatus.Items
                .Cast<System.Web.UI.WebControls.ListItem>()
                .FirstOrDefault(i =>
                    i.Text.Equals(selectedStatus, StringComparison.OrdinalIgnoreCase));

            if (item != null)
            {
                ddlStatus.ClearSelection();
                item.Selected = true;
            }
        }

    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindLeads();
    }

    protected void rptLeads_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView rowView = (DataRowView)e.Item.DataItem;

            DropDownList ddlStatus = (DropDownList)e.Item.FindControl("ddlRowStatus");

            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT StatusID, Status FROM StatusMaster WHERE IsDeleted = 0 ORDER BY Status", con))
            {
                con.Open();
                ddlStatus.DataSource = cmd.ExecuteReader();
                ddlStatus.DataTextField = "Status";
                ddlStatus.DataValueField = "StatusID";
                ddlStatus.DataBind();
            }

            ddlStatus.SelectedValue = rowView["StatusID"].ToString();
        }
    }


    protected void rptLeads_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        //if (e.CommandName == "ChangeStatus")
        //{
        //    int leadId = Convert.ToInt32(e.CommandArgument);

        //    DropDownList ddlStatus =
        //        (DropDownList)e.Item.FindControl("ddlRowStatus");

        //    int statusId = Convert.ToInt32(ddlStatus.SelectedValue);

        //    if (Session["TenantID"] == null)
        //        return;

        //    int tenantId = Convert.ToInt32(Session["TenantID"]);
        //    string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        //    using (SqlConnection con = new SqlConnection(cs))
        //    using (SqlCommand cmd = new SqlCommand(@"
        //    UPDATE Leads
        //    SET StatusID = @StatusID,
        //        ModifiedDate = GETDATE()
        //    WHERE LeadID = @LeadID
        //      AND TenantID = @TenantID", con))
        //    {
        //        cmd.Parameters.AddWithValue("@StatusID", statusId);
        //        cmd.Parameters.AddWithValue("@LeadID", leadId);
        //        cmd.Parameters.AddWithValue("@TenantID", tenantId);

        //        con.Open();
        //        cmd.ExecuteNonQuery();
        //    }

        //    BindLeads();
        //}

        if (e.CommandName == "DeleteLead")
        {
            int leadId = Convert.ToInt32(e.CommandArgument);

            //if (Session["TenantID"] == null)
            //    return;

            int tenantId = Convert.ToInt32(Session["TenantID"]);

            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("SP_DeleteLead", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LeadID", leadId);
                cmd.Parameters.AddWithValue("@TenantID", tenantId);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            BindLeads();
        }
    }


    protected void ddlRowStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList ddlStatus = (DropDownList)sender;
        RepeaterItem item = (RepeaterItem)ddlStatus.NamingContainer;

        HiddenField hfLeadID =
            (HiddenField)item.FindControl("hfLeadID");

        int leadId = Convert.ToInt32(hfLeadID.Value);
        int statusId = Convert.ToInt32(ddlStatus.SelectedValue);

        //if (Session["TenantID"] == null)
        //    return;

        int tenantId = Convert.ToInt32(Session["TenantID"]);

        using (SqlConnection con = new SqlConnection(connStr))
        using (SqlCommand cmd = new SqlCommand("SP_UpdateLeadStatus", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LeadID", leadId);
            cmd.Parameters.AddWithValue("@TenantID", tenantId);
            cmd.Parameters.AddWithValue("@StatusID", statusId);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        BindLeads(); 
    }



    [WebMethod]
    public static List<object> GetRemarks(int leadId)
    {
        var list = new List<object>();
       // if (System.Web.HttpContext.Current.Session["TenantID"] == null) return list;

        int tenantId = Convert.ToInt32(System.Web.HttpContext.Current.Session["TenantID"]);
        string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(cs))
        using (SqlCommand cmd = new SqlCommand("SP_GetLeadRemarks", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LeadID", leadId);
            cmd.Parameters.AddWithValue("@TenantID", tenantId);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new
                {
                    RemarkText = dr["RemarkText"].ToString(),
                    UserID = dr["UserName"].ToString(),
                    RemarkDate = Convert.ToDateTime(dr["CreatedDate"]).ToString("dd/MM/yyyy, h:mm tt")
                });
            }
        }
        return list;
    }

    [WebMethod]
    public static void SaveRemark(int leadId, string remarkText)
    {
        //if (System.Web.HttpContext.Current.Session["TenantID"] == null) return;

        int tenantId = Convert.ToInt32(System.Web.HttpContext.Current.Session["TenantID"]);
        int userId = Convert.ToInt32(System.Web.HttpContext.Current.Session["TenantUserID"]);
        string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(cs))
        using (SqlCommand cmd = new SqlCommand("SP_InsertLeadRemark", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LeadID", leadId);
            cmd.Parameters.AddWithValue("@TenantID", tenantId);
            cmd.Parameters.AddWithValue("@RemarkText", remarkText);
            cmd.Parameters.AddWithValue("@CreatedBy", userId);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    [WebMethod]
    public static List<object> GetLeadHistory(int leadId)
    {
        var list = new List<object>();
        int tenantId = Convert.ToInt32(System.Web.HttpContext.Current.Session["TenantID"]);
        string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(cs))
        using (SqlCommand cmd = new SqlCommand("SP_GetLeadHistory", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LeadID", leadId);
            cmd.Parameters.AddWithValue("@TenantID", tenantId);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(new
                {
                    FieldName = dr["FieldName"],
                    OldValue = dr["OldValue"],
                    NewValue = dr["NewValue"],
                    UpdatedBy = dr["UserName"],
                    UpdatedDate = dr["UpdatedDate"].ToString()
                });
            }
        }
        return list;
    }

    //[WebMethod(EnableSession = true)] 
    //public static void UpdateLeadRequirement(int leadId, int requirementId)
    //{
    //    if (System.Web.HttpContext.Current.Session["TenantID"] == null) return;

    //    int tenantId = Convert.ToInt32(System.Web.HttpContext.Current.Session["TenantID"]);
    //    string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

    //    using (SqlConnection con = new SqlConnection(cs))
    //    using (SqlCommand cmd = new SqlCommand("SP_UpdateLeadRequirement", con))
    //    {
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@LeadID", leadId);
    //        cmd.Parameters.AddWithValue("@TenantID", tenantId);
    //        cmd.Parameters.AddWithValue("@RequirementID", requirementId);

    //        con.Open();
    //        cmd.ExecuteNonQuery();
    //    }
    //}

    //[WebMethod(EnableSession = true)]
    //public static void UpdateLeadStatus(int leadId, int statusId)
    //{
    //    if (HttpContext.Current.Session["TenantID"] == null) return;

    //    int tenantId = Convert.ToInt32(HttpContext.Current.Session["TenantID"]);
    //    string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

    //    using (SqlConnection con = new SqlConnection(cs))
    //    using (SqlCommand cmd = new SqlCommand(@"
    //    UPDATE Leads
    //    SET StatusID = @StatusID,
    //        ModifiedDate = GETDATE()
    //    WHERE LeadID = @LeadID AND TenantID = @TenantID", con))
    //    {
    //        cmd.Parameters.AddWithValue("@LeadID", leadId);
    //        cmd.Parameters.AddWithValue("@StatusID", statusId);
    //        cmd.Parameters.AddWithValue("@TenantID", tenantId);

    //        con.Open();
    //        cmd.ExecuteNonQuery(); // This saves the change
    //    }
    //}
    
    //[WebMethod(EnableSession = true)]
    //public static bool DeleteLead(int leadId)
    //{

    //    if (HttpContext.Current.Session["TenantID"] == null) return false;

    //    int tenantId = Convert.ToInt32(HttpContext.Current.Session["TenantID"]);
    //    string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

    //    using (SqlConnection con = new SqlConnection(cs))
    //    using (SqlCommand cmd = new SqlCommand(@"
    //    UPDATE Leads 
    //    SET IsDeleted = 1, 
    //        ModifiedDate = GETDATE()
    //    WHERE LeadID = @LeadID 
    //      AND TenantID = @TenantID 
    //      AND IsDeleted = 0", con))
    //    {
    //        cmd.Parameters.AddWithValue("@LeadID", leadId);
    //        cmd.Parameters.AddWithValue("@TenantID", tenantId);

    //        con.Open();
    //        int rowsAffected = cmd.ExecuteNonQuery();
    //        return rowsAffected > 0;
    //    }
    //}

    protected string GetStatusOptions(int currentStatusId)
    {
        StringBuilder options = new StringBuilder();

        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connStr))
        {
            SqlCommand cmd = new SqlCommand(@"
            SELECT StatusID, Status 
            FROM StatusMaster 
            WHERE IsDeleted = 0
            ORDER BY Status", con);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                int id = Convert.ToInt32(dr["StatusID"]);
                string name = dr["Status"].ToString();
                bool selected = (id == currentStatusId);
                options.AppendFormat("<option value=\"{0}\"{1}>{2}</option>",
                    id,
                    selected ? " selected" : "",
                    HttpUtility.HtmlEncode(name));
            }
        }
        return options.ToString();
    }

}