using DAL;
using PAL;
using System;
using System.Data;
using Utility;

namespace BAL
{
    public class ViewMediaBAL : ViewMediaPAL
    {
        public DataSet GetMediaList(Int64 SchoolID, Int64 StudentID, string Sem, string Year)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 200, SchoolID),
                new DbParameter("@StudentID", DbParameter.DbType.Int, 200, StudentID),
                new DbParameter("@Sem", DbParameter.DbType.Int, 200, Sem),
                new DbParameter("@Year", DbParameter.DbType.Int, 200, Year)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "GetViewMediaList", dbParam);
        }
    }
}
