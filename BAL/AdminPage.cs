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


/// <summary>
/// Summary descriptBALion for AdminPage
/// </summary>
public class ClsAdminPageBAL : ClsAdminPageProperty
{
    #region Public Constructor
    public ClsAdminPageBAL() { }
   
    #endregion

    #region AdminPage Methods
    public void AdminPageModify()
    {
        DbParameter[] ObjParam = new DbParameter[1];
        ObjParam[0] = new DbParameter("@varAdminPageName", DbParameter.DbType.VarChar, 8000, this.varAdminPageName);
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "[dbo].[prc_AdminPageModify]", ObjParam);
    }
    public DataSet GetAdminPageList(string StrKey, string StrValue, int CurrentPage, int RecordPerPage)
    {
        DbParameter[] ObjParam = new DbParameter[4];
        ObjParam[0] = new DbParameter("@strKey", DbParameter.DbType.VarChar, 1000, StrKey);
        ObjParam[1] = new DbParameter("@strValue", DbParameter.DbType.VarChar, 1000, StrValue);
        ObjParam[2] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, CurrentPage);
        ObjParam[3] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, RecordPerPage);
        return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "[dbo].[prc_GetAdminPageList]", ObjParam);
    }
    public DataTable GetAllAdminPageList(string StrKey, string StrValue)
    {
        DbParameter[] ObjParam = new DbParameter[4];
        ObjParam[0] = new DbParameter("@strKey", DbParameter.DbType.VarChar, 1000, StrKey);
        ObjParam[1] = new DbParameter("@strValue", DbParameter.DbType.VarChar, 1000, StrValue);
        ObjParam[2] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, 0);
        ObjParam[3] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, -1);
        return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "[dbo].[prc_GetAdminPageList]", ObjParam).Tables[0];
    }

    public DataTable GetModuleWisePageList(int ModuleID)
    {
        DbParameter[] ObjParam = new DbParameter[1];
        ObjParam[0] = new DbParameter("@ModuleID", DbParameter.DbType.Int, 10, ModuleID);
        return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "[dbo].[prc_GetModulewisePageName]", ObjParam);
    }

    public void ModifyModulewisePage(int ModuleID)
    {
        DbParameter[] ObjParam = new DbParameter[2];
        ObjParam[0] = new DbParameter("@intRelModPagPID", DbParameter.DbType.VarChar, 1000, varAdminPageName);
        ObjParam[1] = new DbParameter("@intRelModPagMID", DbParameter.DbType.Int, 10, ModuleID);
        DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "[dbo].[prc_RelModulePageModify]", ObjParam);
    }
    #endregion
}
