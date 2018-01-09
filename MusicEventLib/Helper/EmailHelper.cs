using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace MusicEventLib.Helper
{
    public static class EmailHelper
    {
        public static bool SendEmail(string From, string To, string Subject, string Body, Stream file, string FileName, bool IsBodyHtml)
        {
            bool isSuccess = false;
            string MailUserName = ConfigurationManager.AppSettings["MailUserName"] != null ? ConfigurationManager.AppSettings["MailUserName"].ToString() : "";
            string MailPassword = ConfigurationManager.AppSettings["MailPassword"] != null ? ConfigurationManager.AppSettings["MailPassword"].ToString() : "";
            using (MailMessage mm = new MailMessage(From, To))
            {
                mm.Subject = Subject;
                mm.Body = Body;
                if (file != null)
                {
                    mm.Attachments.Add(new Attachment(file, FileName));
                }
                mm.IsBodyHtml = IsBodyHtml;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential(MailUserName, MailPassword);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                try
                {
                    smtp.Send(mm);
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    isSuccess = false;
                }

                return isSuccess;
            }
        }
    }
}
