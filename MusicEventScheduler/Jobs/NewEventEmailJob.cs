using Revalee.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MusicEventScheduler.Jobs
{
    public static class NewEventEmailJob
    {
        private static string MyHostURL = ConfigurationManager.AppSettings["MyHostURL"] != null ? ConfigurationManager.AppSettings["MyHostURL"].ToString() : "http://52.14.29.126:85/";
        private static string CallbackTime = ConfigurationManager.AppSettings["CallbackTime"] != null ? ConfigurationManager.AppSettings["CallbackTime"].ToString() : "60";
        private static DateTimeOffset? previousCallbackTime = null;

        public static void SendNewEventEmailToAllSubscribers()
        {
            // Schedule your callback 10 minutes from now
            DateTimeOffset callbackTime = DateTimeOffset.Now.AddMinutes(Convert.ToInt16(CallbackTime));

            // Your web service's Uri
            Uri callbackUrl = new Uri(MyHostURL + "Subscribe/SendNewEventEmailToSubscribers");

            // Register the callback request with the Revalee service
            RevaleeRegistrar.ScheduleCallback(callbackTime, callbackUrl);

            previousCallbackTime = callbackTime;
        }
    }
}