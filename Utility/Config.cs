namespace Utility
{
    using System;
    using System.Configuration;

    public class Config
    {
        public static string PageNotFoundUrl = ConfigurationManager.AppSettings["PageNotFoundUrl"].ToString();
        public static readonly string Banner = ConfigurationManager.AppSettings["Banner"];
        public static readonly string CMSFiles = ConfigurationManager.AppSettings["CMSFiles"];
        public static string EncryptKey = ConfigurationManager.AppSettings["EncryptKey"].ToString();
        public static string IsVirtualDirSlash = ConfigurationManager.AppSettings["IsVirtualDirSlash"].ToString();
        public static readonly string sponsers = ConfigurationManager.AppSettings["sponsers"];
        public static readonly string TimeZone = ConfigurationManager.AppSettings["TimeZone"];
        public static string VirtualDir = ConfigurationManager.AppSettings["VirtualDir"].ToString();
        public static string WebSiteUrl = ConfigurationManager.AppSettings["WebSiteUrl"].ToString();
        public static string WebsiteName = ConfigurationManager.AppSettings["WebsiteName"].ToString();
        public static string Attachments = ConfigurationManager.AppSettings["Attachments"].ToString();

        public static string GoogleClientID = ConfigurationManager.AppSettings["GoogleClientID"].ToString();
        public static string GoogleClientSecret = ConfigurationManager.AppSettings["GoogleClientSecret"].ToString();
        public static string GoogleAuthURL = ConfigurationManager.AppSettings["GoogleAuthURL"].ToString();
        
    }
}

