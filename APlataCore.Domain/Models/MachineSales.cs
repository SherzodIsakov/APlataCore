using APlataCore.Domain.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.Domain.Models
{
    public class MachineSales : IBaseId
    {

        [JsonProperty("machine_id")]
        public int Id { get ; set ; }

        [JsonProperty("machine_id")]
        public int MachineID { get; set; }

        [JsonProperty("machine_id")]
        public int TerminalID { get; set; }

        [JsonProperty("machine_id")]
        public int SellType { get; set; }

        [JsonProperty("machine_id")]
        public int ItemPrice { get; set; }

        [JsonProperty("machine_id")]
        public int MachineItemID { get; set; }

        [JsonProperty("machine_id")]
        public int ItemID { get; set; }

        [JsonProperty("machine_id")]
        public DateTime Time { get; set; }      

    }
}
