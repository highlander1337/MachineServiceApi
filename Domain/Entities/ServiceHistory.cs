using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MachineServiceApi.Domain.Entities
{
    public class ServiceHistory
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public int EmployerId {get; set;}
        public Machine? Machine { get; set; }
        public Employer? Employer {get; set;}
        public DateTime ServiceDate { get; set; }
        public string ServiceTypeCode { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}