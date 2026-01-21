using System;
using System.Data;
using BAL;
using DAL;
using System.Net.Mail;

namespace BAL
{

    public class GeneralSettings
    {

        public readonly string MetaTitle;
        public readonly string MetaKeywords;
        public readonly string MetaDescription;
        public readonly string AnalyticCode;
        public readonly string DateFormat;
        public readonly string GST;
        public readonly string AdminEmail;
        public readonly string InfoEmail;
        public readonly string Password;
        public readonly string HostName;
        public readonly string PrintInvoiceAddress;
        public readonly string FooterText;

        public readonly string Currency;
        public readonly string PayPal;
        public readonly string BankDetails;
        public readonly string FacebookLink;
        public readonly string TwitterLink;
        public readonly string FlicekerLink;
        public readonly string YoutubeLink;
        public readonly string SiteLink;
        public readonly string BlogLink;
        public readonly string TradingHrs;
        public readonly string ContactUs;
        public readonly string LinkedinLink;
        public readonly string HeaderContactNo;

        public readonly string keyword;

        public readonly string value;

        public static GeneralSettings GS = new GeneralSettings();

        public GeneralSettings()
        {


            DataTable dt = new DataTable();
            GeneralBAL objGeneralBAL = new GeneralBAL();
            dt = objGeneralBAL.GetList();




            MetaTitle = "Richmond";
            MetaKeywords = "Richmond";
            MetaDescription = "Richmond";
            AnalyticCode = "Richmond";
            DateFormat = "Richmond";
            GST = "Richmond";
            AdminEmail = "Richmond";
            InfoEmail = "Richmond";
            Password = "Richmond";
            HostName = "Richmond";
            PrintInvoiceAddress = "Richmond";
            FooterText = "Richmond";

            Currency = "Richmond";
            PayPal = "Richmond";
            BankDetails = "Richmond";
            FacebookLink = "Richmond";
            TwitterLink = "Richmond";
            FlicekerLink = "Richmond";
            FlicekerLink = "Richmond";
            YoutubeLink = "Richmond";
            SiteLink = "Richmond";
            BlogLink = "Richmond";
            TradingHrs = "Richmond";
            ContactUs = "Richmond";



            if (dt.Rows.Count > 0)
            {
                keyword = System.Convert.ToString(dt.Rows[0]["keyword"]);

                value = System.Convert.ToString(dt.Rows[0]["value"]);

                //MetaTitle = System.Convert.ToString(dt.Rows[0]["MetaTitle"]);
                //MetaKeywords = System.Convert.ToString(dt.Rows[0]["MetaKeywords"]);
                //MetaDescription = System.Convert.ToString(dt.Rows[0]["MetaDescription"]);
                //AnalyticCode = System.Convert.ToString(dt.Rows[0]["AnalyticCode"]);
                //DateFormat = System.Convert.ToString(dt.Rows[0]["DateFormat"]);
                //GST = System.Convert.ToString(dt.Rows[0]["GST"]);
                //AdminEmail = System.Convert.ToString(dt.Rows[0]["AdminEmail"]);
                //InfoEmail = System.Convert.ToString(dt.Rows[0]["InfoEmail"]);
                //Password = System.Convert.ToString(dt.Rows[0]["Password"]);
                //HostName = System.Convert.ToString(dt.Rows[0]["HostName"]);
                //PrintInvoiceAddress = System.Convert.ToString(dt.Rows[0]["PrintInvoiceAddress"]);
                //FooterText = System.Convert.ToString(dt.Rows[0]["FooterText"]);

                //Currency = System.Convert.ToString(dt.Rows[0]["Currency"]);
                //PayPal = System.Convert.ToString(dt.Rows[0]["PayPal"]);
                //BankDetails = System.Convert.ToString(dt.Rows[0]["BankDetails"]);
                //FacebookLink = System.Convert.ToString(dt.Rows[0]["FacebookLink"]);
                //TwitterLink = System.Convert.ToString(dt.Rows[0]["TwitterLink"]);
                //FlicekerLink = System.Convert.ToString(dt.Rows[0]["FlicekerLink"]);
                //FlicekerLink = System.Convert.ToString(dt.Rows[0]["FlicekerLink"]);
                //YoutubeLink = System.Convert.ToString(dt.Rows[0]["YoutubeLink"]);
                //SiteLink = System.Convert.ToString(dt.Rows[0]["SiteLink"]);
                //BlogLink = System.Convert.ToString(dt.Rows[0]["BlogLink"]);
                //TradingHrs = System.Convert.ToString(dt.Rows[0]["TradingHrs"]);
                //ContactUs = System.Convert.ToString(dt.Rows[0]["ContactUs"]);

                //LinkedinLink = System.Convert.ToString(dt.Rows[0]["LinkedinLink"]);
                //HeaderContactNo = System.Convert.ToString(dt.Rows[0]["HeaderContactNo"]);


            }
        }

        public DateTime UTCToLocalDate(DateTime UTCDate)
        {
            DateTime DateLocal = TimeZoneInfo.ConvertTimeFromUtc(UTCDate, TimeZoneInfo.FindSystemTimeZoneById(Utility.Config.TimeZone));
            return DateLocal;
        }

        public string getConfigValue(string var)
        {
            string configValue = string.Empty;
            GeneralBAL obj_general = new GeneralBAL();
            using (DataTable dt = obj_general.selectByKeyword(var))
            {
                if (dt != null)
                {
                    configValue = Convert.ToString(dt.Rows[0]["value"] ?? string.Empty);
                }
            }
            return configValue;
        }


        public static bool SendEmail(string strTo, string strFrom, string strSubject, string strBody)
        {
            bool flag = false;
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient client = new SmtpClient();
                message.To.Add(strTo);
                message.From = new MailAddress(strFrom);
                message.Subject = strSubject;
                message.Body = strBody;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.Normal;

                client.Host = new GeneralSettings().getConfigValue("CLTSmtpHost"); //GetSmtpServer();
                client.Port = Convert.ToInt32(new GeneralSettings().getConfigValue("CLTSmtpPort"));
                client.EnableSsl = Convert.ToBoolean(new GeneralSettings().getConfigValue("CLTEnableSSL"));
                client.Credentials = new System.Net.NetworkCredential(new GeneralSettings().getConfigValue("CLTSmtpUsername"), new GeneralSettings().getConfigValue("CLTSmtpPassword"));
                client.Send(message);
                flag = true;
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }

        public static bool SendEmail_AttachFile(string strTo, string strFrom, string strSubject, string strBody, string AFileName)
        {
            bool flag = false;
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient client = new SmtpClient();
                message.To.Add(strTo);
                message.From = new MailAddress(strFrom);
                message.Subject = strSubject;
                message.Body = strBody;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.Normal;

                if (AFileName != string.Empty)
                {
                    Attachment attachment = new Attachment(AFileName);
                    //MailAttachment attachment = new MailAttachment(AFileName);
                    message.Attachments.Add(attachment);
                }
                client.Host = new GeneralSettings().getConfigValue("CLTSmtpHost"); //GetSmtpServer();
                client.Port = Convert.ToInt32(new GeneralSettings().getConfigValue("CLTSmtpPort"));
                client.EnableSsl = Convert.ToBoolean(new GeneralSettings().getConfigValue("CLTEnableSSL"));
                client.Credentials = new System.Net.NetworkCredential(new GeneralSettings().getConfigValue("CLTSmtpUsername"), new GeneralSettings().getConfigValue("CLTSmtpPassword"));
                client.Send(message);
                flag = true;
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }


    }
}


