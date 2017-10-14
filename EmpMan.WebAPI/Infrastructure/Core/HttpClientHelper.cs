using EmpMan.Common;
using EmpMan.Web.Providers;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace EmpMan.Web.Infrastructure.Core
{
    public static class HttpClientHelper<T> where T : class
    {
        public static HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            //httpClient.BaseAddress = new Uri(url);
            //httpClient.DefaultRequestHeaders.Clear();
            //httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("ja-JP"));
            //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //dynamic _token = HttpContext.Current.Session["token"];
            //dynamic _token = ctx.Session["token"];
            //if (_token == null) throw new ArgumentNullException(nameof(_token));
            //httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", _token.AccessToken));

            string _token = HttpContext.Current.Request.Params["HTTP_AUTHORIZATION"].Replace("Bearer ","");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            return httpClient;
        }

        //public async Task<TResult> GetAsync<TResult>(string uriString) where TResult : class
        //{
        //    var uri = new Uri(uriString);
        //    using (var client = GetHttpClient())
        //    {
        //        HttpResponseMessage response = await client.GetAsync(uri);
        //        if (response.StatusCode != HttpStatusCode.OK)
        //        {
        //            //Log.Error(response.ReasonPhrase);
        //            return default(TResult);
        //        }
        //        var json = await response.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<TResult>(json, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        //    }
        //}

        /// <summary>
        /// Thuc thi api su dung httpclient va tra ve List<T>
        /// </summary>
        /// <param name="api">api path . ex : /api/team/getall</param>
        /// <returns></returns>
        public static List<T> GetApiUseHttpClientRetList(string api)
        {
            var client = GetHttpClient();
            List<T> data = null;

            var task = client.GetAsync(CommonConstants.BASE_API + api)
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    data = JsonConvert.DeserializeObject<List<T>>(jsonString.Result);
                });
            task.Wait();
            client.Dispose();

            return data;
        }
        
        /// <summary>
        /// Thuc thi api su dung httpclient
        /// </summary>
        /// <param name="api">api path . ex : /api/team/getall</param>
        /// <returns></returns>
        public static T PostApiUseHttpClient(string api , T param)
        {
            var client = GetHttpClient();
            T data = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

            var task = client.PostAsync(CommonConstants.BASE_API + api , content)
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    data = JsonConvert.DeserializeObject<T>(jsonString.Result);
                });
            task.Wait();
            client.Dispose();

            return data;
        }

        /// <summary>
        /// Thuc thi api su dung httpclient
        /// </summary>
        /// <param name="api">api path . ex : /api/team/getall</param>
        /// <returns></returns>
        public static List<T> PostApiUseHttpClientRetList(string api, T param)
        {
            var client = GetHttpClient();
            List<T> data = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");

            var task = client.PostAsync(CommonConstants.BASE_API + api, content)
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    data = JsonConvert.DeserializeObject<List<T>>(jsonString.Result);
                });
            task.Wait();
            client.Dispose();

            return data;
        }

        /// <summary>
        /// Thuc thi api su dung httpclient
        /// </summary>
        /// <param name="api">api path . ex : /api/team/getall</param>
        /// <param name="queryString"> ex string queryString = "SystemId="+systemId+"&Id="+id</param>
        /// <returns></returns>
        public static List<T> PostApiUseHttpClientRetList(string api, string queryString)
        {
            var client = GetHttpClient();
            List<T> data = null;
            var task = client.PostAsJsonAsync(CommonConstants.BASE_API + api, queryString)
                .ContinueWith((taskwithresponse) =>
                {
                    var response = taskwithresponse.Result;
                    var jsonString = response.Content.ReadAsStringAsync();
                    jsonString.Wait();
                    data = JsonConvert.DeserializeObject<List<T>>(jsonString.Result);
                });
            task.Wait();
            client.Dispose();

            return data;
        }


    }
}