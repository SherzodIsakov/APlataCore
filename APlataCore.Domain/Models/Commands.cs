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
    public class Commands : IBaseId
    {
        [JsonProperty("id")]
        public int Id { get ; set ; }

        [JsonProperty("terminal_id")]
        public int TerminalID { get; set; }

        [DisplayName("Комманда")]
        [JsonProperty("command_id")]
        public int CommandID { get; set; }

        [DisplayName("Параметр 1")]
        [JsonProperty("parameter1")]
        public int Parameter1 { get; set; }

        [DisplayName("Параметр 2")]
        [JsonProperty("parameter2")]
        public int Parameter2 { get; set; }

        [DisplayName("Параметр 3")]
        [JsonProperty("parameter3")]
        public int Parameter3 { get; set; }

        [DisplayName("Параметр 4")]
        [JsonProperty("parameter4")]
        public int Parameter4 { get; set; }

        [JsonProperty("str_parameter1")]
        public string StrParameter1 { get; set; }
        [JsonProperty("str_parameter2")]
        public string StrParameter2 { get; set; }


        [JsonProperty("state")]
        public int State { get; set; }

        [DisplayName("Статус")]
        [JsonProperty("state_name	")]
        public string StateName { get; set; }

        [DisplayName("Дата и время отправки")]
        [JsonProperty("time_created")]
        public DateTime? TimeCreated { get; set; }

        [JsonProperty("time_delivered")]
        public DateTime? TimeDelivered { get; set; }     

    }
}
