using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Final_Project_Car_Wash.Models;

namespace Final_Project_Car_Wash.Data
{
    public class Final_Project_Car_WashDbContext : DbContext
    {
        public Final_Project_Car_WashDbContext (DbContextOptions<Final_Project_Car_WashDbContext> options)
            : base(options)
        {
        }

        public DbSet<Final_Project_Car_Wash.Models.Customer> Customer { get; set; }

        public DbSet<Final_Project_Car_Wash.Models.Services> Services { get; set; }

        public DbSet<Final_Project_Car_Wash.Models.Booking> Booking { get; set; }
    }
}
