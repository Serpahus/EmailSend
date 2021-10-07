using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EmailSend
{
    public class SendService
    {
        public async Task SendEmailAsync (string email ,string theme, string message, string [] addreses)
        {
            
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "login@yandex.ru"));
            emailMessage.To.Add(new MailboxAddress("", (email)));

            for (int i = 0; i < addreses.Length; i++)
            {
                emailMessage.Bcc.Add(new MailboxAddress(addreses[i], addreses[i]));
            }


            emailMessage.Subject = theme;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
              Text = message
            };

            using (var client = new SmtpClient())
            {
                try
                {

                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync("smtp.gmail.com", 465
                        , MailKit.Security.SecureSocketOptions.Auto);

                    // Note: since we don't have an OAuth2 token, disable
                    // the XOAUTH2 authentication mechanism.
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

       
                    await client.AuthenticateAsync("Your_login_from_gmail","Your_Password");
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }

                catch (Exception ex)
                {
                    throw new InvalidOperationException(ex.Message);
                }
            }
        }
    }
}
