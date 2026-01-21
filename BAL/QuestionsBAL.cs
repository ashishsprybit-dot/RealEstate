namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class QuestionsBAL : QuestionsPAL
    {
        public DataTable GetByID()
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID)  ,
                 new DbParameter("@CategoryID", DbParameter.DbType.Int, 20, base.CategoryID)
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "QuestionsList", dbParam);
        }

        public DataTable GetList(ref int CurrentPage, int RecordPerPage, out int TotalRecord, string SortColumn, string SortType)
        {
            DbParameter[] dbParam = new DbParameter[7];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 10, base.ID);
            dbParam[1] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int)CurrentPage);
            dbParam[1].ParamDirection = ParameterDirection.InputOutput;
            dbParam[2] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, RecordPerPage);
            dbParam[3] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((SortColumn != string.Empty) && (SortType != string.Empty))
            {
                dbParam[4] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, SortType);
                dbParam[5] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, SortColumn);
            }
            dbParam[6] = new DbParameter("@CategoryID", DbParameter.DbType.Int, 100, base.CategoryID);
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "QuestionsList", dbParam);
            CurrentPage = Convert.ToInt32(dbParam[1].Value);
            TotalRecord = Convert.ToInt32(dbParam[3].Value);
            return table;
        }

        public int Operation(string Id, Common.DataBaseOperation ObjOperation, int UserID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.VarChar, 2000, Id),
                new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation)),
                new DbParameter("@UserID", DbParameter.DbType.Int, 10,UserID),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output)
                };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "QuestionsOperation", dbParam);
            return Convert.ToInt32(dbParam[3].Value);
        }



        public static string ExcelSave(long categoryID, string questionText, string subHeading, string StudentSkill)
        {
            // Define parameters for the stored procedure
            DbParameter[] dbParam = new DbParameter[]
            {
        new DbParameter("@CategoryID", DbParameter.DbType.Int, 20, categoryID),
        new DbParameter("@QuestionText", DbParameter.DbType.VarChar, 500, questionText),
        new DbParameter("@SubHeading", DbParameter.DbType.VarChar, 500, subHeading),
        new DbParameter("@Skills", DbParameter.DbType.VarChar, 1000, StudentSkill),

        // Output parameter for success message
        new DbParameter("@Message", DbParameter.DbType.VarChar, 255, ParameterDirection.Output)
            };

            // Execute the stored procedure
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "sp_ImportQuestionData", dbParam);

            // Retrieve the output message
            return dbParam[4].Value.ToString();  // Correct index for output parameter
        }

        public static DataSet SaveExcelData(string jsonData, long categoryID)
        {
            try
            {
                DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@pInputData", DbParameter.DbType.NVarChar, -1, jsonData), // ✅ Use -1 for NVARCHAR(MAX)
                new DbParameter("@CategoryID", DbParameter.DbType.Int, 20, categoryID),

                      };

                // ✅ Execute Stored Procedure and Capture Errors (if any)
                // DataTable dtResult = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "uploadTargetFile", dbParam);
                DataSet dtResult = DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "uploadTargetFile", dbParam);

                return dtResult; // Returns error data if validation fails
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public static DataSet SaveExcelData(string jsonData)
        {
            try
            {
                DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@pInputData", DbParameter.DbType.NVarChar, -1, jsonData), // ✅ Use -1 for NVARCHAR(MAX)
               
                };

                // ✅ Execute Stored Procedure and Capture Errors (if any)
                // DataTable dtResult = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "uploadTargetFile", dbParam);
                DataSet dtResult = DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "uploadTargetFileLevelWise", dbParam);

                return dtResult; // Returns error data if validation fails
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public int Save(long CreatedBy)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID),
                new DbParameter("@Question", DbParameter.DbType.VarChar, 500, base.Question),
                new DbParameter("@DescriptionLink", DbParameter.DbType.VarChar, 500, base.DescriptionLink),
                new DbParameter("@CategoryID", DbParameter.DbType.Int, 500, base.CategoryID),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "QuestionsModify", dbParam);
            return Convert.ToInt32(dbParam[4].Value);
        }
        public void QuestionOrderChange(int CategoryID, string Sequence)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@CategoryID", DbParameter.DbType.Int, 20,CategoryID),
                new DbParameter("@Sequence", DbParameter.DbType.VarChar, 50000,Sequence)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "QuestionOrderChange", dbParam);

        }
    }
}

