using DAL;
using System.Data;

namespace BAL
{
    public class SendSchoolNotificationBAL
    {

        public DataTable GetSchoolList()
        {
            DbParameter[] dbParam = new DbParameter[] {};
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "getSchoolList", dbParam);
        }

        public DataSet GetSchoolPendingNotificationList(long SchoolID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 500, SchoolID),
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "GetSchoolPendingNotificationList", dbParam);
        }

        public DataTable SchoolSemesterDetails(int SchoolID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 500, SchoolID),
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "SchoolSemesterDetails", dbParam);
        }
        public DataTable HomeGroupsListBySchoolID(int SchoolID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 500, SchoolID),
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "HomeGroupsListBySchoolID", dbParam);
        }

        public void StudentHomegroupsFinalResultSubmitJob(int SchoolID, string KGCKEY, int SemesterNo, int CurrentYear)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 500, SchoolID),
                new DbParameter("@KGCKEY", DbParameter.DbType.VarChar, 500, KGCKEY),
                new DbParameter("@SemesterNo", DbParameter.DbType.Int, 500, SemesterNo),
                new DbParameter("@CurrentYear", DbParameter.DbType.Int, 500, CurrentYear),
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "StudentHomegroupsFinalResultSubmitJob", dbParam);
        }

        public DataTable StudentListByHomegroupSchoolID(int SchoolID, string KGCKEY)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 500, SchoolID),
                new DbParameter("@KGCKEY", DbParameter.DbType.VarChar, 500, KGCKEY)
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "StudentListByHomegroupSchoolID", dbParam);
        }

        public void HomegroupSubmitFinalResultJob(int SchoolID, int StudentID, int SemesterNo, int CurrentYear)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 500, SchoolID),
                new DbParameter("@StudentID", DbParameter.DbType.Int, 500, StudentID),
                new DbParameter("@CurrentSemester", DbParameter.DbType.Int, 500, SemesterNo),
                new DbParameter("@CurrentYear", DbParameter.DbType.Int, 500, CurrentYear),
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "HomegroupSubmitFinalResultJob", dbParam);
        }
    }
}
