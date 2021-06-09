using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProjectTest.Models
{
  public class Send_Email
  {

    public static Object SendMail(string to, string subject, string msg, string cc=null)
    {
      try
      {

        //msg = msg.Replace("{BaseUrl}", baseUrl);

        string SenderEmailAddress = "syedshamail3.aimsol@gmail.com";
        string SenderEmailPassword = "ghostrider@12345678";
        string SenderSMTPServer = "smtp.gmail.com";
        int Port = 587;
        bool IsSsl = true;
        string DisplayName = "Aim Sol";
        //bool IsLive = 

        MailMessage message = new MailMessage();
        string[] addresses = to.Split(';');
        foreach (string address in addresses)
        {
          message.To.Add(new MailAddress(address));
        }

        if (string.IsNullOrEmpty(cc) == false)
          message.CC.Add(new MailAddress(cc));


        //if (IsLive == false)
        //{

        //}
        //message.bcc.add("saba.aimviz@gmail.com");
        //message.To.Add(new MailAddress("usman.215@hotmail.com"));
        message.From = new MailAddress(SenderEmailAddress, DisplayName);
        message.Subject = subject;
        message.Body = msg;
        message.IsBodyHtml = true;
        SmtpClient client = new SmtpClient();
        client.Host = SenderSMTPServer;
        if (Port > 0)
          client.Port = Port;
        //client.UseDefaultCredentials = false;
        System.Net.NetworkCredential nc = new System.Net.NetworkCredential(SenderEmailAddress, SenderEmailPassword);
        client.EnableSsl = IsSsl;
        client.Credentials = nc;
        client.Send(message);
        return new { notFound = "Email sent", isSuccess = true };
      }
      catch (Exception ex)
      {
        return new { notFound = "Email not sent" + ex.Message };
      }
    }



  }
}
