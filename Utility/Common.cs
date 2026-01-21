using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


namespace Utility
{

    public class Common
    {

        public enum DataBaseOperation
        {
            Remove = 1,
            Active = 2,
            InActive = 3,
            OnHome = 4,
            NotOnHome = 5,
            UpdateRank = 6,
            ActiveIsOnHomePage = 7,
            InActiveIsOnHomePage = 8,
            activesuperadmin = 9,
            inactivesuperadmin = 10,
            activeadmin = 11,
            inactiveadmin = 12,
            activeclient = 13,
            inactiveclient = 14,
            None = 0
        }

        public enum ErrorMsgType
        {
            Error,
            Alert,
            Success
        }

        public enum RenderControlName
        {
            Repeater = 82,
            UserControl = 85,
            HtmlGeneric = 72,
            HtmlTable = 84
        }

        public static string ScriptEndTag;
        public static string ScriptStartTag;
        public static string[] strDirectoryList = new string[] { "adminpanel" };
        private static string strVirtualDir = Config.VirtualDir;
        private static string strWebSiteUrl = Config.WebSiteUrl;

        public Common()
        {
        }

        static Common()
        {

            Utility.Common.ScriptStartTag = "<script type=\"text/javascript\" language=\"javascript\">";
            Utility.Common.ScriptEndTag = "</script>";

        }

        public static void BindAdminPagingControl(int intCurrentPage, int intRecordPerPage, int intPagingLimit, int intTotalRecord, Repeater rptPagingCtrl)
        {
            int num2;
            int num3;
            int num = Convert.ToInt32(Math.Ceiling((double)(((double)intTotalRecord) / (intRecordPerPage * 1.0))));
            DataTable table = new DataTable();
            table.Columns.Add("PageNo", Type.GetType("System.Int32"));
            table.Columns.Add("PageName", Type.GetType("System.String"));
            table.Columns.Add("class", Type.GetType("System.String"));
            if ((intCurrentPage - Convert.ToInt32(Math.Floor((double)(((double)intPagingLimit) / 2.0)))) <= 0)
            {
                num2 = 1;
                num3 = (num <= intPagingLimit) ? num : intPagingLimit;
            }
            else
            {
                num2 = intCurrentPage - Convert.ToInt32(Math.Floor((double)(((double)intPagingLimit) / 2.0)));
                num3 = (((num2 + intPagingLimit) - 1) <= num) ? ((num2 + intPagingLimit) - 1) : num;
            }
            int num4 = 1;
            if (intCurrentPage > 1)
            {
                table.Rows.Add(new object[] { intCurrentPage - 1, "Previous", "step prevLink" });
            }
            for (num4 = num2; num4 <= num3; num4++)
            {
                if (num4 == intCurrentPage)
                {
                    table.Rows.Add(new object[] { num4, num4.ToString(), "currentStep" });
                }
                else
                {
                    table.Rows.Add(new object[] { num4, num4.ToString(), "step" });
                }
            }
            if (intCurrentPage < num)
            {
                table.Rows.Add(new object[] { intCurrentPage + 1, "Next", "step nextLink" });
            }
            if (table.Rows.Count > 1)
            {
                rptPagingCtrl.Visible = true;
                rptPagingCtrl.DataSource = table;
                rptPagingCtrl.DataBind();
            }
            else
            {
                rptPagingCtrl.Visible = false;
            }
        }

        public static void DisplayMessageFront(System.Web.UI.HtmlControls.HtmlGenericControl DivMsg, string Msg, Utility.Common.ErrorMsgType MsgType)
        {
            DivMsg.Visible = true;
            DivMsg.Style.Add("display", System.String.Empty);
            DivMsg.InnerHtml = Msg;
            switch (MsgType)
            {
                case Utility.Common.ErrorMsgType.Alert:
                    DivMsg.Attributes.Add("class", "alert-message");
                    break;

                case Utility.Common.ErrorMsgType.Error:
                    DivMsg.Attributes.Add("class", "alert-message error");
                    break;

                case Utility.Common.ErrorMsgType.Success:
                    DivMsg.Attributes.Add("class", "alert-message success");
                    break;
            }
        }

        public static void RedirectToAdminLoginPage(string strRefUrl)
        {
            HttpContext.Current.Response.Write("<script>window.location.href='" + strVirtualDir + "adminpanel/login.aspx';</script>");
            HttpContext.Current.Response.End();
        }
        
