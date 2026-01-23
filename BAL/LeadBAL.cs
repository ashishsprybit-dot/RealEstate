using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using System.Web;

namespace BAL
{
    public class LeadBAL
    {
        public DataTable GetLeadList(int userID, string role)
        {
            DataTable dt = new DataTable();
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetAllLeads", con))

                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@USER_ID", userID);
                    cmd.Parameters.AddWithValue("@role", role);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public DataTable GetLeadList()
        {
            DataTable dt = new DataTable();
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetAllLeads", con))

                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@USER_ID", userID);
                    //cmd.Parameters.AddWithValue("@role", role);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public void InsertLeadHistory(int leadId, string fieldName, string oldValue, string newValue, int updatedBy)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("SP_InsertLeadHistory", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@LeadID", leadId);
                    cmd.Parameters.AddWithValue("@FieldName", fieldName);
                    cmd.Parameters.AddWithValue("@OldValue", oldValue);
                    cmd.Parameters.AddWithValue("@NewValue", newValue);
                    cmd.Parameters.AddWithValue("@UpdatedBy", updatedBy);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetStatusList()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("SP_GetStatusList", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear(); 


                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }


        public DataTable GetLeadById(int leadId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetLeadDetailsById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LeadMasterID", leadId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public void DeleteLead(int leadId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_DeleteLeadd", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LeadMasterID", leadId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetRemarksByLeadId(int leadId)
        {
            DataTable dt = new DataTable();
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetRemarksByLeadId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LeadID", leadId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public bool InsertRemark(int leadId, string remarkText)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("SP_InsertRemark", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@LeadID", leadId);
                    cmd.Parameters.AddWithValue("@RemarkText", remarkText);
                    cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(HttpContext.Current.Session["UserID"]));

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public string GetSchemeName(long leadId)
        {
            string schemeName = string.Empty;
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetSchemeName", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LeadID", leadId);

                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        schemeName = result.ToString();
                }
            }
            return schemeName;
        }

        public string GetCustomerName(long leadId)
        {
            string customerName = string.Empty;
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("SP_GetCustomerName", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@LeadID", leadId);

                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                        customerName = result.ToString();
                }
            }
            return customerName;
        }

        public bool UpdateLeadStatus(int leadId, string status)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("SP_UpdateLeadStatus", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@LeadID", leadId);
                cmd.Parameters.AddWithValue("@Status", status);

                int updatedBy = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
                cmd.Parameters.AddWithValue("@UpdatedBy", updatedBy);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }

        public DataTable GetAllTransactionTypes()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            return SqlHelper.ExecuteDataTable(connStr, CommandType.StoredProcedure, "SP_GetAllTransactionTypes");
        }

        public DataTable GetLeadHistory(long leadId)
        {
            SqlParameter[] parameters = {
            new SqlParameter("@LeadID", leadId)
            };

            DataTable dt = SqlHelper.ExecuteDataset(
                ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString,
                CommandType.StoredProcedure,
                "SP_GetLeadHistory",
                parameters
            ).Tables[0];

            var fieldMapping = new Dictionary<string, (string TableName, string IdColumn, string NameColumn, string DisplayName)>
            {
              { "TransactionTypeID", ("TransactionTypeMaster", "TransactionTypeID", "TransactionType", "Transaction Type") },
              { "StatusID", ("StatusMaster", "StatusID", "StatusName", "Status") }

            };

            foreach (DataRow row in dt.Rows)
            {
                string field = row["FieldName"].ToString();

                if (fieldMapping.ContainsKey(field))
                {
                    var map = fieldMapping[field];
                    var idToName = LoadNameMap(map.TableName, map.IdColumn, map.NameColumn);

                    string oldId = row["OldValue"].ToString();
                    string newId = row["NewValue"].ToString();

                    row["OldValue"] = idToName.ContainsKey(oldId) ? idToName[oldId] : oldId;
                    row["NewValue"] = idToName.ContainsKey(newId) ? idToName[newId] : newId;

                    row["FieldName"] = map.DisplayName;
                }
                else
                {
                }
            }

            return dt;
        }

        public DataTable GetArchivedLeads()
        {
            int currentUserId = Convert.ToInt32(HttpContext.Current.Session["UserID"]);
            string currentUserRole = HttpContext.Current.Session["Role"]?.ToString();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand("SP_GetArchivedLeads", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CurrentUserID", currentUserId);
                cmd.Parameters.AddWithValue("@CurrentUserRole", currentUserRole ?? (object)DBNull.Value);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        private Dictionary<string, string> LoadNameMap(string tableName, string idColumn, string nameColumn)
        {
            Dictionary<string, string> map = new Dictionary<string, string>();
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string query = $"SELECT {idColumn}, {nameColumn} FROM {tableName}";

            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string id = reader[idColumn].ToString();
                            string name = reader[nameColumn].ToString();
                            map[id] = name;
                        }
                    }
                }
            }
            return map;
        }

        public bool InsertLeadFromExcel(string schemeName, string customerName, string contactNumber, string loanAmount, string propertyValue, string saledeedAmount, string description,  /*string status,*/ int createdBy, int userId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_InsertLeadFromExcel", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SchemeName", schemeName);
                    cmd.Parameters.AddWithValue("@CustomerName", customerName);
                    cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);

                    decimal loanAmt = 0;
                    decimal.TryParse(loanAmount, out loanAmt);
                    cmd.Parameters.AddWithValue("@LoanAmount", loanAmt);
                    decimal propValue = 0;
                    decimal.TryParse(propertyValue, out propValue);
                    cmd.Parameters.AddWithValue("@PropertyValue", propValue);
                    decimal saleDeedAmt = 0;
                    decimal.TryParse(saledeedAmount, out saleDeedAmt);
                    cmd.Parameters.AddWithValue("@SaledeedAmount", saleDeedAmt);

                    cmd.Parameters.AddWithValue("@Description", description);
                    //cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }
    }
}

