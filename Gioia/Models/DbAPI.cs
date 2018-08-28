using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Gioia.Models
{
    public class DbAPI
    {

        public static HttpClient webApiClient = new HttpClient();

        static DbAPI()
        {

            webApiClient.BaseAddress = new Uri("http://192.168.43.205:3050/api/");
            webApiClient.DefaultRequestHeaders.Clear();
            webApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}