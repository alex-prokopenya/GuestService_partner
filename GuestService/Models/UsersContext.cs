namespace GuestService.Models
{
    using System;
    using System.Data.Entity;
    using System.Runtime.CompilerServices;

    public class UsersContext : DbContext
    {
        public UsersContext() : base("db")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}

