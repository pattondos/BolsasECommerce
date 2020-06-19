using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Final_Ecommerce.Repository
{
    public class PayPalConfig
    {
        public static readonly string clientId;
        public static readonly string clientSecret;

        static PayPalConfig()
        {
            var config = GetConfig();
            clientId = config["clientId"];
            clientSecret = config["clientSecret"];
        }

        private static Dictionary<string, string> GetConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string GetAccessToken()
        {
            string accessToken = new OAuthTokenCredential(clientId, clientSecret, GetConfig()).GetAccessToken();
            return accessToken;
        }

        public static APIContext GetAPIContext()
        {
            APIContext apiContext = new APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }

    }
}