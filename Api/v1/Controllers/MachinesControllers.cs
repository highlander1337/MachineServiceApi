using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MachineServiceApi.Api.v1.Dtos.Machines.Requests;
using MachineServiceApi.Api.v1.Dtos.Machines.Responses;
using MachineServiceApi.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MachineServiceApi.Api.v1.Controllers
{
    [ApiController]
    [Route("api/v1/clients/{clientId:int}/machines")]
    public class MachinesControllers : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public MachinesControllers(AppDbContext context)
        {
            _dbContext = context;
        }

        // ASSIGN MACHINE TO CLIENT
        [HttpPost]
        public async Task<IActionResult> AssignMachineToClient(int clientId, [FromBody] CreateMachineRequest request)
        {
            var clientExists = await _dbContext.Clients.FindAsync(clientId);
            if (clientExists == null)
            {
                return NotFound($"Client with ID {clientId} not found.");
            }

            var machine = new Domain.Entities.Machine
            {
                SerialNumber = request.SerialNumber,
                Model = request.Model,
                ClientId = clientId
            };
        
            
            _dbContext.Machines.Add(machine);

            await _dbContext.SaveChangesAsync();

            var response = new RetrieveMachineResponse(
                machine.Id,
                machine.SerialNumber,
                machine.Model
            );

            return Ok(response);
        }

        // GET MACHINES BY CLIENT ID
        [HttpGet]
        public async Task<IActionResult> GetMachinesByClientId(int clientId)
        {
            var clientExists = await _dbContext.Clients.FindAsync(clientId);
            if (clientExists == null)
            {
                return NotFound($"Client with ID {clientId} not found.");
            }

            var machines = await _dbContext.Machines
                .Where(m => m.ClientId == clientId)
                .AsNoTracking()
                .Select(m => new RetrieveMachineResponse(
                    m.Id,
                    m.SerialNumber,
                    m.Model
                ))
                .ToListAsync();

            return Ok(machines);
        }

        // DELETE MACHINE FROM CLIENT
        [HttpDelete("{machineId:int}")]
        public async Task<IActionResult> DeleteMachine(
            int clientId,
            int machineId)
        {
            var machine = await _dbContext.Machines
                .FirstOrDefaultAsync(m =>
                    m.Id == machineId &&
                    m.ClientId == clientId);

            if (machine is null)
            {
                return NotFound(
                    $"Machine with ID {machineId} not found for client {clientId}.");
            }

            _dbContext.Machines.Remove(machine);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

    }
}