using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace TestingMVC.Models.Data
{
    public class AppDbContext: IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
        //Burda men Default olaraq elavelerimi ede bilerem
                new Employee
                {
                    Id = 1,
                    Name = "Jabrail",
                    Department = Dept.IT,
                    Email = "Jabrail@mail.com"
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
