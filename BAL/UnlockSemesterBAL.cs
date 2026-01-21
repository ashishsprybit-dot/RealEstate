using DAL;
using PAL;
using System;
using System.Data;
using System.Runtime.InteropServices;
using Utility;

namespace BAL
{
    public class UnlockSemesterBAL
    {
        public DataTable getStudentList(long SchoolID, int LoginUserID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 20, SchoolID),
                new DbParameter("@LoginUserID", DbParameter.DbType.Int, 20, LoginUserID)
            };

            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "getStudentListForDropDown", dbParam);
        }

        public DataTable getCategoryList(string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                 new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL)
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "getCategoryListForDropDown", dbParam);
        }
        public DataTable getSemesterList(long StudentID, long CategoryID)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@StudentID", DbParameter.DbType.Int, 20, StudentID) ,
                new DbParameter("@CategoryID", DbParameter.DbType.Int, 20, CategoryID) 
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "getSemesterListForDropDown", dbParam);
        }

        public int SaveUnlockSemester(long ID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, ID),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UnlockSemester", dbParam);
            return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
        }
        public int SaveLockSemester(long ID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, ID),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "LockSemester", dbParam);
            return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
        }
    }
}
