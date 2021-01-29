using APlataCore.Domain.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.Domain.Models
{
    public class ResponseDescriptions //: IBaseId
    {
        //public int Id { get ; set ; }

        [JsonProperty("machine_id")]
        public int ResponseCode { get; set; }

        [JsonProperty("machine_id")]
        public string Description { get; set; }     
       
    }
}
