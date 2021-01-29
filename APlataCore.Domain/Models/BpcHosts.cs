using APlataCore.Domain.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.Domain.Models
{
    public class BpcHosts : IBaseId
    {
        [JsonProperty("machine_id")]
        public int Id { get; set; }

        [JsonProperty("machine_id")]
        public int IP { get; set; }

        [JsonProperty("machine_id")]
        public int Port { get; set; }
    }
}
