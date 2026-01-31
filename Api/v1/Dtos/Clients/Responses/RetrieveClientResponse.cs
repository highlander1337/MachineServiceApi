using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineServiceApi.Api.v1.Dtos.Clients.Responses
{
    public record RetrieveClientResponse
    (
        int Id,
        string? Name
    );
}