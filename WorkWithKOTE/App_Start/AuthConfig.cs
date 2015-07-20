using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using WorkWithKOTE.Models;
using WorkWithKOTE.Code;

namespace WorkWithKOTE
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            OAuthWebSecurity.RegisterFacebookClient(
               appId:"878101295595387",
               appSecret:"b5bd7b6a3ddf2c14efdaea289f0f271a");

            OAuthWebSecurity.RegisterClient(
            client: new VKontakteAuthenticationClient(
                 "4913176", "fdUz0732af1ifEkZC3l7"),
            displayName: "ВКонтакте",
            extraData: null);
            //   OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
