<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;
using System.IO;
using System.Linq;
using System.Data;
using ExcelDataReader;
using Newtonsoft.Json;
using System.Diagnostics;

using System.Collections.Generic;
using System.Text.RegularExpressions;
using ClosedXML.Excel; // Free Excel Library

using BAL;

public class Upload : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Expires = -1;

        string action = context.Request.QueryString["method"];

        // Use a switch statement for better readability
        switch (action)
        {
            case "UploadExcelFile":
                UploadExcelFile(context);
                break;

            case "DownloadExcelFormat":
                DownloadExcelFormat(context);
                break;

            case "DownloadErrorFile":
                DownloadErrorFile(context);
                break;

            default:
                context.Response.Write("Invalid action.");
                break;
        }

        if (context.Request.Files.Count > 0)
        {
            HttpPostedFile ObjFile = context.Request.Files[0];
            int KeyIndex = 0;
            foreach (string Key in context.Request.Form.Keys)
            {
                if (Int32.TryParse(Key, out KeyIndex))
                {
                    if (context.Request[Key] == context.Request["FileName"])
                    {
                        try
                        {
                            string savepath = "";
                            string tempPath = "";
                            tempPath = context.Request["folder"];
                            string File = DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(ObjFile.FileName);
                            int intResult = 0;

                            savepath = context.Server.MapPath(tempPath);
                            if (!Directory.Exists(savepath))
                                Directory.CreateDirectory(savepath);
                            if (intResult > 0)
                            {
                                ObjFile.SaveAs(savepath + @"\" + File);
                            }
                            context.Response.Write(tempPath + "/" + File);
                            context.Response.StatusCode = 200;
                        }
                        catch (Exception ex)
                        {
                            context.Response.Write("Error: " + ex.Message);
                        }
                    }
                }
            }
        }
    }



    public static void UploadExcelFile(HttpContext context)
    {
        try
        {
            string jsonResponse = string.Empty;
            HttpPostedFile file = context.Request.Files["file"];

            if (file == null || file.ContentLength == 0)
            {
                context.Response.Write("{\"message\": \"No file uploaded.\"}");
                return;
            }

            DataTable dt = ExcelHelper.ReadSpecificColumnsFromExcel(file);

            string errorMessage = string.Empty;
            List<string> requiredColumns = new List<string> { "Subject", "Strand", "Level", "Question", "SubHeading", "Link" };

            for (int i = 1; i <= 20; i++)
            {
                requiredColumns.Add("Skill" + i);
            }

            int filledSkillCount = 0;

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    if (requiredColumns.Contains(col.ColumnName))
                    {
                        string cellValue = row[col] != DBNull.Value ? row[col].ToString().Trim() : "";

                        if (row[col] != DBNull.Value && string.IsNullOrWhiteSpace(row[col].ToString()))
                        {
                            row[col] = DBNull.Value;
                        }

                        if (col.ColumnName.StartsWith("Skill"))
                        {
                            int skillNumber;
                            if (int.TryParse(col.ColumnName.Replace("Skill", ""), out skillNumber))
                            {
                                if (skillNumber > 15)
                                {
                                    jsonResponse = "{\"status\": \"error\", \"message\": \"Only Skill1 to Skill15 are allowed. Remove extra skill columns.\"}";
                                    context.Response.Write(jsonResponse);
                                    return;
                                }

                                if (!string.IsNullOrEmpty(cellValue))
                                {
                                    filledSkillCount++;
                                }
                            }
                        }

                    }
                    else
                    {
                        jsonResponse = "{\"status\": \"error\", \"message\": \"Invalid column format found in uploaded sheet.\"}";
                        context.Response.Write(jsonResponse);
                        return;
                    }
                }
            }


            string jsonData = JsonConvert.SerializeObject(dt, Formatting.None);

            if (dt.Rows.Count > 5000)
            {
                string msg = "Please upload data less then 5000 rows";
                context.Response.Write("{\"status\": \"Error\", \"message\": \"" + msg + "\"}");
            }
            else
            {
                //DataSet dtErrors = QuestionsBAL.SaveExcelData(jsonData);
                DataSet dtErrors = SaveExcelData(jsonData);

                if (dtErrors.Tables[0].Rows.Count > 0)
                {

                    string filePath = string.Empty;
                    filePath = ExcelHelper.GenerateErrorFile(dtErrors.Tables[0]);
                    jsonResponse = "{\"status\": \"error\", \"message\": \"Duplicate data found. Download error report.\", \"filePath\": \"" + filePath + "\"}";
                    context.Response.Write(jsonResponse);

                }
                else
                {
                    // If no errors, data was imported successfully
                    context.Response.Write("{\"status\": \"success\", \"message\": \"Data imported successfully!\"}");
                }
            }
        }
        catch (Exception ex)
        {
            string msg = ex.Message.ToString();
            msg = msg.Replace("\"", string.Empty);
            context.Response.Write("{\"status\": \"Error\", \"message\": \"" + msg + "\"}");
        }
    }

    public void DownloadExcelFormat(HttpContext context)
    {
        try
        {
            context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            context.Response.AddHeader("content-disposition", "attachment; filename=QuestionUploadFormat.xlsx");

            using (MemoryStream stream = ExcelHelper.GenerateExcelTemplate())
            {
                stream.CopyTo(context.Response.OutputStream);
            }
        }
        catch (Exception ex)
        {
            context.Response.Write("Error: " + ex.Message);
        }
    }
    public void DownloadErrorFile(HttpContext context)
    {
        try
        {
            context.Response.Clear();

            string filePath = context.Request.QueryString["filePath"];
            string serverFile = ExcelHelper.ValidateAndGetFilePath(filePath);
            string serverFilePath = serverFile;

            context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            context.Response.AddHeader("Content-Disposition", "attachment; filename=ErrorReport.xlsx");

            context.Response.TransmitFile(serverFilePath);
            context.Response.Flush();
            context.ApplicationInstance.CompleteRequest();

            ExcelHelper.CleanupFile(serverFilePath);
        }
        catch (Exception ex)
        {
            context.Response.Write("Error: " + ex.Message);
        }
    }

    public static DataSet SaveExcelData(string jsonData)
    {
        try
        {
            DAL.DbParameter[] dbParam = new DAL.DbParameter[] {
                new DAL.DbParameter("@pInputData", DAL.DbParameter.DbType.NVarChar, -1, jsonData)
            };

            DataSet dtResult = DAL.DbConnectionDAL.GetDataSet(CommandType.StoredProcedure, "uploadTargetFileLevelWise", dbParam);
            return dtResult;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }






    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}