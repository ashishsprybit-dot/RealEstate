namespace DAL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Configuration;

    using Microsoft.ApplicationBlocks.Data;

    public sealed class DbConnectionDAL
    {
        // public static string sqlConnectionstring = ConfigurationManager.AppSettings["ConnectionString"].ToString();
        public static string sqlConnectionstring = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        //static SqlConnection cn = new SqlConnection(sqlConnectionstring);
        private static SqlParameter[] ConvertToSqlParameter(DbParameter[] dbParam)
        {
            return DbParameter.GetSqlParameter(dbParam);
        }

        public static void ExecuteNonQuery(CommandType cmdType, string cmdText)
        {
            SqlHelper.ExecuteNonQuery(sqlConnectionstring, cmdType, cmdText);
        }

        public static void ExecuteNonQuery(CommandType cmdType, string cmdText, DbParameter[] dbParam)
        {
            SqlParameter[] spParam = ConvertToSqlParameter(dbParam);
            SqlHelper.ExecuteNonQuery(sqlConnectionstring, cmdType, cmdText, spParam);
            DbParameter.SetDbParameterValueFromSql(spParam, dbParam);
        }

        public static SqlDataReader GetDataReader(CommandType cmdType, string cmdText)
        {
            return SqlHelper.ExecuteReader(sqlConnectionstring, cmdType, cmdText);
        }

        public static SqlDataReader GetDataReader(CommandType cmdType, string cmdText, DbParameter[] dbParam)
        {
            SqlParameter[] spParam = ConvertToSqlParameter(dbParam);
            SqlDataReader dReader = SqlHelper.ExecuteReader(sqlConnectionstring, cmdType, cmdText, spParam);
            DbParameter.SetDbParameterValueFromSql(spParam, dbParam);
            return dReader;
        }

        public static DataSet GetDataSet(CommandType cmdType, string cmdText)
        {
            return SqlHelper.ExecuteDataset(sqlConnectionstring, cmdType, cmdText);
        }

        public static DataSet GetDataSet(CommandType cmdType, string cmdText, DbParameter[] dbParam)
        {
            SqlParameter[] spParam = ConvertToSqlParameter(dbParam);
            DataSet dsetTable = SqlHelper.ExecuteDataset(sqlConnectionstring, cmdType, cmdText, spParam);
            DbParameter.SetDbParameterValueFromSql(spParam, dbParam);
            return dsetTable;
        }

        public static DataTable GetDataTable(CommandType cmdType, string cmdText)
        {
            return SqlHelper.ExecuteDataTable(sqlConnectionstring, cmdType, cmdText);
        }

        public static DataTable GetDataTable(CommandType cmdType, string cmdText, DbParameter[] dbParam)
        {
            SqlParameter[] spParam = ConvertToSqlParameter(dbParam);
            DataTable dtblTable = SqlHelper.ExecuteDataTable(sqlConnectionstring, cmdType, cmdText, spParam);
            DbParameter.SetDbParameterValueFromSql(spParam, dbParam);
            return dtblTable;
        }

        public static object GetScalarValue(CommandType cmdType, string cmdText)
        {
            return SqlHelper.ExecuteScalar(sqlConnectionstring, cmdType, cmdText);
        }

        public static object GetScalarValue(CommandType cmdType, string cmdText, DbParameter[] dbParam)
        {
            SqlParameter[] spParam = ConvertToSqlParameter(dbParam);
            object objValue = SqlHelper.ExecuteScalar(sqlConnectionstring, cmdType, cmdText, spParam);
            DbParameter.SetDbParameterValueFromSql(spParam, dbParam);
            return objValue;
        }
    }
}

