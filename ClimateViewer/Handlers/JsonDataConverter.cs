using Newtonsoft.Json;
using System.Collections.Generic;

namespace ClimateViewer.Handlers
{
    public class JsonDataConverter
    {
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

        public static string deserializedApikey(string data)
        {
            List<Userapi> key = new List<Userapi>();
            key = JsonConvert.DeserializeObject<List<Userapi>>(data);
            return key[0].userapi;
        }

        public static List<Userunits> deserializedUnits(string data)
        {
            List<Userunits> units = new List<Userunits>();
            dynamic unitsdata = JsonConvert.DeserializeObject(data);
            dynamic unitsinfo = unitsdata[0].units;

            for (int i = 0; i < unitsinfo.Count; i++)
            {
                string ui = unitsinfo[i];
                string[] us = ui.Split(',');
                if (us[1] != "null")
                {
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