        public static bool IsNumeric(string strToCheck)
        {
            bool flag1;

            bool flag2 = !System.String.IsNullOrEmpty(strToCheck);
            if (!flag2)
                flag1 = false;
            else
                flag1 = System.Text.RegularExpressions.Regex.IsMatch(strToCheck, "^\\d+(\\.\\d+)?$");
            return flag1;
        }

        public static string MsgBox(string Msg, Utility.Common.ErrorMsgType MsgType)
        {
            return null;
        }

        public static string RandomString(int intLength)
        {
            string s1 = System.String.Empty;
            string[] sArr2 = new string[] {
                                            "0", 
                                            "1", 
                                            "2", 
                                            "3", 
                                            "4", 
                                            "5", 
                                            "6", 
                                            "7", 
                                            "8", 
                                            "9", 
                                            "A", 
                                            "B", 
                                            "C", 
                                            "D", 
                                            "E", 
                                            "F", 
                                            "G", 
                                            "H", 
                                            "I", 
                                            "J", 
                                            "K", 
                                            "L", 
                                            "M", 
                                            "N", 
                                            "O", 
                                            "P", 
                                            "Q", 
                                            "R", 
                                            "S", 
                                            "T", 
                                            "U", 
                                            "V", 
                                            "W", 
                                            "X", 
                                            "Y", 
                                            "Z", 
                                            "a", 
                                            "b", 
                                            "c", 
                                            "d", 
                                            "e", 
                                            "f", 
                                            "g", 
                                            "h", 
                                            "i", 
                                            "j", 
                                            "k", 
                                            "l", 
                                            "m", 
                                            "n", 
                                            "o", 
                                            "p", 
                                            "q", 
                                            "r", 
                                            "s", 
                                            "t", 
                                            "u", 
                                            "v", 
                                            "w", 
                                            "x", 
                                            "y", 
                                            "z" };
            string[] sArr1 = sArr2;
            System.Random random = new System.Random();
            bool flag = intLength >= 4;
            if (!flag)
                intLength = 4;
            int i = 0;
            while (flag)
            {
                s1 += sArr1[random.Next(1, sArr1.Length)].ToString();
                i++;
                flag = i < intLength;
            }
            return s1;
        }

        public static string ReadFiles(string strFullPath)
        {
            string s1 = System.String.Empty;
            System.IO.StreamReader streamReader = new System.IO.StreamReader(strFullPath);
            try
            {
                try
                {
                    s1 = streamReader.ReadToEnd();
                }
                catch
                {
                }
            }
            finally
            {
                streamReader.Close();
            }
            return s1;
        }

        public static string FixLengthString(object objDescription, int intLength)
        {
            string str = string.Empty;
            if (objDescription != null)
            {
                str = Convert.ToString(objDescription);
                if (str.Length > intLength)
                {
                    str = str.Substring(0, intLength - 3) + "...";
                }
            }
            return str;
        }

        public static string RenderControl(System.Web.UI.Control ctrl)
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            System.IO.StringWriter stringWriter = new System.IO.StringWriter(stringBuilder);
            System.Web.UI.HtmlTextWriter htmlTextWriter = new System.Web.UI.HtmlTextWriter(stringWriter);
            ctrl.RenderControl(htmlTextWriter);
            return stringBuilder.ToString();
        }

        public static string RenderControl(object ObjControl, Utility.Common.RenderControlName RenderCtrlName)
        {
            string returnValue = string.Empty;
            StringWriter ObjWrt = new StringWriter();
            HtmlTextWriter ObjHtmlWrt = new HtmlTextWriter(ObjWrt);
            switch (RenderCtrlName)
            {
                case RenderControlName.Repeater:
                    ((Repeater)ObjControl).RenderControl(ObjHtmlWrt);
                    break;
                case RenderControlName.UserControl:
                    ((UserControl)ObjControl).RenderControl(ObjHtmlWrt);
                    break;
                case RenderControlName.HtmlGeneric:
                    ((HtmlGenericControl)ObjControl).RenderControl(ObjHtmlWrt);
                    break;
                case RenderControlName.HtmlTable:
                    ((HtmlTable)ObjControl).RenderControl(ObjHtmlWrt);
                    break;
            }
            returnValue = ObjWrt.ToString().Trim();
            ObjWrt.Close();
            ObjHtmlWrt.Close();
            return returnValue;

        }

        public static string RenderRepeater(System.Web.UI.WebControls.Repeater rptRepeater)
        {
            return null;
        }

