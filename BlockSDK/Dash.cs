using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlockSdk
{
    public class Dash : Base
    {
        public Dash(string api_token) : base(api_token)
        {
        }

        public string getBlockChain()
        {
            return this.request("GET", "/dash/block").Result;
        }

        public string getBlock(Dictionary<string, string> request)
        {
            if (request == null)
            {
                request = new Dictionary<string, string>();
            }
            //Refine Values
            refineValues(request);

            return this.request("GET", "/dash/block/"+request["block"],request).Result;

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

            return this.request("GET", "/dash/mempool", request).Result;

            //Dictionary<string, string> req = new Dictionary<string, string>();
            //req.Add("block", "531421");
            //req.Add("rawtx", "true");
            //req.Add("offset", "0");
            //req.Add("limit", "10");
        }

        public string getAddressInfo(Dictionary<string, string> request)
        {
            if (request == null)
            {
                request = new Dictionary<string, string>();
            }
            //Refine Values
            refineValues(request);

            return this.request("GET", "/dash/block/" + request["address"], request).Result;

            //Dictionary<string, string> req = new Dictionary<string, string>();
            //req.Add("block", "531421");
            //req.Add("rawtx", "true");
            //req.Add("offset", "0");
            //req.Add("limit", "10");
        }

        public string getAddressBalance(Dictionary<string, string> request)
        {
            return this.request("GET", "/dash/address/" + request["address"]+"/balance", request).Result;

            //Dictionary<string, string> req = new Dictionary<string, string>();
            //req.Add("address", "531421");
            
        }

        public string listWallet(Dictionary<string, string> request)
        {
            if (request == null)
            {
                request = new Dictionary<string, string>();
            }
            //Refine Values
            refineValues(request,false);

            return this.request("GET", "/dash/wallet", request).Result;

            //Dictionary<string, string> req = new Dictionary<string, string>();
            //req.Add("offset", "0");
            //req.Add("limit", "10");
        }

        public string createWallet(Dictionary<string, string> request)
        {
            if (request == null)
            {
                request = new Dictionary<string, string>();
            }

            if (!request.ContainsKey("name"))
            {
                request.Add("name", "false");
            }

            return this.request("POST", "/dash/wallet", request).Result;
        }

        public string loadWallet(Dictionary<string, string> request)
        {
            return this.request("POST", "/dash/wallet" + request["wallet_id"]+"/load", request).Result;
        }

        public string unLoadWallet(Dictionary<string, string> request)
        {
            return this.request("POST", "/dash/wallet" + request["wallet_id"] + "/unload", request).Result;
        }

        public string listWalletAddress(Dictionary<string, string> request)
        {
            if (request == null)
            {
                request = new Dictionary<string, string>();
            }

            if (!request.ContainsKey("address"))
            {
                request.Add("address", "false");
            }
            if (!request.ContainsKey("hdkeypath"))
            {
                request.Add("hdkeypath", "false");
            }

            refineValues(request, false);

            return this.request("GET", "/dash/wallet/" + request["wallet_id"] + "/address").Result;
        }

        public string createWalletAddress(Dictionary<string, string> request)
        {
            if (request == null)
            {
                request = new Dictionary<string, string>();
            }

            if (!request.ContainsKey("seed_wif"))
            {
                request.Add("seed_wif", "false");
            }
            if (!request.ContainsKey("password"))
            {
                request.Add("password", "false");
            }

            return this.request("POST", "/dash/wallet/" + request["wallet_id"] + "/address", request).Result;

        }

        public string getWalletBalance(Dictionary<string, string> request)
        {
            return this.request("GET", "/dash/wallet/" + request["wallet_id"] + "/balance").Result;
        }

        public string getWalletTransaction(Dictionary<string, string> request)
        {
            if (request == null)
            {
                request = new Dictionary<string, string>();
            }

            if (!request.ContainsKey("category"))
            {
                request.Add("category", "all");
            }
            if (!request.ContainsKey("order"))
            {
                request.Add("order", "desc");
            }

            refineValues(request, false);
            return this.request("GET", "/dash/wallet/" + request["wallet_id"] + "/transaction", request).Result;

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

            if (!request.ContainsKey("seed_wif"))
            {
                request.Add("seed_wif", "false");
            }
            if (!request.ContainsKey("password"))
            {
                request.Add("password", "false");
            }

            return this.request("POST", "/dash/wallet/" + request["wallet_id"] + "/sendtoaddress", request).Result;

        }

        public string sendMany(Dictionary<string, string> request)
        {
            if (request == null)
            {
                request = new Dictionary<string, string>();
            }

            if (!request.ContainsKey("seed_wif"))
            {
                request.Add("seed_wif", "false");
            }
            if (!request.ContainsKey("password"))
            {
                request.Add("password", "false");
            }

            return this.request("POST", "/dash/wallet/" + request["wallet_id"] + "/sendmany", request).Result;

        }

        public string sendTransaction(Dictionary<string, string> request)
        { 
             return this.request("POST", "/dash/transaction/", request).Result;
        }

        public string getTransaction(Dictionary<string, string> request)
        {
            return this.request("GET", "/dash/transaction/" + request["hash"]).Result;
        }
    }
}
