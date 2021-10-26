using JuhinAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuhinAPI.Services
{
    public interface IEmailService
    {
        Task Send(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount);
    }
}
