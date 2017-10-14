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
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using EmpMan.Data.Infrastructure;
using EmpMan.Data.Repositories;
using EmpMan.Model.Models;

namespace EmpMan.Web.Infrastructure.SmsHelper
{
    public static class SmsHelper
    {
        private struct SpeedSmsCredential
        {
            public string token;
            public string pw;
        }

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

        public static void SendeSms(string fromNumber, string toNumber, string msg)
        {
            string api = "http://rest.esms.vn/MainService.svc/json/SendMultipleMessage_V4_get?Phone={Phone}&Content={Content}&ApiKey={ApiKey}&SecretKey={SecretKey}&IsUnicode={IsUnicode}&SmsType={SmsType}";

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

            api = api.Replace("{Phone}", toNumber);
            api = api.Replace("{Content}", msg);
            api = api.Replace("{ApiKey}", "***");
            api = api.Replace("{SecretKey}", "****");
            api = api.Replace("{IsUnicode}", "0");
            api = api.Replace("{SmsType}", "4");

            string result = SendGetRequest(api);
            JObject ojb = JObject.Parse(result);
            int CodeResult = (int)ojb["CodeResult"];//100 is successfull

            string SMSID = (string)ojb["SMSID"];//id of SMS

        }

        private static string SendGetRequest(string RequestUrl)
        {
            Uri address = new Uri(RequestUrl);
            HttpWebRequest request;
            HttpWebResponse response = null;
            StreamReader reader;
            if (address == null) { throw new ArgumentNullException("address"); }
            try
            {
                request = WebRequest.Create(address) as HttpWebRequest;
                request.UserAgent = ".NET Sample";
                request.KeepAlive = false;
                request.Timeout = 15 * 1000;
                response = request.GetResponse() as HttpWebResponse;
                if (request.HaveResponse == true && response != null)
                {
                    reader = new StreamReader(response.GetResponseStream());
                    string result = reader.ReadToEnd();
                    result = result.Replace("</string>", "");
                    return result;
                }
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (HttpWebResponse errorResponse = (HttpWebResponse)wex.Response)
                    {
                        Console.WriteLine(
                            "The server returned '{0}' with the status code {1} ({2:d}).",
                            errorResponse.StatusDescription, errorResponse.StatusCode,
                            errorResponse.StatusCode);
                    }
                }
            }
            finally
            {
                if (response != null) { response.Close(); }
            }
            return null;
        }

        public const int TYPE_QC = 1;
        public const int TYPE_CSKH = 2;
        public const int TYPE_BRANDNAME = 3;
        public const int TYPE_BRANDNAME_NOTIFY = 4; // Gửi sms sử dụng brandname Notify
        public const int TYPE_GATEWAY = 5; // Gửi sms sử dụng app android từ số di động cá nhân, download app tại đây: https://play.google.com/store/apps/details?id=com.speedsms.gateway

        const String rootURL = "http://api.speedsms.vn/index.php";
        private const String accessToken = "****";

        public static String getUserInfo()
        {
            String url = rootURL + "/user/info";
            NetworkCredential myCreds = new NetworkCredential(accessToken, ":x");
            WebClient client = new WebClient();
            client.Credentials = myCreds;
            Stream data = client.OpenRead(url);
            StreamReader reader = new StreamReader(data);
            return reader.ReadToEnd();
        }
        private static SpeedSmsCredential getSpeedSmsToken()
        {
            SpeedSmsCredential returnValue;

            SystemConfig systemConfig=null ;

            if (HttpContext.Current != null)
            {
                systemConfig = ServiceFactory.Get<ISystemConfigService>().GetByAccount(HttpContext.Current.User.Identity.Name);
            }
            else
            {
                IDbFactory dbFactory;
                ISystemConfigRepository objRepository;
                IUnitOfWork unitOfWork;

                dbFactory = new DbFactory();
                objRepository = new SystemConfigRepository(dbFactory);
                unitOfWork = new UnitOfWork(dbFactory);
                //TODO : Thay the user name --- dang lay tri co dinh la admin 
                systemConfig = objRepository.GetAll().Where(x => (x.Code.ToLower() == CommonConstants.AdminUser.ToLower())).FirstOrDefault();

            }

            var empFilterViewModel = new JavaScriptSerializer().Deserialize<EmpFilterViewModel>(systemConfig.EmpFilterDataValue);

            //ma hoa du lieu
            string sid = StringCipher.Decrypt(empFilterViewModel.systemValue.SidT, CommonConstants.SecKeyString);
            empFilterViewModel.systemValue.SidT = sid;
            string token = StringCipher.Decrypt(empFilterViewModel.systemValue.TokT, CommonConstants.SecKeyString);
            empFilterViewModel.systemValue.TokT = token;

            // Your Account SID 
            var accountSid = sid;
            // Your Auth Token 
            var authToken = token;
            returnValue.token = token;
            returnValue.pw = sid;

            return returnValue;
        }

        public static String sendSpeedSMS(String[] phones, String content, int type, String sender)
        {
            SpeedSmsCredential auth = getSpeedSmsToken();
            String url = rootURL + "/sms/send";
            if (phones.Length <= 0)
                return "";
            if (content.Equals(""))
                return "";
            if (type < TYPE_QC || type > TYPE_GATEWAY)
                return "";
            if (type == TYPE_BRANDNAME && sender.Equals(""))
                return "";
            if (!sender.Equals("") && sender.Length > 11)
                return "";

            NetworkCredential myCreds = new NetworkCredential(auth.token, auth.pw);

            WebClient client = new WebClient();
            client.Credentials = myCreds;
            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            string builder = "{\"to\":[";

            for (int i = 0; i < phones.Length; i++)
            {
                builder += "\"" + phones[i] + "\"";
                if (i < phones.Length - 1)
                {
                    builder += ",";
                }
            }
            builder += "], \"content\": \"" + StringHelper.ToVietnameseUnsign(content) + "\", \"type\":" + type + ", \"sender\": \"" + sender + "\"}";

            String json = builder.ToString();
            return client.UploadString(url, json);
        }

    }
}