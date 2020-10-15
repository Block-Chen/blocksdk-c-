using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlockSdk
{
    public class Monero : Base
    {
        public Monero(string api_token) : base(api_token)
        {
        }

        public string getBlockChain()
        {
            return this.request("GET", "/xmr/block").Result;
        }

        public string getBlock(Dictionary<string, string> request)
        {
            if (request == null)
            {
                request = new Dictionary<string, string>();
            }
            //Refine Values
            refineValues(request);

            return this.request("GET", "/xmr/block/" + request["block"],request).Result;

            //Dictionary<string, string> req = new Dictionary<string, string>();
            //req.Add("block", "531421");
            //req.Add("rawtx", "true");
            //req.Add("offset", "0");
            //req.Add("limit", "10");
        }

        public string getMemPool(Dictionary<string, string> request)
        {
            if (request == null)
            {
                request = new Dictionary<string, string>();
            }
            //Refine Values
            refineValues(request);

            return this.request("GET", "/xmr/mempool", request).Result;

            //Dictionary<string, string> req = new Dictionary<string, string>();
            //req.Add("block", "531421");
            //req.Add("rawtx", "true");
            //req.Add("offset", "0");
            //req.Add("limit", "10");
        }

        public string listAddress(Dictionary<string, string> request)
        {
            if (request == null)
            {
                request = new Dictionary<string, string>();
            }
            //Refine Values
            refineValues(request, false);
            return this.request("GET", "/xmr/address", request).Result;
        }

        public string createAddress(Dictionary<string, string> request)
        {
            if (request == null)
            {
                request = new Dictionary<string, string>();
            }

            if (!request.ContainsKey("name"))
            {
                request.Add("name", "false");
            }

            return this.request("POST", "/xmr/address/", request).Result;
        }

        public string getAddressInfo(Dictionary<string, string> request)
        {
            if (request == null)
            {
                request = new Dictionary<string, string>();
            }

            if (!request.ContainsKey("reverse"))
            {
                request.Add("reverse", "false");
            }

            //Refine Values
            refineValues(request);

            return this.request("GET", "/xmr/block/" + request["address_id"], request).Result;

            //Dictionary<string, string> req = new Dictionary<string, string>();
            //req.Add("block", "531421");
            //req.Add("rawtx", "true");
            //req.Add("offset", "0");
            //req.Add("limit", "10");
        }

        public string getAddressBalance(Dictionary<string, string> request)
        {
            return this.request("GET", "/xmr/address/" + request["address_id"] +"/balance", request).Result;

            //Dictionary<string, string> req = new Dictionary<string, string>();
            //req.Add("address", "531421");
            
        }

         public string loadAddress(Dictionary<string, string> request)
        {
            return this.request("POST", "/xmr/address/" + request["address_id"] +"/load", request).Result;
        }

        public string unLoadAddress(Dictionary<string, string> request)
        {
            return this.request("POST", "/xmr/address/" + request["address_id"] + "/unload", request).Result;
        }

        
        public string sendToAddress(Dictionary<string, string> request)
        {
            if (request == null)
            {
                request = new Dictionary<string, string>();
            }

            if (!request.ContainsKey("kbfee"))
            {
                var blockChain = this.getBlockChain();
                JObject jobj = JObject.Parse(blockChain);
                request.Add("kbfree", (string)jobj["medium_fee_per_kb"]);
            }

            if (!request.ContainsKey("private_spend_key"))
            {
                request.Add("private_spend_key", "false");
            }
            if (!request.ContainsKey("password"))
            {
                request.Add("password", "false");
            }

            return this.request("POST", "/xmr/address/" + request["address_id"] + "/sendtoaddress", request).Result;

        }

        public string getTransaction(Dictionary<string, string> request)
        {
            return this.request("GET", "/xmr/transaction/" + request["hash"]).Result;
        }
    }
}
