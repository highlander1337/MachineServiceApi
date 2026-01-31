using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineServiceApi.Api.v1.Dtos.Machines.Responses
{
    public record RetrieveMachineResponse
    (
        int MachineId,
        string? SerialNumber,
        string? Model
    );
}