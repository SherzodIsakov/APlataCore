using APlataCore.Domain.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.Domain.Models
{
    public class MachineItemsMap : IBaseId
    {
        [JsonProperty("id")]
        public int Id { get ; set ; }

        [DisplayName("Кнопка")]
        [JsonProperty("machine_id")]
        public string MachineID { get; set; }

        [JsonProperty("machine_item_id")]
        public int MachineItemID { get; set; }

        [DisplayName("Товар")]
        [JsonProperty("item_id")]
        public int ItemID { get; set; }


        [JsonProperty("owner_id")]
        public int OwnerID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("sales_total")]
        public int SalesTotal { get; set; }

        [JsonProperty("last_time")]
        public DateTime LastTime { get; set; }     
    }
}
