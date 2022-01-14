using System.Net;
using System.Net.Mail;

namespace Infinity_States.Services;

public abstract class EmailNotificationSender : IEmailNotificationSender
{
    public abstract string MailName { get; set; }
    public abstract string SenderMail { get; set; }
    public abstract string SenderMailPassword { get; set; }
    public abstract string RecipientMail { get; set; }
    public abstract string Subject { get; set; }
    public abstract string Body { get; set; }
    public virtual bool IsBodyHTML => false;
    public abstract string SmtpLink { get; set; }
    public virtual int SmtpPort => 587;
    public virtual bool IsSmtpSSL => true;

    public EmailNotificationSender()
    {
        this.MailName = MailName;
        this.SenderMail = SenderMail;
        this.SenderMailPassword = SenderMailPassword;
        this.RecipientMail = RecipientMail;
        this.Subject = Subject;
        this.Body = Body;
    }

    public virtual void Send()
    {
        MailAddress senderAddress = new MailAddress(this.SenderMail, this.MailName);
        MailAddress recipientAddress = new MailAddress(this.RecipientMail);

        MailMessage mailMessage = new MailMessage(senderAddress, recipientAddress)
        {
            Subject = this.Subject,
            Body = this.Body,
            IsBodyHtml = this.IsBodyHTML
        };

        SmtpClient smtpClient = new SmtpClient(SmtpLink, SmtpPort) 
        { 
            Credentials = new NetworkCredential(this.SenderMail, this.SenderMailPassword),
            EnableSsl = IsSmtpSSL
        };
        
        smtpClient.Send(mailMessage);
    }
}

public class SupportMailNotification : EmailNotificationSender, IEmailNotificationSender
{
    public override string MailName { get; set; }
    public override string SenderMail { get; set; }
    public override string SenderMailPassword { get; set; }
    public override string RecipientMail { get; set; }
    public override string Subject { get; set; }
    public override string Body { get; set; }
    public override bool IsBodyHTML => true;
    public override string SmtpLink { get; set; }
    public override int SmtpPort => base.SmtpPort;
    public override bool IsSmtpSSL => base.IsSmtpSSL;

    public SupportMailNotification() : base() {}

    public override void Send() 
    {
        base.Send();
    }
}

public class SecurityMailNotification : EmailNotificationSender, IEmailNotificationSender
{
    public override string MailName { get; set; }
    public override string SenderMail { get; set; }
    public override string SenderMailPassword { get; set; }
    public override string RecipientMail { get; set; }
    public override string Subject { get; set; }
    public override string Body { get; set; }
    public override bool IsBodyHTML => true;
    public override string SmtpLink { get; set; }
    public override int SmtpPort => base.SmtpPort;
    public override bool IsSmtpSSL => base.IsSmtpSSL;

    public SecurityMailNotification() : base() { }

    public override void Send()
    {
        base.Send();
    }
}

public class AccountMailNotification : EmailNotificationSender, IEmailNotificationSender
{
    public override string MailName { get; set; }
    public override string SenderMail { get; set; }
    public override string SenderMailPassword { get; set; }
    public override string RecipientMail { get; set; }
    public override string Subject { get; set; }
    public override string Body { get; set; }
    public override bool IsBodyHTML => true;
    public override string SmtpLink { get; set; }
    public override int SmtpPort => base.SmtpPort;
    public override bool IsSmtpSSL => base.IsSmtpSSL;

    public AccountMailNotification() : base() { }

    public override void Send()
    {
        base.Send();
    }
}