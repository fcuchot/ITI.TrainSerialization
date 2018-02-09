using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using ITI.TrainSerialization.Classes;
using ITI.TrainSerialization.Interfaces;
using Newtonsoft.Json;

namespace ITI.TrainSerialization.Serialization
{
    public class JSONSerialization
    {
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            });         
        }

        
        public static T Deserialize<T>(string json)
        {         
            return JsonConvert.DeserializeObject<T>(json);
        }
      
        public static T Clone<T>(object source)
        {
            return Deserialize<T>(Serialize(source));
        }
    }
}
