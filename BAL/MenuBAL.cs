namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using Utility;

    public class MenuBAL : MenuPAL
    {
        public DataTable BindFooterMenu()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BindFooterMenu");
        }

        public DataTable BindFooterMenuFront()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BindFooterMenufront1");
        }

        public DataTable BindFooterServiceMenu()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BindFooterServiceMenu");
        }

        public DataTable BindFooterSubMenu()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "BindFooterSubMenu");
        }

        public DataSet GetBreadCrumbList(string strParentMenu, string strPageURL)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ParentMenu", DbParameter.DbType.VarChar, 100, strParentMenu), new DbParameter("@PageURL", DbParameter.DbType.VarChar, 0x3e8, strPageURL) };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "GetBreadCrumbList", dbParam);
        }

        public DataTable GetList()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID), new DbParameter("@ParentID", DbParameter.DbType.Int, 20, base.ParentID), new DbParameter("@Name", DbParameter.DbType.VarChar, 50, base.Name) };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "MenuList", dbParam);
        }

        public DataTable GetList1()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID) };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "MenuList1", dbParam);
        }

        public DataTable GetMenuString(int Front)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@Front", DbParameter.DbType.Int, 4, Front) };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "GetMenuString", dbParam);
        }

        public DataTable getparentid()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "getparentid");
        }

        public DataTable GetSubMenuList(string ParentURL)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ParentID", DbParameter.DbType.Int, 20, base.ParentID), new DbParameter("@ParentURL", DbParameter.DbType.VarChar, 100, ParentURL) };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "MenuSubList", dbParam);
        }

        public DataTable GetSubMenuList(string ParentURL, int MainParentID)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@MenuParentID", DbParameter.DbType.Int, 20, MainParentID), new DbParameter("@MenuType", DbParameter.DbType.VarChar, 100, ParentURL) };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "GSubTreeFront", dbParam);
        }

        public DataTable GetSubMenuList_Mobile(string ParentURL, int MainParentID)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@MenuParentID", DbParameter.DbType.Int, 20, MainParentID), new DbParameter("@MenuType", DbParameter.DbType.VarChar, 100, ParentURL) };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "GSubTreeMobileFront", dbParam);
        }

        public DataTable GetTreeFrontMenu()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "GTreeFront");
        }

        public DataSet GetTreeFrontMenuDS()
        {
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "GTreeFront");
        }

        public DataTable GetTreeFrontMenuWithIcon()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "GTreeFrontWithImage");
        }

        public DataTable GetTreeMenu()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "GTree");
        }

        public DataTable GetTreeMenuFront()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "GTreeMenuFront");
        }

        public DataTable GTreeFooterFront()
        {
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "GTreeFooterFront");
        }

        public DataTable MenuDropdown(string strMode)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@mode", DbParameter.DbType.VarChar, 100, strMode.ToLower()), new DbParameter("@ID", DbParameter.DbType.VarChar, 0x3e8, base.ID) };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "MenuDropdown", dbParam);
        }

        public int Operation(string strConatctID, Common.DataBaseOperation ObjOperation)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.VarChar, 200, strConatctID), new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation)), new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "MenuOperation", dbParam);
            return Convert.ToInt32(dbParam[2].Value);
        }

        public int Save()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 4, base.ID), new DbParameter("@ParentID", DbParameter.DbType.Int, 4, base.ParentID), new DbParameter("@Name", DbParameter.DbType.VarChar, 100, base.Name), new DbParameter("@ExternalLink", DbParameter.DbType.Int, 2, base.ExternalLink), new DbParameter("@CMSID", DbParameter.DbType.Int, 4, base.CMSID), new DbParameter("@ExternalLinkURL", DbParameter.DbType.VarChar, 500, base.ExternalLinkURL), new DbParameter("@ReturnVal", DbParameter.DbType.Int, 2, ParameterDirection.Output), new DbParameter("@Position", DbParameter.DbType.Int, 4, base.Position), new DbParameter("@StaticURL", DbParameter.DbType.VarChar, 100, base.StaticURL), new DbParameter("@MenuURL", DbParameter.DbType.VarChar, 500, base.MenuURL), new DbParameter("@LinkType", DbParameter.DbType.Int, 4, base.LinkType), new DbParameter("@ShowInMenu", DbParameter.DbType.Int, 4, base.ShowInMenu) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "MenuAddUpdate", dbParam);
            return Convert.ToInt16(dbParam[6].Value);
        }

        public void SetMenuSequence(string menuID)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@MenuID", DbParameter.DbType.VarChar, 0x3e8, menuID) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SetMenuSequence", dbParam);
        }

        public void SetSubMenuLevel(string levelType)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.Int, 4, base.ID), new DbParameter("@ParentID", DbParameter.DbType.Int, 4, base.ParentID), new DbParameter("@LevelNo", DbParameter.DbType.Int, 4, base.LevelNo), new DbParameter("@LevelType", DbParameter.DbType.VarChar, 2, levelType) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SetSubMenuLevel", dbParam);
        }

        public void UpdateMenuSequence(string menuID, string sequenceNo)
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@ID", DbParameter.DbType.VarChar, 0x3e8, menuID), new DbParameter("@SequenceNo", DbParameter.DbType.VarChar, 0x3e8, sequenceNo) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateMenuSequence", dbParam);
        }
    }
}

