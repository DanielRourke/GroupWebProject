using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GroupWebProject.Models;

namespace GroupWebProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GroupWebProject.Models.Customer> Customer { get; set; }
        public DbSet<GroupWebProject.Models.Room> Room { get; set; }
        public DbSet<GroupWebProject.Models.Booking> Booking { get; set; }
    }
}
