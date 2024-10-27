using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=PC-ALPHA; Database=FilesDB; Trusted_Connection=True; TrustServerCertificate=True;")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<FileEntity> Files { get; set; }
    }
}
