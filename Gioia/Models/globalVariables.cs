using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Gioia.Models
{
    public static class globalVariables
    {
        public static HttpClient webApiClient = new HttpClient();

       static globalVariables()
        {
            webApiClient.BaseAddress = new Uri("http://192.168.43.205:3050/api/");

            webApiClient.DefaultRequestHeaders.Clear();

            webApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("applicaion/json"));

        }

    }
}
