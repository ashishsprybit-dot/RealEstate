namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class QuestionSubHeadingSkillsBAL : QuestionSubHeadingSkillsPAL
    {      
        public DataTable GetByID()
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 200, base.ID)  ,
                 new DbParameter("@SubHeadingID", DbParameter.DbType.Int, 20, base.SubHeadingID)
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "QuestionSubHeadingSkillsList", dbParam);
        }

        public DataTable GetList( ref int CurrentPage, int RecordPerPage, out int TotalRecord, string SortColumn, string SortType)
        {
            DbParameter[] dbParam = new DbParameter[7];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 10, base.ID);            
            dbParam[1] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int)CurrentPage);
            dbParam[1].ParamDirection = ParameterDirection.InputOutput;
            dbParam[2] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, RecordPerPage);
            dbParam[3] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((SortColumn != string.Empty) && (SortType != string.Empty))
            {
                dbParam[4] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, SortType);
                dbParam[5] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, SortColumn);
            }
            dbParam[6] = new DbParameter("@SubHeadingID", DbParameter.DbType.Int, 100, base.SubHeadingID);
            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "QuestionSubHeadingSkillsList", dbParam);
            CurrentPage = Convert.ToInt32(dbParam[1].Value);
            TotalRecord = Convert.ToInt32(dbParam[3].Value);
            return table;
        }
      
        public int Operation(string Id, Common.DataBaseOperation ObjOperation, int UserID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.VarChar, 2000, Id),                 
                new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation)),
                new DbParameter("@UserID", DbParameter.DbType.Int, 10,UserID),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output)
                };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "QuestionSubHeadingSkillsOperation", dbParam);
            return Convert.ToInt32(dbParam[3].Value);
        }
      
        public int Save(long CreatedBy)
        {
            DbParameter[] dbParam = new DbParameter[] { 
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID), 
                new DbParameter("@Skill", DbParameter.DbType.VarChar, 1000000, base.Skill),                 
                new DbParameter("@SubHeadingID", DbParameter.DbType.Int, 500, base.SubHeadingID),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output)                
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "QuestionSubHeadingSkillsModify", dbParam);
            return Convert.ToInt32(dbParam[3].Value);
        }
        public void QuestionSubHeadingSkillOrderChange(int SubHeadingID, string Sequence)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SubHeadingID", DbParameter.DbType.Int, 20, SubHeadingID),
                new DbParameter("@Sequence", DbParameter.DbType.VarChar, 50000,Sequence)                
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "QuestionSubHeadingSkillOrderChange", dbParam);
            
        }
    }
}

