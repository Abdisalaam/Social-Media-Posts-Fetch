using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace BrandWatchElaqat.Utilites
{
    public class MailManager
    {
        public static bool SendEmail(string address, string subject, string message)
        {

            try
            {
                
                var email = "qayed2004@hotmail.com";                
                var companyName = "Elaqat";
                var msg = new MailMessage { From = new MailAddress(email, companyName) };
                msg.Subject = subject;
                msg.Body = message + "</br></br></br>" + @"<img  src = ""https://www.alhowaishfarm.com/elaqat/Image/elaqat_logo.png"" style="" width:100%;height:200px; "" />";
                var mServer = "smtp.office365.com";
                var mServerPort = 578;
                var mServerUserId = "qayed2004@hotmail.com";
                var mServerPassword = "Malik@123";
                msg.To.Add(new MailAddress(address));
                msg.IsBodyHtml = true;
                var loginInfo = new NetworkCredential(mServerUserId, mServerPassword);
                var smtpClient = new SmtpClient
                {
                    UseDefaultCredentials = false,
                    Credentials = loginInfo,
                    Port = mServerPort,
                    Host = mServer,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true
                    
                };
                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {
                var e = ex.Message;
                return false;
            }
            return true;
        }
        public static bool SendEmailWithAttachment(string address, string subject, string message, string PathAttach)
        {

            try
            {

                var email = "a.ahmed@elaqat.com.sa";
                var companyName = "Elaqat";
                var msg = new MailMessage { From = new MailAddress(email, companyName) };
                msg.Subject = subject;
                msg.Body = message + "</br></br></br>" + @"<img  src = ""https://www.alhowaishfarm.com/elaqat/Image/elaqat_logo.png"" style="" width:100%;height:200px; "" />";
                var mServer = "smtp.office365.com";
                var mServerPort = 587;
                var mServerUserId = "a.ahmed@elaqat.com.sa";
                var mServerPassword = "QayedMalik1979@";
                msg.To.Add(new MailAddress(address));
                Attachment data = new Attachment(PathAttach, MediaTypeNames.Application.Octet);
                msg.Attachments.Add(data);
                msg.IsBodyHtml = true;
                var loginInfo = new NetworkCredential(mServerUserId, mServerPassword);
                var smtpClient = new SmtpClient
                {
                    Host = mServer,
                    Port = mServerPort,
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = loginInfo
                };
                smtpClient.Send(msg);
            }
            catch (Exception ex)
            {
                var e = ex.Message;
                return false;
            }
            return true;
        }


    }
}