namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class ContactUsBAL : ContactUsPAL
    {
        /// <summary>
        /// Get Contact Us Details by ID
        /// </summary>
        /// <returns></returns>
        public DataTable GetContactUSByID()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID) };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "ContactUSByID", dbParam);
        }

        /// <summary>
        /// Contact Us List Paging
        /// </summary>
        /// <param name="intCurrentPage"></param>
        /// <param name="intRecordPerPage"></param>
        /// <param name="intTotalRecord"></param>
        /// <param name="strSortColumn"></param>
        /// <param name="strSortType"></param>
        /// <returns></returns>
        public DataTable GetList(ref int intCurrentPage, int intRecordPerPage, out int intTotalRecord, string strSortColumn, string strSortType)
        {
            DbParameter[] dbParam = new DbParameter[9];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID);
            dbParam[1] = new DbParameter("@Name", DbParameter.DbType.VarChar, 100, base.Name);
            dbParam[2] = new DbParameter("@Subject", DbParameter.DbType.VarChar, 100, string.Empty);
            dbParam[3] = new DbParameter("@EmailAddress", DbParameter.DbType.VarChar, 100, base.EmailAddress);
            dbParam[4] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int)intCurrentPage);
            dbParam[4].ParamDirection = ParameterDirection.InputOutput;
            dbParam[5] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, intRecordPerPage);
            dbParam[6] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((strSortColumn != string.Empty) && (strSortType != string.Empty))
            {
                dbParam[7] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, strSortType);
                dbParam[8] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, strSortColumn);
            }
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "ContactUSList", dbParam);
            intCurrentPage = Convert.ToInt32(dbParam[4].Value);
            intTotalRecord = Convert.ToInt32(dbParam[6].Value);
            return table;
        }

        /// <summary>
        /// Contact Us Operations Active/Deactive/Delete
        /// </summary>
        /// <param name="strConatctID"></param>
        /// <param name="ObjOperation"></param>
        public void Operation(string strConatctID, Common.DataBaseOperation ObjOperation)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.VarChar, 0x7d0, strConatctID), 
                new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation))
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "ContactUsOperation", dbParam);
        }

        /// <summary>
        /// Save Contact Us Details
        /// </summary>
        /// <returns></returns>
        public long Save()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 20, ID), 
                new DbParameter("@BusinessID", DbParameter.DbType.Int, 100,BusinessID),
                new DbParameter("@Name", DbParameter.DbType.VarChar, 100, Name), 
                new DbParameter("@Phone", DbParameter.DbType.VarChar, 100,Phone),
                new DbParameter("@Subject", DbParameter.DbType.VarChar, 100,Subject),
                new DbParameter("@EmailAddress", DbParameter.DbType.VarChar, 100, EmailAddress),
                new DbParameter("@Comments", DbParameter.DbType.NText, 8000, Comments),                 
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output)                
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "ContactUSSave", dbParam);
            return Convert.ToInt64(dbParam[7].Value);
        }
        
    }
}

