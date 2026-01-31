using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineServiceApi.Api.v1.Dtos.ServiceHistory.Requests
{
    public class CreateServiceHistoryRequest
    {
        public string ServiceTypeCode { get; set; } = default!;
        public DateTime ServiceDate { get; set; }
        public string Details { get; set; } = default!;
    }
}