using APlataCore.Domain.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.Domain.Models
{
    public class Terminals : IBaseId
    {
        [JsonProperty("id")]
        public int Id { get ; set ; }

        [JsonProperty("serial_number")]
        public long TerminalNumber { get; set; }

        [JsonProperty("bank_id")]
        public string MerchantID { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("last_online_time")]
        public DateTime LastOnlineTime { get; set; }

        [JsonProperty("machine_name")]
        public string MachineName { get; set; }

        [JsonProperty("gsm_operator")]
        public string GsmOperator { get; set; }

        [JsonProperty("gsm_rssi")]
        public int GsmRssi { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        //------------------------------------------------------------

        public string TerminalID { get; set; }
        public int MachineID { get; set; }
        public int TerminalMac { get; set; }
        public string SSLCert { get; set; }
        public string SSLKey { get; set; }      
        public string TerminalAddress { get; set; }
        public string TerminalPlace { get; set; }
        public int Ping { get; set; }        
        public int OwnerID { get; set; }
        public int STAN { get; set; }    
        public int GsmQuality { get; set; } 
        public int GpsInaccuracy { get; set; }
        public int SlaveMode { get; set; }
    }

    
}
