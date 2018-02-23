using System;
using Microsoft.EntityFrameworkCore;

namespace BeltExam.Models
{
    public class Context : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<RegisterUser> User { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<UserActivity> UserActivity { get; set; }
    }

}