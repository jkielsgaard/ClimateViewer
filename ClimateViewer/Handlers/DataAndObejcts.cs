﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateViewer.Handlers
{
    /// <summary>
    /// To store data between function used in the clientsoftware
    /// </summary>
    public static class UserInformation
    {
        public static string ApiKey { get; set; }
        public static string Mail { get; set; }
        public static string Password { get; set; }
    }

    /// <summary>
    /// JsonDataConverter.cs objects
    /// </summary>
    public class unitData
    {
        public int datestamp { get; set; }
        public ClimateData climatedata { get; set; }
    }

    public class ClimateData
    {
        public float temperature { get; set; }
        public float humidity { get; set; }
        public float heatindex { get; set; }
    }

    public class ClimateUser
    {
        public string usermailid { get; set; }
        public string userpassword { get; set; }
    }

    public class Userapi
    {
        public string userapi { get; set; }
    }

    public class Userunits
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}
