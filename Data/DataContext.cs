// using Microsoft.EntityFrameworkCore;
// using ContactManager.Models;

// namespace ContactManager.Data
// {
//     public class DataContext : DbContext
//     {
//         public DataContext()
//         {

//         }

//         public DataContext(DbContextOptions<DataContext> options) : base
//         (options)
//         { }
//         public DbSet<TBLContact> tbl_Contact { get; set; }
//     }
// }
using Microsoft.EntityFrameworkCore;
using ContactManager.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactManager.Data
{
    public class DataContext : DbContext
    {
        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        public DbSet<TBLContact> tbl_contact { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;User Id=postgres;Password=Sph@2022;Port=5432");
            }
        }
    }
}
