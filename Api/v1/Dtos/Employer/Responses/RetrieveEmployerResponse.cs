using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineServiceApi.Api.v1.Dtos.Employer.Responses
{
    public record RetrieveEmployerResponse
    (
        int Id,
        string? Name
    );
}