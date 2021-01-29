using APlataCore.Domain.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.Domain.Models
{
    public class Log //: IBaseId
    {
        //public int Id { get ; set ; }

        [JsonProperty("machine_id")]
        public int SessionID { get; set; }

        [JsonProperty("machine_id")]
        public DateTime Time { get; set; }

        [JsonProperty("machine_id")]
        public int LogPart { get; set; }

        [JsonProperty("machine_id")]
        public int LogType { get; set; }

        [JsonProperty("machine_id")]
        public int LogCode { get; set; }

        [JsonProperty("machine_id")]
        public string Text { get; set; }      
       
    }
}
