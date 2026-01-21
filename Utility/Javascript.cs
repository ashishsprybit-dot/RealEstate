namespace Utility
{
    using System;

    public class Javascript
    {
        public static string AdminSectionMoveToListPage(string strPageName, bool blnIncludeScriptTag)
        {
            if (blnIncludeScriptTag)
            {
                return ("<script>window.setTimeout(\"window.location.href='" + strPageName + "'\",3000);</script>");
            }
            return ("window.setTimeout(\"window.location.href='" + strPageName + "'\",3000);");
        }

        public static string DisplayMsg(string strCtrlName, string strMessage, MessageType objMessageType, bool blnIncludeScriptTag)
        {
            string str = string.Empty;
            switch (objMessageType)
            {
                case MessageType.Success:
                    str = "alert-message success";
                    break;

                case MessageType.Error:
                    str = "alert-message error";
                    break;
            }
            if (blnIncludeScriptTag)
            {
                return ("<script>DisplMsg('" + strCtrlName + "','" + strMessage.Replace("'", @"\'") + "','" + str + "');</script>");
            }
            return ("DisplMsg('" + strCtrlName + "','" + strMessage.Replace("'", @"\'") + "','" + str + "');");
        }

        public static string DisplayMsgAdmin(string strCtrlName, string strMessage, MessageType objMessageType, bool blnIncludeScriptTag)
        {
            string str = string.Empty;
            switch (objMessageType)
            {
                case MessageType.Success:
                    str = "alert-message success";
                    break;

                case MessageType.Error:
                    str = "alert-message error";
                    break;

                case MessageType.Alert:
                    str = "alert-message";
                    break;
            }
            if (blnIncludeScriptTag)
            {
                return ("<script>DisplMsg('" + strCtrlName + "','" + strMessage.Replace("'", "''") + "','" + str + "');</script>");
            }
            return ("DisplMsg('" + strCtrlName + "','" + strMessage.Replace("'", "''") + "','" + str + "');");
        }

        public static string DisplayMsgFront(string strCtrlName, string strMessage, MessageType objMessageType, bool blnIncludeScriptTag)
        {
            string str = string.Empty;
            switch (objMessageType)
            {
                case MessageType.Success:
                    str = "Thank you";
                    break;

                case MessageType.Error:
                    str = "Error Message";
                    break;

                case MessageType.Alert:
                    str = "Alert Message";
                    break;

                case MessageType.None:
                    str = string.Empty;
                    break;
            }
            if (blnIncludeScriptTag)
            {
                return ("<script>msgbox('" + strMessage.Replace("'", @"\'") + "','" + str + "');</script>");
            }
            return ("msgbox('" + strMessage.Replace("'", @"\'") + "','" + str + "');");
        }

        public static string DisplayMsgTop(string strMessage, MessageType objMessageType, bool blnIncludeScriptTag)
        {
            string str = string.Empty;
            switch (objMessageType)
            {
                case MessageType.Success:
                    str = "success";
                    break;

                case MessageType.Error:
                    str = "error";
                    break;

                case MessageType.Alert:
                    str = "alert";
                    break;

                case MessageType.None:
                    str = string.Empty;
                    break;
            }
            if (blnIncludeScriptTag)
            {
                return ("<script>msgboxTop('" + strMessage.Replace("'", @"\'") + "','" + str + "');</script>");
            }
            return ("msgboxTop('" + strMessage.Replace("'", @"\'") + "','" + str + "');");
        }

        public static string RedirectToPage(string strPath, bool blnIncludeScriptTag)
        {
            if (blnIncludeScriptTag)
            {
                return ("<script>window.location.href='" + strPath + "';</script>");
            }
            return ("window.location.href='" + strPath + "';");
        }

        public static string ScriptEndTag
        {
            get
            {
                return "</script>";
            }
        }

        public static string ScriptStartTag
        {
            get
            {
                return "<script>";
            }
        }

        public enum MessageType
        {
            Success,
            Error,
            Alert,
            None
        }
    }
}

