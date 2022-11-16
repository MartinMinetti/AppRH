using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppRH.Models;

    public class AppRHContext : DbContext
    {
        public AppRHContext (DbContextOptions<AppRHContext> options)
            : base(options)
        {
        }

        public DbSet<AppRH.Models.Customer> Customer { get; set; } = default!;

        public DbSet<AppRH.Models.House>? House { get; set; }

        public DbSet<AppRH.Models.Rental>? Rental { get; set; }

        public DbSet<AppRH.Models.RentalDetail>? RentalDetail { get; set; }

        public DbSet<AppRH.Models.RentalDetailTemp>? RentalDetailTemp { get; set; }

        public DbSet<AppRH.Models.Return>? Return { get; set; }

        public DbSet<AppRH.Models.ReturnDetail>? ReturnDetail { get; set; }

        public DbSet<AppRH.Models.ReturnDetailTemp>? ReturnDetailTemp { get; set; }
    }
