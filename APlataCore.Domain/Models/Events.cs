using APlataCore.Domain.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.Domain.Models
{
    public class Events : IBaseId
    {

        [JsonProperty("machine_id")]
        public int Id { get ; set ; }

        [JsonProperty("machine_id")]
        public int MachineID { get; set; }

        [JsonProperty("machine_id")]
        public int TerminalID { get; set; }

        [JsonProperty("machine_id")]
        public int EventID { get; set; }

        [JsonProperty("machine_id")]
        public int EventValue { get; set; }

        [JsonProperty("machine_id")]
        public DateTime Time { get; set; }      
    }
}
