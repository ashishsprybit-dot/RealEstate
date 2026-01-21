namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class CMSBAL : CMSPAL
    {
        /// <summary>
        /// CMS Operations Active/Deactive/Delete
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="ObjOperation"></param>
        public void CMSOperation(string ID, Common.DataBaseOperation ObjOperation)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.VarChar, 500, ID), new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation)) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "CMSOperation", dbParam);
        }

        /// <summary>
        /// Get CMS List
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllCMSList()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "CMSAllList");
        }
        /// <summary>
        /// Get CMS Details By ID
        /// </summary>
        /// <returns></returns>
        public DataTable GetCMSByID()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID) };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "CMSList", dbParam);
        }
        /// <summary>
        /// Get CMS Details at front by CMS URL
        /// </summary>
        /// <returns></returns>
        public DataTable GetCMSData()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@PageUrl", DbParameter.DbType.VarChar, 0xfa0, base.PageUrl) };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "CMSGetData", dbParam);
        }
        /// <summary>
        /// Get CMS Details at front by CMS URL
        /// </summary>
        /// <returns></returns>
        public DataTable GetCMSFrontData()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@PageUrl", DbParameter.DbType.VarChar, 0xfa0, base.PageUrl) };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "CMSFrontGetData", dbParam);
        }


        /// <summary>
        /// Get Home Page Details
        /// </summary>
        /// <returns></returns>
        public DataSet GetHomePageDEtails()
        {
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "GetHomePageDetails");
        }

        /// <summary>
        ///  Get Footer Details
        /// </summary>
        /// <returns></returns>
        public DataSet FooterDetails()
        {
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "FooterDetails");
        }

        /// <summary>
        /// CMS list Paging
        /// </summary>
        /// <param name="CurrentPage"></param>
        /// <param name="RecordPerPage"></param>
        /// <param name="TotalRecord"></param>
        /// <param name="SortColumn"></param>
        /// <param name="SortType"></param>
        /// <returns></returns>
        public DataTable GetList(ref int CurrentPage, int RecordPerPage, out int TotalRecord, string SortColumn, string SortType)
        {
            DbParameter[] dbParam = new DbParameter[7];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID);
            dbParam[1] = new DbParameter("@PageTitle", DbParameter.DbType.VarChar, 200, base.PageTitle);
            dbParam[2] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int) CurrentPage);
            dbParam[2].ParamDirection = ParameterDirection.InputOutput;
            dbParam[3] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, RecordPerPage);
            dbParam[4] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((SortColumn != string.Empty) && (SortType != string.Empty))
            {
                dbParam[5] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, SortType);
                dbParam[6] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, SortColumn);
            }
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "CMSList", dbParam);
            CurrentPage = Convert.ToInt32(dbParam[2].Value);
            TotalRecord = Convert.ToInt32(dbParam[4].Value);
            return table;
        }

        /// <summary>
        /// CMS Details Save
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 4, base.ID), new DbParameter("@PageTitle", DbParameter.DbType.VarChar, 200, base.PageTitle), new DbParameter("@PageUrl", DbParameter.DbType.VarChar, 200, base.PageUrl), new DbParameter("@PageDescription", DbParameter.DbType.NText, 0x989680, base.PageDescription), new DbParameter("@CMSMetaTitle", DbParameter.DbType.VarChar, 200, base.CMSMetaTitle), new DbParameter("@CMSMetaKeyword", DbParameter.DbType.VarChar, 500, base.CMSMetaKeyword), new DbParameter("@CMSMetaDescription", DbParameter.DbType.VarChar, 500, base.CMSMetaDescription), new DbParameter("@ImageName", DbParameter.DbType.VarChar, 100, base.ImageName), new DbParameter("@HeadText", DbParameter.DbType.VarChar, 0x7d0, base.HeadText), new DbParameter("@BodyFooterText", DbParameter.DbType.VarChar, 0x7d0, base.BodyFooterText), new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "CMSModify", dbParam);
            return Convert.ToInt16(dbParam[10].Value);
        }

        public DataTable GetCMSByID_Webservice()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID) };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "CMSBYID_Webservice", dbParam);
        }
    }
}

