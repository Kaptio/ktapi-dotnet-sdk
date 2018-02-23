using System;
using Microsoft.EntityFrameworkCore;
namespace Client2.Models
{

    public class PackageContext : DbContext
    {
        public PackageContext(DbContextOptions<PackageContext> options)
            : base(options)
        {
        }

        public DbSet<Package> Package { get; set; }
    }

    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SfdcId { get; set; }
        public string Data { get; set; }
        public string Details { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}
