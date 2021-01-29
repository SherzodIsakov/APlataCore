using APlataCore.Domain.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.Domain.Models
{
    public class MachineStatesLog : IBaseId
    {

        [JsonProperty("machine_id")]
        public int Id { get ; set ; }

        [JsonProperty("machine_id")]
        public int MachineID { get; set; }

        [JsonProperty("machine_id")]
        public int MachineStateID { get; set; }

        [JsonProperty("machine_id")]
        public string Comment { get; set; }

        [JsonProperty("machine_id")]
        public DateTime TimeStart { get; set; }

        [JsonProperty("machine_id")]
        public DateTime TimeEnd { get; set; }
    }
}
