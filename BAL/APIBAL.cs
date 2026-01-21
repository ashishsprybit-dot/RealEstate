using DAL;
using PAL;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Utility;

namespace BAL
{
    public class APIBAL
    {
        #region Login
        public DataTable APITeacherLogin(string UserName, string Password)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@UserName", DbParameter.DbType.VarChar, 2000, UserName),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 2000, Password)
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "APITeacherLogin", dbParam);
        }

        #endregion

        #region Validate Request
        public static bool ValidateTeacherRequest(string AuthToken)
        {
            bool returnVal = false;
            try
            {
                if (string.IsNullOrEmpty(AuthToken))
                {
                    return false;
                }
                using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    var cmd = Conn.CreateCommand();
                    cmd.CommandTimeout = 3600;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "APITeacherTokenValidate";

                    cmd.Parameters.Add(new SqlParameter("@AuthToken", AuthToken));
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

        #endregion

        #region Validate Attchment Request
        public static string ValidateAttachmentToken(string AuthToken, long AttachmentID, string AttachmentType)
        {
            string returnVal = "";
            try
            {
                if (string.IsNullOrEmpty(AuthToken) || AttachmentID == 0 || AttachmentType == "")
                {
                    return "";
                }

                using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    var cmd = Conn.CreateCommand();
                    cmd.CommandTimeout = 3600;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "APIAttachmentTokenValidate";

                    cmd.Parameters.Add(new SqlParameter("@AuthToken", AuthToken));
                    cmd.Parameters.Add(new SqlParameter("@AttachmentID", AttachmentID));
                    cmd.Parameters.Add(new SqlParameter("@AttachmentType", AttachmentType));
                    cmd.Parameters.Add(new SqlParameter("@ReturnVal", ""));
                    cmd.Parameters["@ReturnVal"].DbType = DbType.String;
                    cmd.Parameters["@ReturnVal"].Size = int.MaxValue;


                    cmd.Parameters["@ReturnVal"].Direction = ParameterDirection.Output;

                    Conn.Open();
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["@ReturnVal"].Value != null)
                    {
                        returnVal = Convert.ToString(cmd.Parameters["@ReturnVal"].Value);
                    }
                    else
                    {
                        returnVal = "";
                    }
                    Conn.Close();
                }
            }
            catch (Exception ex)
            {
                return "";
            }
            return returnVal;
        }

        #endregion
        public DataTable APIStudentListWithResult(string AuthToken, string HomeGroups)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@AuthToken", DbParameter.DbType.VarChar, 2000, AuthToken),
                new DbParameter("@HomeGroups", DbParameter.DbType.VarChar, 2000000, HomeGroups)
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "APIStudentListWithResult", dbParam);
        }
        public DataTable APIStudentListWithResultCohorts(string AuthToken, string Cohorts)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@AuthToken", DbParameter.DbType.VarChar, 2000, AuthToken),
                new DbParameter("@Cohorts", DbParameter.DbType.VarChar, 2000000, Cohorts )
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "APIStudentListWithResultCohorts", dbParam);
        }
        public DataTable APIStudentResultWithYearSemester(string AuthToken, string HomeGroups, int Year, int Semester)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@AuthToken", DbParameter.DbType.VarChar, 2000, AuthToken),
                new DbParameter("@HomeGroups", DbParameter.DbType.VarChar, 2000000, HomeGroups),
                new DbParameter("@Year", DbParameter.DbType.VarChar, 2000000, Year ),
                new DbParameter("@Semester", DbParameter.DbType.VarChar, 2000000, Semester)
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "APIStudentResultWithYearSemester", dbParam);
        }

        public DataTable APIStudentListDetailResult(string AuthToken, string HomeGroups, int Year, int Semester)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@AuthToken", DbParameter.DbType.VarChar, 2000, AuthToken),
                new DbParameter("@HomeGroups", DbParameter.DbType.VarChar, 2000000, HomeGroups),
                new DbParameter("@Year", DbParameter.DbType.Int, 20, Year),
                new DbParameter("@Semester", DbParameter.DbType.Int, 20, Semester)
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "APIStudentListDetailResult", dbParam);
        }
        public DataSet APIStudentListDetailResultQuestions(int StudentID, int CategoryID, int Year, int Semester)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@StudentID", DbParameter.DbType.Int, 2000, StudentID),
                new DbParameter("@CategoryID", DbParameter.DbType.Int, 2000000, CategoryID),
                new DbParameter("@Year", DbParameter.DbType.Int, 20, Year),
                new DbParameter("@Semester", DbParameter.DbType.Int, 20, Semester)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "APIStudentListDetailResultQuestions", dbParam);
        }

        public DataTable APIStudentListDetailResultWithSkill(string AuthToken, string HomeGroups, int Year, int Semester)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@AuthToken", DbParameter.DbType.VarChar, 2000, AuthToken),
                new DbParameter("@HomeGroups", DbParameter.DbType.VarChar, 2000000, HomeGroups),
                new DbParameter("@Year", DbParameter.DbType.Int, 20, Year),
                new DbParameter("@Semester", DbParameter.DbType.Int, 20, Semester)
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "APIStudentListDetailResultWithSkill", dbParam);
        }

        public DataSet APIStudentListDetailResultQuestionsWithSkill(int StudentID, int CategoryID, int Year, int Semester)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@StudentID", DbParameter.DbType.Int, 2000, StudentID),
                new DbParameter("@CategoryID", DbParameter.DbType.Int, 2000000, CategoryID),
                new DbParameter("@Year", DbParameter.DbType.Int, 20, Year),
                new DbParameter("@Semester", DbParameter.DbType.Int, 20, Semester)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "APIStudentListDetailResultQuestionsWithSkill", dbParam);
        }
    }
}
