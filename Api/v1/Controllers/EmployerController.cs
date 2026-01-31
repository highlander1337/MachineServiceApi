using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MachineServiceApi.Api.v1.Dtos.Employer.Requests;
using MachineServiceApi.Domain.Entities;
using MachineServiceApi.Api.v1.Dtos.Employer.Responses;
using MachineServiceApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MachineServiceApi.Api.v1.Controllers
{
    [ApiController]
    [Route("api/v1/employers")]
    public class EmployerController : ControllerBase
    {
    
        private readonly AppDbContext _dbContext;
        public EmployerController(AppDbContext context)
        {
            _dbContext = context;
        }

        // CREATE EMPLOYER
        [HttpPost]
        public async Task<IActionResult> CreateEmployer([FromBody] CreateEmployerRequest request)
        {
            var client = new Employer
            {
                Name = request.Name
            };

            _dbContext.Employers.Add(client);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAll), new { id = client.Id }, client);
        }

        // GET ALL EMPLOYERS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        => Ok(await _dbContext.Employers
        .Select(m => new RetrieveEmployerResponse(
            m.Id,
            m.Name
        )).ToListAsync());
    }
    
}