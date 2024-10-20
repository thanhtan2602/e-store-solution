using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Common.Service.Interfaces
{
    public interface ISpeedSMSApiService
    {
        Task SendSMS(string toPhoneNumber, string message);
    }
}
