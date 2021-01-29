using APlataCore.Domain.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.Domain.Models
{
    public class MachineModels : IBaseId
    {

        [JsonProperty("machine_model_id")]
        public int Id { get ; set ; }

        [JsonProperty("machine_model_name")]
        public string Name { get; set; }

        [JsonProperty("vendor_id")]
        public int VendorID { get; set; }

        [JsonProperty("owner_id")]
        public int OwnerID { get; set; }     

    }
}
