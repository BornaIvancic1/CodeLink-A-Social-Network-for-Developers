using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace API.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DatabaseConnection",
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();   
            
            modelBuilder.Entity<User>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();  
            
            modelBuilder.Entity<Post>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd(); 
            
            modelBuilder.Entity<Technology>()
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Technology>()
                .Property(t => t.SkillLevel)
                .HasConversion<string>();

            modelBuilder.Entity<Post>()
     .HasOne(p => p.User)
     .WithMany()
     .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Admin)
                .WithMany()
                .HasForeignKey(p => p.AdminId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);


        }


        public DbSet<User> User { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Message> Message { get; set; } 
        
        public DbSet<Friend> Friend { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Technology> Technology { get; set; }
 
    }
}
