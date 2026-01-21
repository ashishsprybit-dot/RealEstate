namespace Utility
{
    using System;
    using System.Text.RegularExpressions;

    public static class Validation
    {
        public static string CompareField(string strFirstField, string strSecondField, string strErrorMsg)
        {
            if (strSecondField != strFirstField)
            {
                return strErrorMsg;
            }
            return string.Empty;
        }

        public static string CompareFieldLength(string strField, int intLength, CompareType objCompare, string strErrorMsg, bool IgnoreIfBlank)
        {
            if (!IgnoreIfBlank || (strField.Length != 0))
            {
                switch (objCompare)
                {
                    case CompareType.Equal:
                        if (strField.Length == intLength)
                        {
                            break;
                        }
                        return strErrorMsg;

                    case CompareType.LessThanEqual:
                        if (strField.Length <= intLength)
                        {
                            break;
                        }
                        return strErrorMsg;

                    case CompareType.LessThan:
                        if (strField.Length < intLength)
                        {
                            break;
                        }
                        return strErrorMsg;

                    case CompareType.GreaterThanEqual:
                        if (strField.Length >= intLength)
                        {
                            break;
                        }
                        return strErrorMsg;

                    case CompareType.GreaterThan:
                        if (strField.Length > intLength)
                        {
                            break;
                        }
                        return strErrorMsg;
                }
            }
            return string.Empty;
        }

        public static string EmailID(string strFieldValue, string strErrorMsg)
        {
            if (IsEmail(strFieldValue))
            {
                return string.Empty;
            }
            return strErrorMsg;
        }

        public static string GenerateErrorMessage(string[] strErrMsg, string strConcateString)
        {
            string str = string.Empty;
            for (int i = 0; i < strErrMsg.Length; i++)
            {
                if (!string.IsNullOrEmpty(strErrMsg[i]))
                {
                    if (str.Length == 0)
                    {
                        str = strErrMsg[i];
                    }
                    else
                    {
                        str = str + strConcateString + strErrMsg[i];
                    }
                }
            }
            if (str.Length != 0)
            {
                str = "Please correct the following error(s).<br/> - " + str;
            }
            return str;
        }

        public static string GenerateErrorMessageIncludeHeader(string strErrMsg)
        {
            return ("Please correct the following error(s).<br/>" + strErrMsg);
        }

        public static bool IsEmail(string strEmail)
        {
            string pattern = @"^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\.([a-z][a-z|0-9]*(\.[a-z][a-z|0-9]*)?)$";
            return Regex.Match(strEmail.Trim(), pattern, RegexOptions.IgnoreCase).Success;
        }

        public static bool IsURL(string strURL)
        {
            string pattern = @"((https?|ftp|gopher|telnet|file|notes|ms-help):((//)|(\\\\))+[\w\d:#@%/;$()~_?\+-=\\\.&]*)";
            return Regex.Match(strURL.Trim(), pattern, RegexOptions.IgnoreCase).Success;
        }

        public static string PasswordCheck(string strFirstCtrl, string strSecondCtrl)
        {
            string str = "Password and confirm password must be same.";
            if (!(strFirstCtrl == strSecondCtrl))
            {
                return str;
            }
            if (strFirstCtrl.Length >= 6)
            {
                int num = 0;
                int num2 = 0;
                for (int i = 0; i < strFirstCtrl.Length; i++)
                {
                    if (char.IsDigit(strFirstCtrl[i]))
                    {
                        num++;
                    }
                    else if (char.IsLetter(strFirstCtrl[i]))
                    {
                        num2++;
                    }
                }
                if ((num > 0) && (num2 > 0))
                {
                    return string.Empty;
                }
            }
            return "Password must be at least 6 characters long and should contain at least one number and a letter.";
        }

        public static bool RequireField(string strFieldValue)
        {
            if (strFieldValue.Trim().Length == 0)
            {
                return false;
            }
            return true;
        }

        public static string RequireField(string strFieldValue, string strErrorMsg)
        {
            if (strFieldValue == null)
            {
                return strErrorMsg;
            }
            if (strFieldValue.Trim().Length == 0)
            {
                return strErrorMsg;
            }
            return string.Empty;
        }

        public static string RequireField(string strFieldValue, string strDefaultValue, string strErrorMsg)
        {
            if ((strFieldValue.Trim().Length == 0) || (strDefaultValue == strFieldValue))
            {
                return strErrorMsg;
            }
            return string.Empty;
        }

        public static bool RequireFieldAndEmailID(string strFieldValue)
        {
            bool flag = false;
            flag = RequireField(strFieldValue);
            if (flag)
            {
                flag = IsEmail(strFieldValue);
            }
            return flag;
        }

        public static string RequireFieldAndEmailID(string strFieldValue, string strErrorMsg)
        {
            string str = string.Empty;
            str = RequireField(strFieldValue, strErrorMsg);
            if ((str.Length == 0) && !IsEmail(strFieldValue))
            {
                str = "Invalid " + strErrorMsg;
            }
            return str;
        }

        public static string RequireFieldAndEmailID(string strFieldValue, string strDefaultValue, string strErrorMsg)
        {
            string str = string.Empty;
            str = RequireField(strFieldValue, strDefaultValue, strErrorMsg);
            if ((str.Length == 0) && !IsEmail(strFieldValue))
            {
                str = strErrorMsg;
            }
            return str;
        }

        public static string RequireFieldAndURL(string strFieldValue, string strErrorMsg)
        {
            string str = string.Empty;
            str = RequireField(strFieldValue, strErrorMsg);
            if ((str.Length == 0) && !IsURL(strFieldValue))
            {
                str = "Invalid " + strErrorMsg;
            }
            return str;
        }

        public static string RequireURL(string strFieldValue, string strErrorMsg)
        {
            string str = string.Empty;
            if (!IsURL(strFieldValue))
            {
                str = "Invalid " + strErrorMsg;
            }
            return str;
        }

        public enum CompareType
        {
            Equal,
            LessThanEqual,
            LessThan,
            GreaterThanEqual,
            GreaterThan
        }

        public enum ValidtionType
        {
            RequireField,
            RequireFieldAndEmailID,
            EmailID,
            URL,
            PasswordCheck
        }
    }
}

