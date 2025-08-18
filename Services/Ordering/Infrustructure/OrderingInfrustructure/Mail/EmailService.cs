
using OrderingApplication.Contracts.Infrastructure;
using OrderingApplication.Models;

namespace OrderingInfrastructure.Mail;

public class EmailService : IEmailService
{
    public async Task<bool> SendEmailAsync(Email email)
    {
        return true;
    }
}
