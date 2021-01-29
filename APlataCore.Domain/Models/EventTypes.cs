using APlataCore.Domain.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.Domain.Models
{
    public class EventTypes : IBaseId
    {

        [JsonProperty("machine_id")]
        public int Id { get ; set ; }

        [JsonProperty("machine_id")]
        public string Name { get; set; }

        [JsonProperty("machine_id")]
        public int MachineStateID { get; set; }

    }
}
