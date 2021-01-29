using APlataCore.Domain.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.Domain.Models
{
    public class Vendors : IBaseId
    {
        [JsonProperty("id")]
        public int Id { get ; set ; }

        [JsonProperty("name")]
        public string Name { get; set; }       
    }
}
