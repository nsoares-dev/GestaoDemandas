using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GestaoDemanda_Master.Models;

namespace GestaoDemanda_Master.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GestaoDemanda_Master.Models.Responsavel> Responsavel { get; set; } = default!;
        public DbSet<GestaoDemanda_Master.Models.Nivel> Nivel { get; set; } = default!;
        public DbSet<GestaoDemanda_Master.Models.StatusDemanda> StatusDemanda { get; set; } = default!;
    }
}
