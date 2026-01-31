using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineServiceApi.Api.v1.Dtos.ServiceHistory.Responses
{
    public record RetrieveServiceResponse
    (
        int Id,
        int EmployerId,
        DateTime ServiceDate,
        string ServiceType,
        string Description
    );
}