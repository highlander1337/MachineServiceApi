using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineServiceApi.Api.v1.Dtos.Machines.Requests
{
    public class CreateMachineRequest
    {
        public string SerialNumber { get; set; } = default!;
        public string Model { get; set; } = default!;
    }
}