namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class BannerBAL : BannerPAL
    {
        public DataTable BannerListAllFront()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "[BannerListAllFront]");
        }

        public DataTable GetList()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID) };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BannerList", dbParam);
        }

        public DataTable GetList(ref int intCurrentPage, int intRecordPerPage, out int intTotalRecord, string strSortColumn, string strSortType)
        {
            DbParameter[] dbParam = new DbParameter[7];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID);
            dbParam[1] = new DbParameter("@Title", DbParameter.DbType.VarChar, 100, base.Title);
            dbParam[2] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int) intCurrentPage);
            dbParam[2].ParamDirection = ParameterDirection.InputOutput;
            dbParam[3] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, intRecordPerPage);
            dbParam[4] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((strSortColumn != string.Empty) && (strSortType != string.Empty))
            {
                dbParam[5] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, strSortType);
                dbParam[6] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, strSortColumn);
            }
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BannerListNew", dbParam);
            intCurrentPage = Convert.ToInt32(dbParam[2].Value);
            intTotalRecord = Convert.ToInt32(dbParam[4].Value);
            return table;
        }

        public void Operation(string strConatctID, Common.DataBaseOperation ObjOperation)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.VarChar, 0x7d0, strConatctID), new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation)) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BannerOperation", dbParam);
        }

        public int Save()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 4, base.ID), new DbParameter("@Details", DbParameter.DbType.VarChar, 0x1f40, base.Details), new DbParameter("@ExternalLink", DbParameter.DbType.Int, 2, base.ExternalLink), new DbParameter("@MenuID", DbParameter.DbType.Int, 4, base.MenuID), new DbParameter("@ExternalLinkURL", DbParameter.DbType.VarChar, 500, base.ExternalLinkURL), new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output), new DbParameter("@BannerFile", DbParameter.DbType.VarChar, 500, base.BannerFile), new DbParameter("@StaticURL", DbParameter.DbType.VarChar, 100, base.StaticURL), new DbParameter("@Title", DbParameter.DbType.VarChar, 500, base.Title), new DbParameter("@LinkType", DbParameter.DbType.Int, 4, base.LinkType), new DbParameter("@Title2", DbParameter.DbType.VarChar, 8000, base.Title2) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "BannerSave", dbParam);
            return Convert.ToInt16(dbParam[5].Value);
        }

        public void UpdateBannerSequence(string BannerID, string sequenceNo)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.VarChar, 0x3e8, BannerID), new DbParameter("@SequenceNo", DbParameter.DbType.VarChar, 0x3e8, sequenceNo) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateBannerSequence", dbParam);
        }
    }
}

