using System;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http.Headers;

namespace DBTest
{
	public static class ApiHelper
	{
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();  
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}

