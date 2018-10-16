using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateViewer.Handlers
{
    /// To store data between function used in the clientsoftware
    #region static data information
    public static class UserInformation
    {
        public static string ApiKey { get; set; }
        public static string Mail { get; set; }
        public static string Password { get; set; }
    }
    #endregion
    ///

    /// JsonDataConverter.cs 
    #region objects
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

    public class ChangeUnits
    {
        public string usermailid { get; set; }
        public string[] units { get; set; }
    }

    public class ChangePassword
    {
        public string usermailid { get; set; }
        public string userpassword { get; set; }
        public string newpassword { get; set; }
    }
    #endregion
    ///
}
