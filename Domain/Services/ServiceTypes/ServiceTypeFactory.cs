using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MachineServiceApi.Domain.Services.ServiceTypes
{
    public static class ServiceTypeFactory
    {
        public static ServiceType GetServiceTypeByCode(string code)
        {
            return code.ToUpper() switch
            {
                "INSPECTION" => new InspectionService(),
                "INSTALLATION" => new InstallationService(),
                "CLEANING" => new CleaningService(),
                "FIX" => new FixService(),
                _ => throw new ArgumentException($"Unknown service type code: {code}")
            };
        }
    }
}