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
                unitData IOTtemp = new unitData();
                ClimateData Climatetemp = new ClimateData();
                IOTtemp.datestamp = item.datestamp;
                Climatetemp.temperature = item.climatedata.temperature;
                Climatetemp.humidity = item.climatedata.humidity;
                Climatetemp.heatindex = item.climatedata.heatindex;
                IOTtemp.climatedata = Climatetemp;

                ClimateDataList.Add(IOTtemp);
            }

            return ClimateDataList;
        }

        public static string deserializedApikey(string data)
        {
            string apikey = null;
            dynamic key = JsonConvert.DeserializeObject(data);
            foreach (dynamic item in key) { apikey = item.userapi; }
            return apikey;
        }

        public static List<Userunits> deserializedUnits(string data)
        {
            List<Userunits> units = new List<Userunits>();
            dynamic unitsdata = JsonConvert.DeserializeObject(data);

            foreach (dynamic item in unitsdata)
            {
                dynamic unitsinfo = item.units;

                for (int i = 0; i < unitsinfo.Count; i++)
                {
                    string ui = unitsinfo[i];
                    string[] u = ui.Split(',');
                    if (u[1] != "null")
                    {
                        units.Add(new Userunits
                        {
                            id = u[0],
                            name = u[1]
                        });
                    }
                }
            }
            return units;
        }
    }


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
