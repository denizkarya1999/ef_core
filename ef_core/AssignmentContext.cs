//Implement Entity Framework Core
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ef_core
{
    public class AssignmentContext : DbContext
    {
        //Create entities of Assigments and Employees
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Employee> Employees { get; set; }


        //Seed employee Data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var E_Id = System.Guid.NewGuid();
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = E_Id,
                    FirstName = "Melisa",
                    LastName = "Tarpids"
                }
            );
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connect to the SQL server named EPITEC-27\SQLEXPRESS04 and database named ContactsDb
            optionsBuilder.UseSqlServer(
                @"Server =EPITEC-27\SQLEXPRESS04; Database = ContactsDb; Integrated Security = True");
        }


    }
}