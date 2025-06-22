using ARC.Application.Abstractions.Services;
using ARC.Domain.Entities;

namespace ARC.Infrastructure.Email
{
    /// <summary>
    /// Service for sending user-related emails using templates
    /// </summary>
    public class UserEmailService : IUserEmailService
    {
        private readonly IEmailService _emailService;

        public UserEmailService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
        {
            var message = new EmailMessage(
                "Please Confirm Your Email Address",
                "EmailConfirmation",
                new[]
                {
                    new Placeholder("UserName", user.UserName ?? user.Email),
                    new Placeholder("ConfirmationLink", confirmationLink)
                },
                new EmailAddress("noreply@arc.com", "ARC Team"),
                new[] { (EmailAddress)email }
            );

            await _emailService.SendAsync(message);
        }

        public async Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
        {
            var message = new EmailMessage(
                "Reset Your Password",
                "PasswordReset",
                new[]
                {
                    new Placeholder("UserName", user.UserName ?? user.Email),
                    new Placeholder("ResetLink", resetLink)
                },
                new EmailAddress("noreply@arc.com", "ARC Team"),
                new[] { (EmailAddress)email }
            );

            await _emailService.SendAsync(message);
        }
    }
} 