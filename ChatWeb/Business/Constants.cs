using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ChatWeb.Business
{
    public class Constants
    {
        public static string URL_API
        {
            get
            {
                return ConfigurationManager.AppSettings["url_ws"];
            }
        }

      public class Url
        {
            public static string REGISTER
            {
                get { return URL_API + "api/User/Register/"; }
            }

            public static string ACESS
            {
                get { return URL_API + "api/Access/Login/"; }
            }

            public static string SignalR
            {
                get { return URL_API + "signalr/"; }
            }

            public static string SignalRHub
            {
                get { return URL_API + "signalr/hubs/"; }
            }


            public static string  Rooms
            {
                get { return URL_API + "api/Room/List/"; }
            }

            public static string Messages
            {
                get { return URL_API + "api/Messages/Get/"; }
            }

        }
    }
}