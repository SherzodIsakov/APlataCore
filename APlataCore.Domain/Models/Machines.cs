using APlataCore.Domain.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APlataCore.Domain.Models
{
    public class Machines : IBaseId
    {
        [JsonProperty("machine_id")]
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        [JsonProperty("machine_name")]
        public string Name { get; set; }

        [Display(Name = "Модель")]
        [JsonProperty("machine_model")]
        public string MachineModel { get; set; }
       
        [JsonProperty("model_id")]
        public int ModelID { get; set; }

        [Display(Name = "Адрес")]
        [JsonProperty("machine_address")]
        public string Address { get; set; }

        [Display(Name = "ID Терминала")]
        [JsonProperty("terminal_id")]
        public int? StateID { get; set; }

        [JsonProperty("owner_id")]
        public int OwnerID { get; set; }
    }  
}
