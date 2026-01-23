using BAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Utility;

public partial class dashboard : System.Web.UI.Page
{
    #region public declarations
    LeadBAL objLeadBAL = new LeadBAL();
        public string data = "[]";
    DataTable dtStatusList = new DataTable();
    #endregion

    #region Page Events
    protected string UserRole = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //string UserRole = Convert.ToString(Session["Role"]);
        //if (UserRole == "BusinessPartner")
        //{
        //    Response.Redirect("dash-board.aspx"); 
        //    return;
        //}

        if (Request.QueryString["DeleteLeadID"] != null)
        {
            int leadId = Convert.ToInt32(Request.QueryString["DeleteLeadID"]);
            objLeadBAL.DeleteLead(leadId);

            Response.Redirect("lead-list.aspx?deleted=1", false);
            Context.ApplicationInstance.CompleteRequest();
            return;
        }

        if (!IsPostBack)
        {
            if (Request.QueryString["success"] == "LeadCreated")
            {
                divMsg.Visible = true;
                divMsg.Attributes["class"] = "alert alert-success";
                divMsg.InnerHtml = "Lead created successfully!";
                divMsg.Style["display"] = "block";

                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "HideCreatedMsg",
                    "setTimeout(function() { document.getElementById('" + divMsg.ClientID + "').style.display='none'; }, 2000);",
                    true
                );
            }
            else if (Request.QueryString["success"] == "LeadUpdated")
            {
                divMsg.Visible = true;
                divMsg.Attributes["class"] = "alert alert-success";
                divMsg.InnerHtml = "Lead updated successfully!";
                divMsg.Style["display"] = "block";

                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "HideUpdatedMsg",
                    "setTimeout(function() { document.getElementById('" + divMsg.ClientID + "').style.display='none'; }, 2000);",
                    true
                );
            }
            else if (Request.QueryString["deleted"] == "1")
            {
                divMsg.Visible = true;
                divMsg.Attributes["class"] = "alert alert-danger";
                divMsg.InnerHtml = "Lead deleted successfully!";
                divMsg.Style["display"] = "block";

                ScriptManager.RegisterStartupScript(
                    this,
                    GetType(),
                    "HideDeletedMsg",
                    "setTimeout(function() { document.getElementById('" + divMsg.ClientID + "').style.display='none'; }, 2000);",
                    true
                );
            }

            UserRole = Convert.ToString(Session["Role"]);
            ViewState["UserRole"] = UserRole;

            if (UserRole == "BusinessPartner")
            {
                //HideMenu("liDashboard");
                HideMenu("liUserManagement");
                HideMenu("liTransaction");
                HideMenu("liStatus");
            }

            if (UserRole == "TeamMember")
            {
                HideMenu("liUserManagement");
                HideMenu("liTransaction");
                HideMenu("liStatus");


                ScriptManager.RegisterStartupScript(this, GetType(), "DisableDelete", "setTimeout(function() { $('.btn-delete-lead').remove(); }, 300);", true);
            }

            if (Session["UserID"] != null)
            {
                int currentUserId = Convert.ToInt32(Session["UserID"]);
                string script = string.Format("var currentUserId = {0};", currentUserId);
                ClientScript.RegisterStartupScript(this.GetType(), "SetUserId", script, true);
            }
        }
        else
        {
            UserRole = Convert.ToString(ViewState["UserRole"]);
        }

        BindStatusCards();

        string selectedStatus = hfSelectedStatus.Value;
        if (!string.IsNullOrEmpty(selectedStatus))
        {
            BindLeadListByStatus(selectedStatus);
        }
        else
        {
            //rptLeads.DataSource = null;
            //rptLeads.DataBind();
        }

    }

    private void HideMenu(string menuId)
    {
        HtmlGenericControl li = Master.FindControl(menuId) as HtmlGenericControl;
        if (li != null) li.Visible = false;
    }

    private Control FindControlRecursive(Control root, string id)
    {
        if (root.ID == id)
            return root;

        foreach (Control child in root.Controls)
        {
            Control found = FindControlRecursive(child, id);
            if (found != null)
                return found;
        }

        return null;
    }

    #endregion

    #region Bind List

    private void BindList(int transactionTypeId = 0, string status = "")
    {
        DataTable dt = objLeadBAL.GetLeadList();

        if (dt != null && dt.Rows.Count > 0)
        {
            string filterExpression = "1=1";

            if (transactionTypeId > 0)
            {
                filterExpression += " AND TransactionTypeID = " + transactionTypeId;
            }

            if (!string.IsNullOrEmpty(status) && status != "All")
            {
                filterExpression += " AND Status = '" + status.Replace("'", "''") + "'";
            }

            dt.DefaultView.RowFilter = filterExpression;
            dt.DefaultView.Sort = SortExpression + " " + SortDirection;
            DataTable sortedDt = dt.DefaultView.ToTable();

            //rptLeads.DataSource = sortedDt;
            //rptLeads.DataBind();

            data = JsonConvert.SerializeObject(sortedDt, Formatting.None);
        }
        else
        {
            //rptLeads.DataSource = null;
            //rptLeads.DataBind();
            data = "[]";
        }

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
    }
    private void BindStatusCards()
    {
        DataTable dt = GetStatusCounts();
        StringBuilder sb = new StringBuilder();

        foreach (DataRow row in dt.Rows)
        {
            string status = row["Status"].ToString();
            int count = Convert.ToInt32(row["LeadCount"]);

            sb.Append(
                "<div class='status-card' data-status='" + status + "' onclick='filterByStatus(this)'>" +
                    "<div class='status-name'>" + status + "</div>" +
                    "<div class='lead-number'>" + count + "</div>" +
                "</div>"
            );
        }

        divStatusCards.InnerHtml = sb.ToString();
    }





    private void BindLeadListByStatus(string status)
    {
        int selectedTransactionTypeId = 0;
        BindList(selectedTransactionTypeId, status);
    }
    private DataTable GetStatusCounts()
    {
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connStr))
        using (SqlCommand cmd = new SqlCommand("SP_GetStatusCounts", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            return dt;
        }
    }



    private DataTable GetStatusList()
    {
        return objLeadBAL.GetStatusList();
    }
    #endregion

    protected void SortLeads(object sender, CommandEventArgs e)
    {
        string sortField = e.CommandArgument.ToString();

        if (SortExpression == sortField)
        {
            SortDirection = (SortDirection == "ASC") ? "DESC" : "ASC";
        }
        else
        {
            SortExpression = sortField;
            SortDirection = "ASC";
        }

        //BindLeadList();
    }

    protected string SortExpression
    {
        get { return ViewState["SortExpression"] as string ?? "LeadMasterID"; }
        set { ViewState["SortExpression"] = value; }
    }

    protected string SortDirection
    {
        get { return ViewState["SortDirection"] as string ?? "DESC"; }
        set { ViewState["SortDirection"] = value; }
    }

    protected void rptLeads_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DropDownList ddlStatus = (DropDownList)e.Item.FindControl("ddlStatus");
            HiddenField hfLeadId = (HiddenField)e.Item.FindControl("hfLeadId");

            DataTable dtStatus = GetStatusList();
            ddlStatus.DataSource = dtStatus;
            ddlStatus.DataTextField = "Status";
            ddlStatus.DataValueField = "Status";
            ddlStatus.DataBind();

            object statusObj = DataBinder.Eval(e.Item.DataItem, "Status");
            string currentStatus = statusObj != null ? statusObj.ToString() : string.Empty;

            if (!string.IsNullOrEmpty(currentStatus))
            {
                ListItem item = ddlStatus.Items.FindByValue(currentStatus);
                if (item != null)
                {
                    ddlStatus.ClearSelection();
                    item.Selected = true;
                }
            }

            ddlStatus.Attributes["data-leadid"] = hfLeadId.Value;

            string role = Convert.ToString(Session["Role"]).Replace(" ", "").Trim();

            var deleteButton = (HtmlAnchor)e.Item.FindControl("lnkDelete");
            var editButton = (HtmlAnchor)e.Item.FindControl("lnkEdit");

            if (role.Equals("BusinessPartner", StringComparison.OrdinalIgnoreCase))
            {
                if (deleteButton != null) deleteButton.Visible = false;
                if (editButton != null) editButton.Visible = false;

            }
            else if (role.Equals("TeamMember", StringComparison.OrdinalIgnoreCase))
            {
                if (deleteButton != null) deleteButton.Visible = false;
                if (editButton != null) editButton.Visible = true;
            }

        }
    }

    protected void ddlTransactionFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindLeadList();
    }

    protected void ddlStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindLeadList();
    }

    public void RaisePostBackEvent(string eventArgument)
    {
        if (eventArgument == "ShowLeadsByStatus")
        {
            string selectedStatus = hfSelectedStatus.Value;
            BindLeadListByStatus(selectedStatus);
        }
    }

    [System.Web.Services.WebMethod]
    public static List<string> GetNotifications()
    {
        var notifications = new List<string>();
        string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        int userId = 0;
        if (HttpContext.Current.Session["UserID"] != null)
        {
            userId = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
        }
        else
        {
            return notifications;
        }

        using (SqlConnection con = new SqlConnection(connStr))
        {
            con.Open();

            var cmd = new SqlCommand("SP_GetNotifications", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", userId);

            using (var dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    notifications.Add(dr["Message"].ToString());
                }
            }
        }

        return notifications;
    }
}