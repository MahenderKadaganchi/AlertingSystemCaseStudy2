using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HttpServiceRequestLib
{
    public class HttpServiceRequest
    {
        public static string HttpGetRequest(string baseAddress, string uri)
        {
            var client = new HttpClient { BaseAddress = new Uri(baseAddress) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(uri).Result;
            string result = null;
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsStringAsync().Result;
            }

            char[] charArray = new[] { '[', ']' };
            if (result != null)
            {
                result = result.Trim(charArray);
                return result;
            }
            return "";
        }

        public static string HttpPostRequest(string baseAddress, string methodUri, string input)
        {
            var client = new HttpClient { BaseAddress = new Uri(baseAddress) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var stringContent = new StringContent(input, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(methodUri, stringContent).Result;
            string result = null;
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsStringAsync().Result;
            }
            char[] charArray = new[] { '[', ']' };
            if (result != null)
            {
                result = result.Trim(charArray);
                return result;
            }
            return "";
        }
    }
}
