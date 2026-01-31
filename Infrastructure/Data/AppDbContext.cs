using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MachineServiceApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace MachineServiceApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Machine> Machines => Set<Machine>();
        public DbSet<ServiceHistory> ServiceHistories => Set<ServiceHistory>();
        public DbSet<Employer> Employers => Set<Employer>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}