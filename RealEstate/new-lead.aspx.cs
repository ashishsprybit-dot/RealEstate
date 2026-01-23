using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Windows;

public partial class new_lead : System.Web.UI.Page
{
    private readonly string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindStatus();
            BindRequirement();

            // Check if we are in EDIT mode
            if (Request.QueryString["LeadID"] != null)
            {
                int leadId;
                if (int.TryParse(Request.QueryString["LeadID"], out leadId))
                {
                    LoadLeadData(leadId);
                    btnSave.Text = "Update Lead"; // Change button text to indicate update
                }
            }
        }
    }

    private void LoadLeadData(int leadId)
    {
        if (Session["TenantID"] == null) return;

        using (SqlConnection con = new SqlConnection(connStr))
        {
            // Using a simple query, but you can use a Stored Procedure like SP_GetLeadDetails
            string query = "SELECT * FROM Leads WHERE LeadID = @LeadID AND TenantID = @TenantID";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@LeadID", leadId);
                cmd.Parameters.AddWithValue("@TenantID", Session["TenantID"]);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    // Map database values to your form fields
                    txtSchemeName.Value = dr["SchemeName"].ToString();
                    txtCustomerName.Value = dr["CustomerName"].ToString();
                    txtContactNumber.Value = dr["ContactNumber"].ToString();
                    txtLoanAmount.Value = dr["LoanAmount"].ToString();
                    txtDescription.Value = dr["Description"].ToString();

                    if (dr["BirthDate"] != DBNull.Value)
                        txtBirthDate.Value = Convert.ToDateTime(dr["BirthDate"]).ToString("dd-MM-yyyy");

                    txtPropertyValue.Value = dr["PropertyValue"].ToString();
                    txtSaledeedAmount.Value = dr["SaledeedAmount"].ToString();

                    // Set Dropdowns
                    if (ddlStatus.Items.FindByValue(dr["StatusID"].ToString()) != null)
                        ddlStatus.SelectedValue = dr["StatusID"].ToString();


                    if (ddlRequirement.Items.FindByValue(dr["RequirementID"].ToString()) != null)
                        ddlRequirement.SelectedValue = dr["RequirementID"].ToString();
                }
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        // Decide whether to Update or Insert based on QueryString
        if (Request.QueryString["LeadID"] != null)
        {
            UpdateLead(Convert.ToInt32(Request.QueryString["LeadID"]));
        }
        else
        {
            SaveLead();            
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // Ensure the filename 'lead-list.aspx' is exactly correct 
        // and located in the same folder.
        Response.Redirect("lead-list.aspx", false);
        Context.ApplicationInstance.CompleteRequest();
    }

    private void UpdateLead(int leadId)
    {
        // Create an "UpdateLead" logic similar to SaveLead 
        // but using an UPDATE SQL statement or SP_UpdateLead
        using (SqlConnection con = new SqlConnection(connStr))
        {
            string query = @"UPDATE Leads SET 
    SchemeName = @SchemeName,
    CustomerName = @CustomerName,
    ContactNumber = @ContactNumber,
    LoanAmount = @LoanAmount,
    BirthDate = @BirthDate,
    PropertyValue = @PropertyValue,
    SaledeedAmount = @SaledeedAmount,
    Description = @Description,
    StatusID = @StatusID,
    RequirementID = @RequirementID,
    ModifiedDate = GETDATE()
WHERE LeadID = @LeadID AND TenantID = @TenantID
";

            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@LeadID", leadId);
                cmd.Parameters.AddWithValue("@TenantID", Session["TenantID"]);
                cmd.Parameters.AddWithValue("@SchemeName", txtSchemeName.Value.Trim());
                cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Value.Trim());
                cmd.Parameters.AddWithValue("@ContactNumber", txtContactNumber.Value.Trim());
                cmd.Parameters.AddWithValue("@LoanAmount", Convert.ToDecimal(txtLoanAmount.Value));

                cmd.Parameters.AddWithValue("@BirthDate", string.IsNullOrEmpty(txtBirthDate.Value) ? (object)DBNull.Value : DateTime.ParseExact(txtBirthDate.Value, "dd-MM-yyyy", null));
                cmd.Parameters.AddWithValue("@PropertyValue", string.IsNullOrEmpty(txtPropertyValue.Value) ? (object)DBNull.Value : Convert.ToDecimal(txtPropertyValue.Value));
                cmd.Parameters.AddWithValue("@SaledeedAmount", string.IsNullOrEmpty(txtSaledeedAmount.Value) ? (object)DBNull.Value : Convert.ToDecimal(txtSaledeedAmount.Value));

                cmd.Parameters.AddWithValue("@Description", txtDescription.Value.Trim());
                cmd.Parameters.AddWithValue("@StatusID", Convert.ToInt32(ddlStatus.SelectedValue));
                cmd.Parameters.AddWithValue("@RequirementID", string.IsNullOrEmpty(ddlRequirement.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlRequirement.SelectedValue));

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        ShowMessage("Lead updated successfully!");
    }

    private void ShowMessage(string msg)
    {
        divMsg.Visible = true;
        divMsg.Attributes["class"] = "alert alert-success";
        divMsg.InnerHtml = msg;
    }


    private void BindRequirement()
    {
        using (SqlConnection con = new SqlConnection(connStr))
        using (SqlCommand cmd = new SqlCommand("SP_GetRequirementList", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            ddlRequirement.DataSource = cmd.ExecuteReader();
            ddlRequirement.DataTextField = "RequirementName";
            ddlRequirement.DataValueField = "RequirementID";
            ddlRequirement.DataBind();

            ddlRequirement.Items.Insert(0, new ListItem("-- Select Requirement --", ""));
        }
    }


    private void SaveLead()
    {
        if (Session["TenantID"] == null || Session["TenantUserID"] == null)
        {
            Response.Redirect("login.aspx");
            return;
        }

        int tenantId = Convert.ToInt32(Session["TenantID"]);
        int userId = Convert.ToInt32(Session["TenantUserID"]);

        using (SqlConnection con = new SqlConnection(connStr))
        using (SqlCommand cmd = new SqlCommand("SP_InsertLead", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TenantID", tenantId);
            cmd.Parameters.AddWithValue("@SchemeName", txtSchemeName.Value.Trim());
            cmd.Parameters.AddWithValue("@CustomerName", txtCustomerName.Value.Trim());
            cmd.Parameters.AddWithValue("@ContactNumber", txtContactNumber.Value.Trim());
            cmd.Parameters.AddWithValue("@LoanAmount", Convert.ToDecimal(txtLoanAmount.Value));

            cmd.Parameters.AddWithValue("@BirthDate",
                string.IsNullOrEmpty(txtBirthDate.Value)
                    ? (object)DBNull.Value
                    : DateTime.ParseExact(txtBirthDate.Value, "dd-MM-yyyy", null));

            cmd.Parameters.AddWithValue("@PropertyValue",
                string.IsNullOrEmpty(txtPropertyValue.Value)
                    ? (object)DBNull.Value
                    : Convert.ToDecimal(txtPropertyValue.Value));

            cmd.Parameters.AddWithValue("@SaledeedAmount",
                string.IsNullOrEmpty(txtSaledeedAmount.Value)
                    ? (object)DBNull.Value
                    : Convert.ToDecimal(txtSaledeedAmount.Value));

            cmd.Parameters.AddWithValue("@Description", txtDescription.Value.Trim());
            cmd.Parameters.AddWithValue("@StatusID", Convert.ToInt32(ddlStatus.SelectedValue));

            cmd.Parameters.AddWithValue("@RequirementID",
                string.IsNullOrEmpty(ddlRequirement.SelectedValue) ? (object)DBNull.Value : Convert.ToInt32(ddlRequirement.SelectedValue));

            cmd.Parameters.AddWithValue("@CreatedBy", userId);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        divMsg.Visible = true;
        divMsg.Attributes["class"] = "alert alert-success";
        divMsg.InnerHtml = "Lead saved successfully!";
        
    }

    private void BindStatus()
    {
        using (SqlConnection con = new SqlConnection(connStr))
        using (SqlCommand cmd = new SqlCommand("SP_GetStatusList", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            ddlStatus.DataSource = cmd.ExecuteReader();
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "StatusID"; // ✅ FIX
            ddlStatus.DataBind();

            ddlStatus.Items.Insert(0, new ListItem("-- Select --", "0"));
        }
    }

}
