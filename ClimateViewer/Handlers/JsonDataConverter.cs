using Newtonsoft.Json;
using System.Collections.Generic;

namespace ClimateViewer.Handlers
{
    /// <summary>
    /// JsonDataConverter.cs is to convert all string data return from webAPI to readable objects for the application
    /// </summary>

    public class JsonDataConverter
    {
        /// <summary>
        /// Change JSON string to climatedata obejct list
        /// </summary>
        /// <param name="data">climatedata in JSON string</param>
        /// <returns>List of objects in unitData </returns>
        public static List<unitData> deserializedClimateData(string data)
        {
            List<unitData> ClimateDataList = new List<unitData>();
            dynamic deserialized = JsonConvert.DeserializeObject(data);

            foreach (dynamic item in deserialized)
            {
                unitData IOT = new unitData();
                ClimateData Climatetemp = new ClimateData();
                IOT.datestamp = item.datestamp;
                Climatetemp.temperature = item.climatedata.temperature;
                Climatetemp.humidity = item.climatedata.humidity;
                Climatetemp.heatindex = item.climatedata.heatindex;
                IOT.climatedata = Climatetemp;

                ClimateDataList.Add(IOT);
            }
            return ClimateDataList;
        }

        /// <summary>
        /// Change JSON string to Apikey string
        /// </summary>
        /// <param name="data">APIkey in JSON string</param>
        /// <returns>return APIkey in a string</returns>
        public static string deserializedApikey(string data)
        {
            List<Userapi> key = new List<Userapi>();
            key = JsonConvert.DeserializeObject<List<Userapi>>(data);
            return key[0].userapi;
        }

        /// <summary>
        /// Change JSON string to Userunits obejct list
        /// </summary>
        /// <param name="data">Userunits in JSON string</param>
        /// <param name="FilterNull">If the return should contain units with null names (false)</param>
        /// <returns>return a list of the units</returns>
        public static List<Userunits> deserializedUnits(string data, bool FilterNull)
        {
            List<Userunits> units = new List<Userunits>();

            dynamic unitsdata = JsonConvert.DeserializeObject(data);
            dynamic unitsinfo = unitsdata[0].units;

            for (int i = 0; i < unitsinfo.Count; i++)
            {
                string ui = unitsinfo[i];
                string[] us = ui.Split(',');
                if (FilterNull == true)
                {
                    if (us[1] != "null")
                    {
                        units.Add(new Userunits
                        {
                            id = us[0],
                            name = us[1]
                        });
                    }
                } else {
                    units.Add(new Userunits
                    {
                        id = us[0],
                        name = us[1]
                    });
                }
            }          
            return units;
        }
    } 
}
