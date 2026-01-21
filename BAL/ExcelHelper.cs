using ClosedXML.Excel;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BAL
{
    public class ExcelHelper
    {
        // Read Excel File and return DataTable
        public static DataTable ReadExcel(HttpPostedFile file)
        {
            try
            {
                using (var stream = file.InputStream)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    });

                    var dt = result.Tables.Cast<DataTable>().FirstOrDefault();
                    if (dt == null || dt.Rows.Count == 0) return new DataTable();

                    // Remove Empty Rows Safely
                    var filteredRows = dt.AsEnumerable()
                        .Where(row => row.ItemArray.Any(field => field != DBNull.Value && !string.IsNullOrWhiteSpace(field.ToString())))
                        .ToList();

                    return filteredRows.Any() ? filteredRows.CopyToDataTable() : dt.Clone();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error reading Excel file: " + ex.Message);
                return new DataTable(); // Return an empty table in case of error
            }
        }



        public static DataTable ReadSpecificColumnsFromExcel(HttpPostedFile file)
        {
            try
            {
                using (var stream = file.InputStream)
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    });

                    var dt = result.Tables.Cast<DataTable>().FirstOrDefault();
                    if (dt == null || dt.Rows.Count == 0) return new DataTable();

                    //  Define Required Columns
                    List<string> requiredColumns = new List<string>
                     {
                         "Subject", "Strand", "Level", "Question", "SubHeading", "Link"
                     };

                    //  Add Skill Columns Dynamically (Skill A1 to Skill A15)
                    for (int i = 1; i <= 20; i++)
                    {
                        string skillColumn = $"Skill{i}";
                        if (dt.Columns.Contains(skillColumn))
                        {
                            requiredColumns.Add(skillColumn);
                        }
                    }

                    List<string> availableColumns = dt.Columns.Cast<DataColumn>().Select(col => col.ColumnName.Trim()).ToList();

                    List<string> missingColumns = requiredColumns.Where(col => availableColumns.Contains(col)).ToList();

                    if (missingColumns.Count != requiredColumns.Count)
                    {

                        return dt;
                    }


                    //  Remove Empty Rows
                    var filteredRows = dt.AsEnumerable()
                            .Where(row => row.ItemArray.Any(field => field != DBNull.Value && !string.IsNullOrWhiteSpace(field.ToString())))
                            .ToList();

                    var filteredTable = filteredRows.Any() ? filteredRows.CopyToDataTable() : dt.Clone();


                    // Trim Whitespace from All String Values
                    foreach (DataRow row in filteredTable.Rows)
                    {
                        foreach (DataColumn col in filteredTable.Columns)
                        {
                            if (row[col] is string)
                            {
                                row[col] = row[col].ToString().Trim();
                            }
                        }
                    }

                    //  Create New DataTable with Selected Columns
                    DataTable finalTable = new DataTable();
                    foreach (string column in requiredColumns)
                    {
                        if (filteredTable.Columns.Contains(column))
                        {
                            finalTable.Columns.Add(column, filteredTable.Columns[column].DataType);
                        }
                    }

                    //  Copy Data from Selected Columns
                    foreach (DataRow row in filteredTable.Rows)
                    {
                        DataRow newRow = finalTable.NewRow();
                        foreach (string column in requiredColumns)
                        {
                            if (filteredTable.Columns.Contains(column))
                            {
                                newRow[column] = row[column];
                            }
                        }
                        finalTable.Rows.Add(newRow);
                    }

                    return finalTable;




                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error reading Excel file: " + ex.Message);
                return new DataTable(); // Return an empty table in case of error
            }
        }






        // Generate Error Excel File
        public static string GenerateErrorFile(DataTable FileError)
        {
            try
            {
                string folderPath = HttpContext.Current.Server.MapPath("~/source/ErrorFiles/");
                string fileName = "ErrorReport.xlsx";
                string filePath = Path.Combine(folderPath, fileName);

                Directory.CreateDirectory(folderPath);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Errors");

                    for (int col = 0; col < FileError.Columns.Count; col++)
                    {
                        worksheet.Cell(1, col + 1).Value = FileError.Columns[col].ColumnName;
                        worksheet.Cell(1, col + 1).Style.Font.Bold = true;
                    }

                    for (int row = 0; row < FileError.Rows.Count; row++)
                    {
                        for (int col = 0; col < FileError.Columns.Count; col++)
                        {
                            worksheet.Cell(row + 2, col + 1).Value = FileError.Rows[row][col].ToString();
                        }
                    }



                    foreach (var column in worksheet.ColumnsUsed())
                    {
                        try
                        {
                            column.AdjustToContents();
                        }
                        catch (Exception colEx)
                        {
                            System.Diagnostics.Debug.WriteLine($"Error adjusting column {column.ColumnNumber()}: {colEx.Message}");
                        }
                    }
                    // worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(filePath);
                }

                return VirtualPathUtility.ToAbsolute("~/source/ErrorFiles/" + fileName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error generating Excel error file: " + ex.Message);
                return string.Empty; // Return an empty string in case of failure
            }
        }

        // Generate Excel Template
        //public static MemoryStream GenerateExcelTemplate()
        //{
        //    try
        //    {
        //        using (XLWorkbook workbook = new XLWorkbook())
        //        {
        //            var worksheet = workbook.Worksheets.Add("Data");
        //            string[] headers = { "Subject", "Strand", "Level", "Question", "DescriptionLink", "SubHeading", "Skill1","Skill2" };
        //            for (int i = 0; i < headers.Length; i++)
        //            {
        //                worksheet.Cell(1, i + 1).Value = headers[i];
        //                worksheet.Cell(1, i + 1).Style.Font.Bold = true;
        //            }

        //            string[] questions =
        //            {
        //            "use mathematical modelling to solve practical problems involving additive and multiplicative situations...",
        //            "", "", "quantify sets of objects, to at least 120, by partitioning collections...",
        //            "", "", "", ""
        //        };

        //            string[] descriptionLinks =
        //            {
        //            "https://victoriancurriculum.vcaa.vic.edu.au/Curriculum/ContentDescription/VCAMUP011",
        //            "", "", "https://www.google.com",
        //            "", "", "", ""
        //        };

        //            string[] subHeadings =
        //            {
        //            "When solving practical problems involving additive and multiplicative situations...",
        //            "", "", "When quantifying sets of objects...",
        //            "Using physical and virtual materials students can;", "",
        //            "Using diagrams, physical and virtual materials students can;", ""
        //        };

        //            string[] skills =
        //            {
        //            "Solve practical problems Formulate and develop own problems",
        //            "Choose appropriate operations to solve problem Write calculation strategies",
        //            "Choose efficient mental and written strategies Interpret and communicate solutions",
        //            "Partition collections into equal groups",
        //            "Count collections efficiently using skip counting",
        //            "Add to 20 using part-part whole",
        //            "Subtract within 20 using part-part whole",
        //            "Be able to use a variety of calculation strategies"
        //        };

        //            int maxLength = Math.Max(questions.Length, Math.Max(descriptionLinks.Length, Math.Max(subHeadings.Length, skills.Length)));

        //            for (int i = 0; i < maxLength; i++)
        //            {

        //                worksheet.Cell(i + 2, 1).Value = "Mathematics 2.0";
        //                worksheet.Cell(i + 2, 2).Value = "Number";
        //                worksheet.Cell(i + 2, 3).Value = "FL";

        //                worksheet.Cell(i + 2, 1).Value = i < questions.Length ? questions[i] : "";
        //                worksheet.Cell(i + 2, 2).Value = i < descriptionLinks.Length ? descriptionLinks[i] : "";
        //                worksheet.Cell(i + 2, 3).Value = i < subHeadings.Length ? subHeadings[i] : "";
        //                worksheet.Cell(i + 2, 4).Value = i < skills.Length ? skills[i] : "";
        //            }

        //            worksheet.Columns().AdjustToContents();

        //            MemoryStream stream = new MemoryStream();
        //            workbook.SaveAs(stream);
        //            stream.Position = 0;
        //            return stream;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine("Error generating Excel template: " + ex.Message);
        //        return null; // Return null to indicate failure
        //    }
        //}

        public static MemoryStream GenerateExcelTemplate()
        {
            try
            {
                using (XLWorkbook workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Data");

                    // Define headers as per the given format
                    string[] headers = { "Subject", "Strand", "Level", "Link", "Question", "SubHeading" };
                    for (int i = 1; i <= 15; i++)
                    {
                        headers = headers.Append($"Skill{i}").ToArray();
                    }

                    // Add headers to the first row
                    for (int i = 0; i < headers.Length; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = headers[i];
                        worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                    }

                    // Example data
                    string[,] data =
                    {
                { "Mathematics 2.0", "Number", "FL", "https://f10.vcaa.vic.edu.au/learning-areas/mathematics", "use mathematical modelling to solve practical problems", "With the use of digital tools, students can;" },
                { "Mathematics 2.0", "Number", "FL", "https://www.w3schools.com/java/default.asp", "What is Razor Pages in ASP.NET Core?", "Web Development" },
                { "Mathematics 2.0", "Number", "FL", "https://www.w3schools.com/java/default.asp", "What is Razor Pages in ASP.NET Core?", "Page" }
            };

                    string[,] skills =
                    {
                { "Solve practical problems", "Choose appropriate operations", "Choose efficient strategies", "Partition collections", "Count collections efficiently", "Add to 20", "Subtract within 20", "Use various strategies", "", "", "", "", "", "", "" },
                { "Simplifies web development", "Uses page-based architecture", "Uses .cshtml files", "Provides separation of concerns", "", "", "", "", "", "", "", "", "", "", "" },
                { "T1", "T2", "T3", "T4", "", "", "", "", "", "", "", "", "", "", "" }
            };

                    for (int row = 0; row < data.GetLength(0); row++)
                    {
                        for (int col = 0; col < data.GetLength(1); col++)
                        {
                            worksheet.Cell(row + 2, col + 1).Value = data[row, col];
                        }
                        for (int skillCol = 0; skillCol < skills.GetLength(1); skillCol++)
                        {
                            worksheet.Cell(row + 2, data.GetLength(1) + skillCol + 1).Value = skills[row, skillCol];
                        }
                    }

                    worksheet.Columns().AdjustToContents();

                    MemoryStream stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    stream.Position = 0;
                    return stream;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error generating Excel template: " + ex.Message);
                return null;
            }
        }



        public static string ValidateAndGetFilePath(string filePath)
        {
            try
            {
                if (string.IsNullOrEmpty(filePath))
                {
                    throw new Exception("Invalid file path.");
                }

                string serverFilePath = HttpContext.Current.Server.MapPath("~/" + filePath);

                if (!File.Exists(serverFilePath))
                {
                    throw new Exception("File not found.");
                }

                return serverFilePath;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error validating file path: " + ex.Message);
                throw;
            }
        }

        public static void CleanupFile(string serverFilePath)
        {
            try
            {
                if (File.Exists(serverFilePath))
                {
                    File.Delete(serverFilePath);
                }

                string directoryPath = Path.GetDirectoryName(serverFilePath);
                if (Directory.Exists(directoryPath) && Directory.GetFiles(directoryPath).Length == 0)
                {
                    Directory.Delete(directoryPath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error cleaning up file: " + ex.Message);
            }
        }

    }
}
