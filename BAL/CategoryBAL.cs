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
    public class CategoryBAL : CategoryPAL
    {   

        #region Category GetList
         public DataTable GetList( ref int CurrentPage, int RecordPerPage, out int TotalRecord, string SortColumn, string SortType)
        {
            DbParameter[] dbParam = new DbParameter[7];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 10, ID);
            dbParam[1] = new DbParameter("@Name", DbParameter.DbType.VarChar, 50, Name);
            dbParam[2] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, CurrentPage);
            dbParam[2].ParamDirection = ParameterDirection.InputOutput;
            dbParam[3] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, RecordPerPage);
            dbParam[4] = new DbParameter("@TotalRecords", DbParameter.DbType.Int, 4, ParameterDirection.Output);


            if (SortColumn != string.Empty && SortType != string.Empty)
            {
                dbParam[5] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, SortType);
                dbParam[6] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, SortColumn);
            }            
            DataTable dtblCategoryList = new DataTable();
            dtblCategoryList = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "CategoryList", dbParam);
            CurrentPage = Convert.ToInt32(dbParam[2].Value);
            TotalRecord = Convert.ToInt32(dbParam[4].Value);
            return dtblCategoryList;
        }


        #endregion

        #region Category GetByID
        public DataTable GetByID()
        {            
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, ID);            
            DataTable dtblCategoryListByID = new DataTable();
            dtblCategoryListByID = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "CategoryList", dbParam);
            return dtblCategoryListByID;
        }
        public DataTable CategoryListALL()
        {
            return  DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "CategoryListALL");
        }
        public DataSet CategoryListForMenu()
        {
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "CategoryListForMenu");
        }
        public DataTable GetCategoryForProduct()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "GetCategoryForProduct");
        }
        
        #endregion

        #region Category Save
        public int Save(string SchoolCategory)
        {
            DbParameter[] dbParam = new DbParameter[11];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, ID);
            dbParam[1] = new DbParameter("@Name", DbParameter.DbType.VarChar, 100, Name);
            dbParam[2] = new DbParameter("@Description", DbParameter.DbType.VarChar, 8000, Description);
            dbParam[3] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            dbParam[4] = new DbParameter("@ImageName", DbParameter.DbType.VarChar, 100, ImageName);
            dbParam[5] = new DbParameter("@CategoryMetaTitle", DbParameter.DbType.VarChar, 200, CategoryMetaTitle);
            dbParam[6] = new DbParameter("@CategoryMetaKeyword", DbParameter.DbType.VarChar, 500, CategoryMetaKeyword);
            dbParam[7] = new DbParameter("@CategoryMetaDescription", DbParameter.DbType.VarChar, 8000, CategoryMetaDescription);
            dbParam[8] = new DbParameter("@ParentID", DbParameter.DbType.Int, 4, ParentID);
            dbParam[9] = new DbParameter("@SchoolCategory", DbParameter.DbType.VarChar, 900000, SchoolCategory);
            dbParam[10] = new DbParameter("@CLT_Version", DbParameter.DbType.Int, 10, CLT_Version);
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "CategorySave", dbParam);
            return Convert.ToInt32(dbParam[3].Value);
        }
        #endregion

        #region Category Operation
        public int Operation(string Id, Common.DataBaseOperation ObjOperation)
        {     
            DbParameter[] dbParam = new DbParameter[3];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.VarChar, 200, Id);
            dbParam[1] = new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation));
            dbParam[2] = new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output);
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "CategoryOperations", dbParam);
            return Convert.ToInt32(dbParam[2].Value);
        }
        #endregion

        public DataSet CategoryDetailsAndProducts(string URL)
        {
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = new DbParameter("@URL", DbParameter.DbType.VarChar, 200, URL);            
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "CategoryDetailsAndProducts", dbParam);            
        }

        public DataTable CategoryString()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "CategoryRankString");
        }
        public void UpdateCategorySequence(string categoryID)
        {
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.VarChar, 40000, categoryID);
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "CategoryUpdateSequence", dbParam);
        }
        public DataTable CategoryParentCategorynameByID()
        {
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, ID);
            DataTable dtblCategoryListByID = new DataTable();
            dtblCategoryListByID = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "CategoryParentCategorynameByID", dbParam);
            return dtblCategoryListByID;
        }
        public DataTable CategoryDetailsByID()
        {
            DbParameter[] dbParam = new DbParameter[1];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 20, ID);
            DataTable dtblCategoryListByID = new DataTable();
            dtblCategoryListByID = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "CategoryDetailsByID", dbParam);
            return dtblCategoryListByID;
        }
    }
}
