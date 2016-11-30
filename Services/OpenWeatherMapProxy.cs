using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAlpha.Services
{
    public class OpenWeatherMapProxy
    {
        //Adorn the returned object as a task
        //Means when this function is complete it should give back the data type RootObject
        public async static Task<RootObject> GetWeather(double lat, double lon)
        {
            //Get URL
            var http = new HttpClient();
            var url = String.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid=45c73e9c6f6d1b5a0e3bee0115d78046&units=metric", lat, lon);
            var response = await http.GetAsync(url);
            //Read URL content
            var result = await response.Content.ReadAsStringAsync();
            //Serialize json
            var serializer = new DataContractJsonSerializer(typeof(RootObject));
            //Tell program to work in this encoding format (UTF8 in this case)
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));

            var data = (RootObject)serializer.ReadObject(ms);
            return data;
        }
    }

    //From OpenWeatherAPI's jsonstring, important codes to get weather
    //Converts GPS coordinates from phone to jsonstring and then to data to 
    //fit different classes below.

    #region WeatherProxy
    [DataContract]
    public class Coord
    {
        [DataMember]
        public double lon { get; set; }
        [DataMember]
        public double lat { get; set; }
    }

    [DataContract]
    public class Weather
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string main { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string icon { get; set; }
    }

    [DataContract]
    public class Main
    {
        [DataMember]
        public double temp { get; set; }
        [DataMember]
        public double pressure { get; set; }
        [DataMember]
        public int humidity { get; set; }
        [DataMember]
        public double temp_min { get; set; }
        [DataMember]
        public double temp_max { get; set; }
        [DataMember]
        public double sea_level { get; set; }
        [DataMember]
        public double grnd_level { get; set; }
    }

    [DataContract]
    public class Wind
    {
        [DataMember]
        public double speed { get; set; }
        [DataMember]
        public double deg { get; set; }
    }

    [DataContract]
    public class Clouds
    {
        [DataMember]
        public int all { get; set; }
    }

    [DataContract]
    public class Sys
    {
        [DataMember]
        public double message { get; set; }
        [DataMember]
        public string country { get; set; }
        [DataMember]
        public int sunrise { get; set; }
        [DataMember]
        public int sunset { get; set; }
    }

    [DataContract]
    public class RootObject
    {
        [DataMember]
        public Coord coord { get; set; }
        [DataMember]
        public List<Weather> weather { get; set; }
        [DataMember]
        public string @base { get; set; }
        [DataMember]
        public Main main { get; set; }
        [DataMember]
        public Wind wind { get; set; }
        [DataMember]
        public Clouds clouds { get; set; }
        [DataMember]
        public int dt { get; set; }
        [DataMember]
        public Sys sys { get; set; }
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int cod { get; set; }
    }

    #endregion
}
