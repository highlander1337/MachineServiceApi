using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineServiceApi.Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Machine> Machines = new List<Machine>();
    }
}