using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDI.Data.Models;

namespace TDI.Application.Interfaces
{
    public interface IEmailSender
    {
        EmailSettings EmailSettings { get; set; }
        Task SendEmailAsync(string email, string subject, string message, List<string> cc = null);
        Task SendEmailAsync(string subject, string message, List<string> to, List<string> cc);
    }
}
