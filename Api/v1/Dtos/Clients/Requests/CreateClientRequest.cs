using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineServiceApi.Api.v1.Dtos.Clients.Requests
{
    public class CreateClientRequest
    {
        public string Name { get; set; } = default!;
    }
}