using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using application_programming_interface.Models;

namespace application_programming_interface.Services
{
    public interface IMailService
    {
        Task SendEmail(MailRequest mailRequest);
    }
}
