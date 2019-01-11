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

        //public DbSet<MessageCategory> MessageCategory { get; set; }

        //public DbSet<MessageContent> MessageContent { get; set; }

        //public DbSet<Message> Message { get; set; }

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
                e.HasOne(x => x.Role).WithMany(y => y.Users).HasForeignKey(x => x.RoleId);
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
                e.ToTable("ShowRoomVlewer");
                //e.HasKey(x => x.Id);
                //e.Property(x => x.Id).ValueGeneratedOnAdd();
                //e.Property(x => x.Id).ValueGeneratedOnAdd().UseMySqlIdentityColumn();
                e.HasKey(x => new
                {
                    x.UserId
                ,
                    x.ShowRoomId
                });
                e.HasOne(x => x.ShowRoom).WithMany(y => y.ShowRoomVlewers).HasForeignKey(x => x.ShowRoomId);
                e.HasOne(x => x.User).WithMany(y => y.ShowRoomVlewers).HasForeignKey(x => x.UserId);
                e.Property(x => x.TimeStamp).IsRowVersion();
            });

            //modelBuilder.Entity<MessageCategory>(e =>
            //{
            //    e.ToTable("MessageCategory");
            //    e.HasKey(x => x.Id);
            //    e.Property(x => x.Id).ValueGeneratedOnAdd();
            //    e.Property(x => x.TimeStamp).IsRowVersion();
            //});

            //modelBuilder.Entity<MessageContent>(e => 
            //{
            //    e.ToTable("MessageContent");
            //    e.HasKey(x => x.Id);
            //    e.Property(x => x.Id).ValueGeneratedOnAdd();
            //    e.Property(x => x.TimeStamp).IsRowVersion();
            //    e.HasOne(x => x.MessageCategory).WithMany(y => y.MessageContents).HasForeignKey(x => x.CategoryId);
            //});

            //modelBuilder.Entity<Message>(e =>
            //{
            //    e.ToTable("Message");
            //    e.HasKey(x => x.Id);
            //    e.Property(x => x.Id).ValueGeneratedOnAdd();
            //    e.Property(x => x.TimeStamp).IsRowVersion();
            //    e.HasOne(x => x.MessageContent).WithMany(y => y.Messages).HasForeignKey(x => x.ContentId);
            //});
        }
    }
}
