using BLL.Services.Interfaces;
using Database;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmailConfirmationService : IEmailConfirmationService
    {
        private readonly IEmailService _emailService;
        private readonly DataContext _context;

        public EmailConfirmationService(IEmailService emailService,DataContext context)
        {
            _emailService = emailService;
            _context = context;
        }
        public async Task SendEmailConfirmation(string emailAddress)
        {
            var code = Guid.NewGuid();
            var email = _context.Emails.First(e => e.EmailAddress == emailAddress);
            var emailConfirmation = new EmailConfirmationCode
            {
                Email = email,
                Id = code
            };
            _context.EmailConfirmationCodes.Add(emailConfirmation);
            await _emailService.SendEmailAsync(emailAddress, "Email confirmation",$"Your confirmation link: {EmailSettings.ConfirmationLink}/{code}");
            await _context.SaveChangesAsync();
        }
        public bool VerifyConfirmationCode(string code)
        {
            var emailCode = _context.EmailConfirmationCodes.Include(e => e.Email).FirstOrDefault(ec => ec.Id.ToString() == code);
            if (emailCode is null) return false;
            var email = _context.Emails.First(e => e.EmailAddress == emailCode.Email.EmailAddress);
            email.IsConfirmed = true;
            _context.SaveChanges();
            return true;
            
        }
    }
}
