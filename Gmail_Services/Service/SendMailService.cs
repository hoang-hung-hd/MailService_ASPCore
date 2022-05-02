using MimeKit;
using MailKit.Security;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

public class SendMailService
{
    MailSettings _mailSettings {set;get;}
    public SendMailService(IOptions<MailSettings> mailSettings)
    {
        _mailSettings = mailSettings.Value;
    }
    public async Task<string> SendMail(MailContent mailContent)
    {
        var email = new MimeMessage();
        email.Sender = new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail);
        email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
        email.To.Add(new MailboxAddress(mailContent.DisplayName, mailContent.To));
        email.Subject = mailContent.Subject;

        var builder = new BodyBuilder();
        builder.HtmlBody = mailContent.Body;

        email.Body = builder.ToMessageBody();

        using var smtp = new MailKit.Net.Smtp.SmtpClient();
        try {

            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            return "Send Email Success";

        }catch(Exception ex)
        {
            
            return "Fail" + ex.Message;
        }
        
    }

    
}
public class MailContent{
    public string DisplayName {set;get;}
    public string To {set;get;}
    public string Subject {set;get;}

    public string Body {set;get;}

    
}