
using OrderingApplication.Models;

namespace OrderingApplication.Contracts.Infrastructure;

public interface IEmailService
{
    Task<bool> SendEmailAsync(Email email);
}
