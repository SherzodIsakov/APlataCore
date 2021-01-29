using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.Domain.Models
{
    public class TokensInfo
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("owner_id")]
        public int OwnerID { get; set; }
    }
}
