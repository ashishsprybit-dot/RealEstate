namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class AdminBAL : AdminPAL
    {
        public DataSet AdminLoginCheck()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 50, base.ID) };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "prc_AdminLoginCheck", dbParam);
        }

        public int Changepassword()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@AdminID", DbParameter.DbType.Int, 10, base.ID), new DbParameter("@Password", DbParameter.DbType.VarChar, 50, base.OldPassword), new DbParameter("@NewPassword", DbParameter.DbType.VarChar, 50, base.Password), new DbParameter("@ReturnVal", DbParameter.DbType.Int, 10, ParameterDirection.Output) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "AdminChangePassword", dbParam);
            return Convert.ToInt32(dbParam[3].Value);
        }

        public DataTable CheckPasswordToken(string strPasswordToken)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@strPasswordToken", DbParameter.DbType.VarChar, 50, strPasswordToken) };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "AdminCheckPasswordToken", dbParam);
        }

        public DataTable GetAdminDetails(int UserID)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@UserID", DbParameter.DbType.Int, 4, UserID) };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "GetAdminDetails", dbParam);
        }

        public DataTable GetByID()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID), new DbParameter("@IsSuperAdmin", DbParameter.DbType.Int, 20, base.ID) };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "AdminGetList", dbParam);
        }
        public DataTable AdminDetailsByID()
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID)
                 };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "AdminDetailsByID", dbParam);
        }

        public DataTable GetList(int IsSuperAdmin, ref int CurrentPage, int RecordPerPage, out int TotalRecord, string SortColumn, string SortType)
        {
            DbParameter[] dbParam = new DbParameter[11];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 10, base.ID);
            dbParam[1] = new DbParameter("@FirstName", DbParameter.DbType.VarChar, 50, base.FirstName);
            dbParam[2] = new DbParameter("@LastName", DbParameter.DbType.VarChar, 50, base.LastName);
            dbParam[3] = new DbParameter("@UserName", DbParameter.DbType.VarChar, 50, base.UserName);
            dbParam[4] = new DbParameter("@EmailID", DbParameter.DbType.VarChar, 100, base.EmailID);
            dbParam[5] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int)CurrentPage);
            dbParam[5].ParamDirection = ParameterDirection.InputOutput;
            dbParam[6] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, RecordPerPage);
            dbParam[7] = new DbParameter("@TotalAdmin", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((SortColumn != string.Empty) && (SortType != string.Empty))
            {
                dbParam[8] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, SortType);
                dbParam[9] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, SortColumn);
            }
            dbParam[10] = new DbParameter("@IsSuperAdmin", DbParameter.DbType.Int, 4, IsSuperAdmin);
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "AdminGetList", dbParam);
            CurrentPage = Convert.ToInt32(dbParam[5].Value);
            TotalRecord = Convert.ToInt32(dbParam[7].Value);
            return table;
        }

        public DataTable GetPassword()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@EmailID", DbParameter.DbType.VarChar, 100, base.EmailID) };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "AdminGetPassword", dbParam);
        }

        public DataTable LoginCeck(out short intResult)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@UserName", DbParameter.DbType.VarChar, 50, base.UserName), new DbParameter("@Password", DbParameter.DbType.VarChar, 100, base.Password), new DbParameter("@IsError", DbParameter.DbType.SmallInt, 2, ParameterDirection.Output) };
            DataTable table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "AdminLoginCheck", dbParam);
            intResult = Convert.ToInt16(dbParam[2].Value);
            return table;
        }

        public void Operation(string Id, Common.DataBaseOperation ObjOperation, long UpdatedUserID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.VarChar, 2000, Id), 
                new DbParameter("@UpdatedUserID", DbParameter.DbType.Int, 2000, UpdatedUserID), 
                new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation)) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "AdminOperation", dbParam);
        }

        public int ResetPassword(string strPasswordToken)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@PasswordToken", DbParameter.DbType.VarChar, 500, strPasswordToken), new DbParameter("@newPassword", DbParameter.DbType.VarChar, 500, base.Password), new DbParameter("@ReturnVal", DbParameter.DbType.Int, 10, ParameterDirection.Output) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "AdminResetPassword", dbParam);
            return Convert.ToInt16(dbParam[2].Value);
        }

        public int Save(long CreatedBy, string Pages)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID), 
                new DbParameter("@FirstName", DbParameter.DbType.VarChar, 500, base.FirstName), 
                new DbParameter("@LastName", DbParameter.DbType.VarChar, 500, base.LastName),
                new DbParameter("@UserName", DbParameter.DbType.VarChar, 500, base.UserName),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 500, base.Password),
                new DbParameter("@EmailID", DbParameter.DbType.VarChar, 500, base.EmailID), 
                new DbParameter("@Phone", DbParameter.DbType.VarChar, 20, base.Phone),
                new DbParameter("@AdminType", DbParameter.DbType.Int, 4, base.AdminType),
                new DbParameter("@CreatedBy", DbParameter.DbType.Int, 200, CreatedBy),                
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output) ,
                new DbParameter("@ImageName", DbParameter.DbType.VarChar, 1000, ImageName),
                new DbParameter("@Pages", DbParameter.DbType.VarChar, 8000, Pages)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "AdminSave", dbParam);
            return Convert.ToInt32(dbParam[9].Value);
        }
        public int ProfileSave(long CreatedBy)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID), 
                new DbParameter("@FirstName", DbParameter.DbType.VarChar, 500, base.FirstName), 
                new DbParameter("@LastName", DbParameter.DbType.VarChar, 500, base.LastName),
                new DbParameter("@UserName", DbParameter.DbType.VarChar, 500, base.UserName),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 500, base.Password),
                new DbParameter("@EmailID", DbParameter.DbType.VarChar, 500, base.EmailID), 
                new DbParameter("@Phone", DbParameter.DbType.VarChar, 20, base.Phone),
                new DbParameter("@AdminType", DbParameter.DbType.Int, 4, base.AdminType),
                new DbParameter("@CreatedBy", DbParameter.DbType.Int, 200, CreatedBy),                
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output) ,
                new DbParameter("@ImageName", DbParameter.DbType.VarChar, 1000, ImageName),                
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "ProfileSave", dbParam);
            return Convert.ToInt32(dbParam[9].Value);
        }
        public void SetIPAddress(string strIPAddress, int UserID)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@IpAddress", DbParameter.DbType.VarChar, 100, strIPAddress), new DbParameter("@UserID", DbParameter.DbType.Int, 4, UserID) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SetIPAddress", dbParam);
        }
        public void SetIPAddress(string strIPAddress, int UserID, bool isLogout, string token = "")
        {
            DbParameter[] dbParam = new DbParameter[]
            {
                new DbParameter("@IpAddress", DbParameter.DbType.VarChar, 100, strIPAddress)
                , new DbParameter("@UserID", DbParameter.DbType.Int, 4, UserID)
                , new DbParameter("@IsLogout", DbParameter.DbType.Bit, 4, isLogout)
                , new DbParameter("@Token", DbParameter.DbType.VarChar, 1000, token)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SetIPAddress", dbParam);
        }

        public int SetPasswordToken(string strPasswordToken)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@EmailID", DbParameter.DbType.VarChar, 100, base.EmailID), new DbParameter("@PasswordToken", DbParameter.DbType.VarChar, 0x19, strPasswordToken), new DbParameter("@ReturnVal", DbParameter.DbType.Int, 10, ParameterDirection.Output) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "AdminSetPasswordToken", dbParam);
            return Convert.ToInt32(dbParam[2].Value);
        }
        public DataSet GetAgentList(int intID)
        {
            DbParameter[] dbParam = new DbParameter[] {               
                new DbParameter("@TeamManagerID", DbParameter.DbType.Int, 4, intID)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "AgentList", dbParam);
        }
        public void AgentUpdate(string strAgent, int intID)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 4, intID), 
                new DbParameter("@AgentID", DbParameter.DbType.VarChar, 8000, strAgent)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "AgentUpdate", dbParam);
        }

        public DataSet BindDropdowns()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@UserID", DbParameter.DbType.Int, 4, ID)
             };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "BindDropdowns", dbParam);
        }
        public DataSet BindAgentTeamManager()
        {
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "BindAgentTeamManager");
        }
        public DataTable AdminByID()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@UserID", DbParameter.DbType.Int, 4, ID)
             };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "AdminByID", dbParam);
        }
        public DataTable AdminPageRights()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@AdminID", DbParameter.DbType.Int, 400, ID)
             };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "AdminPageRights", dbParam);
        }

        public DataTable AdminDashboard()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "AdminDashBoard");
            
        }

        public DataSet DashboardReport(int Year)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@Year", DbParameter.DbType.Int, 400, Year)
             };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "DashboardReport", dbParam);
            
        }
        public DataSet AdminListEmails(int TeacherUserID, string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@TeacherUserID", DbParameter.DbType.Int, 400, TeacherUserID),
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 400, SchoolURL)
             };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "AdminListEmails", dbParam);
        }        
    }
}

