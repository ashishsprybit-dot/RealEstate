using DAL;
using PAL;
using System;
using System.Data;
using System.Runtime.InteropServices;
using Utility;

namespace BAL
{
    public class PreviousSemesterResultBAL
    {
        public DataTable getGroupList(string SchoolURL, long TeacherUserID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 1000, SchoolURL),
                new DbParameter("@LoginUserID", DbParameter.DbType.Int, 20, TeacherUserID)
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "getHomeGroupsListForDropDown", dbParam);
        }

        public DataSet StudentCurrentCategoryScoreAndPercentage(string SchoolURL, string CategoryURL, long StudentID, int CalledFromHomeGroup,int Year,int Sem)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, SchoolURL),
                new DbParameter("@CategoryURL", DbParameter.DbType.VarChar, 200,CategoryURL),
                new DbParameter("@StudentID", DbParameter.DbType.Int, 200, StudentID),
                new DbParameter("@CalledFromHomeGroup", DbParameter.DbType.Int, 200, CalledFromHomeGroup),
                new DbParameter("@Year", DbParameter.DbType.Int, 200, Year),
                new DbParameter("@Semester", DbParameter.DbType.Int, 200, Sem)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "StudentCurrentCategoryScoreAndPercentagePreviousResult", dbParam);
        }
        public DataSet StudentCurrentCategoryScoreAndPercentageV2(string SchoolURL, string CategoryURL, long StudentID, int CalledFromHomeGroup, int Year, int Sem)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, SchoolURL),
                new DbParameter("@CategoryURL", DbParameter.DbType.VarChar, 200,CategoryURL),
                new DbParameter("@StudentID", DbParameter.DbType.Int, 200, StudentID),
                new DbParameter("@CalledFromHomeGroup", DbParameter.DbType.Int, 200, CalledFromHomeGroup),
                new DbParameter("@Year", DbParameter.DbType.Int, 200, Year),
                new DbParameter("@Semester", DbParameter.DbType.Int, 200, Sem)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "StudentCurrentCategoryScoreAndPercentagePreviousResultV2", dbParam);
        }
        public DataSet getGroupListForHistory(string SchoolURL, long TeacherUserID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 1000, SchoolURL),
                new DbParameter("@LoginUserID", DbParameter.DbType.Int, 20, TeacherUserID)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "getHomeGroupsListForDropDownForHistory", dbParam);
        }

        public DataSet StudentHistoryList(int SchoolID, string HomeGroup, int Year, int Semester)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 20, SchoolID),
                new DbParameter("@HomeGroup", DbParameter.DbType.VarChar, 1000, HomeGroup),
                new DbParameter("@Year", DbParameter.DbType.Int, 20, Year),
                new DbParameter("@Semester", DbParameter.DbType.Int, 20, Semester)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "StudentHistoryList", dbParam);
        }

        public DataTable getGroupListForReport(string SchoolURL, long TeacherUserID, int SelectedYear)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 1000, SchoolURL),
                new DbParameter("@LoginUserID", DbParameter.DbType.Int, 20, TeacherUserID),
                new DbParameter("@SelectedYear", DbParameter.DbType.Int, 20, SelectedYear),
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "getHomeGroupsListForDropDownForReport", dbParam);
        }
    }
}
