﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClimateViewer.Handlers
{
    public class HttpApiRequest
    {
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
            using (StreamReader sr = new System.IO.StreamReader(response.GetResponseStream()))
            {
                Response = sr.ReadToEnd();
            }
            return Response;
        }

        public static string ClimateLogin( string usermail, string password)
        {
            ClimateUser cu = new ClimateUser();
            cu.usermailid = usermail;
            cu.userpassword = password;

            string dataurl = "https://gab8d2upqj.execute-api.eu-west-1.amazonaws.com/dev/climatelogin";
            string urlparams = "?httpFunction=userlogin";
            string body = JsonConvert.SerializeObject(cu);
    
            byte[] bodydata = Encoding.ASCII.GetBytes(body);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(dataurl + urlparams);
            request.Method = "post";
            request.ContentType = "application/json";          
            request.ContentLength = bodydata.Length;

            Stream newStream = request.GetRequestStream();
            newStream.Write(bodydata, 0, bodydata.Length);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string JSONapikey = "";
            using (StreamReader sr = new StreamReader(response.GetResponseStream())) { JSONapikey = sr.ReadToEnd(); }

            if (JSONapikey == "[]") { return null; }
            else
            {
                return JSONapikey;
            }
        }

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
            request.Method = "post";
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
