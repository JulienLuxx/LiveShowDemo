using LiveShow.Domain.Entitis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace LiveShow.Domain
{
    public class LiveShowDBContext:DbContext
    {
        public LiveShowDBContext() { }

        public DbSet<User> User { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<ShowRoom> ShowRoom { get; set; }

        public DbSet<ShowRoomVlewer> ShowRoomViewer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //MSSql
            optionsBuilder.UseLazyLoadingProxies().ConfigureWarnings(action => action.Ignore(CoreEventId.DetachedLazyLoadingWarning)).UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=LiveShowDB;Trusted_Connection=True;");
            //MySql
            //optionsBuilder.UseMySql(@"server=localhost;database=TestDB;user=root;password=1234;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("User");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                //e.Property(x => x.Id).ValueGeneratedOnAdd().UseMySqlIdentityColumn();
                e.Property(x => x.TimeStamp).IsRowVersion();
            });

            modelBuilder.Entity<Role>(e =>
            {
                e.ToTable("Role");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                //e.Property(x => x.Id).ValueGeneratedOnAdd().UseMySqlIdentityColumn();
                e.Property(x => x.TimeStamp).IsRowVersion();
            });

            modelBuilder.Entity<ShowRoom>(e =>
            {
                e.ToTable("ShowRoom");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                //e.Property(x => x.Id).ValueGeneratedOnAdd().UseMySqlIdentityColumn();
                e.Property(x => x.TimeStamp).IsRowVersion();
            });

            modelBuilder.Entity<ShowRoomVlewer>(e =>
            {
                e.ToTable("ShowRoomUser");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                //e.Property(x => x.Id).ValueGeneratedOnAdd().UseMySqlIdentityColumn();
                e.Property(x => x.TimeStamp).IsRowVersion();
            });
        }
    }
}
