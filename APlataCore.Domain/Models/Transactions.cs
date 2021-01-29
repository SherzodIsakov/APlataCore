using APlataCore.Domain.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlataCore.Domain.Models
{
    public class Transactions : IBaseId
    {                                                

        [JsonProperty("machine_id")]
        public int Id { get; set; }                  

        [JsonProperty("machine_id")]   
        public float Summ { get; set; }              

        [JsonProperty("machine_id")]
        public string RRN { get; set; }              

        [JsonProperty("machine_id")]
        public string ApprovalNumber { get; set; }   

        [JsonProperty("machine_id")]
        public int ResponseCode { get; set; }        

        [JsonProperty("machine_id")]
        public int TerminalID { get; set; }          

        [JsonProperty("machine_id")]
        public int TerminalMac { get; set; }         

        [JsonProperty("machine_id")]
        public DateTime Time { get; set; }           

        [JsonProperty("machine_id")]
        public int Result { get; set; }              

        [JsonProperty("machine_id")]
        public string Kassa_fn { get; set; }         

        [JsonProperty("machine_id")]
        public string Kassa_i { get; set; }          

        [JsonProperty("machine_id")]
        public string Kassa_fd { get; set; }         

        [JsonProperty("machine_id")]
        public int BankDelay { get; set; }           

        [JsonProperty("machine_id")]
        public int TotalServerDelay { get; set; }    

        [JsonProperty("machine_id")]
        public int TotalClientDelay { get; set; }    

        [JsonProperty("machine_id")]
        public int STAN { get; set; }                

        [JsonProperty("machine_id")]
        public string MTI { get; set; }              

        [JsonProperty("machine_id")]
        public string CardNumber { get; set; }

        [JsonProperty("machine_id")]
        public int ReverseID { get; set; }

        /// <summary>
        /// BLOB
        /// </summary>
        public byte[] BankPacket { get; set; }

    }
}
