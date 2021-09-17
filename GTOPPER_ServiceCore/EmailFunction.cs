using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using MimeKit;
using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace GTOPPER_ClassLibrary
{
    public class EmailFunction
    {
        
        /*
            nuget package MailKit
            currently no attachement used and no body template
         */





        public   async Task<bool> SendEmail(string email,string filepath )
        {
            try
            {
                
                MimeMessage msg = new MimeMessage();
                MailboxAddress from = new MailboxAddress("Test","gtIT@goldentopper.com");

                msg.From.Add(from);

                MailboxAddress to = new MailboxAddress("user", email);

                msg.To.Add(to);
                msg.Subject = "Test Email Send";
                SmtpClient client = new SmtpClient();
                client.Connect("mail.goldentopper.com", 465, true);
                client.Authenticate("syrle.f@goldentopper.com", "7Qud[Ex6zYF3");
                //SmtpClient client = new SmtpClient()
                //{
                //    Host = "mail.goldentopper.com",
                //    Port = 465,
                //    EnableSsl = true,
                //    DeliveryMethod = SmtpDeliveryMethod.Network,
                //    UseDefaultCredentials = false,
                   
                //    Credentials = new NetworkCredential()
                //    {
                //        UserName = "syrle.f@goldentopper.com",
                //        Password = "7Qud[Ex6zYF3"
                //    },

                //};

                //get current directory then go to QR then filename

                //var filepath = HttpContext.Current.Server.MapPath("~/Images/" + id + ".jpeg");

                //Attachment fileAttach;
                //fileAttach = new Attachment(file path);
                //MailAddress mailFrom = new MailAddress("syrle.f@goldentopper.com", "TestMail");
                //MailAddress mailTo = new MailAddress(email);
                //MailMessage msg = new MailMessage()
                //{
                //    From = mailFrom,
                //    Subject = "Citilion Properties.",
                //    IsBodyHtml = true

                //};
                ////msg.AlternateViews.Add(SetEmailTemplate( filepath));
                ////msg.Attachments.Add(fileAttach);
                //msg.To.Add(mailTo);

                try
                {
                      client.Send(msg);
                    client.Dispose();

                    return true;
                }
                catch (Exception ex)
                {
                    client.Dispose();
                    System.Console.WriteLine(ex);
                    return false;
                }

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                return false;
            }
        }



//        public static  AlternateView SetEmailTemplate( string filepath)
//        {
//            //var currDirectory = Directory.GetCurrentDirectory();
//            //var filepath = Path.Combine(currDirectory, "ClientApp", "src", "QR", id + ".png");
//            //var filepath = HttpContext.Current.Server.MapPath("~/Images/" + id + ".jpeg");
//            LinkedResource res = new LinkedResource(filepath);
//            res.ContentId = Guid.NewGuid().ToString();

//            string htmlBody = htmlEmail(res);
//            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
//            alternateView.LinkedResources.Add(res);
//            return alternateView;
//        }

//        public static string htmlEmail(LinkedResource res)
//        {

//            string email = "<!DOCTYPE html>" +
//"<html lang='en'> " +
//"<head>" +
//"    <meta charset='UTF-8'>" +
//"    <meta http-equiv='X-UA-Compatible' content='IE=edge'>" +
//"    <meta name='viewport' content='width=device-width, initial-scale=1.0'>" +
//"    <title>Document</title>" +
//"</head>" +
//"<body style='text-align:center;font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;" +
//"background-color: rgb(236, 233, 233); padding-left: 5%; padding-right: 5%; '>" +
//"   <table style='margin-left:auto;margin-right:auto;max-width:300px;padding: 0px;background-color: white; border:1px solid #cccc'>" +
//"       <tr style='padding:0px; margin:0px;'>" +
//"           <td style='padding:0px;margin:0px;'>" +
//"    <table style='padding:0px;margin:0px; box-shadow: 0px 0px 8px 4px #cccc;'>" +
//"        <tbody style='margin:0px; padding:0px; border: 1px solid rgb(184, 180, 180) initial;'>" +
//"            <tr style='height:80px; background-color:#00CFFB'><td></td></tr>" +
//"            <tr style='height:200px; background-color:white; '>" +
//"                <td style='padding-top: 0px;margin: 0px;'>" +
//"                <table style='margin:0px;'>" +
//"                    <tr style='height:20px;'>" +
//"                        <td style='text-align:center'> " +
//"                            <h1><b style='color: #069ec0a1; '>Congratulations</b> " +
//"                            </h1>" +
//"                        </td>" +
//"                    </tr>" +
//"                    <tr>" +
//"                        <td style='text-align:center; align-content:center;'>" +
//"                            <p style='margin-left: 5px; margin-right: 5px;'>You are now fully registered and you can now use your QR Code when entering the any<b>Golden Topper Properties premise</b> </p>" +
//@"<img src='cid:" + res.ContentId + @"'/>" +
//"                        </td>" +
//"                    </tr>" +
//"                </table>" +
//"            </td></tr>" +
//"            <tr style='height:50px; background-color:#00CFFB;color:white'><td><p>Please do not reply on this email</p></td></tr> " +
//"        </tbody>" +
//"    </table>" +
//"</td>" +
//"</tr>" +
//"</table>" +
//"</body> " +
//"</html>";
//            return email;
//        }
    }
}
