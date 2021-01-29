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
    public class MachinesViewModel
    {
        public Machines Machine { get; set; }
        public List<Commands> Commands { get; set; }
    }
}
