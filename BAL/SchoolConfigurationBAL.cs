using DAL;
using PAL;
using System;
using System.Data;

namespace BAL
{
    public class SchoolConfigurationBAL : SchoolConfigurationPAL
    {
        public DataTable GetBySchoolID(Int64 SchoolID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 20, SchoolID)
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "GetSchoolConfiguration", dbParam);
        }

        public DataTable GetSchoolSemesterConfigurationBySchoolID(Int64 SchoolID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 20, SchoolID)
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "GetSchoolSemesterConfiguration", dbParam);
        }

        public int Save(long CreatedBy)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 5, base.SchoolID),
                new DbParameter("@NextLevelPercentage", DbParameter.DbType.Float, 5, base.NextLevelPercentage),
                new DbParameter("@NotificationDays", DbParameter.DbType.VarChar, 100000, base.NotificationDays),
                new DbParameter("@CreatedBy", DbParameter.DbType.Int, 200, CreatedBy),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SchoolConfigurationSave", dbParam);
            return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
        }

        public int SaveSemesterConfiguration(long CreatedBy, SchoolSemesterConfigurationPAL model )
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 10, model.ID),
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 5, model.SchoolID),
                new DbParameter("@Sem1StartDate", DbParameter.DbType.DateTime, 20, model.Sem1StartDate),
                new DbParameter("@Sem1EndDate", DbParameter.DbType.DateTime, 20, model.Sem1EndDate),
                new DbParameter("@Sem2StartDate", DbParameter.DbType.DateTime, 20, model.Sem2StartDate),
                new DbParameter("@Sem2EndDate", DbParameter.DbType.DateTime, 20, model.Sem2EndDate),
                new DbParameter("@CreatedBy", DbParameter.DbType.Int, 200, CreatedBy),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SchoolSemesterConfigurationSave", dbParam);
            return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
        }

        public int DeleteSemester(Int64 SchoolID, Int64 ID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 10, ID),
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 5, SchoolID),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "DeleteSchoolSemester", dbParam);
            return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
        }
    }
}
