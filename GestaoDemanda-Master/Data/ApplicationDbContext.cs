using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestaoDemanda_Master.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<GestaoDemanda_Master.Models.Responsavel> Responsavel { get; set; } = default!;

public DbSet<GestaoDemanda_Master.Models.Nivel> Nivel { get; set; } = default!;
    }
