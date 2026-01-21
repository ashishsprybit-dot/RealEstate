namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class CohortsBAL : CohortsPAL
    {
        public DataTable GetByID(string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID),
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL),
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "CohortByID", dbParam);
        }
        public DataTable GetList(string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[1];           
            dbParam[0] = new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 20, SchoolURL);
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "CohortList", dbParam);            
        }
     
        public void Operation(string Id, Common.DataBaseOperation ObjOperation, long UpdatedUserID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.VarChar, 2000, Id),
                new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation)) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "CohortOperation", dbParam);
        }


        public long Save(long CreatedBy, string strSchoolURL, DataTable dt)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID),
                new DbParameter("@Name", DbParameter.DbType.VarChar, 500, base.Name),                
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, strSchoolURL),
                new DbParameter("@CreatedBy", DbParameter.DbType.Int, 100, CreatedBy),
                new DbParameter("@Students", DbParameter.DbType.Structured, 0, dt),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 40, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "CohortSave", dbParam);
            return Convert.ToInt64(dbParam[dbParam.Length - 1].Value);
        }

    }
}



