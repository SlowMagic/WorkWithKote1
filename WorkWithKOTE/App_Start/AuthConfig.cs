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
                appId: "336589873199739",
               appSecret: "9caea526cdd34503d9cff1bba4ccf7a3");
           
            OAuthWebSecurity.RegisterClient(
            client: new VKontakteAuthenticationClient(
              "4913176", "fdUz0732af1ifEkZC3l7"),
            displayName: "ВКонтакте", 
            extraData: null);
         //   OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
