using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClimateViewer.Handlers
{
    /// <summary>
    /// HttpApiRequest.cs is a handler for all webapi request
    /// </summary>

    public class HttpApiRequest
    {
        /// <summary>
        /// Get dataset from unix datestamp and 24h forward
        /// </summary>
        /// <param name="apikey">Privat user API key</param>
        /// <param name="usermail">User login mail</param>
        /// <param name="unitid">ID on the unit to get dataset from</param>
        /// <param name="datestamp">unix datestamp ex. if 08-10-2018 00:00:00 is 1538956800 in unix</param>
        /// <param name="CompressionLVL">How much data should be compressed, 1 dataset from 1 dataset, 1 dataset from 2 dataset and so on op to 4</param>
        /// <returns>Will return dataset from request in JSON format</returns>
        public static string GetClimateData(string apikey, string usermail, string unitid, string datestamp, string CompressionLVL)
        {
            string dataurl = "https://gab8d2upqj.execute-api.eu-west-1.amazonaws.com/dev/climateapi";
            string urlparams = "?httpFunction=climatedata&usermailid=" + usermail + "&unitid=" + unitid + "&date=" + datestamp + "&dataCompression=" + CompressionLVL;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(dataurl + urlparams);
            request.Method = "get";
            request.ContentType = "application/json";
            request.Headers.Add("x-api-key", apikey);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string Response = "";
            using (StreamReader sr = new StreamReader(response.GetResponseStream())) { Response = sr.ReadToEnd(); }
            return Response;
        }

        /// <summary>
        /// Login and get privat APIkey
        /// </summary>
        /// <param name="usermail">User login mail</param>
        /// <param name="password">User login password</param>
        /// <returns>Will return privat API key in JSON format</returns>
        public static string ClimateLogin(string usermail, string password)
        {
            ClimateUser cu = new ClimateUser();
            cu.usermailid = usermail;
            cu.userpassword = password;

            string dataurl = "https://gab8d2upqj.execute-api.eu-west-1.amazonaws.com/dev/climatelogin";
            string urlparams = "?httpFunction=userlogin";
            string body = JsonConvert.SerializeObject(cu);
    
            byte[] bodydata = Encoding.ASCII.GetBytes(body);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(dataurl + urlparams);
            request.Method = "POST";
            request.ContentType = "application/json";          
            request.ContentLength = bodydata.Length;

            Stream newStream = request.GetRequestStream();
            newStream.Write(bodydata, 0, bodydata.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string JSONapikey = "";
            using (StreamReader sr = new StreamReader(response.GetResponseStream())) { JSONapikey = sr.ReadToEnd(); }

            if (JSONapikey == "[]") { return null; }
            else { return JSONapikey; }
        }

        /// <summary>
        /// Change user password
        /// </summary>
        /// <param name="apikey">User rivat API key</param>
        /// <param name="usermail">User login mail</param>
        /// <param name="userpassword">users login password</param>
        /// <param name="newpassword">New user login password</param>
        /// <returns>will return "Password changed" if successful</returns>
        public static string ChangePassword(string apikey, string usermail, string userpassword, string newpassword)
        {
            ChangePassword cp = new ChangePassword();
            cp.usermailid = usermail;
            cp.userpassword = userpassword;
            cp.newpassword = newpassword;

            string dataurl = "https://gab8d2upqj.execute-api.eu-west-1.amazonaws.com/dev/climateapi";
            string urlparams = "?httpFunction=changepassword";
            string body = JsonConvert.SerializeObject(cp);

            byte[] bodydata = Encoding.ASCII.GetBytes(body);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(dataurl + urlparams);
            request.Method = "PUT";
            request.Headers.Add("x-api-key", apikey);
            request.ContentType = "application/json";
            request.ContentLength = bodydata.Length;

            Stream newStream = request.GetRequestStream();
            newStream.Write(bodydata, 0, bodydata.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string JSONapikey = "";
            using (StreamReader sr = new StreamReader(response.GetResponseStream())) { JSONapikey = sr.ReadToEnd(); }

            if (JSONapikey == "[]") { return null; }
            else { return JSONapikey; }
        }

        /// <summary>
        /// Get all users units
        /// </summary>
        /// <param name="apikey">User rivat API key</param>
        /// <param name="usermail">User login mail</param>
        /// <param name="password">User login password</param>
        /// <returns>Will return all units information in JSON format</returns>
        public static string Userunits(string apikey, string usermail, string password)
        {
            ClimateUser cu = new ClimateUser();
            cu.usermailid = usermail;
            cu.userpassword = password;

            string dataurl = "https://gab8d2upqj.execute-api.eu-west-1.amazonaws.com/dev/climateapi";
            string urlparams = "?httpFunction=userunits";
            string body = JsonConvert.SerializeObject(cu);

            byte[] bodydata = Encoding.ASCII.GetBytes(body);
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(dataurl + urlparams);
            request.Method = "POST";
            request.Headers.Add("x-api-key", apikey);
            request.ContentType = "application/json";
            request.ContentLength = bodydata.Length;

            Stream newStream = request.GetRequestStream();
            newStream.Write(bodydata, 0, bodydata.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string Response = "";
            using (StreamReader sr = new StreamReader(response.GetResponseStream())) { Response = sr.ReadToEnd(); }
            return Response;
        }

        /// <summary>
        /// Change units information
        /// </summary>
        /// <param name="apikey">User rivat API key</param>
        /// <param name="usermail">User login mail</param>
        /// <param name="units">new units information in JSON format</param>
        /// <returns>Will return "Units updated" if successful</returns>
        public static string Changeunits(string apikey, string usermail, List<Userunits> units)
        {
            string[] unitsarray = new string[units.Count()];
            for (int i = 0; i < units.Count(); i++)
            {
                unitsarray[i] = units[i].id + "," + units[i].name;
            }

            ChangeUnits cu = new ChangeUnits();
            cu.usermailid = usermail;
            cu.units = unitsarray;

            string dataurl = "https://gab8d2upqj.execute-api.eu-west-1.amazonaws.com/dev/climateapi";
            string urlparams = "?httpFunction=units";
            string body = JsonConvert.SerializeObject(cu);

            byte[] bodydata = Encoding.ASCII.GetBytes(body);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(dataurl + urlparams);
            request.Method = "PUT";
            request.Headers.Add("x-api-key", apikey);
            request.ContentType = "application/json";
            request.ContentLength = bodydata.Length;

            Stream newStream = request.GetRequestStream();
            newStream.Write(bodydata, 0, bodydata.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string Response = "";
            using (StreamReader sr = new StreamReader(response.GetResponseStream())) { Response = sr.ReadToEnd(); }
            return Response;
        }
    }
}
