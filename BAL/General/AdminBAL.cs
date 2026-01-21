using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PAL;
using DAL;
using Utility;

namespace BAL
{
    public class AdminBAL : AdminPAL
    {

        public DataSet AdminLoginCheck()
        {

            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 50, ID);

            DataSet dtblAdminData = DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "prc_AdminLoginCheck", dbParam);


            return dtblAdminData;
        }

        #region AdminLoginProcess
        public DataTable LoginCeck(out Int16 intResult)
        {
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = new DbParameter("@UserName", DbParameter.DbType.VarChar, 50, UserName);
            dbParam[1] = new DbParameter("@Password", DbParameter.DbType.VarChar, 100, Password);
            dbParam[2] = new DbParameter("@IsError", DbParameter.DbType.SmallInt, 2, ParameterDirection.Output);
            DataTable dtblAdminData = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "AdminLoginCheck", dbParam);
            intResult = Convert.ToInt16(dbParam[2].Value);
            return dtblAdminData;
        }
        #endregion

        #region Admin GetPassword
        public DataTable GetPassword()
        {
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = new DbParameter("@EmailID", DbParameter.DbType.VarChar, 100, EmailID);
            DataTable dtblAdminPassword = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "AdminGetPassword", dbParam);
            return dtblAdminPassword;
        }
        public int SetPasswordToken(string strPasswordToken)
        {
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = new DbParameter("@EmailID", DbParameter.DbType.VarChar, 100, EmailID);
            dbParam[1] = new DbParameter("@PasswordToken", DbParameter.DbType.VarChar, 25, strPasswordToken);
            dbParam[2] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 10, ParameterDirection.Output);
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure,"AdminSetPasswordToken", dbParam);
            return Convert.ToInt32(dbParam[2].Value);
        }
        #endregion

        #region Admin GetList
         public DataTable GetList(int IsSuperAdmin, ref int CurrentPage, int RecordPerPage, out int TotalRecord, string SortColumn, string SortType)
        {
            DbParameter[] dbParam = new DbParameter[11];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 10, ID);
            dbParam[1] = new DbParameter("@FirstName", DbParameter.DbType.VarChar, 50, FirstName);
            dbParam[2] = new DbParameter("@LastName", DbParameter.DbType.VarChar, 50, LastName);
            dbParam[3] = new DbParameter("@UserName", DbParameter.DbType.VarChar, 50, UserName);
            dbParam[4] = new DbParameter("@EmailID", DbParameter.DbType.VarChar, 100, EmailID);
            dbParam[5] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, CurrentPage);
            dbParam[5].ParamDirection = ParameterDirection.InputOutput;
            dbParam[6] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, RecordPerPage);
            dbParam[7] = new DbParameter("@TotalAdmin", DbParameter.DbType.Int, 4, ParameterDirection.Output);


            if (SortColumn != string.Empty && SortType != string.Empty)
            {
                dbParam[8] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, SortType);
                dbParam[9] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, SortColumn);
            }
            dbParam[10] = new DbParameter("@IsSuperAdmin", DbParameter.DbType.Int, 4, IsSuperAdmin);
            DataTable dtblAdminList = new DataTable();
            dtblAdminList = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "AdminGetList", dbParam);
            CurrentPage = Convert.ToInt32(dbParam[5].Value);
            TotalRecord = Convert.ToInt32(dbParam[7].Value);
            return dtblAdminList;
        }


        #endregion

        #region Admin GetByID
        public DataTable GetByID()
        {            
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, ID);
            dbParam[1] = new DbParameter("@IsSuperAdmin", DbParameter.DbType.Int, 20, ID);
            DataTable dtblAdminListByID = new DataTable();
            dtblAdminListByID = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "AdminGetList", dbParam);
            return dtblAdminListByID;
        }
        #endregion

        #region Admin Save
        public int Save()
        {
            DbParameter[] dbParam = new DbParameter[9];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, ID);
            dbParam[1] = new DbParameter("@FirstName", DbParameter.DbType.VarChar, 50, FirstName);
            dbParam[2] = new DbParameter("@LastName", DbParameter.DbType.VarChar, 50, LastName);
            dbParam[3] = new DbParameter("@UserName", DbParameter.DbType.VarChar, 50, UserName);
            dbParam[4] = new DbParameter("@Password", DbParameter.DbType.VarChar, 50, Password);
            dbParam[5] = new DbParameter("@EmailID", DbParameter.DbType.VarChar, 100, EmailID);
            dbParam[6] = new DbParameter("@Phone", DbParameter.DbType.VarChar, 20, Phone);
            dbParam[7] = new DbParameter("@AdminType", DbParameter.DbType.Int, 4, AdminType);
            dbParam[8] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output);

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "AdminSave", dbParam);
            return Convert.ToInt32(dbParam[8].Value);
        }
        #endregion

        #region Admin Operation
        public void Operation(string Id, Common.DataBaseOperation ObjOperation)
        {
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.VarChar, 200, Id);
            dbParam[1] = new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation));
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "AdminOperation", dbParam);
        }
        #endregion

        #region Check Password token
        public DataTable CheckPasswordToken(string strPasswordToken)
        {
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = new DbParameter("@strPasswordToken", DbParameter.DbType.VarChar, 50, strPasswordToken);
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "AdminCheckPasswordToken", dbParam);
        }
        #endregion

        #region Reset Password
        public Int32 ResetPassword(string strPasswordToken)
        {
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = new DbParameter("@PasswordToken", DbParameter.DbType.VarChar, 500, strPasswordToken);
            dbParam[1] = new DbParameter("@newPassword", DbParameter.DbType.VarChar, 500, Password);
            dbParam[2] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 10, ParameterDirection.Output);
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "AdminResetPassword", dbParam);
            return Convert.ToInt16(dbParam[2].Value);
        }
        #endregion

        #region Changepassword
        public int Changepassword()
        {
            DbParameter[] dbParam = new DbParameter[4];
            dbParam[0] = new DbParameter("@AdminID", DbParameter.DbType.Int, 10, ID);
            dbParam[1] = new DbParameter("@Password", DbParameter.DbType.VarChar, 50, OldPassword);
            dbParam[2] = new DbParameter("@NewPassword", DbParameter.DbType.VarChar, 50, Password);
            dbParam[3] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 10, ParameterDirection.Output);
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "AdminChangePassword", dbParam);
            return Convert.ToInt32(dbParam[3].Value);
        }
        #endregion

        #region SetIpAddress
        public void SetIPAddress(string strIPAddress, int UserID)
        {
            DbParameter[] dbParam = new DbParameter[2];
            dbParam[0] = new DbParameter("@IpAddress", DbParameter.DbType.VarChar, 100,strIPAddress);
            dbParam[1] = new DbParameter("@UserID", DbParameter.DbType.Int, 4, UserID);           
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SetIPAddress", dbParam);
            
        }
        public DataTable GetAdminDetails(int UserID)
        {
            DbParameter[] dbParam = new DbParameter[1];            
            dbParam[0] = new DbParameter("@UserID", DbParameter.DbType.Int, 4, UserID);
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "GetAdminDetails", dbParam);
        }
        #endregion
    }
}
