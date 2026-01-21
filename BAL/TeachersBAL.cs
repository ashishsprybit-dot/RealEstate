namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    public class TeachersBAL : TeachersPAL
    {

        public DataTable GetByID()
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID),
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "TeachersGetList", dbParam);
        }
        public DataTable GetByID(string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID),
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 200, SchoolURL),
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "TeachersGetList", dbParam);
        }
        public DataTable GetList(ref int CurrentPage, int RecordPerPage, out int TotalRecord, string SortColumn, string SortType, string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[10];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 10, base.ID);
            dbParam[1] = new DbParameter("@FirstName", DbParameter.DbType.VarChar, 50, base.FirstName);
            dbParam[2] = new DbParameter("@LastName", DbParameter.DbType.VarChar, 50, base.LastName);
            dbParam[3] = new DbParameter("@EmailID", DbParameter.DbType.VarChar, 100, base.EmailID);
            dbParam[4] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int)CurrentPage);
            dbParam[4].ParamDirection = ParameterDirection.InputOutput;
            dbParam[5] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, RecordPerPage);
            dbParam[6] = new DbParameter("@TotalRecords", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((SortColumn != string.Empty) && (SortType != string.Empty))
            {
                dbParam[7] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, SortType);
                dbParam[8] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, SortColumn);
            }
            dbParam[9] = new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 20, SchoolURL);

            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "TeachersGetList", dbParam);
            CurrentPage = Convert.ToInt32(dbParam[4].Value);
            TotalRecord = Convert.ToInt32(dbParam[6].Value);
            return table;
        }
        public DataTable SchoolAdminGetList(ref int CurrentPage, int RecordPerPage, out int TotalRecord, string SortColumn, string SortType)
        {
            DbParameter[] dbParam = new DbParameter[9];
            dbParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 10, base.ID);
            dbParam[1] = new DbParameter("@FirstName", DbParameter.DbType.VarChar, 50, base.FirstName);
            dbParam[2] = new DbParameter("@LastName", DbParameter.DbType.VarChar, 50, base.LastName);
            dbParam[3] = new DbParameter("@EmailID", DbParameter.DbType.VarChar, 100, base.EmailID);
            dbParam[4] = new DbParameter("@CurrentPage", DbParameter.DbType.Int, 10, (int)CurrentPage);
            dbParam[4].ParamDirection = ParameterDirection.InputOutput;
            dbParam[5] = new DbParameter("@RecordPerPage", DbParameter.DbType.Int, 10, RecordPerPage);
            dbParam[6] = new DbParameter("@TotalRecords", DbParameter.DbType.Int, 4, ParameterDirection.Output);
            if ((SortColumn != string.Empty) && (SortType != string.Empty))
            {
                dbParam[7] = new DbParameter("@SortOrd", DbParameter.DbType.VarChar, 20, SortType);
                dbParam[8] = new DbParameter("@SortColumn", DbParameter.DbType.VarChar, 20, SortColumn);
            }

            DataTable table = new DataTable();
            table = DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "SchoolAdminGetList", dbParam);
            CurrentPage = Convert.ToInt32(dbParam[4].Value);
            TotalRecord = Convert.ToInt32(dbParam[6].Value);
            return table;
        }
        public DataTable SchoolAdminGetByID()
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID),
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "SchoolAdminGetList", dbParam);
        }

        public void Operation(string Id, Common.DataBaseOperation ObjOperation, long UpdatedUserID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.VarChar, 2000, Id),
                new DbParameter("@OprType", DbParameter.DbType.Int, 10, Convert.ToInt16(ObjOperation)) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "TeachersOperation", dbParam);
        }


        public int Save(long CreatedBy, string Pages, string assignedPageAccess, string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID),
                new DbParameter("@FirstName", DbParameter.DbType.VarChar, 500, base.FirstName),
                new DbParameter("@LastName", DbParameter.DbType.VarChar, 500, base.LastName),
                new DbParameter("@EmailID", DbParameter.DbType.VarChar, 500, base.EmailID),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 500, base.Password),
                new DbParameter("@Phone", DbParameter.DbType.VarChar, 20, base.Phone),
                new DbParameter("@ImageName", DbParameter.DbType.VarChar, 1000, ImageName),
                new DbParameter("@CreatedBy", DbParameter.DbType.Int, 200, CreatedBy),
                new DbParameter("@HomeGroups", DbParameter.DbType.VarChar, 8000, Pages),
                new DbParameter("@Status", DbParameter.DbType.Int, 80, Status),
                new DbParameter("@Pages", DbParameter.DbType.VarChar, 8000, assignedPageAccess),
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, SchoolURL),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 40, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "TeachersSave", dbParam);
            return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
        }
        public DataTable PagesList(int ForTeacher)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ForTeacher", DbParameter.DbType.Int, 20,ForTeacher)
            };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "TeacherPagesList", dbParam);
        }
        public int SchoolAdminSave(long CreatedBy, string Pages, int Status, int IsAPIAccess)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID),
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 20, base.SchoolID),
                new DbParameter("@FirstName", DbParameter.DbType.VarChar, 500, base.FirstName),
                new DbParameter("@LastName", DbParameter.DbType.VarChar, 500, base.LastName),
                new DbParameter("@EmailID", DbParameter.DbType.VarChar, 500, base.EmailID),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 500, base.Password),
                new DbParameter("@Phone", DbParameter.DbType.VarChar, 20, base.Phone),
                new DbParameter("@ImageName", DbParameter.DbType.VarChar, 1000, ImageName),
                new DbParameter("@CreatedBy", DbParameter.DbType.Int, 200, CreatedBy),
                new DbParameter("@Pages", DbParameter.DbType.VarChar, 8000, Pages),
                new DbParameter("@Status", DbParameter.DbType.Int, 80, Status),
                new DbParameter("@IsAPIAccess", DbParameter.DbType.Int, 80, IsAPIAccess),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 40, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "SchoolAdminSave", dbParam);
            return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
        }

        public DataSet TeacherLogin(string GoogleID, string RequestedSchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@UserName", DbParameter.DbType.VarChar, 500, base.UserName),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 500, base.Password),
                new DbParameter("@GoogleID", DbParameter.DbType.VarChar, 500, GoogleID),
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 500, RequestedSchoolURL),
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "TeacherLogin", dbParam);
        }
        public DataTable TeacherPageRights()
        {
            DbParameter[] dbParam = new DbParameter[] { new DbParameter("@TeacherID", DbParameter.DbType.Int, 400, ID)
             };
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "TeacherPageRights", dbParam);
        }

        public int TeacherGoogleSignup(long CreatedBy, string Pages, string assignedPageAccess, string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID),
                new DbParameter("@FirstName", DbParameter.DbType.VarChar, 500, base.FirstName),
                new DbParameter("@LastName", DbParameter.DbType.VarChar, 500, base.LastName),
                new DbParameter("@EmailID", DbParameter.DbType.VarChar, 500, base.EmailID),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 500, base.Password),
                new DbParameter("@Phone", DbParameter.DbType.VarChar, 20, base.Phone),
                new DbParameter("@GoogleID", DbParameter.DbType.VarChar, 1000, GoogleID),
                new DbParameter("@CreatedBy", DbParameter.DbType.Int, 200, CreatedBy),
                new DbParameter("@Status", DbParameter.DbType.Int, 80, Status),
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, SchoolURL),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 40, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "TeacherGoogleSignup", dbParam);
            return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
        }


        public int StudentRatingSaveV2(long CurrentUnlockYear, long CurrentUnlockSemesterNo, long CreatedBy, long QuestionID, long StudentID, int RowNo, string SchoolURL, string Date, string Description, int Rating, string Attachment, long PreviousAssessmentID, string QUrls, long QuestionSubheadingSkillID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@CurrentUnlockYear", DbParameter.DbType.Int, 200,CurrentUnlockYear),
                new DbParameter("@CurrentUnlockSemesterNo", DbParameter.DbType.Int, 200,CurrentUnlockSemesterNo),
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, SchoolURL),
                new DbParameter("@QuestionID", DbParameter.DbType.Int, 200,QuestionID),
                new DbParameter("@StudentID", DbParameter.DbType.Int, 200, StudentID),
                new DbParameter("@RowNo", DbParameter.DbType.Int, 200, RowNo),
                new DbParameter("@Date", DbParameter.DbType.VarChar, 50, Date),
                new DbParameter("@Description", DbParameter.DbType.VarChar, 800000,Description),
                new DbParameter("@CreatedBy", DbParameter.DbType.Int, 20,CreatedBy),
                new DbParameter("@Rating", DbParameter.DbType.Int, 20,Rating),
                new DbParameter("@Attachment", DbParameter.DbType.VarChar, 8000000,Attachment),
                new DbParameter("@PreviousAssessmentID", DbParameter.DbType.Int, 200,PreviousAssessmentID),
                new DbParameter("@QuestionURLS", DbParameter.DbType.VarChar, 8000000,QUrls),
                new DbParameter("@QuestionSubheadingSkillID", DbParameter.DbType.Int, 200,QuestionSubheadingSkillID),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 40, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "StudentRatingSaveV2", dbParam);
            return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
        }


        public DataTable TeacherDetaiolsByID()
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID),
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "TeacherDetaiolsByID", dbParam);
        }
        public DataTable TeacherDashboard(int TeacherID, int SchoolID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@TeacherID", DbParameter.DbType.Int, 20, TeacherID),
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 20, SchoolID),
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "TeacherDashboard", dbParam);
        }
        public int StudentRatingSave(long CurrentUnlockYear, long CurrentUnlockSemesterNo, long CreatedBy, long QuestionID, long StudentID, int RowNo, string SchoolURL, string Date, string Description, int Rating, string Attachment, long PreviousAssessmentID, string QUrls)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@CurrentUnlockYear", DbParameter.DbType.Int, 200,CurrentUnlockYear),
                new DbParameter("@CurrentUnlockSemesterNo", DbParameter.DbType.Int, 200,CurrentUnlockSemesterNo),
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, SchoolURL),
                new DbParameter("@QuestionID", DbParameter.DbType.Int, 200,QuestionID),
                new DbParameter("@StudentID", DbParameter.DbType.Int, 200, StudentID),
                new DbParameter("@RowNo", DbParameter.DbType.Int, 200, RowNo),
                new DbParameter("@Date", DbParameter.DbType.VarChar, 50, Date),
                new DbParameter("@Description", DbParameter.DbType.VarChar, 800000,Description),
                new DbParameter("@CreatedBy", DbParameter.DbType.Int, 20,CreatedBy),
                new DbParameter("@Rating", DbParameter.DbType.Int, 20,Rating),
                new DbParameter("@Attachment", DbParameter.DbType.VarChar, 8000000,Attachment),
                new DbParameter("@PreviousAssessmentID", DbParameter.DbType.Int, 200,PreviousAssessmentID),
                new DbParameter("@QuestionURLS", DbParameter.DbType.VarChar, 8000000,QUrls),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 40, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "StudentRatingSave", dbParam);
            return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
        }


        public int StudentRatingSkillSave(long CurrentUnlockYear, long CurrentUnlockSemesterNo, long CreatedBy, long QuestionID, long StudentID, int RowNo, string SchoolURL, string Date, string Description, int Rating, string Attachment, long PreviousAssessmentID, string QUrls,long QuestionSubHeadingSkillId, int CLT_Version)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@CurrentUnlockYear", DbParameter.DbType.Int, 200,CurrentUnlockYear),
                new DbParameter("@CurrentUnlockSemesterNo", DbParameter.DbType.Int, 200,CurrentUnlockSemesterNo),
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, SchoolURL),
                new DbParameter("@QuestionID", DbParameter.DbType.Int, 200,QuestionID),
                new DbParameter("@StudentID", DbParameter.DbType.Int, 200, StudentID),

                new DbParameter("@QuestionSubHeadingSkillId", DbParameter.DbType.Int, 200, QuestionSubHeadingSkillId),
                new DbParameter("@CLT_Version", DbParameter.DbType.Int, 200, CLT_Version),



                new DbParameter("@RowNo", DbParameter.DbType.Int, 200, RowNo),
                new DbParameter("@Date", DbParameter.DbType.VarChar, 50, Date),
                new DbParameter("@Description", DbParameter.DbType.VarChar, 800000,Description),
                new DbParameter("@CreatedBy", DbParameter.DbType.Int, 20,CreatedBy),
                new DbParameter("@Rating", DbParameter.DbType.Int, 20,Rating),
                new DbParameter("@Attachment", DbParameter.DbType.VarChar, 8000000,Attachment),
                new DbParameter("@PreviousAssessmentID", DbParameter.DbType.Int, 200,PreviousAssessmentID),
                new DbParameter("@QuestionURLS", DbParameter.DbType.VarChar, 8000000,QUrls),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 40, ParameterDirection.Output)
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "StudentRatingSaveWithSkill", dbParam);
            return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
        }





        public void CompleteLevel(long CreatedBy, long CategoryID, long StudentID, string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, SchoolURL),
                new DbParameter("@CategoryID", DbParameter.DbType.Int, 200,CategoryID),
                new DbParameter("@StudentID", DbParameter.DbType.Int, 200, StudentID),
                new DbParameter("@CreatedBy", DbParameter.DbType.Int, 20,CreatedBy),                
            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "CompleteLevel", dbParam);
        }
        public DataSet StudentCurrentCategoryScoreAndPercentage(string SchoolURL, string CategoryURL, long StudentID, int CalledFromHomeGroup, long PreviousAssessmentID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, SchoolURL),
                new DbParameter("@CategoryURL", DbParameter.DbType.VarChar, 200,CategoryURL),
                new DbParameter("@StudentID", DbParameter.DbType.Int, 200, StudentID),
                new DbParameter("@CalledFromHomeGroup", DbParameter.DbType.Int, 200, CalledFromHomeGroup),
                new DbParameter("@PreviousAssessmentID", DbParameter.DbType.Int, 200, PreviousAssessmentID)
            };
          return  DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "StudentCurrentCategoryScoreAndPercentage", dbParam);
        }


        public DataSet StudentCurrentCategoryScoreAndPercentageV2(string SchoolURL, string CategoryURL, long StudentID, int CalledFromHomeGroup, long PreviousAssessmentID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, SchoolURL),
                new DbParameter("@CategoryURL", DbParameter.DbType.VarChar, 200,CategoryURL),
                new DbParameter("@StudentID", DbParameter.DbType.Int, 200, StudentID),
                new DbParameter("@CalledFromHomeGroup", DbParameter.DbType.Int, 200, CalledFromHomeGroup),
                new DbParameter("@PreviousAssessmentID", DbParameter.DbType.Int, 200, PreviousAssessmentID)
            };
            //return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "StudentCurrentCategoryScoreAndPercentage", dbParam);
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "StudentCurrentCategoryScoreAndPercentageV2", dbParam);
        }





        public DataTable getCohortList(long SchoolID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 20, SchoolID),
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataTable(CommandType.StoredProcedure, "getCohortListForDropDown", dbParam);
        }

        #region Reports
        public DataSet Report_ComparisionTool(string SchoolURL, int YEAR1, int YEAR2, int Semester1, int Semester2)
        {           
            DbParameter[] dbParam = new DbParameter[] {
                 new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, SchoolURL),
                new DbParameter("@YEAR1", DbParameter.DbType.Int, 20, YEAR1),
                new DbParameter("@YEAR2", DbParameter.DbType.Int, 20, YEAR2),
                new DbParameter("@Semester1", DbParameter.DbType.Int, 20, Semester1),
                new DbParameter("@Semester2", DbParameter.DbType.Int, 20, Semester2),
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "Report_ComparisionTool", dbParam);
        }

        public DataSet Report_SummaryBySection(string SchoolURL, int YEAR1, int YEAR2, int Semester1, int Semester2,long CohortID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                 new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, SchoolURL),
                new DbParameter("@YEAR1", DbParameter.DbType.Int, 20, YEAR1),
                new DbParameter("@YEAR2", DbParameter.DbType.Int, 20, YEAR2),
                new DbParameter("@Semester1", DbParameter.DbType.Int, 20, Semester1),
                new DbParameter("@Semester2", DbParameter.DbType.Int, 20, Semester2),
                new DbParameter("@CohortID", DbParameter.DbType.Int, 20, CohortID)
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "Report_SummaryBySection", dbParam);
        }

        public DataSet Report_SummaryWholeSchool(string SchoolURL, int YEAR1, int YEAR2, int Semester1, int Semester2)
        {
            DbParameter[] dbParam = new DbParameter[] {
                 new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, SchoolURL),
                new DbParameter("@YEAR1", DbParameter.DbType.Int, 20, YEAR1),
                new DbParameter("@YEAR2", DbParameter.DbType.Int, 20, YEAR2),
                new DbParameter("@Semester1", DbParameter.DbType.Int, 20, Semester1),
                new DbParameter("@Semester2", DbParameter.DbType.Int, 20, Semester2),
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "Report_SummaryWholeSchool", dbParam);
        }
        public DataSet Report_SummaryByLevel(string SchoolURL, int YEAR1, int YEAR2, int Semester1, int Semester2)
        {
            DbParameter[] dbParam = new DbParameter[] {
                 new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, SchoolURL),
                new DbParameter("@YEAR1", DbParameter.DbType.Int, 20, YEAR1),
                new DbParameter("@YEAR2", DbParameter.DbType.Int, 20, YEAR2),
                new DbParameter("@Semester1", DbParameter.DbType.Int, 20, Semester1),
                new DbParameter("@Semester2", DbParameter.DbType.Int, 20, Semester2),
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "Report_SummaryByLevel", dbParam);
        }
        #endregion

        public int Changepassword()
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@TeacherID", DbParameter.DbType.Int, 10, base.ID),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 50, base.OldPassword),
                new DbParameter("@NewPassword", DbParameter.DbType.VarChar, 50, base.Password),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 10, ParameterDirection.Output) };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "TeacherChangePassword", dbParam);
            return Convert.ToInt32(dbParam[3].Value);
        }
        public DataSet TeacherDetailsForgotPassword(string EmailAddress, string TempPassword, string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@EmailAddress", DbParameter.DbType.VarChar, 500, EmailAddress),
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, SchoolURL),
                new DbParameter("@TempPassword", DbParameter.DbType.VarChar, 50, TempPassword)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "TeacherDetailsForgotPassword", dbParam);            
        }
        public int AdminGoogleSignup(long CreatedBy, string Pages, string assignedPageAccess, string SchoolURL)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@ID", DbParameter.DbType.Int, 20, base.ID),
                new DbParameter("@FirstName", DbParameter.DbType.VarChar, 500, base.FirstName),
                new DbParameter("@LastName", DbParameter.DbType.VarChar, 500, base.LastName),
                new DbParameter("@EmailID", DbParameter.DbType.VarChar, 500, base.EmailID),
                new DbParameter("@Password", DbParameter.DbType.VarChar, 500, base.Password),
                new DbParameter("@Phone", DbParameter.DbType.VarChar, 20, base.Phone),
                new DbParameter("@GoogleID", DbParameter.DbType.VarChar, 1000, GoogleID),
                new DbParameter("@CreatedBy", DbParameter.DbType.Int, 200, CreatedBy),
                new DbParameter("@Status", DbParameter.DbType.Int, 80, Status),
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, SchoolURL),
                new DbParameter("@ReturnVal", DbParameter.DbType.Int, 40, ParameterDirection.Output)
            };

            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "AdminGoogleSignup", dbParam);
            return Convert.ToInt32(dbParam[dbParam.Length - 1].Value);
        }

        public DataSet Report_CohortBySection(string SchoolURL, int YEAR1, int YEAR2, int Semester1, int Semester2, long CohortID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                 new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, SchoolURL),
                new DbParameter("@YEAR1", DbParameter.DbType.Int, 20, YEAR1),
                new DbParameter("@YEAR2", DbParameter.DbType.Int, 20, YEAR2),
                new DbParameter("@Semester1", DbParameter.DbType.Int, 20, Semester1),
                new DbParameter("@Semester2", DbParameter.DbType.Int, 20, Semester2),
                new DbParameter("@CohortID", DbParameter.DbType.Int, 20, CohortID)
            };
            DataTable table = new DataTable();
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "Report_CohortBySection", dbParam);
        }

        public DataSet StudentCurrentCategoryScoreAndPercentageUpdateInReportingTable(string SchoolURL, long CategoryID, long StudentID, int CalledFromHomeGroup, long PreviousAssessmentID)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolURL", DbParameter.DbType.VarChar, 100, SchoolURL),
                new DbParameter("@CategoryID", DbParameter.DbType.Int, 200,CategoryID),
                new DbParameter("@StudentID", DbParameter.DbType.Int, 200, StudentID),
                new DbParameter("@CalledFromHomeGroup", DbParameter.DbType.Int, 200, CalledFromHomeGroup),
                new DbParameter("@PreviousAssessmentID", DbParameter.DbType.Int, 200, PreviousAssessmentID)
            };
            return DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "GetScoreAndPercentageUpdateInReportingTable", dbParam);
        }


        public void UpdateTableStudentReport(long SchoolID, long StudentID, long CategoryID, int Year, int Sem, string tempRating, string tempPoints, string tempLastlevel)
        {
            DbParameter[] dbParam = new DbParameter[] {
                new DbParameter("@SchoolID", DbParameter.DbType.Int, 100, SchoolID),
                new DbParameter("@StudentID", DbParameter.DbType.Int, 200,StudentID),
                new DbParameter("@CategoryID", DbParameter.DbType.Int, 200, CategoryID),
                new DbParameter("@Year", DbParameter.DbType.Int, 100,Year),
                new DbParameter("@Sem", DbParameter.DbType.Int, 100, Sem),
                new DbParameter("@tempRating", DbParameter.DbType.VarChar, 200,tempRating),
                new DbParameter("@tempPoints", DbParameter.DbType.VarChar, 200, tempPoints),
                new DbParameter("@tempLastlevel", DbParameter.DbType.VarChar, 200,tempLastlevel),

            };
            DbConnectionDAL.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateStudentReportTabledata", dbParam);
        }
    }
}