        private static bool IsDirectory(string strDirectoryName)
        {
            for (int i = 0; i < strDirectoryList.Length; i++)
            {
                if (strDirectoryList[i] == strDirectoryName)
                {
                    return true;
                }
            }
            return false;
        }

        #region EncodeScriptTags

        public static string EncodeScriptTags(string content)
        {

            int intScriptStartIndex = -1;
            int intScriptEndIndex = -1;
            int Counter = 0;

            do
            {
                Counter++;

                if (Counter > 10)
                {
                    break;
                }

                intScriptStartIndex = content.IndexOf("<script", StringComparison.CurrentCultureIgnoreCase);

                if (intScriptStartIndex > -1)
                {
                    //replace < from <script>
                    content = content.Remove(intScriptStartIndex, 1);
                    content = content.Insert(intScriptStartIndex, "&lt;");

                    //replace > from <script> , wont be possible if language etc attributes are specified
                    //intScriptStartIndex = intScriptStartIndex + 3 + 7 ;
                    //content = content.Remove(intScriptStartIndex, 1);
                    //content = content.Insert(intScriptStartIndex, "&gt;");
                }

                intScriptEndIndex = content.IndexOf("</script>", StringComparison.CurrentCultureIgnoreCase);
                if (intScriptEndIndex > -1)
                {
                    //replace < from </script>
                    content = content.Remove(intScriptEndIndex, 1);
                    content = content.Insert(intScriptEndIndex, "&lt;");
                    intScriptEndIndex = intScriptEndIndex + 3;

                    //replace > from </script>
                    intScriptEndIndex = intScriptEndIndex + 8;
                    content = content.Remove(intScriptEndIndex, 1);
                    content = content.Insert(intScriptEndIndex, "&gt;");
                }

            } while (intScriptStartIndex > -1 || intScriptEndIndex > -1);

            return content;
        }

        #endregion

