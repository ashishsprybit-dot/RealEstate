namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class HomeGroupsBAL : HomeGroupsPAL
    {
        public DataTable GetByID()
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID)
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "HomeGroupsList", dbParam);
        }

        public DataSet GetList(ref int CurrentPage, int RecordPerPage, out int TotalRecord, string SortColumn, string SortType, string SchoolURL, int LoginUserID, int IsReadOnly)
        {
            DbParameter[] dbParam = new DbParameter[10];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 10, base.ID);
            dbParam[1] = new DbParameter("@Name", DbParameter.DbType.VarChar, 50, base.KGCKEY);
            dbParam[2] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int)CurrentPage);
            dbParam[2].ParamDirection = ParameterDirection.InputOutput;
            dbParam[3] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, RecordPerPage);
            dbParam[4] = new DbParameter("@TotalRecord", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((SortColumn != string.Empty) && (SortType != string.Empty))
            {
                dbParam[5] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, SortType);
                dbParam[6] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, SortColumn);
            }
            dbParam[7] = new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL);
            dbParam[8] = new DbParameter("@LoginUserID", DbParameter.DbType.Int, 20, LoginUserID);
            dbParam[9] = new DbParameter("@IsReadOnly", DbParameter.DbType.Int, 20, IsReadOnly);
            DataSet table = new DataSet();
            table = DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "HomeGroupsList", dbParam);
            CurrentPage = Convert.ToInt32(dbParam[2].Value);
            TotalRecord = Convert.ToInt32(dbParam[4].Value);
            return table;
        }

        public void Operation(string Id, Common.DataBaseOperation ObjOperation)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.VarChar, 2000, Id),
                new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation)) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "HomeGroupsOperation", dbParam);
        }


        public int Save(long CreatedBy)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID),
                new DbParameter("@KGCKEY", DbParameter.DbType.VarChar, 500, base.KGCKEY),
                new DbParameter("@DESCRIPTION", DbParameter.DbType.VarChar, 500, base.DESCRIPTION),
                new DbParameter("@STUDENTS", DbParameter.DbType.Int, 500, base.STUDENTS),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "HomeGroupsModify", dbParam);
            return Convert.ToInt32(dbParam[4].Value);
        }


        public DataSet GetStudentQuestionsByCategoryURLV2(long StudentID, string CategoryURL, string SchoolURL, long PreviousAssessmentID, int FullLocked)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@StudentID", DbParameter.DbType.Int, 200, StudentID),
                  new DbParameter("@CategoryURL", DbParameter.DbType.VarChar, 200, CategoryURL),
                  new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL),
                  new DbParameter("@PreviousAssessmentID", DbParameter.DbType.Int, 200, PreviousAssessmentID),
                  new DbParameter("@FullLocked", DbParameter.DbType.Int, 200, FullLocked)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "GetStudentQuestionsByCategoryURLV2", dbParam);
        }
        public void ImportHomeGroups(DataTable dt, string Code)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@Code", DbParameter.DbType.VarChar, 500, Code),
                new DbParameter("@HomeGroups", DbParameter.DbType.Structured, 0, dt)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "Import_HomeGroups", dbParam);
        }
        public void ImportStudents(DataTable dt, string Code)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@Code", DbParameter.DbType.VarChar, 500, Code),
                new DbParameter("@Students", DbParameter.DbType.Structured, 0, dt)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "Import_Students", dbParam);
        }

        public DataSet GetHomeGroupStudents(string HomeGroup, string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@HomeGroup", DbParameter.DbType.VarChar, 200, HomeGroup),
                  new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "GetHomeGroupStudents", dbParam);
        }
        public DataSet GetStudentQuestionsByCategoryURL(long StudentID, string CategoryURL, string SchoolURL, long PreviousAssessmentID, int FullLocked)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@StudentID", DbParameter.DbType.Int, 200, StudentID),
                  new DbParameter("@CategoryURL", DbParameter.DbType.VarChar, 200, CategoryURL),
                  new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL),
                  new DbParameter("@PreviousAssessmentID", DbParameter.DbType.Int, 200, PreviousAssessmentID),
                  new DbParameter("@FullLocked", DbParameter.DbType.Int, 200, FullLocked)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "GetStudentQuestionsByCategoryURL", dbParam);
        }

        public DataSet GetStudentQuestionsByCategoryURLWithSkill(long StudentID, string CategoryURL, string SchoolURL, long PreviousAssessmentID, int FullLocked)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@StudentID", DbParameter.DbType.Int, 200, StudentID),
                  new DbParameter("@CategoryURL", DbParameter.DbType.VarChar, 200, CategoryURL),
                  new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL),
                  new DbParameter("@PreviousAssessmentID", DbParameter.DbType.Int, 200, PreviousAssessmentID),
                  new DbParameter("@FullLocked", DbParameter.DbType.Int, 200, FullLocked)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "GetStudentQuestionsByCategoryURLExampleWithSkill", dbParam);
        }





        public DataSet HomeGroupDistributionChart(string HomeGroup, string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@HomeGroup", DbParameter.DbType.VarChar, 200, HomeGroup),
                  new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "HomeGroupDistributionChart", dbParam);
        }
        public DataSet StudentGrowthChart(long StudentID, string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@StudentID", DbParameter.DbType.Int, 200, StudentID),
                  new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "StudentGrowthChart", dbParam);
        }

        public DataSet HomeGroupGrowthChart(string HomeGroup, string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@HomeGroup", DbParameter.DbType.VarChar, 200, HomeGroup),
                  new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "HomeGroupGrowthChart", dbParam);
        }
        public DataSet CohortsDistributionChart(int CohortID, string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@CohortID", DbParameter.DbType.Int, 200, CohortID),
                  new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "CohortsDistributionChart", dbParam);
        }

        public DataSet GetSubHeading(long QuestionID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@QuestionID", DbParameter.DbType.Int, 200, QuestionID),
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "GetSubHeadingAndQuestion", dbParam);
        }


      

        public DataSet GetSkill(long SubHeadingId, long StudentID, string SchoolURL,string CategoryURL,  long PreviousAssessmentID,long FullLocked)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@QuestionSubHeadingId", DbParameter.DbType.Int, 200, SubHeadingId),
                new DbParameter("@StudentID", DbParameter.DbType.Int, 200, StudentID),
                new DbParameter("@CategoryURL", DbParameter.DbType.VarChar, 200, CategoryURL),
                  new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL),
                  new DbParameter("@PreviousAssessmentID", DbParameter.DbType.Int, 200, PreviousAssessmentID),
                                    new DbParameter("@FullLocked", DbParameter.DbType.Int, 200, FullLocked)


            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "GetSubHeadingAndSkill", dbParam);
        }


        public DataSet GetStudentSkillModelData(long QuestionSubHeadingSkillId, long StudentID, string CategoryURL, string SchoolURL, long PreviousAssessmentID, long QuestionSubHeadingId)
        {
            DbParameter[] dbParam = new DbParameter[] {
                           new DbParameter("@QuestionSubHeadingSkillId", DbParameter.DbType.Int, 200, QuestionSubHeadingSkillId),
                           new DbParameter("@StudentID", DbParameter.DbType.Int, 200, StudentID),
                           new DbParameter("@CategoryURL", DbParameter.DbType.VarChar, 200, CategoryURL),
                           new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL),
                           new DbParameter("@PreviousAssessmentID", DbParameter.DbType.Int, 200, PreviousAssessmentID),
                           new DbParameter("@QuestionSubHeadingId", DbParameter.DbType.Int, 200, QuestionSubHeadingId),

            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "GetSubHeadingAndSkillWithModel", dbParam);
        }




        public int DeletePreviuosLevel(int CategoryID, int StudentID, string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@CategoryID", DbParameter.DbType.Int, 20, CategoryID),
                new DbParameter("@StudentID", DbParameter.DbType.Int, 500, StudentID),
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "DeletePreviuosLevel", dbParam);
            return Convert.ToInt32(dbParam[3].Value);
        }
        public int StudentHomegroupsFinalResultSubmit(int SchoolID, string KGCKEY, int UserID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 20, SchoolID),
                new DbParameter("@KGCKEY", DbParameter.DbType.VarChar, 500, KGCKEY),
                new DbParameter("@UserID", DbParameter.DbType.Int, 200, UserID),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "StudentHomegroupsFinalResultSubmit", dbParam);
            return Convert.ToInt32(dbParam[3].Value);
        }
        public DataSet ExportStudentResult(int LoginUserID, int SemesterNo, string SchoolURL, int Year, string HomeGroup)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL),
                new DbParameter("@LoginUserID", DbParameter.DbType.VarChar, 200, LoginUserID),
                new DbParameter("@SemesterNo", DbParameter.DbType.Int, 200, SemesterNo),
                new DbParameter("@Year", DbParameter.DbType.VarChar, 200, Year),
                new DbParameter("@HomeGroup", DbParameter.DbType.VarChar, 200, HomeGroup)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "ExportStudentResult", dbParam);
        }
        public int UnSubmitHomegroupResult(int SchoolID, string KGCKEY, int SemesterNo, int Year)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 20, SchoolID),
                new DbParameter("@HomeGroup", DbParameter.DbType.VarChar, 500, KGCKEY),
                new DbParameter("@Year", DbParameter.DbType.Int, 200, Year),
                new DbParameter("@SemesterNo", DbParameter.DbType.Int, 200, SemesterNo),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 4, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UnSubkitHomegroupResult", dbParam);
            return Convert.ToInt32(dbParam[4].Value);
        }
    }
}

