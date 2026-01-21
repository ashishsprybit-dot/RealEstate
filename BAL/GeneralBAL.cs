namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public class GeneralBAL : GeneralSettingsPAL
    {
        public DataTable GetList()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "GeneralSettingList");
        }

        public int Save(string strKeyword, string strValue)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@Keyword", DbParameter.DbType.VarChar, 200, strKeyword), new DbParameter("@Value", DbParameter.DbType.VarChar, 0xbb8, strValue), new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "GeneralSettingAddEdit", dbParam);
            return Convert.ToInt16(dbParam[2].Value);
        }

        public DataTable selectByKeyword(string keyword)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@keyword", DbParameter.DbType.VarChar, 500, keyword) };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "sp_selectbykeyword", dbParam);
        }

        public int updatebyleyword(string keyword, string value)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@keyword", DbParameter.DbType.VarChar, 200, keyword), new DbParameter("@value", DbParameter.DbType.VarChar, 0x2710, value) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "sp_siteconfig_updatebykeyword", dbParam);
            return Convert.ToInt16(dbParam[1].Value);
        }
        public DataTable GetDataFromTableName(string strTableName)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@strTableName", DbParameter.DbType.VarChar, 500, strTableName) };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "GetDataFromTableName", dbParam);
        }

        public string ValidateReferralCode(string referralCode)
        {
            string value = "";
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@referralCode", DbParameter.DbType.VarChar, 500, referralCode), new DbParameter("@result", DbParameter.DbType.VarChar, 500, value, ParameterDirection.Output) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "ReferralCodeValidate", dbParam);
            return dbParam[1].Value.ToString();
        }
        public DataTable PagesList()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "PagesList");
        }
        public void UpdateNoticationsCountForUsers(string strUsers, string strBusiness)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@Users", DbParameter.DbType.VarChar, 8000, strUsers),
                new DbParameter("@Business", DbParameter.DbType.VarChar, 8000, strBusiness)
                };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateNoticationsCountForUsers", dbParam);
        }
        public static bool ValidateSchoolURLRequest(string SchoolURL)
        {
            bool returnVal = false;
            try
            {
                using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    var cmd = Conn.CreateCommand();
                    cmd.CommandTimeout = 3600;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "ValidateTenantURL";

                    cmd.Parameters.Add(new SqlParameter("@SchoolURL", SchoolURL));
                    cmd.Parameters.Add(new SqlParameter("@ReturnVal", 0));
                    cmd.Parameters["@ReturnVal"].Direction = ParameterDirection.Output;

                    Conn.Open();
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["@ReturnVal"].Value != null)
                    {
                        if (Convert.ToInt32(cmd.Parameters["@ReturnVal"].Value) == 1)
                        {
                            returnVal = true;
                        }
                    }
                    else
                    {
                        returnVal = false;
                    }
                    Conn.Close();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return returnVal;
        }
        public static bool ValidateSchoolTeacher(int SchoolID, int TeacherID)
        {
            bool returnVal = false;
            try
            {

                using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    var cmd = Conn.CreateCommand();
                    cmd.CommandTimeout = 3600;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "ValidateSchoolTeacher";

                    cmd.Parameters.Add(new SqlParameter("@SchoolID", SchoolID));
                    cmd.Parameters.Add(new SqlParameter("@TeacherID", TeacherID));
                    cmd.Parameters.Add(new SqlParameter("@ReturnVal", 0));
                    cmd.Parameters["@ReturnVal"].Direction = ParameterDirection.Output;

                    Conn.Open();
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["@ReturnVal"].Value != null)
                    {
                        if (Convert.ToInt32(cmd.Parameters["@ReturnVal"].Value) == 1)
                        {
                            returnVal = true;
                        }
                        else
                        {
                            returnVal = false;
                        }
                    }
                    else
                    {
                        returnVal = false;
                    }
                    Conn.Close();
                }
                //  returnVal = true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return returnVal;
        }

    }
}

