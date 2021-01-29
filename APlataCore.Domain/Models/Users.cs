using APlataCore.Domain.Infrastructure;
using Newtonsoft.Json;
using System;

namespace APlataCore.Domain.Models
{
    public class Users : IBaseId
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("role")]
        public int Role { get; set; }

        [JsonProperty("owner_id")]
        public int OwnerID { get; set; }

        [JsonProperty("owner_name")]
        public string UserToken { get; set; }

        //--------------------------------------

        public DateTime NotificationsFrom { get; set; }
        public DateTime NotificationsTo { get; set; }
        public DateTime LastOnlineTime { get; set; }


    }
}
