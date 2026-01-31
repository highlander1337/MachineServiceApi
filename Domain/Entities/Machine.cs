using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineServiceApi.Domain.Entities
{
    public class Machine
    {
        public int Id { get; set; }
        public string? SerialNumber { get; set; }
        public string? Model { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
    }
}