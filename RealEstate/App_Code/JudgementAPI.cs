using BAL;
using ClosedXML.Excel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

/// <summary>
/// Summary description for JudgeemntAPI
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class JudgementAPI : System.Web.Services.WebService
{
    public JudgementAPI()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    #region Common API

    #region Login


    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]

    public void Login(string UserName, string Password)
    {
        Response objResponse = new Response();
        DataTable dt = new DataTable();
        APIBAL objAPIBAL = new APIBAL();
        Password = Utility.Security.EncryptDescrypt.EncryptString(Password);
        dt = objAPIBAL.APITeacherLogin(UserName, Password);
        if (dt.Rows.Count > 0)
        {
            string AuthToken = Convert.ToString(dt.Rows[0]["AuthToken"]);
            if (AuthToken != string.Empty)
            {
                objResponse.success = "true";
                objResponse.message = "Login successfull.";
                Users objUsers = new Users();
                objUsers.Token = AuthToken;
                objUsers.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                objUsers.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                objUsers.Email = Convert.ToString(dt.Rows[0]["EmailID"]);
                objResponse.Users = objUsers;
                string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                strResponseName = strResponseName.Replace("\"Users\"", "\"data\"");
                HttpContext.Current.Response.Write(strResponseName);
            }
            else
            {
                objResponse.success = "false";
                objResponse.message = "Invalid login details.";
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
            }
        }

        HttpContext.Current.Response.End();
    }

    #endregion

    #region StudentListWithResult

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void StudentListWithResult(string HomeGroups)
    {
        Response objResponse = new Response();

        string AuthToken = Convert.ToString(HttpContext.Current.Request.Headers["Token"]);

        if (!APIBAL.ValidateTeacherRequest(AuthToken))
        {
            Unauthorized();
        }
        else
        {
            DataTable dtMain = new DataTable();
            APIBAL objAPIBAL = new APIBAL();
            dtMain = objAPIBAL.APIStudentListWithResult(AuthToken, HomeGroups);
            if (dtMain.Rows.Count > 0)
            {

                DataView dv = new DataView(dtMain);
                dv.Sort = "SURNAME ASC";
                DataTable dtStudents = new DataTable();
                dtStudents = dv.ToTable(true, "StudentID", "STKEY", "SURNAME", "FIRST_NAME", "PREF_NAME", "HOME_GROUP", "STATUS");
                if (dtStudents.Rows.Count > 0)
                {
                    objResponse.success = "true";
                    objResponse.message = dtStudents.Rows.Count + " records found.";

                    StudentResult objStudentResult = new StudentResult();

                    objStudentResult.Students = new Students[dtStudents.Rows.Count];
                    for (int i = 0; i < dtStudents.Rows.Count; i++)
                    {
                        objStudentResult.Students[i] = new Students();
                        objStudentResult.Students[i].CasesID = Convert.ToString(dtStudents.Rows[i]["STKEY"]);
                        objStudentResult.Students[i].SurName = Convert.ToString(dtStudents.Rows[i]["SURNAME"]);
                        objStudentResult.Students[i].FirstName = Convert.ToString(dtStudents.Rows[i]["FIRST_NAME"]);
                        objStudentResult.Students[i].PrefName = Convert.ToString(dtStudents.Rows[i]["PREF_NAME"]);
                        objStudentResult.Students[i].HomeGroup = Convert.ToString(dtStudents.Rows[i]["HOME_GROUP"]);
                        objStudentResult.Students[i].Status = Convert.ToString(dtStudents.Rows[i]["STATUS"]);

                        //============= Category ===============================
                        #region Category
                        DataTable dtCategory = new DataTable();
                        DataView dvCategory = new DataView(dtMain);
                        dvCategory.RowFilter = "StudentID=" + Convert.ToInt32(dtStudents.Rows[i]["StudentID"]);
                        dvCategory.Sort = "SequenceNo ASC";
                        dtCategory = dvCategory.ToTable(true, "ParentCategoryName", "ParentID");


                        objStudentResult.Students[i].Category = new Category[dtCategory.Rows.Count];
                        for (int j = 0; j < dtCategory.Rows.Count; j++)
                        {
                            objStudentResult.Students[i].Category[j] = new Category();
                            objStudentResult.Students[i].Category[j].Name = Convert.ToString(dtCategory.Rows[j]["ParentCategoryName"]);


                            //============= Sub Category ===============================
                            #region Sub Category
                            DataTable dtSubCategory = new DataTable();
                            DataView dvSubCategory = new DataView(dtMain);
                            dvSubCategory.RowFilter = "StudentID=" + Convert.ToInt32(dtStudents.Rows[i]["StudentID"]) + " AND ParentID=" + Convert.ToInt32(dtCategory.Rows[j]["ParentID"]);
                            dvSubCategory.Sort = "LevelNo ASC";
                            dtSubCategory = dvSubCategory.ToTable(true, "CategoryName", "CategoryID", "Ratings", "Points");


                            objStudentResult.Students[i].Category[j].SubCategory = new SubCategory[dtSubCategory.Rows.Count];
                            for (int k = 0; k < dtSubCategory.Rows.Count; k++)
                            {
                                objStudentResult.Students[i].Category[j].SubCategory[k] = new SubCategory();
                                objStudentResult.Students[i].Category[j].SubCategory[k].Name = Convert.ToString(dtSubCategory.Rows[k]["CategoryName"]);

                                //Judgmenet
                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgement = new Judgement();
                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgement.Formatted = Convert.ToString(dtSubCategory.Rows[k]["Ratings"]);
                                if (Convert.ToString(dtSubCategory.Rows[k]["Points"]) != string.Empty)
                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgement.Raw = Convert.ToDecimal(dtSubCategory.Rows[k]["Points"]);

                            }
                            #endregion


                        }
                        #endregion




                    }

                    objResponse.StudentResult = objStudentResult;
                    string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    strResponseName = strResponseName.Replace("\"StudentResult\"", "\"data\"");
                    HttpContext.Current.Response.Write(strResponseName);
                }


            }
            else
            {
                NoRecordExists();
            }


            objResponse.success = "true";
            objResponse.StatusCode = "200";
            objResponse.message = "Validated";
        }


        HttpContext.Current.Response.End();
    }
    #endregion

    #region StudentListWithResult Cohorts

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void StudentListWithCohorts(string Cohorts)
    {
        Response objResponse = new Response();

        string AuthToken = Convert.ToString(HttpContext.Current.Request.Headers["Token"]);

        if (!APIBAL.ValidateTeacherRequest(AuthToken))
        {
            Unauthorized();
        }
        else
        {
            DataTable dtMain = new DataTable();
            APIBAL objAPIBAL = new APIBAL();
            dtMain = objAPIBAL.APIStudentListWithResultCohorts(AuthToken, Cohorts);
            if (dtMain.Rows.Count > 0)
            {

                DataView dv = new DataView(dtMain);
                dv.Sort = "SURNAME ASC";
                DataTable dtStudents = new DataTable();
                dtStudents = dv.ToTable(true, "StudentID", "STKEY", "SURNAME", "FIRST_NAME", "PREF_NAME", "HOME_GROUP", "STATUS");
                if (dtStudents.Rows.Count > 0)
                {
                    objResponse.success = "true";
                    objResponse.message = dtStudents.Rows.Count + " records found.";

                    StudentResult objStudentResult = new StudentResult();

                    objStudentResult.Students = new Students[dtStudents.Rows.Count];
                    for (int i = 0; i < dtStudents.Rows.Count; i++)
                    {
                        objStudentResult.Students[i] = new Students();
                        objStudentResult.Students[i].CasesID = Convert.ToString(dtStudents.Rows[i]["STKEY"]);
                        objStudentResult.Students[i].SurName = Convert.ToString(dtStudents.Rows[i]["SURNAME"]);
                        objStudentResult.Students[i].FirstName = Convert.ToString(dtStudents.Rows[i]["FIRST_NAME"]);
                        objStudentResult.Students[i].PrefName = Convert.ToString(dtStudents.Rows[i]["PREF_NAME"]);
                        objStudentResult.Students[i].HomeGroup = Convert.ToString(dtStudents.Rows[i]["HOME_GROUP"]);
                        objStudentResult.Students[i].Status = Convert.ToString(dtStudents.Rows[i]["STATUS"]);

                        //============= Category ===============================
                        #region Category
                        DataTable dtCategory = new DataTable();
                        DataView dvCategory = new DataView(dtMain);
                        dvCategory.RowFilter = "StudentID=" + Convert.ToInt32(dtStudents.Rows[i]["StudentID"]);
                        dvCategory.Sort = "SequenceNo ASC";
                        dtCategory = dvCategory.ToTable(true, "ParentCategoryName", "ParentID");


                        objStudentResult.Students[i].Category = new Category[dtCategory.Rows.Count];
                        for (int j = 0; j < dtCategory.Rows.Count; j++)
                        {
                            objStudentResult.Students[i].Category[j] = new Category();
                            objStudentResult.Students[i].Category[j].Name = Convert.ToString(dtCategory.Rows[j]["ParentCategoryName"]);


                            //============= Sub Category ===============================
                            #region Sub Category
                            DataTable dtSubCategory = new DataTable();
                            DataView dvSubCategory = new DataView(dtMain);
                            dvSubCategory.RowFilter = "StudentID=" + Convert.ToInt32(dtStudents.Rows[i]["StudentID"]) + " AND ParentID=" + Convert.ToInt32(dtCategory.Rows[j]["ParentID"]);
                            dvSubCategory.Sort = "LevelNo ASC";
                            dtSubCategory = dvSubCategory.ToTable(true, "CategoryName", "CategoryID", "Ratings", "Points");


                            objStudentResult.Students[i].Category[j].SubCategory = new SubCategory[dtSubCategory.Rows.Count];
                            for (int k = 0; k < dtSubCategory.Rows.Count; k++)
                            {
                                objStudentResult.Students[i].Category[j].SubCategory[k] = new SubCategory();
                                objStudentResult.Students[i].Category[j].SubCategory[k].Name = Convert.ToString(dtSubCategory.Rows[k]["CategoryName"]);

                                //Judgmenet
                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgement = new Judgement();
                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgement.Formatted = Convert.ToString(dtSubCategory.Rows[k]["Ratings"]);
                                if (Convert.ToString(dtSubCategory.Rows[k]["Points"]) != string.Empty)
                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgement.Raw = Convert.ToDecimal(dtSubCategory.Rows[k]["Points"]);

                            }
                            #endregion


                        }
                        #endregion




                    }

                    objResponse.StudentResult = objStudentResult;
                    string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    strResponseName = strResponseName.Replace("\"StudentResult\"", "\"data\"");
                    HttpContext.Current.Response.Write(strResponseName);
                }


            }
            else
            {
                NoRecordExists();
            }


            objResponse.success = "true";
            objResponse.StatusCode = "200";
            objResponse.message = "Validated";
        }


        HttpContext.Current.Response.End();
    }
    #endregion

    #region Student Result With Year Semester

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void StudentResultWithYearSemester(string HomeGroups)
    {
        Responses objResponse = new Responses();

        string AuthToken = Convert.ToString(HttpContext.Current.Request.Headers["Token"]);

        if (!APIBAL.ValidateTeacherRequest(AuthToken))
        {
            Unauthorized();
        }
        else
        {
            DataTable dtMain = new DataTable();
            APIBAL objAPIBAL = new APIBAL();
            dtMain = objAPIBAL.APIStudentListWithResult(AuthToken, HomeGroups);
            if (dtMain.Rows.Count > 0)
            {
                // Year Datatable
                DataView dvYear = new DataView(dtMain);
                DataTable dtYears = new DataTable();
                dtYears = dvYear.ToTable(true, "Year");
                //

                DataView dv = new DataView(dtMain);
                dv.Sort = "SURNAME ASC";
                DataTable dtStudents = new DataTable();
                dtStudents = dv.ToTable(true, "StudentID", "STKEY", "SURNAME", "FIRST_NAME", "PREF_NAME", "HOME_GROUP", "STATUS");
                if (dtStudents.Rows.Count > 0)
                {
                    objResponse.success = "true";
                    objResponse.message = dtStudents.Rows.Count + " records found.";

                    StudentResults objStudentResult = new StudentResults();

                    objStudentResult.Students = new StudentList[dtStudents.Rows.Count];
                    for (int i = 0; i < dtStudents.Rows.Count; i++)
                    {
                        objStudentResult.Students[i] = new StudentList();
                        objStudentResult.Students[i].CasesID = Convert.ToString(dtStudents.Rows[i]["STKEY"]);
                        objStudentResult.Students[i].SurName = Convert.ToString(dtStudents.Rows[i]["SURNAME"]);
                        objStudentResult.Students[i].FirstName = Convert.ToString(dtStudents.Rows[i]["FIRST_NAME"]);
                        objStudentResult.Students[i].PrefName = Convert.ToString(dtStudents.Rows[i]["PREF_NAME"]);
                        objStudentResult.Students[i].HomeGroup = Convert.ToString(dtStudents.Rows[i]["HOME_GROUP"]);
                        objStudentResult.Students[i].Status = Convert.ToString(dtStudents.Rows[i]["STATUS"]);

                        //============= Category ===============================
                        #region Category
                        DataTable dtCategory = new DataTable();
                        DataView dvCategory = new DataView(dtMain);
                        dvCategory.RowFilter = "StudentID=" + Convert.ToInt32(dtStudents.Rows[i]["StudentID"]);
                        dvCategory.Sort = "SequenceNo ASC";
                        dtCategory = dvCategory.ToTable(true, "ParentCategoryName", "ParentID");


                        objStudentResult.Students[i].Category = new Categories[dtCategory.Rows.Count];
                        for (int j = 0; j < dtCategory.Rows.Count; j++)
                        {
                            objStudentResult.Students[i].Category[j] = new Categories();
                            objStudentResult.Students[i].Category[j].Name = Convert.ToString(dtCategory.Rows[j]["ParentCategoryName"]);


                            //============= Sub Category ===============================
                            #region Sub Category
                            DataTable dtSubCategory = new DataTable();
                            DataView dvSubCategory = new DataView(dtMain);
                            dvSubCategory.RowFilter = "StudentID=" + Convert.ToInt32(dtStudents.Rows[i]["StudentID"]) + " AND ParentID=" + Convert.ToInt32(dtCategory.Rows[j]["ParentID"]);
                            dvSubCategory.Sort = "LevelNo ASC";
                            dtSubCategory = dvSubCategory.ToTable(true, "CategoryName", "CategoryID", "Ratings", "Points", "Year", "Semester");


                            objStudentResult.Students[i].Category[j].SubCategory = new SubCategories[dtSubCategory.Rows.Count];
                            for (int k = 0; k < dtSubCategory.Rows.Count; k++)
                            {
                                objStudentResult.Students[i].Category[j].SubCategory[k] = new SubCategories();
                                objStudentResult.Students[i].Category[j].SubCategory[k].Name = Convert.ToString(dtSubCategory.Rows[k]["CategoryName"]);

                                //Judgements
                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements = new Judgements();
                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels = new Levels[dtYears.Rows.Count];

                                for (int l = 0; l < dtYears.Rows.Count; l++)
                                {
                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l] = new Levels();
                                    int Year = 0;
                                    decimal Sem1Result = -1;
                                    decimal Sem2Result = -1;
                                    Year = Convert.ToInt32(dtYears.Rows[l]["Year"]);
                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Year = Year;

                                    DataTable dtResult = new DataTable();

                                    //Semester 1
                                    DataView dvYearResult = new DataView(dtSubCategory);
                                    dvYearResult.RowFilter = "Year=" + Year + " AND Semester=1";
                                    dtResult = dvYearResult.ToTable();
                                    if (dtResult.Rows.Count > 0)
                                    {
                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].S1Formatted = Convert.ToString(dtResult.Rows[0]["Ratings"]);
                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].S1Raw = Convert.ToDecimal(dtResult.Rows[0]["Points"]);

                                        Sem1Result = Convert.ToDecimal(dtResult.Rows[0]["Points"]);
                                    }
                                    else
                                    {
                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].S1Formatted = null;
                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].S1Raw = null;

                                    }

                                    //Semester 2
                                    dvYearResult = new DataView(dtSubCategory);
                                    dvYearResult.RowFilter = "Year=" + Year + " AND Semester=2";
                                    dtResult = dvYearResult.ToTable();
                                    if (dtResult.Rows.Count > 0)
                                    {
                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].S2Formatted = Convert.ToString(dtResult.Rows[0]["Ratings"]);
                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].S2Raw = Convert.ToDecimal(dtResult.Rows[0]["Points"]);
                                        Sem2Result = Convert.ToDecimal(dtResult.Rows[0]["Points"]);
                                    }
                                    else
                                    {
                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].S2Formatted = null;
                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].S2Raw = null;

                                    }

                                    if (Sem1Result != -1 && Sem2Result != -1)
                                    {
                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Change = Sem2Result - Sem1Result;
                                    }
                                    else
                                    {
                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Change = null;
                                    }
                                }
                            }
                            #endregion

                        }
                        #endregion

                    }

                    objResponse.StudentResult = objStudentResult;
                    //string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Include });
                    strResponseName = strResponseName.Replace("\"StudentResult\"", "\"data\"");
                    HttpContext.Current.Response.Write(strResponseName);
                }


            }
            else
            {
                NoRecordExists();
            }


            objResponse.success = "true";
            objResponse.StatusCode = "200";
            objResponse.message = "Validated";
        }


        HttpContext.Current.Response.End();
    }
    #endregion


    #region Student Result With Year Semester

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void StudentDetailResultWithYearSemester(string HomeGroups, int Year, int Semester)
    {
        ResponsesDetail objResponse = new ResponsesDetail();

        string AuthToken = Convert.ToString(HttpContext.Current.Request.Headers["Token"]);

        if (!APIBAL.ValidateTeacherRequest(AuthToken))
        {
            Unauthorized();
        }
        else
        {
            DataTable dtMain = new DataTable();
            APIBAL objAPIBAL = new APIBAL();
            dtMain = objAPIBAL.APIStudentListDetailResult(AuthToken, HomeGroups, Year, Semester);
            if (dtMain.Rows.Count > 0)
            {
                // Year Datatable
                DataView dvYear = new DataView(dtMain);
                DataTable dtYears = new DataTable();
                dtYears = dvYear.ToTable(true, "Year");
                //

                DataView dv = new DataView(dtMain);
                dv.Sort = "SURNAME ASC";
                DataTable dtStudents = new DataTable();
                dtStudents = dv.ToTable(true, "StudentID", "STKEY", "SURNAME", "FIRST_NAME", "PREF_NAME", "HOME_GROUP", "STATUS");
                if (dtStudents.Rows.Count > 0)
                {
                    objResponse.success = "true";
                    objResponse.message = dtStudents.Rows.Count + " records found.";

                    StudentResultDetail objStudentResult = new StudentResultDetail();

                    objStudentResult.Students = new StudentListDetail[dtStudents.Rows.Count];
                    for (int i = 0; i < dtStudents.Rows.Count; i++)
                    {
                        objStudentResult.Students[i] = new StudentListDetail();
                        objStudentResult.Students[i].CasesID = Convert.ToString(dtStudents.Rows[i]["STKEY"]);
                        objStudentResult.Students[i].SurName = Convert.ToString(dtStudents.Rows[i]["SURNAME"]);
                        objStudentResult.Students[i].FirstName = Convert.ToString(dtStudents.Rows[i]["FIRST_NAME"]);
                        objStudentResult.Students[i].PrefName = Convert.ToString(dtStudents.Rows[i]["PREF_NAME"]);
                        objStudentResult.Students[i].HomeGroup = Convert.ToString(dtStudents.Rows[i]["HOME_GROUP"]);
                        objStudentResult.Students[i].Status = Convert.ToString(dtStudents.Rows[i]["STATUS"]);

                        //============= Category ===============================
                        #region Category
                        DataTable dtCategory = new DataTable();
                        DataView dvCategory = new DataView(dtMain);
                        dvCategory.RowFilter = "StudentID=" + Convert.ToInt32(dtStudents.Rows[i]["StudentID"]);
                        dvCategory.Sort = "SequenceNo ASC";
                        dtCategory = dvCategory.ToTable(true, "ParentCategoryName", "ParentID");


                        objStudentResult.Students[i].Category = new CategoriesDetail[dtCategory.Rows.Count];
                        for (int j = 0; j < dtCategory.Rows.Count; j++)
                        {
                            objStudentResult.Students[i].Category[j] = new CategoriesDetail();
                            objStudentResult.Students[i].Category[j].Name = Convert.ToString(dtCategory.Rows[j]["ParentCategoryName"]);


                            //============= Sub Category ===============================
                            #region Sub Category
                            DataTable dtSubCategory = new DataTable();
                            DataView dvSubCategory = new DataView(dtMain);
                            dvSubCategory.RowFilter = "StudentID=" + Convert.ToInt32(dtStudents.Rows[i]["StudentID"]) + " AND ParentID=" + Convert.ToInt32(dtCategory.Rows[j]["ParentID"]);
                            dvSubCategory.Sort = "LevelNo ASC";
                            dtSubCategory = dvSubCategory.ToTable(true, "CategoryName", "CategoryID", "Ratings", "Points", "Year", "Semester");


                            objStudentResult.Students[i].Category[j].SubCategory = new SubCategoriesDetail[dtSubCategory.Rows.Count];
                            for (int k = 0; k < dtSubCategory.Rows.Count; k++)
                            {
                                objStudentResult.Students[i].Category[j].SubCategory[k] = new SubCategoriesDetail();
                                objStudentResult.Students[i].Category[j].SubCategory[k].Name = Convert.ToString(dtSubCategory.Rows[k]["CategoryName"]);

                                //Judgements
                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements = new JudgementsDetail();
                                DataTable dtResult = new DataTable();
                                DataView dvYearResult = new DataView(dtSubCategory);
                                dvYearResult.RowFilter = "Year=" + Year + " AND Semester=" + Semester;
                                dtResult = dvYearResult.ToTable();
                                if (dtResult.Rows.Count > 0)
                                {
                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Formatted = Convert.ToString(dtResult.Rows[0]["Ratings"]);
                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Raw = Convert.ToDecimal(dtResult.Rows[0]["Points"]);
                                }
                                else
                                {
                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Formatted = null;
                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Raw = null;
                                }

                                DataSet dsAllQuestions = new DataSet();
                                DataTable dtLevelsWithAllQuestions = new DataTable();
                                dsAllQuestions = objAPIBAL.APIStudentListDetailResultQuestions(Convert.ToInt32(dtStudents.Rows[i]["StudentID"]), Convert.ToInt32(dtSubCategory.Rows[k]["CategoryID"]), Year, Semester);
                                dtLevelsWithAllQuestions = dsAllQuestions.Tables[0];

                                DataView dvLevels = new DataView(dtLevelsWithAllQuestions);
                                DataTable dtLevels = new DataTable();
                                dvLevels.Sort = "LevelNo ASC";
                                dtLevels = dvLevels.ToTable(true, "LevelName");
                                string WebsiteURL = Convert.ToString(Utility.Config.WebSiteUrl);


                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels = new LevelsDetail[dtLevels.Rows.Count];
                                for (int l = 0; l < dtLevels.Rows.Count; l++)
                                {
                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l] = new LevelsDetail();
                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Name = Convert.ToString(dtLevels.Rows[l]["LevelName"]);


                                    DataTable dtQuestions = new DataTable();
                                    DataView dvQuestions = new DataView(dtLevelsWithAllQuestions);
                                    dvQuestions.RowFilter = "LevelName='" + Convert.ToString(objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Name) + "'";

                                    dtQuestions = dvQuestions.ToTable(true, "QuestionID", "Question");

                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions = new Questions[dtQuestions.Rows.Count];
                                    for (int m = 0; m < dtQuestions.Rows.Count; m++)
                                    {
                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m] = new Questions();
                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Title = Convert.ToString(dtQuestions.Rows[m]["Question"]);


                                        // Demonstration 1

                                        DataTable dtQuestionDemonstrations = new DataTable();
                                        DataView dvQuestionDemonstrations = new DataView(dtLevelsWithAllQuestions);
                                        dvQuestionDemonstrations.RowFilter = "QuestionID=" + Convert.ToString(dtQuestions.Rows[m]["QuestionID"]) + " AND RowNo=1";
                                        dtQuestionDemonstrations = dvQuestionDemonstrations.ToTable(true, "Date", "Description", "StudentRatingID");

                                        if (dtQuestionDemonstrations.Rows.Count > 0)
                                        {
                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1 = new Demonstration1();
                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Date = Convert.ToDateTime(dtQuestionDemonstrations.Rows[0]["Date"]).ToString("dd/MMM/yyyy");
                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.SourceOfEvidence = Convert.ToString(dtQuestionDemonstrations.Rows[0]["Description"]);
                                            long StudentRatingID = Convert.ToInt64(dtQuestionDemonstrations.Rows[0]["StudentRatingID"]);

                                            DataTable dtAttachment = new DataTable();
                                            DataView dvAttachment = new DataView(dsAllQuestions.Tables[1]);
                                            dvAttachment.RowFilter = "StudentRatingID=" + StudentRatingID;
                                            dtAttachment = dvAttachment.ToTable();

                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Attachment = new Attachment[dtAttachment.Rows.Count];
                                            for (int n = 0; n < dtAttachment.Rows.Count; n++)
                                            {
                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Attachment[n] = new Attachment();

                                                if (Convert.ToString(dtAttachment.Rows[n]["AttachmentType"]) == "A")
                                                {
                                                    // objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Attachment[n].URL = Utility.Config.WebSiteUrl + "source/Attachments/" + Convert.ToString(dtAttachment.Rows[n]["URL"]);
                                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Attachment[n].URL = WebsiteURL + "attachment.aspx?token=" + AuthToken + "&type=a&file=" + Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dtAttachment.Rows[n]["AttachementID"])));
                                                }
                                                else
                                                {
                                                    // objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Attachment[n].URL = Convert.ToString(dtAttachment.Rows[n]["URL"]);
                                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Attachment[n].URL = WebsiteURL + "attachment.aspx?token=" + AuthToken + "&type=u&file=" + Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dtAttachment.Rows[n]["AttachementID"])));

                                                }
                                            }


                                        }

                                        // Demonstration 2

                                        dtQuestionDemonstrations = new DataTable();
                                        dvQuestionDemonstrations = new DataView(dtLevelsWithAllQuestions);
                                        dvQuestionDemonstrations.RowFilter = "QuestionID=" + Convert.ToString(dtQuestions.Rows[m]["QuestionID"]) + " AND RowNo=2";
                                        dtQuestionDemonstrations = dvQuestionDemonstrations.ToTable(true, "Date", "Description", "StudentRatingID");

                                        if (dtQuestionDemonstrations.Rows.Count > 0)
                                        {
                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration2 = new Demonstration2();
                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration2.Date = Convert.ToDateTime(dtQuestionDemonstrations.Rows[0]["Date"]).ToString("dd/MMM/yyyy");
                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration2.SourceOfEvidence = Convert.ToString(dtQuestionDemonstrations.Rows[0]["Description"]);

                                            long StudentRatingID = Convert.ToInt64(dtQuestionDemonstrations.Rows[0]["StudentRatingID"]);

                                            DataTable dtAttachment = new DataTable();
                                            DataView dvAttachment = new DataView(dsAllQuestions.Tables[1]);
                                            dvAttachment.RowFilter = "StudentRatingID=" + StudentRatingID;
                                            dtAttachment = dvAttachment.ToTable();

                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration2.Attachment = new Attachment[dtAttachment.Rows.Count];
                                            for (int n = 0; n < dtAttachment.Rows.Count; n++)
                                            {
                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration2.Attachment[n] = new Attachment();
                                                if (Convert.ToString(dtAttachment.Rows[n]["AttachmentType"]) == "A")
                                                {
                                                    // objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration2.Attachment[n].URL = Utility.Config.WebSiteUrl + "source/Attachments/" + Convert.ToString(dtAttachment.Rows[n]["URL"]);
                                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration2.Attachment[n].URL = WebsiteURL + "attachment.aspx?token=" + AuthToken + "&type=a&file=" + Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dtAttachment.Rows[n]["AttachementID"])));
                                                }
                                                else
                                                {
                                                    //objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration2.Attachment[n].URL = Convert.ToString(dtAttachment.Rows[n]["URL"]);
                                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration2.Attachment[n].URL = WebsiteURL + "attachment.aspx?token=" + AuthToken + "&type=u&file=" + Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dtAttachment.Rows[n]["AttachementID"])));
                                                }
                                            }
                                        }

                                        // Demonstration 3

                                        dtQuestionDemonstrations = new DataTable();
                                        dvQuestionDemonstrations = new DataView(dtLevelsWithAllQuestions);
                                        dvQuestionDemonstrations.RowFilter = "QuestionID=" + Convert.ToString(dtQuestions.Rows[m]["QuestionID"]) + " AND RowNo=3";
                                        dtQuestionDemonstrations = dvQuestionDemonstrations.ToTable(true, "Date", "Description", "StudentRatingID");

                                        if (dtQuestionDemonstrations.Rows.Count > 0)
                                        {
                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration3 = new Demonstration3();
                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration3.Date = Convert.ToDateTime(dtQuestionDemonstrations.Rows[0]["Date"]).ToString("dd/MMM/yyyy");
                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration3.SourceOfEvidence = Convert.ToString(dtQuestionDemonstrations.Rows[0]["Description"]);

                                            long StudentRatingID = Convert.ToInt64(dtQuestionDemonstrations.Rows[0]["StudentRatingID"]);

                                            DataTable dtAttachment = new DataTable();
                                            DataView dvAttachment = new DataView(dsAllQuestions.Tables[1]);
                                            dvAttachment.RowFilter = "StudentRatingID=" + StudentRatingID;
                                            dtAttachment = dvAttachment.ToTable();

                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration3.Attachment = new Attachment[dtAttachment.Rows.Count];
                                            for (int n = 0; n < dtAttachment.Rows.Count; n++)
                                            {
                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration3.Attachment[n] = new Attachment();
                                                if (Convert.ToString(dtAttachment.Rows[n]["AttachmentType"]) == "A")
                                                {
                                                    // objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration3.Attachment[n].URL = Utility.Config.WebSiteUrl + "source/Attachments/" + Convert.ToString(dtAttachment.Rows[n]["URL"]);
                                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration3.Attachment[n].URL = WebsiteURL + "attachment.aspx?token=" + AuthToken + "&type=a&file=" + Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dtAttachment.Rows[n]["AttachementID"])));
                                                }
                                                else
                                                {
                                                    //objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration3.Attachment[n].URL = Convert.ToString(dtAttachment.Rows[n]["URL"]);
                                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration3.Attachment[n].URL = WebsiteURL + "attachment.aspx?token=" + AuthToken + "&type=u&file=" + Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dtAttachment.Rows[n]["AttachementID"])));
                                                }
                                            }

                                        }



                                    }
                                }

                            }
                            #endregion

                        }
                        #endregion

                    }

                    objResponse.StudentResult = objStudentResult;
                    //string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Include });
                    strResponseName = strResponseName.Replace("\"StudentResult\"", "\"data\"");
                    HttpContext.Current.Response.Write(strResponseName);
                }


            }
            else
            {
                NoRecordExists();
            }


            objResponse.success = "true";
            objResponse.StatusCode = "200";
            objResponse.message = "Validated";
        }


        HttpContext.Current.Response.End();
    }
    #endregion

    #region Student Result With Year Semester

    [WebMethod]
    [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json, XmlSerializeString = false)]
    public void StudentDetailResultWithYearSemesterSkill(string HomeGroups, int Year, int Semester)
    {
        ResponsesDetailSkill objResponse = new ResponsesDetailSkill();

        string AuthToken = Convert.ToString(HttpContext.Current.Request.Headers["Token"]);

        if (!APIBAL.ValidateTeacherRequest(AuthToken))
        {
            Unauthorized();
        }
        else
        {
            DataTable dtMain = new DataTable();
            APIBAL objAPIBAL = new APIBAL();
            dtMain = objAPIBAL.APIStudentListDetailResultWithSkill(AuthToken, HomeGroups, Year, Semester);
            if (dtMain.Rows.Count > 0)
            {
                // Year Datatable
                DataView dvYear = new DataView(dtMain);
                DataTable dtYears = new DataTable();
                dtYears = dvYear.ToTable(true, "Year");
                //

                DataView dv = new DataView(dtMain);
                dv.Sort = "SURNAME ASC";
                DataTable dtStudents = new DataTable();
                dtStudents = dv.ToTable(true, "StudentID", "STKEY", "SURNAME", "FIRST_NAME", "PREF_NAME", "HOME_GROUP", "STATUS");
                if (dtStudents.Rows.Count > 0)
                {
                    objResponse.success = "true";
                    objResponse.message = dtStudents.Rows.Count + " records found.";

                    StudentResultDetailSkill objStudentResult = new StudentResultDetailSkill();

                    objStudentResult.Students = new StudentListDetailSkill[dtStudents.Rows.Count];
                    for (int i = 0; i < dtStudents.Rows.Count; i++)
                    {
                        objStudentResult.Students[i] = new StudentListDetailSkill();
                        objStudentResult.Students[i].CasesID = Convert.ToString(dtStudents.Rows[i]["STKEY"]);
                        objStudentResult.Students[i].SurName = Convert.ToString(dtStudents.Rows[i]["SURNAME"]);
                        objStudentResult.Students[i].FirstName = Convert.ToString(dtStudents.Rows[i]["FIRST_NAME"]);
                        objStudentResult.Students[i].PrefName = Convert.ToString(dtStudents.Rows[i]["PREF_NAME"]);
                        objStudentResult.Students[i].HomeGroup = Convert.ToString(dtStudents.Rows[i]["HOME_GROUP"]);
                        objStudentResult.Students[i].Status = Convert.ToString(dtStudents.Rows[i]["STATUS"]);

                        //============= Category ===============================
                        #region Category
                        DataTable dtCategory = new DataTable();
                        DataView dvCategory = new DataView(dtMain);
                        dvCategory.RowFilter = "StudentID=" + Convert.ToInt32(dtStudents.Rows[i]["StudentID"]);
                        dvCategory.Sort = "SequenceNo ASC";
                        dtCategory = dvCategory.ToTable(true, "ParentCategoryName", "ParentID");


                        objStudentResult.Students[i].Category = new CategoriesDetailSkill[dtCategory.Rows.Count];
                        for (int j = 0; j < dtCategory.Rows.Count; j++)
                        {
                            objStudentResult.Students[i].Category[j] = new CategoriesDetailSkill();
                            objStudentResult.Students[i].Category[j].Name = Convert.ToString(dtCategory.Rows[j]["ParentCategoryName"]);


                            //============= Sub Category ===============================
                            #region Sub Category
                            DataTable dtSubCategory = new DataTable();
                            DataView dvSubCategory = new DataView(dtMain);
                            dvSubCategory.RowFilter = "StudentID=" + Convert.ToInt32(dtStudents.Rows[i]["StudentID"]) + " AND ParentID=" + Convert.ToInt32(dtCategory.Rows[j]["ParentID"]);
                            dvSubCategory.Sort = "LevelNo ASC";
                            dtSubCategory = dvSubCategory.ToTable(true, "CategoryName", "CategoryID", "Ratings", "Points", "Year", "Semester", "CLT_Version");


                            objStudentResult.Students[i].Category[j].SubCategory = new SubCategoriesDetailSkill[dtSubCategory.Rows.Count];
                            for (int k = 0; k < dtSubCategory.Rows.Count; k++)
                            {
                                objStudentResult.Students[i].Category[j].SubCategory[k] = new SubCategoriesDetailSkill();
                                objStudentResult.Students[i].Category[j].SubCategory[k].Name = Convert.ToString(dtSubCategory.Rows[k]["CategoryName"]);

                                //Judgements
                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements = new JudgementsDetailSkill();
                                DataTable dtResult = new DataTable();
                                DataView dvYearResult = new DataView(dtSubCategory);
                                dvYearResult.RowFilter = "Year=" + Year + " AND Semester=" + Semester;
                                dtResult = dvYearResult.ToTable();
                                if (dtResult.Rows.Count > 0)
                                {
                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Formatted = Convert.ToString(dtResult.Rows[0]["Ratings"]);
                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Raw = Convert.ToDecimal(dtResult.Rows[0]["Points"]);
                                }
                                else
                                {
                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Formatted = null;
                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Raw = null;
                                }

                                DataSet dsAllQuestions = new DataSet();
                                DataTable dtLevelsWithAllQuestions = new DataTable();
                                string WebsiteURL = Convert.ToString(Utility.Config.WebSiteUrl);

                                if (Convert.ToInt32(dtSubCategory.Rows[k]["CLT_Version"]) == 2)
                                {
                                    dsAllQuestions = objAPIBAL.APIStudentListDetailResultQuestionsWithSkill(Convert.ToInt32(dtStudents.Rows[i]["StudentID"]), Convert.ToInt32(dtSubCategory.Rows[k]["CategoryID"]), Year, Semester);
                                    dtLevelsWithAllQuestions = dsAllQuestions.Tables[0];

                                    DataView dvLevels = new DataView(dtLevelsWithAllQuestions);
                                    DataTable dtLevels = new DataTable();
                                    dvLevels.Sort = "LevelNo ASC";
                                    dtLevels = dvLevels.ToTable(true, "LevelName");

                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels = new LevelsDetailSkill[dtLevels.Rows.Count];
                                    for (int l = 0; l < dtLevels.Rows.Count; l++)
                                    {                                        
                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l] = new LevelsDetailSkill();
                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Name = Convert.ToString(dtLevels.Rows[l]["LevelName"]);


                                        DataTable dtQuestions = new DataTable();
                                        DataView dvQuestions = new DataView(dtLevelsWithAllQuestions);
                                        dvQuestions.RowFilter = "LevelName='" + Convert.ToString(objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Name) + "'";

                                        dtQuestions = dvQuestions.ToTable(true, "QuestionID", "Question");

                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions = new QuestionsSkill[dtQuestions.Rows.Count];
                                        for (int m = 0; m < dtQuestions.Rows.Count; m++)
                                        {
                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m] = new QuestionsSkill();
                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Title = Convert.ToString(dtQuestions.Rows[m]["Question"]);


                                            // Bind Subheading  

                                            DataTable dtSubheadings = new DataTable();
                                            DataView dvSubheadings = new DataView(dtLevelsWithAllQuestions);
                                            dvSubheadings.RowFilter = "QuestionID=" + Convert.ToString(dtQuestions.Rows[m]["QuestionID"]);

                                            dtSubheadings = dvSubheadings.ToTable(true, "SubHeadingID", "SubHeadingTitle");

                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Subheadings = new QuestionsSubheadings[dtSubheadings.Rows.Count];

                                            for (int n = 0; n< dtSubheadings.Rows.Count; n++)
                                            {
                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Subheadings[n] = new QuestionsSubheadings();
                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Subheadings[n].Title = Convert.ToString(dtSubheadings.Rows[n]["SubHeadingTitle"]);

                                                // Bind Skills

                                                DataTable dtSkills = new DataTable();
                                                DataView dvSkills = new DataView(dtLevelsWithAllQuestions);
                                                dvSkills.RowFilter = "SubHeadingID=" + Convert.ToInt32(dtSubheadings.Rows[n]["SubHeadingID"]);

                                                dtSkills = dvSkills.ToTable(true, "SkillID", "SkillTitle");

                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Subheadings[n].Skills = new Skill[dtSkills.Rows.Count];
                                                for (int p = 0; p < dtSkills.Rows.Count; p++)
                                                {
                                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Subheadings[n].Skills[p] = new Skill();
                                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Subheadings[n].Skills[p].Title = Convert.ToString(dtSkills.Rows[p]["SkillTitle"]);

                                                    //Demonstration

                                                    DataTable dtQuestionDemonstrations = new DataTable();
                                                    DataView dvQuestionDemonstrations = new DataView(dtLevelsWithAllQuestions);
                                                    dvQuestionDemonstrations.RowFilter = "SkillID=" + Convert.ToString(dtSkills.Rows[p]["SkillID"]);
                                                    dtQuestionDemonstrations = dvQuestionDemonstrations.ToTable(true, "Date", "Description", "StudentRatingID");

                                                    if (dtQuestionDemonstrations.Rows.Count > 0)
                                                    {
                                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Subheadings[n].Skills[p].Demonstration = new Demonstration();
                                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Subheadings[n].Skills[p].Demonstration.Date = Convert.ToDateTime(dtQuestionDemonstrations.Rows[0]["Date"]).ToString("dd/MMM/yyyy");
                                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Subheadings[n].Skills[p].Demonstration.SourceOfEvidence = Convert.ToString(dtQuestionDemonstrations.Rows[0]["Description"]);
                                                        long StudentRatingID = Convert.ToInt64(dtQuestionDemonstrations.Rows[0]["StudentRatingID"]);

                                                        DataTable dtAttachment = new DataTable();
                                                        DataView dvAttachment = new DataView(dsAllQuestions.Tables[1]);
                                                        dvAttachment.RowFilter = "StudentRatingID=" + StudentRatingID;
                                                        dtAttachment = dvAttachment.ToTable();

                                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Subheadings[n].Skills[p].Demonstration.Attachment = new AttachmentSkill[dtAttachment.Rows.Count];
                                                        for (int q = 0; q < dtAttachment.Rows.Count; q++)
                                                        {
                                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Subheadings[n].Skills[p].Demonstration.Attachment[q] = new AttachmentSkill();

                                                            if (Convert.ToString(dtAttachment.Rows[q]["AttachmentType"]) == "A")
                                                            {
                                                                 objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Subheadings[n].Skills[p].Demonstration.Attachment[q].URL = WebsiteURL + "attachment.aspx?token=" + AuthToken + "&type=a&file=" + Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dtAttachment.Rows[q]["AttachementID"])));
                                                            }
                                                            else
                                                            {
                                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Subheadings[n].Skills[p].Demonstration.Attachment[q].URL = WebsiteURL + "attachment.aspx?token=" + AuthToken + "&type=u&file=" + Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dtAttachment.Rows[q]["AttachementID"])));

                                                            }
                                                        }


                                                    }




                                                }

                                                    //==========================
                                                }

                                                #region Demonstration

                                                // Demonstration 1
                                                /*
                                                DataTable dtQuestionDemonstrations = new DataTable();
                                                DataView dvQuestionDemonstrations = new DataView(dtLevelsWithAllQuestions);
                                                dvQuestionDemonstrations.RowFilter = "QuestionID=" + Convert.ToString(dtQuestions.Rows[m]["QuestionID"]) + " AND RowNo=1";
                                                dtQuestionDemonstrations = dvQuestionDemonstrations.ToTable(true, "Date", "Description", "StudentRatingID");

                                                if (dtQuestionDemonstrations.Rows.Count > 0)
                                                {
                                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1 = new Demonstration1();
                                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Date = Convert.ToDateTime(dtQuestionDemonstrations.Rows[0]["Date"]).ToString("dd/MMM/yyyy");
                                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.SourceOfEvidence = Convert.ToString(dtQuestionDemonstrations.Rows[0]["Description"]);
                                                    long StudentRatingID = Convert.ToInt64(dtQuestionDemonstrations.Rows[0]["StudentRatingID"]);

                                                    DataTable dtAttachment = new DataTable();
                                                    DataView dvAttachment = new DataView(dsAllQuestions.Tables[1]);
                                                    dvAttachment.RowFilter = "StudentRatingID=" + StudentRatingID;
                                                    dtAttachment = dvAttachment.ToTable();

                                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Attachment = new Attachment[dtAttachment.Rows.Count];
                                                    for (int n = 0; n < dtAttachment.Rows.Count; n++)
                                                    {
                                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Attachment[n] = new Attachment();

                                                        if (Convert.ToString(dtAttachment.Rows[n]["AttachmentType"]) == "A")
                                                        {
                                                            // objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Attachment[n].URL = Utility.Config.WebSiteUrl + "source/Attachments/" + Convert.ToString(dtAttachment.Rows[n]["URL"]);
                                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Attachment[n].URL = WebsiteURL + "attachment.aspx?token=" + AuthToken + "&type=a&file=" + Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dtAttachment.Rows[n]["AttachementID"])));
                                                        }
                                                        else
                                                        {
                                                            // objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Attachment[n].URL = Convert.ToString(dtAttachment.Rows[n]["URL"]);
                                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Attachment[n].URL = WebsiteURL + "attachment.aspx?token=" + AuthToken + "&type=u&file=" + Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dtAttachment.Rows[n]["AttachementID"])));

                                                        }
                                                    }


                                                }

                                               */

                                                #endregion

                                            }
                                    }
                                }
                                else
                                {

                                    dsAllQuestions = objAPIBAL.APIStudentListDetailResultQuestions(Convert.ToInt32(dtStudents.Rows[i]["StudentID"]), Convert.ToInt32(dtSubCategory.Rows[k]["CategoryID"]), Year, Semester);
                                    dtLevelsWithAllQuestions = dsAllQuestions.Tables[0];

                                    DataView dvLevels = new DataView(dtLevelsWithAllQuestions);
                                    DataTable dtLevels = new DataTable();
                                    dvLevels.Sort = "LevelNo ASC";
                                    dtLevels = dvLevels.ToTable(true, "LevelName");



                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels = new LevelsDetailSkill[dtLevels.Rows.Count];
                                    for (int l = 0; l < dtLevels.Rows.Count; l++)
                                    {
                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l] = new LevelsDetailSkill();
                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Name = Convert.ToString(dtLevels.Rows[l]["LevelName"]);


                                        DataTable dtQuestions = new DataTable();
                                        DataView dvQuestions = new DataView(dtLevelsWithAllQuestions);
                                        dvQuestions.RowFilter = "LevelName='" + Convert.ToString(objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Name) + "'";

                                        dtQuestions = dvQuestions.ToTable(true, "QuestionID", "Question");

                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions = new QuestionsSkill[dtQuestions.Rows.Count];
                                        for (int m = 0; m < dtQuestions.Rows.Count; m++)
                                        {
                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m] = new QuestionsSkill();
                                            objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Title = Convert.ToString(dtQuestions.Rows[m]["Question"]);

                                            #region Demonstration

                                            // Demonstration 1

                                            DataTable dtQuestionDemonstrations = new DataTable();
                                            DataView dvQuestionDemonstrations = new DataView(dtLevelsWithAllQuestions);
                                            dvQuestionDemonstrations.RowFilter = "QuestionID=" + Convert.ToString(dtQuestions.Rows[m]["QuestionID"]) + " AND RowNo=1";
                                            dtQuestionDemonstrations = dvQuestionDemonstrations.ToTable(true, "Date", "Description", "StudentRatingID");

                                            if (dtQuestionDemonstrations.Rows.Count > 0)
                                            {
                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1 = new Demonstration1();
                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Date = Convert.ToDateTime(dtQuestionDemonstrations.Rows[0]["Date"]).ToString("dd/MMM/yyyy");
                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.SourceOfEvidence = Convert.ToString(dtQuestionDemonstrations.Rows[0]["Description"]);
                                                long StudentRatingID = Convert.ToInt64(dtQuestionDemonstrations.Rows[0]["StudentRatingID"]);

                                                DataTable dtAttachment = new DataTable();
                                                DataView dvAttachment = new DataView(dsAllQuestions.Tables[1]);
                                                dvAttachment.RowFilter = "StudentRatingID=" + StudentRatingID;
                                                dtAttachment = dvAttachment.ToTable();

                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Attachment = new Attachment[dtAttachment.Rows.Count];
                                                for (int n = 0; n < dtAttachment.Rows.Count; n++)
                                                {
                                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Attachment[n] = new Attachment();

                                                    if (Convert.ToString(dtAttachment.Rows[n]["AttachmentType"]) == "A")
                                                    {
                                                        // objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Attachment[n].URL = Utility.Config.WebSiteUrl + "source/Attachments/" + Convert.ToString(dtAttachment.Rows[n]["URL"]);
                                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Attachment[n].URL = WebsiteURL + "attachment.aspx?token=" + AuthToken + "&type=a&file=" + Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dtAttachment.Rows[n]["AttachementID"])));
                                                    }
                                                    else
                                                    {
                                                        // objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Attachment[n].URL = Convert.ToString(dtAttachment.Rows[n]["URL"]);
                                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration1.Attachment[n].URL = WebsiteURL + "attachment.aspx?token=" + AuthToken + "&type=u&file=" + Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dtAttachment.Rows[n]["AttachementID"])));

                                                    }
                                                }


                                            }

                                            // Demonstration 2

                                            dtQuestionDemonstrations = new DataTable();
                                            dvQuestionDemonstrations = new DataView(dtLevelsWithAllQuestions);
                                            dvQuestionDemonstrations.RowFilter = "QuestionID=" + Convert.ToString(dtQuestions.Rows[m]["QuestionID"]) + " AND RowNo=2";
                                            dtQuestionDemonstrations = dvQuestionDemonstrations.ToTable(true, "Date", "Description", "StudentRatingID");

                                            if (dtQuestionDemonstrations.Rows.Count > 0)
                                            {
                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration2 = new Demonstration2();
                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration2.Date = Convert.ToDateTime(dtQuestionDemonstrations.Rows[0]["Date"]).ToString("dd/MMM/yyyy");
                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration2.SourceOfEvidence = Convert.ToString(dtQuestionDemonstrations.Rows[0]["Description"]);

                                                long StudentRatingID = Convert.ToInt64(dtQuestionDemonstrations.Rows[0]["StudentRatingID"]);

                                                DataTable dtAttachment = new DataTable();
                                                DataView dvAttachment = new DataView(dsAllQuestions.Tables[1]);
                                                dvAttachment.RowFilter = "StudentRatingID=" + StudentRatingID;
                                                dtAttachment = dvAttachment.ToTable();

                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration2.Attachment = new Attachment[dtAttachment.Rows.Count];
                                                for (int n = 0; n < dtAttachment.Rows.Count; n++)
                                                {
                                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration2.Attachment[n] = new Attachment();
                                                    if (Convert.ToString(dtAttachment.Rows[n]["AttachmentType"]) == "A")
                                                    {
                                                        // objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration2.Attachment[n].URL = Utility.Config.WebSiteUrl + "source/Attachments/" + Convert.ToString(dtAttachment.Rows[n]["URL"]);
                                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration2.Attachment[n].URL = WebsiteURL + "attachment.aspx?token=" + AuthToken + "&type=a&file=" + Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dtAttachment.Rows[n]["AttachementID"])));
                                                    }
                                                    else
                                                    {
                                                        //objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration2.Attachment[n].URL = Convert.ToString(dtAttachment.Rows[n]["URL"]);
                                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration2.Attachment[n].URL = WebsiteURL + "attachment.aspx?token=" + AuthToken + "&type=u&file=" + Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dtAttachment.Rows[n]["AttachementID"])));
                                                    }
                                                }
                                            }

                                            // Demonstration 3

                                            dtQuestionDemonstrations = new DataTable();
                                            dvQuestionDemonstrations = new DataView(dtLevelsWithAllQuestions);
                                            dvQuestionDemonstrations.RowFilter = "QuestionID=" + Convert.ToString(dtQuestions.Rows[m]["QuestionID"]) + " AND RowNo=3";
                                            dtQuestionDemonstrations = dvQuestionDemonstrations.ToTable(true, "Date", "Description", "StudentRatingID");

                                            if (dtQuestionDemonstrations.Rows.Count > 0)
                                            {
                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration3 = new Demonstration3();
                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration3.Date = Convert.ToDateTime(dtQuestionDemonstrations.Rows[0]["Date"]).ToString("dd/MMM/yyyy");
                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration3.SourceOfEvidence = Convert.ToString(dtQuestionDemonstrations.Rows[0]["Description"]);

                                                long StudentRatingID = Convert.ToInt64(dtQuestionDemonstrations.Rows[0]["StudentRatingID"]);

                                                DataTable dtAttachment = new DataTable();
                                                DataView dvAttachment = new DataView(dsAllQuestions.Tables[1]);
                                                dvAttachment.RowFilter = "StudentRatingID=" + StudentRatingID;
                                                dtAttachment = dvAttachment.ToTable();

                                                objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration3.Attachment = new Attachment[dtAttachment.Rows.Count];
                                                for (int n = 0; n < dtAttachment.Rows.Count; n++)
                                                {
                                                    objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration3.Attachment[n] = new Attachment();
                                                    if (Convert.ToString(dtAttachment.Rows[n]["AttachmentType"]) == "A")
                                                    {
                                                        // objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration3.Attachment[n].URL = Utility.Config.WebSiteUrl + "source/Attachments/" + Convert.ToString(dtAttachment.Rows[n]["URL"]);
                                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration3.Attachment[n].URL = WebsiteURL + "attachment.aspx?token=" + AuthToken + "&type=a&file=" + Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dtAttachment.Rows[n]["AttachementID"])));
                                                    }
                                                    else
                                                    {
                                                        //objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration3.Attachment[n].URL = Convert.ToString(dtAttachment.Rows[n]["URL"]);
                                                        objStudentResult.Students[i].Category[j].SubCategory[k].Judgements.Levels[l].Questions[m].Demonstration3.Attachment[n].URL = WebsiteURL + "attachment.aspx?token=" + AuthToken + "&type=u&file=" + Convert.ToString(Utility.Security.Rijndael128Algorithm.EncryptString(Convert.ToString(dtAttachment.Rows[n]["AttachementID"])));
                                                    }
                                                }

                                            }


                                            #endregion

                                        }
                                    }

                                }



                            }
                            #endregion

                        }
                        #endregion

                    }

                    objResponse.StudentResult = objStudentResult;
                    //string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                    string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Include });
                    strResponseName = strResponseName.Replace("\"StudentResult\"", "\"data\"");
                    HttpContext.Current.Response.Write(strResponseName);
                }


            }
            else
            {
                NoRecordExists();
            }


            objResponse.success = "true";
            objResponse.StatusCode = "200";
            objResponse.message = "Validated";
        }


        HttpContext.Current.Response.End();
    }
    #endregion

    #endregion

    #region Common Methods
    public void Unauthorized()
    {
        Response objResponse = new Response();
        objResponse.success = "false";
        objResponse.StatusCode = "401";
        objResponse.message = "Unauthorized Access.";
        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
    }
    public void BadRequest()
    {
        Response objResponse = new Response();
        objResponse.success = "false";
        objResponse.StatusCode = "400";
        objResponse.message = "Bad Request";
        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
    }
    public void NoRecordExists()
    {
        Response objResponse = new Response();
        Blank ObjBlank = new Blank();
        objResponse.success = "true";
        objResponse.message = "No records exists.";
        objResponse.Blank = ObjBlank;
        string strResponseName = JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        strResponseName = strResponseName.Replace("\"Blank\"", "\"data\"");
        HttpContext.Current.Response.Write(strResponseName);
    }



    public void ExceptionTRack(string strException)
    {
        Response objResponse = new Response();
        objResponse.success = "false";
        objResponse.message = strException;
        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
        //List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //Dictionary<string, object> row;
        //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

        //row = new Dictionary<string, object>();
        //row.Add("success", "false");
        //row.Add("message", strException);
        //rows.Add(row);
        //HttpContext.Current.Response.Write(serializer.Serialize(rows));
    }
    public void InvalidLogin()
    {
        Response objResponse = new Response();
        objResponse.success = "false";
        objResponse.message = "Invalid Email or Password.";
        HttpContext.Current.Response.Write(JsonConvert.SerializeObject(objResponse, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore }));
    }
    #endregion

}

