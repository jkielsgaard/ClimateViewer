using ClimateViewer.Handlers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class ClimateViewer_tests
    {
        /// <summary>
        /// unittests to test all nonweb handlers function in climateviewer and login webapi to ensure connection to cloudsystem
        /// </summary>

        #region HttpApiRequest.cs
        [TestMethod]
        public void HttpApiRequest_ClimateLogin()
        {
            string mail = "test";
            string password = "test";
            string expectedoutput_Noaccess = null;
            string expectedoutput_Access = "userapi";

            string JSONapikey = HttpApiRequest.ClimateLogin(mail, password);

            if (mail == "test")
            {
                Assert.AreEqual(expectedoutput_Noaccess, JSONapikey);
            }
            else
            {
                Assert.IsTrue(JSONapikey.Contains(expectedoutput_Access));
            }
        }
        #endregion

        #region JsonDataConverter.cs      
        [TestMethod]
        public void JsonDataConverter_deserializedClimateData()
        {
            string Testdataset = "[{\"datestamp\":\"1500000000\",\"climatedata\":{\"temperature\":\"20\",\"humidity\":\"50\",\"heatindex\":\"21\"}}]";
            int expectedoutput_datestamp = 1500000000;
            int expectedoutput_temperature = 20;
            int expectedoutput_humidity = 50;
            int expectedoutput_heatindex = 21;

            List<unitData> dataset = JsonDataConverter.deserializedClimateData(Testdataset);

            Assert.AreEqual(expectedoutput_datestamp, dataset[0].datestamp);
            Assert.AreEqual(expectedoutput_temperature, dataset[0].climatedata.temperature);
            Assert.AreEqual(expectedoutput_humidity, dataset[0].climatedata.humidity);
            Assert.AreEqual(expectedoutput_heatindex, dataset[0].climatedata.heatindex);
        }

        [TestMethod]
        public void JsonDataConverter_deserializedUnits()
        {
            string Testunits = "[{ \"units\":[\"0000A1,test01\",\"0000A2,test02\",\"0000A3,test03\",\"0000A4,null\"]}]";
            string expectedoutput_01 = "0000A1,test01";
            string expectedoutput_02 = "0000A2,test02";
            string expectedoutput_03 = "0000A3,test03";
            string expectedoutput_04 = "0000A4,null";
            int expectedoutputcount = 3;

            List<Userunits> unitsWithNull = JsonDataConverter.deserializedUnits(Testunits, false);
            List<Userunits> unitsWithOutNull = JsonDataConverter.deserializedUnits(Testunits, true);
            string[] unitsarray = new string[unitsWithNull.Count];

            for (int i = 0; i < unitsWithNull.Count; i++)
            {
                unitsarray[i] = unitsWithNull[i].id + "," + unitsWithNull[i].name;
            }

            Assert.AreEqual(expectedoutput_01, unitsarray[0]);
            Assert.AreEqual(expectedoutput_02, unitsarray[1]);
            Assert.AreEqual(expectedoutput_03, unitsarray[2]);
            Assert.AreEqual(expectedoutput_04, unitsarray[3]);
            Assert.AreEqual(expectedoutputcount, unitsWithOutNull.Count);
        }

        [TestMethod]
        public void JsonDataConverter_deserializedApikey()
        {
            string Testapikey = "[{\"userapi\":\"testapikey\"}]";
            string expectedoutput = "testapikey";

            string key = JsonDataConverter.deserializedApikey(Testapikey);

            Assert.AreEqual(expectedoutput, key);
        }
        #endregion

        #region UnixStampConvert
        [TestMethod]
        public void UnixStampConvert_FromDateToUnixTimeStamp()
        {
            string format = "dd.MM.yyyy_HH:mm:ss";
            string expectedoutput = "01.01.2018_12:00:00";

            string date = UnixStampConvert.UnixTimeToDateTime(1514808000).ToString(format);

            Assert.AreEqual(expectedoutput, date);
        }
        #endregion
    }
}
