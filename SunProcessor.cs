using System;
namespace DBTest
{
	public class SunProcessor
	{
        public static async Task<SunModel> LoadSunInformation()
        {
            string url = "https://api.sunrise-sunset.org/json?lat=65.590652&lng=19.167610&date=today"; // Arvidsjaur

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                
                if (response.IsSuccessStatusCode)
                {
                    SunResultModel result = await response.Content.ReadAsAsync<SunResultModel>();
                    Console.WriteLine($"Status: {result.Status}");
                    return result.Results;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}

