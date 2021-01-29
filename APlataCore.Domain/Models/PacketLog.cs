using APlataCore.Domain.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.Domain.Models
{
    public class PacketLog : IBaseId
    {

        [JsonProperty("machine_id")]
        public int Id { get ; set ; }

        [JsonProperty("machine_id")]
        public int SessionID { get; set; }

        [JsonProperty("machine_id")]
        public int Type { get; set; }

        [JsonProperty("machine_id")]
        public DateTime Time { get; set; }

        [JsonProperty("machine_id")]
        public int Direction { get; set; }

        [JsonProperty("machine_id")]
        public byte[] Data { get; set; }

        [JsonProperty("machine_id")]
        public DateTime TimeServer { get; set; }
      
    }
}
