using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Radio.Models;

namespace Radio
{
    public partial class radiostationContext : IdentityDbContext<User>
    {
        public radiostationContext()
        {
        }

        public radiostationContext(DbContextOptions<radiostationContext> options): base(options)
        {
        }

        public virtual DbSet<Artists> Artists { get; set; }
        public virtual DbSet<Ganrs> Ganrs { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<Records> Records { get; set; }
        public virtual DbSet<Staffs> Staffs { get; set; }
        public virtual DbSet<Streams> Streams { get; set; }

    } 
}
