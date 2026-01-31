using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MachineServiceApi.Api.v1.Dtos.Clients.Responses;
using MachineServiceApi.Api.v1.Dtos.Clients.Requests;
using MachineServiceApi.Domain.Entities;
using MachineServiceApi.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MachineServiceApi.Api.v1.Controllers
{
    [ApiController]
    [Route("api/v1/clients")]
    public class ClientsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ClientsController(AppDbContext context)
        {
            _dbContext = context;
        }

        // CREATE CLIENT
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientRequest request)
        {
            var client = new Client
            {
                Name = request.Name
            };

            _dbContext.Clients.Add(client);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAll), new { id = client.Id }, client);
        }

        // GET ALL CLIENTS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        => Ok(await _dbContext.Clients
        .Select(m => new RetrieveClientResponse(
            m.Id,
            m.Name
        ))
        .ToListAsync());
    }
}