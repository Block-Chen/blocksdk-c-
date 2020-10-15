using System;
using System.Collections.Generic;
using System.Text;

namespace BlockSdk
{
    public class Webhook : Base
    {
        public Webhook(string api_token) : base(api_token)
        {
        }

        public string create(Dictionary<string,string> request)
        {
            return this.request("POST", "/hook",request).Result;
        }

        public string list(Dictionary<string, string> request=null)  
        {

            if (request == null)
            {
                request = new Dictionary<string, string>();
            }

            refineValues(request, false);
            return this.request("GET", "/hook", request).Result;
        }

        public string get(Dictionary<string, string> request)
        {
            return this.request("GET", "/hook/"+request["hook_id"]).Result;
        }

        public string delete(Dictionary<string, string> request)
        {
            return this.request("POST", "/hook/" + request["hook_id"]+"/delete").Result;
        }


        public string listResponse(Dictionary<string, string> request=null)
        {

            if (request == null)
            {
                request = new Dictionary<string, string>();
            }

            refineValues(request, false);
            return this.request("GET", "/hook/response", request).Result;
        }

        public string getResponse(Dictionary<string, string> request)
        {

            if (request == null)
            {
                request = new Dictionary<string, string>();
            }

            refineValues(request, false);
            return this.request("GET", "/hook/" + request["hook_id"] + "/response").Result;
        }
    }
}
