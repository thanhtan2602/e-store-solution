using Store.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Common.Service.Interfaces
{
    public interface IEmailSenderService
    {
        void SendEmail(Message message);
    }
}
