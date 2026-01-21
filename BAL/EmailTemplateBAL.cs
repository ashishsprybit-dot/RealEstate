namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using System.Text;
    using Utility;

    public class EmailTemplateBAL : EmailTemplatePAL
    {
        /// <summary>
        /// Get Email Template Details by ID
        /// </summary>
        /// <param name="intID"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public DataTable GetByID(int intID, int intType)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 4, intID), new DbParameter("@Type", DbParameter.DbType.Int, 4, intType) };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "EmailTemplateByID", dbParam);
        }

        /// <summary>
        /// Email Template List Paging
        /// </summary>
        /// <param name="intCurrentPage"></param>
        /// <param name="RecordPerPage"></param>
        /// <param name="intTotalRecord"></param>
        /// <param name="strSortColumn"></param>
        /// <param name="strSortType"></param>
        /// <returns></returns>
        public DataTable GetList(ref int intCurrentPage, int RecordPerPage, out int intTotalRecord, string strSortColumn, string strSortType)
        {
            DbParameter[] dbParam = new DbParameter[7];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 4, base.ID);
            dbParam[1] = new DbParameter("@TemplateName", DbParameter.DbType.VarChar, 100, base.TemplateName);
            dbParam[2] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 4, (int) intCurrentPage, ParameterDirection.InputOutput);
            dbParam[3] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 4, RecordPerPage);
            dbParam[4] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((strSortColumn != string.Empty) && (strSortType != string.Empty))
            {
                dbParam[5] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 10, strSortType);
                dbParam[6] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 50, strSortColumn);
            }
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "EmailTemplateList", dbParam);
            intCurrentPage = Convert.ToInt32(dbParam[2].Value);
            intTotalRecord = Convert.ToInt32(dbParam[4].Value);
            return table;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objTemplateName"></param>
        /// <returns></returns>
        public static string GetSubject(TemplateType objTemplateName)
        {
            string str = string.Empty;
            switch (objTemplateName)
            {
                case TemplateType.ContactUsUser:
                    return "Thank you for contacting us through our website ";

                case TemplateType.ContactUsAdmin:
                    return "Contact Us";

                case TemplateType.AdminForgotPassword:
                    return "Password Request";
            }
            return str;
        }
        /// <summary>
        /// GET Email Template Details
        /// </summary>
        /// <param name="tmpType"></param>
        /// <returns></returns>
        public StringBuilder GetTemplate(TemplateType tmpType)
        {
            StringBuilder builder = new StringBuilder(string.Empty);
            DataTable byID = new DataTable();
            byID = this.GetByID((int) tmpType, Convert.ToInt32(OperationType.Select));
            if (byID.Rows.Count > 0)
            {
                builder = new StringBuilder(Convert.ToString(byID.Rows[0]["Template"]));
                builder.Replace("{%WebSiteUrl%}", Config.WebSiteUrl);
            }
            return builder;
        }
        /// <summary>
        /// Get Email Template Details with Subject
        /// </summary>
        /// <param name="tmpType"></param>
        /// <param name="strSubject"></param>
        /// <returns></returns>
        public StringBuilder GetTemplateWithSubject(TemplateType tmpType, out string strSubject)
        {
            StringBuilder builder = new StringBuilder(string.Empty);
            DataTable byID = new DataTable();
            byID = this.GetByID((int) tmpType, Convert.ToInt32(OperationType.Select));
            strSubject = string.Empty;
            if (byID.Rows.Count > 0)
            {
                strSubject = Convert.ToString(byID.Rows[0]["Subject"]);
                builder = new StringBuilder(Convert.ToString(byID.Rows[0]["Template"]));
                builder.Replace("{%WebSiteUrl%}", Config.WebSiteUrl);
            }
            return builder;
        }
        /// <summary>
        /// Email Template  Operation Active/Deactive/Delete/
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ObjOperation"></param>
        public void Operations(string ID, Common.DataBaseOperation ObjOperation)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.VarChar, 0x7d0, ID), new DbParameter("@OprType", DbParameter.DbType.SmallInt, 2, Convert.ToInt16(ObjOperation)) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "EmailTemplateOperation", dbParam);
        }
        /// <summary>
        /// Save Email Template Details
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            DbParameter[] dbParam = new DbParameter[5];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 4, base.ID, ParameterDirection.InputOutput);
            dbParam[1] = new DbParameter("@TemplateName", DbParameter.DbType.VarChar, 100, base.TemplateName);
            dbParam[2] = new DbParameter("@Template", DbParameter.DbType.VarChar, 0x1f40, base.Template);
            dbParam[4] = new DbParameter("@Subject", DbParameter.DbType.VarChar, 0x3e8, base.Subject);
            dbParam[3] = new DbParameter("@IsError", DbParameter.DbType.Int, 2, ParameterDirection.Output);
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "EmailTemplateSave", dbParam);
            return Convert.ToInt32(dbParam[3].Value);
        }

        public enum OperationType
        {
            Update,
            Select
        }

        public enum TemplateType
        {
            AdminForgotPassword = 3,
            ContactUsAdmin = 2,
            ContactUsUser = 1            
        }
    }
}

