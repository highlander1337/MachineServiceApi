using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineServiceApi.Api.v1.Dtos.Employer.Requests
{
    public class CreateEmployerRequest
    {
        public string Name { get; set; } = default!;
    }
}