        public static void UrlRewriting(string[] strRawUrl, string strQuery)
        {
            string queryString = string.Empty;
            int num = 1;
            int index = 0;
            if (strRawUrl.Length != 0)
            {
                string path = string.Empty;
                bool flag = false;

                flag = IsDirectory(strRawUrl[0].ToLower());
                if (strRawUrl[0].ToLower() != "")
                {
                    flag = IsDirectory(strRawUrl[1].ToLower());
                }

                if (flag)
                {
                    if (strRawUrl.Length > 1)
                    {
                        path = strRawUrl[0] + "/" + strRawUrl[1];
                        if (strRawUrl[0].ToLower() != "")
                        {
                            path = strRawUrl[1] + "/" + strRawUrl[2];
                        }
                    }
                    else
                    {
                        path = strRawUrl[0] + "/default.aspx";
                        if (strRawUrl[0].ToLower() != "")
                        {
                            path = strRawUrl[1] + "/default.aspx";
                        }
                    }
                }
                else
                {

                    path = strRawUrl[0];
                    if (path.ToLower() != "")
                    {
                        path = strRawUrl[1];
                    }
                }
                string str3 = string.Empty;
                if (Path.GetExtension(path).Length == 0)
                {
                    str3 = ".aspx";
                }
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(strVirtualDir + path + str3)))
                {
                    //if (HttpContext.Current.Request.Url.ToString().StartsWith("https://"))
                    //{
                    //    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.ToString().Replace("https://", "http://"));
                    //}
                    path = path + str3;
                    if (flag)
                    {
                        index = 2;
                    }
                    else
                    {
                        index = 1;
                    }

                    if (strRawUrl[0].ToLower() != "")
                    {
                        index = 0;
                    }



                }
                else
                {
                    if (str3 != string.Empty)
                    {
                        path = "page.aspx";
                    }
                    else
                    {
                        path = "page.aspx" + str3;
                    }
                    index = 0;
                }
                int num3 = index;
                for (index = num3; index <= (strRawUrl.Length - 1); index++)
                {
                    object obj2;
                    if (num == 1)
                    {
                        obj2 = queryString;
                        queryString = string.Concat(new object[] { obj2, "c", num, "=", strRawUrl[index] });
                    }
                    else
                    {
                        obj2 = queryString;
                        queryString = string.Concat(new object[] { obj2, "&c", num, "=", strRawUrl[index] });
                    }
                    num++;
                }
                if (!string.IsNullOrEmpty(strQuery))
                {
                    if (queryString.Length == 0)
                    {
                        queryString = strQuery;
                    }
                    else
                    {
                        queryString = queryString + "&" + strQuery;
                    }
                }
                HttpContext.Current.RewritePath(strVirtualDir + path, "", queryString);
            }
        }
        //public static void UrlRewriting(string[] strRawUrl, string strQuery)
        //{
        //    string queryString = string.Empty;
        //    int num = 1;
        //    int index = 0;
        //    if (strRawUrl.Length != 0)
        //    {
        //        string path = string.Empty;
        //        bool flag = IsDirectory(strRawUrl[0].ToLower());
        //        if (flag)
        //        {
        //            if (strRawUrl.Length > 1)
        //            {
        //                path = strRawUrl[0] + "/" + strRawUrl[1];
        //            }
        //            else
        //            {
        //                path = strRawUrl[0] + "/default.aspx";
        //            }
        //        }
        //        else
        //        {
        //            path = strRawUrl[0];
        //        }
        //        string str3 = string.Empty;
        //        if (Path.GetExtension(path).Length == 0)
        //        {
        //            str3 = ".aspx";
        //        }
        //        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(strVirtualDir + path + str3)))
        //        {
        //            if (HttpContext.Current.Request.Url.ToString().StartsWith("https://"))
        //            {
        //                HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.ToString().Replace("https://", "http://"));
        //            }
        //            path = path + str3;
        //            if (flag)
        //            {
        //                index = 2;
        //            }
        //            else
        //            {
        //                index = 1;
        //            }
        //        }
        //        else
        //        {
        //            if (str3 != string.Empty)
        //            {
        //                path = "page.aspx";
        //            }
        //            else
        //            {
        //                path = "page.aspx" + str3;
        //            }
        //            index = 0;
        //        }
        //        int num3 = index;
        //        for (index = num3; index <= (strRawUrl.Length - 1); index++)
        //        {
        //            object obj2;
        //            if (num == 1)
        //            {
        //                obj2 = queryString;
        //                queryString = string.Concat(new object[] { obj2, "c", num, "=", strRawUrl[index] });
        //            }
        //            else
        //            {
        //                obj2 = queryString;
        //                queryString = string.Concat(new object[] { obj2, "&c", num, "=", strRawUrl[index] });
        //            }
        //            num++;
        //        }
        //        if (!string.IsNullOrEmpty(strQuery))
        //        {
        //            if (queryString.Length == 0)
        //            {
        //                queryString = strQuery;
        //            }
        //            else
        //            {
        //                queryString = queryString + "&" + strQuery;
        //            }
        //        }
        //        HttpContext.Current.RewritePath(strVirtualDir + path, "", queryString);
        //    }
        //}

        public static string SetDescriptionString(object objDescrString)
        {
            string s;

            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder(System.Convert.ToString(objDescrString));
            bool flag = stringBuilder.Length <= 0;
            if (!flag)
            {
                stringBuilder.Replace("<", "&lt;");
                stringBuilder.Replace(">", "&gt;");
                stringBuilder.Replace("\n", "&lt;br/&gt;");
                stringBuilder.Replace("\r", "");
                stringBuilder.Replace("'", "&#39");
                stringBuilder.Replace("\"", "&quot;");
                s = stringBuilder.ToString();
            }
            else
            {
                s = System.String.Empty;
            }
            return s;
        }
        
        public static void ExportToCSV(string Filename, DataTable dtTable, bool ShowColumnHeader)
        {
            StringBuilder sbContent = new StringBuilder(String.Empty);
            HttpContext.Current.Response.Clear();
            int i = 0;
            //Output Column Headers
            if (ShowColumnHeader)
            {
                int iCol = 0;
                for (iCol = 0; iCol <= dtTable.Columns.Count - 1; iCol++)
                    sbContent.Append("\"" + dtTable.Columns[iCol].ToString() + "\"|");
                sbContent.Replace("|", "\r\n", sbContent.Length - 1, 1);
            }
            foreach (DataRow dr in dtTable.Rows)
            {
                for (i = 0; i < dtTable.Columns.Count; i++)
                {
                    sbContent.Append("\"" + Convert.ToString(dr[i]).Replace("\"", "\"\"") + "\"|");
                }
                sbContent.Replace("|", "\r\n", sbContent.Length - 1, 1);
            }
            HttpContext.Current.Response.ContentType = "text/txt";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Filename);
            HttpContext.Current.Response.Write(sbContent.ToString());
            HttpContext.Current.Response.End();
        }

        public static void ExportToCSVComma(string Filename, DataTable dtTable, bool ShowColumnHeader)
        {
            StringBuilder sbContent = new StringBuilder(String.Empty);
            HttpContext.Current.Response.Clear();
            int i = 0;
            //Output Column Headers
            if (ShowColumnHeader)
            {
                int iCol = 0;
                for (iCol = 0; iCol <= dtTable.Columns.Count - 1; iCol++)
                    sbContent.Append("\"" + dtTable.Columns[iCol].ToString() + "\",");
                sbContent.Replace(",", "\r\n", sbContent.Length - 1, 1);
            }
            foreach (DataRow dr in dtTable.Rows)
            {
                for (i = 0; i < dtTable.Columns.Count; i++)
                {
                    sbContent.Append("\"" + Convert.ToString(dr[i]).Replace("\"", "\"\"") + "\",");
                }
                sbContent.Replace(",", "\r\n", sbContent.Length - 1, 1);
            }
            HttpContext.Current.Response.ContentType = "text/txt";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + Filename);
            HttpContext.Current.Response.Write(sbContent.ToString());
            HttpContext.Current.Response.End();
        }

        public static bool IsFileExist(string strFolderPath, string strImageName)
        {
            return System.IO.File.Exists(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + strFolderPath + strImageName);
        }

        public static bool FileDelete(string strFolderName, string strFileName)
        {
            bool flag1 = false;
            bool flag3 = strFileName == "noimage.jpg" || strFileName == System.String.Empty;
            if (!flag3)
            {
                try
                {
                    System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "\\" + strFolderName + strFileName));
                    flag1 = true;
                }
                catch
                {
                    flag1 = false;
                }
            }
            return flag1;
        }

        public static string GetSubString(string strData, int MaxLengh)
        {
            string str = string.Empty;

            if (strData.Length > MaxLengh)
            {
                str = strData.Substring(0, MaxLengh) + "..";
            }
            else
            {
                str = strData;
            }
            return str;
        }

        public static string ReplaceURL(string strUrl)
        {
            Regex re = new Regex(@"[()<>"";+\n\r`]|^&+|&+$");

            string result = re.Replace(strUrl, string.Empty).ToString();
            result = result.Trim();
           // result = result.Replace("  ", "-");
          //  result = result.Replace(" ", "-");
            result = result.Replace(",", "-");
            result = result.Replace("/", "-");
            result = result.Replace("---", "-");
            result = result.Replace("--", "-");
            result = result.Replace("&", "");
          // result = result.ToLower().Trim();
            return result;
        }
        
        public static string StripHTML(string htmlString)
        {

            string pattern = @"<(.|\n)*?>";

            return Regex.Replace(htmlString, pattern, string.Empty);
        }

        public static string ShowMessage(string strMessage, string strMessageType, string divMessage)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<link href='" + Config.VirtualDir + "style/style.css' rel='Stylesheet' type='text/css' />");
            sb.Append("<script type='text/javascript' language='javascript' src='" + Config.VirtualDir + "adminpanel/assets/js/jquery.js'></script>");
            sb.Append("<script type='text/javascript' language='javascript' src='" + Config.VirtualDir + "adminpanel/assets/js/general.js'></script>");
            sb.Append("<script type='text/javascript'>var virtualDir = '" + Config.VirtualDir + "';</script>");
            sb.Append("$(document.ready(function {");
            sb.Append(Common.ScriptStartTag + "parent.$('#" + divMessage + "').show();" + Common.ScriptEndTag);
            sb.Append(Common.ScriptStartTag + "parent.$('#" + divMessage + "').html('" + strMessage + "');" + Common.ScriptEndTag);
            sb.Append(Common.ScriptStartTag + "parent.$('#" + divMessage + "').attr('class', '" + strMessageType + "');" + Common.ScriptEndTag);
            sb.Append("});");

            return sb.ToString();
        }
        public static void ExportToExcel(string Filename, string Content)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + Filename);
            HttpContext.Current.Response.ContentType = "application/vnd.xls";
            HttpContext.Current.Response.Write(Content.ToString());
            HttpContext.Current.Response.End();
        }
        public static Dictionary<int, string> GetMonthList()
        {
            Dictionary<int, string> months = new Dictionary<int, string>();
            months.Add(1, "January");
            months.Add(2, "February");
            months.Add(3, "March");
            months.Add(4, "April");
            months.Add(5, "May");
            months.Add(6, "June");
            months.Add(7, "July");
            months.Add(8, "August");
            months.Add(9, "September");
            months.Add(10, "October");
            months.Add(11, "November");
            months.Add(12, "December");
            return months;
        }
    } 
}

