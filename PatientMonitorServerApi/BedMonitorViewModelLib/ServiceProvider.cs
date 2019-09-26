using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace BedMonitorViewModelLib
{
    public static class ServiceProvider
    {

        public static string HttpGetPatientVitals(string patientId)
        {
            var client = new HttpClient {BaseAddress = new Uri("http://localhost:5000/")};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var stringContent = new StringContent(patientId, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("api/BedMonitor/",stringContent).Result;
            string result = null;
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsStringAsync().Result;
            }

            char[] charArray = new[] {'[', ']'};
            if (result != null)
            {
                result = result.Trim(charArray);
                return result;
            }

            return "";
        }
        public static string HttpGetPatientId()
        {
            var client = new HttpClient {BaseAddress = new Uri("http://localhost:5000/")};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var stringContent = new StringContent("patientId", System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.GetAsync("api/BedMonitor/").Result;
            string result = null;
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsStringAsync().Result;
            }

            char[] charArray = new[] { '[', '"', ']' };
            if (result != null)
            {
                result = result.Trim(charArray);
                return result;
            }

            return "";
        }
        public static string HttpDischargePatient(string patientId)
        {
            var client = new HttpClient {BaseAddress = new Uri("http://localhost:5000/")};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var stringContent = new StringContent(patientId, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("api/PatientMonitor/Discharge/",stringContent).Result;
            string result = null;
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsStringAsync().Result;
            }

            char[] charArray = new[] { '[', '"', ']' };
            if (result != null)
            {
                result = result.Trim(charArray);
                return result;
            }
            return "";
        }

    }
    
}
