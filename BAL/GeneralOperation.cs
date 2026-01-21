using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using PAL;
using Utility;
using DAL;


namespace BAL
{
    public class ClsGeneralOperation
    {
        public int UpdateRecord(string IdentityValues, char Action, string TableName, string IdentityColumn, string StatusColumnName)
        {
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@strID", DbParameter.DbType.VarChar, 1000, IdentityValues);
            ObjParam[1] = new DbParameter("@strType", DbParameter.DbType.VarChar, 1000, Action);
            ObjParam[2] = new DbParameter("@strTableName", DbParameter.DbType.VarChar, 1000, TableName);
            ObjParam[3] = new DbParameter("@strIDField", DbParameter.DbType.VarChar, 1000, IdentityColumn);
            ObjParam[4] = new DbParameter("@strStatusField", DbParameter.DbType.VarChar, 1000, StatusColumnName);
            ObjParam[5] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "[dbo].[prc_UpdateRecord]", ObjParam);
            return Convert.ToInt32(ObjParam[5].Value);
        }
    }
}
