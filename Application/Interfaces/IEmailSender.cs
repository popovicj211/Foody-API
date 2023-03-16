using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEmailSender
    {
        string ToEmail { get; set; }
        string Body { get; set; }
        string Subject { get; set; }
        void Send();
    }
}
