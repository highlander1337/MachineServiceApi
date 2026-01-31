using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MachineServiceApi.Api.v1.Dtos.ServiceHistory.Requests;
using MachineServiceApi.Api.v1.Dtos.ServiceHistory.Responses;
using MachineServiceApi.Domain.Entities;
using MachineServiceApi.Domain.Services.ServiceTypes;
using MachineServiceApi.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MachineServiceApi.Api.v1.Controllers
{
    [ApiController]
    [Route("api/v1/clients/{clientId:int}/machines/{machineId:int}/employer/{employerId:int}/service-history")]
    public class ServiceHistoryController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        public ServiceHistoryController(AppDbContext context)
        {
            _dbContext = context;
        }

        // CREATE SERVICE HISTORY RECORD
        [HttpPost]
        public async Task<IActionResult> CreateServiceHistoryRecord(
            int clientId, 
            int machineId,
            int employerId,
            CreateServiceHistoryRequest request)
        {
            var machine = await _dbContext.Machines
            .Include(m => m.Client)
            .FirstOrDefaultAsync(m => m.Id == machineId && m.ClientId == clientId);
            if (machine == null)
            {
                return NotFound($"Machine with ID {machineId} for Client ID {clientId} not found.");
            }

            var serviceType = ServiceTypeFactory.GetServiceTypeByCode(request.ServiceTypeCode);

            var history = new ServiceHistory
            {
                MachineId = machineId,
                EmployerId = employerId,
                ServiceDate = request.ServiceDate,
                ServiceTypeCode = request.ServiceTypeCode,
                Description = request.Details
            };

            _dbContext.ServiceHistories.Add(history);

            await _dbContext.SaveChangesAsync();

            var response = new RetrieveServiceResponse(
                history.Id,
                history.EmployerId,
                history.ServiceDate,
                history.ServiceTypeCode,
                history.Description
            );
            return Ok(response);
        }

        // GET ALL SERVICES OF A MACHINE
        [HttpGet]
        public async Task<IActionResult> GetServiceHistoryByMachineId(
            int clientId, 
            int machineId)
        {
            var services = await _dbContext.ServiceHistories
                .Where(sh => sh.MachineId == machineId && sh.Machine!.ClientId == clientId)
                .AsNoTracking()
                .Select(m => new RetrieveServiceResponse(
                        m.Id,
                        m.EmployerId,
                        m.ServiceDate,
                        m.ServiceTypeCode,
                        m.Description
                    )
                )
                .ToListAsync();

            return Ok(services);
        }

        // GET ALL SERVICE DETAILS OF A SERVICE RECORD
        [HttpGet("{serviceHistoryId:int}")]
        public async Task<IActionResult> GetServiceDetails(
            int clientId, 
            int machineId, 
            int serviceHistoryId)
        {
            var service = await _dbContext.ServiceHistories
                .Include(sh => sh.Machine)
                .FirstOrDefaultAsync(sh => sh.Id == serviceHistoryId 
                    && sh.MachineId == machineId 
                    && sh.Machine!.ClientId == clientId);
            if (service == null)
            {
                return NotFound($"Service history record with ID {serviceHistoryId} not found for Machine ID {machineId} and Client ID {clientId}");
            }

            var response = new RetrieveServiceResponse(
                service.Id,
                service.EmployerId,
                service.ServiceDate,
                service.ServiceTypeCode,
                service.Description
            );

            return Ok(response);
        }

        // DELETE SERVICE HISTORY
        [HttpDelete("{serviceId:int}")]
        public async Task<IActionResult> Delete(
            int clientId,
            int machineId,
            int serviceId)
        {
            var service = await _dbContext.ServiceHistories
                .Include(s => s.Machine)
                .FirstOrDefaultAsync(s =>
                    s.Id == serviceId &&
                    s.MachineId == machineId &&
                    s.Machine!.ClientId == clientId);

            if (service is null)
                return NotFound("Service history not found for the given client and machine");

            _dbContext.ServiceHistories.Remove(service);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}