using BA.WesternSiding.DataModels;
using BA.WesternSiding.Configuration;
using BA.Common.Models;
using BA.Common.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BA.WesternSiding.Adapters
{
    public class ContactUsAdapter
    {
        private readonly IConfiguration _config;
        private readonly EmailConfigurationModel _emailConfiguration;
        private readonly EmailTemplateModel _emailTemplate;
        private readonly ISmtpService _smtpService;

        public EmailServerConfigModel ServerConfig { get; private set; }
        public EmailMessageModel Message { get; private set; }

        public ContactUsAdapter(IConfiguration config, ISmtpService smtpService)
        {
            _config = config;
            _emailConfiguration = _config.GetSection("EmailConfiguration").Get<EmailConfigurationModel>();
            _emailTemplate = _config.GetSection("EmailTemplates:ContactUs").Get<EmailTemplateModel>();
            _smtpService = smtpService;
        }

        public bool CreateAndSendEmail(ContactUsModel contactUs)
        {
            try
            {
                //Get Template
                string body = GetEmailTemplate();

                //Get Subject from Body
                string messageSubject = GetEmailSubject(body);

                //Replace Tokens
                string messageBody = ReplaceTokens(body, contactUs);

                //MAP ContactUsAdapter to SMTPService
                EmailAddressModel fromAddress = new EmailAddressModel(contactUs.Name, contactUs.Email);
                List<EmailAddressModel> toAddresses = new List<EmailAddressModel>()
                {
                    new EmailAddressModel(_emailConfiguration.ToName, _emailConfiguration.DefaultToAddress)
                };

                List<EmailAddressModel> ccAddresses = new List<EmailAddressModel>()
                {
                    new EmailAddressModel("Support", "support@shawnwbailey.com")
                };

                Message = new EmailMessageModel(fromAddress, toAddresses, ccAddresses, messageSubject, messageBody, _emailTemplate.TemplateType);
                ServerConfig = new EmailServerConfigModel(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, _emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
            }
            catch(Exception e)
            {
                throw new Exception("ContactUsAdapter.Create", e);
            }

            return (SendMail());

        }

        private string GetEmailTemplate()
        {
            string body = null;
            FileStream file = new FileStream(string.Format(@"Templates\\{0}", _emailTemplate.TemplateName), FileMode.Open);
            using(StreamReader reader = new StreamReader(file))
            {
                body = reader.ReadToEnd();
            }
            return (body);
        }

        private string GetEmailSubject(string body)
        {
            Match match = Regex.Match(body, @"<title>\s*(.+?)\s*</title>*");
            if(match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                return string.Empty;
            }
        }

        private string ReplaceTokens(string body, ContactUsModel contact)
        {
            string _body = body.Replace(@"[Name]", contact.Name)
                .Replace(@"[Email]", contact.Email)
                .Replace(@"[Phone]", contact.Phone)
                .Replace(@"[Referral]", contact.Referral)
                .Replace(@"[Comments]", contact.Comments);

            return _body;
        }

        private bool SendMail()
        {
            try
            {
                return(_smtpService.SendMail(Message, ServerConfig));
            }
            catch(Exception e)
            {
                throw new Exception("ContactUsAdapter failed to send email", e);
            }
        }
    }
}
