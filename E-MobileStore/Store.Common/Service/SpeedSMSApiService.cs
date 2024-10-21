using Store.Common.Configuration;
using Store.Common.Model;
using Store.Common.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Store.Common.Service
{
    public class SpeedSMSApiService : ISpeedSMSApiService
    {
        private readonly SpeedSMSAPIConfiguration _speedSMSAPIConfig;
        public SpeedSMSApiService(SpeedSMSAPIConfiguration speedSMSAPIConfig)
        {
            _speedSMSAPIConfig = speedSMSAPIConfig;
        }
        public async Task SendSMS(string toPhoneNumber, string message)
        {
            SpeedSMSAPI api = new SpeedSMSAPI(_speedSMSAPIConfig.ApiAccessToken);
            string[] toPhone = new string[] { toPhoneNumber };
            var response = api.sendSMS(toPhone, message, 2, "");
            api.sendSMS(toPhone, message, 5, "0359584961");
        }
    }
}
