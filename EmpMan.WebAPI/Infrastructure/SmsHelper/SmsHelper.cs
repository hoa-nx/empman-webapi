using EmpMan.Service;
using EmpMan.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Web.Http;
using EmpMan.Common;
using System.Web.Script.Serialization;
using EmpMan.Common.ViewModels;

namespace EmpMan.Web.Infrastructure.SmsHelper
{
    public static class SmsHelper
    {
        static async Task SendSms(string fromNumber , string toNumber , string msg)
        {
            var systemConfig = ServiceFactory.Get<ISystemConfigService>().GetByAccount(HttpContext.Current.User.Identity.Name);

            var empFilterViewModel = new JavaScriptSerializer().Deserialize<EmpFilterViewModel>(systemConfig.EmpFilterDataValue);

            //ma hoa du lieu
            string data1 = StringCipher.Decrypt(empFilterViewModel.systemValue.SidT, CommonConstants.SecKeyString);
            empFilterViewModel.systemValue.SidT = data1;
            string data2 = StringCipher.Decrypt(empFilterViewModel.systemValue.TokT, CommonConstants.SecKeyString);
            empFilterViewModel.systemValue.TokT = data2;
            
            // Your Account SID 
            var accountSid = data1;
            // Your Auth Token 
            var authToken = data2;

            TwilioClient.Init(accountSid, authToken);

            var message = await MessageResource.CreateAsync(to: new PhoneNumber(toNumber),from: new PhoneNumber(fromNumber),body: msg);

        }
    }
}