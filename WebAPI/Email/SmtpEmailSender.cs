﻿using Application.Interfaces;
using System.Net.Mail;
using System.Net;

namespace WebAPI.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        private string host;
        private int port;
        private string from;
        private string password;

        public SmtpEmailSender(string host, int port, string from, string password)
        {
            this.host = host;
            this.port = port;
            this.from = from;
            this.password = password;
        }

        public string ToEmail { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }

        public void Send()
        {
            var smtp = new SmtpClient
            {
                Host = this.host,
                Port = this.port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(this.from, this.password)
            };

            using (var message = new MailMessage(this.from, ToEmail)
            {
                Subject = Subject,
                Body = Body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
