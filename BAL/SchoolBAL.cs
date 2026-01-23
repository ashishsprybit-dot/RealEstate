namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class SchoolBAL : SchoolPAL
    {      
        public DataTable GetByID()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID)               
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "TenantList", dbParam);
        }
        public DataTable SchoolsDetailsByURL(string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL)
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "SchoolsDetailsByURL", dbParam);
        }
        public DataTable GetList( ref int CurrentPage, int RecordPerPage, out int TotalRecord, string SortColumn, string SortType)
        {
            DbParameter[] dbParam = new DbParameter[7];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 10, base.ID);
            dbParam[1] = new DbParameter("@Name", DbParameter.DbType.VarChar, 50, base.Name);
            dbParam[2] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int)CurrentPage);
            dbParam[2].ParamDirection = ParameterDirection.InputOutput;
            dbParam[3] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, RecordPerPage);
            dbParam[4] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((SortColumn != string.Empty) && (SortType != string.Empty))
            {
                dbParam[5] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, SortType);
                dbParam[6] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, SortColumn);
            }            
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "TenantList", dbParam);
            CurrentPage = Convert.ToInt32(dbParam[2].Value);
            TotalRecord = Convert.ToInt32(dbParam[4].Value);
            return table;
        }
      
        public void Operation(string Id, Common.DataBaseOperation ObjOperation)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.VarChar, 2000, Id),                 
                new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation)) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "TenantOperation", dbParam);
        }

      
        public int Save(long CreatedBy, string SchoolURL,string SchoolCategory)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID), 
                new DbParameter("@Name", DbParameter.DbType.VarChar, 500, base.Name), 
                new DbParameter("@Logo", DbParameter.DbType.VarChar, 500, base.Logo),
                new DbParameter("@TenantURL", DbParameter.DbType.VarChar, 500, SchoolURL),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output)                
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "TenantModify", dbParam);
            return Convert.ToInt32(dbParam[dbParam.Length-1].Value);
        }
        //public int StudentQuestionRatingSave(long CreatedBy, long StudentID, long QuestionID, int Rating, string SchoolURL)
        //{
        //    DbParameter[] dbParam = new DbParameter[] {
        //        new DbParameter("@CreatedBy", DbParameter.DbType.Int, 200,CreatedBy),
        //        new DbParameter("@StudentID", DbParameter.DbType.Int, 200, StudentID),
        //        new DbParameter("@QuestionID", DbParameter.DbType.Int, 200, QuestionID),
        //        new DbParameter("@Rating", DbParameter.DbType.Int, 200, Rating),
        //        new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL),                
        //        new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output)
        //    };

        //    DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "StudentQuestionRatingSave", dbParam);
        //    return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
        //}
        public DataSet StudentList(string SchoolURL,int LoginUserID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                  new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL),
                  new DbParameter("@LoginUserID", DbParameter.DbType.VarChar, 200, LoginUserID)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "StudentList", dbParam);
        }
        public DataTable SchoolCurrentSemester(string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                  new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL)
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "CurrentSemester", dbParam);
        }
        public DataSet SchoolAndAdminList(string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                  new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "SchoolAndAdminList", dbParam);
        }
        public DataSet GetCurrentSemester(string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                  new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "CurrentSemester", dbParam);
        }
    }
}

