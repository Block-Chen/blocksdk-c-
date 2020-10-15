using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace BlockSdk
{
    public class Base
    {
        public string api_token;
        public Base(string api_token)
        {
            this.api_token = api_token;
        }

        public string getUsage(Dictionary<string, string> request)
        {
            if (request == null)
            {
               request = new Dictionary<string, string>();
            }

            if (!request.ContainsKey("start_date") || String.IsNullOrEmpty(request["start_date"]))
            {
                request.Add("start_date",DateTime.Now.AddSeconds(-604800).ToString("yyyy-MM-dd"));
            }

            if (!request.ContainsKey("end_date") || String.IsNullOrEmpty(request["end_date"]))
            {
                request.Add("end_date", DateTime.Now.ToString("yyyy-MM-dd"));
            }

            return this.request("GET", "/usage", request).Result;

        }

        public string listPrice()
        {
            return this.request("GET", "/price").Result;
        }

        public string getHashType(Dictionary<string, string> request)
        {
            return this.request("GET", "/auto/" + request["hash"] + "/type").Result;
        }

        public async Task<String> request(string method, string path, Dictionary<string, string> data = null)
        {
            string url = "https://api.blocksdk.com/v1" + path;
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", api_token);
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

            if (method == "GET" && data!=null && data.Count > 0)
            {
                var builder = new UriBuilder(url);
                builder.Port = -1;
                var query = HttpUtility.ParseQueryString(builder.Query);
                // ... Loop over the data.
                foreach (var pair in data)
                {
                    string key = pair.Key;
                    string value = pair.Value;
                    query[key] = value;
                }

                builder.Query = query.ToString();
                url = builder.ToString();
            }

            if (method == "POST")
            {
                var json = JsonConvert.SerializeObject(data);
                var jsonData = new StringContent(json, Encoding.UTF8, "application/json");
                var datas = await jsonData.ReadAsStringAsync();
                //  var resp = await client.PostAsync(url, jsonData);
                //  string result = resp.Content.ReadAsStringAsync().Result;
                //   return result;
                return datas;
            }

            if (method == "GET")
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var resp = await response.Content.ReadAsStringAsync();
                return resp;
            }
            return "";
        }

        protected void refineValues(Dictionary<string, string> request, bool rawtx = true)
        {
            if (!request.ContainsKey("rawtx") && rawtx)
            {
                request.Add("rawtx", "false");
            }

            if (!request.ContainsKey("offset"))
            {
                request.Add("offset", "0");
            }

            if (!request.ContainsKey("limit"))
            {
                request.Add("limit", "10");
            }
        }
    }
}