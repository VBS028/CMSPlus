using CMSPlus.Domain.Dtos;
using CMSPlus.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CMSPlus.Domain.Interfaces.Factories
{
    public interface IEmailMessageFactory
    {
        MailMessage CreateMailMessage(string email, string subject, string htmlMessage);

        MailMessage CreateMailMessageWithAttachments(string emails, string subject, string htmlMessage,
            string attachmentPath);
    }
}