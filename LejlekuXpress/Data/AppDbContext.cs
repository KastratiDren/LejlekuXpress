using LejlekuXpress.Models;
using Microsoft.EntityFrameworkCore;

namespace LejlekuXpress.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<User>? User { get; set; }

        public DbSet<Roles>? Roles { get; set; }

        public DbSet<Country>? Country { get; set; }

        public DbSet<ShippingAddress>? ShippingAddress { get; set; }

        public DbSet<Payment>? Payment { get; set; }

        public DbSet<Product>? Product { get; set; }

        public DbSet<Category>? Category { get; set; }

        public DbSet<Wishlist>? Wishlist { get; set; }

        public DbSet<Cart>? Cart { get; set; }

        public DbSet<CheckOut>? CheckOut { get; set; }

        public DbSet<Orders>? Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure cascade delete only for the User-Cart relationship
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull); // Cascade only for Cart

            // Configure ClientSetNull or Restrict for other relationships to avoid multiple cascade paths
            modelBuilder.Entity<CheckOut>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull); // Avoids cascading by setting to null

            modelBuilder.Entity<Orders>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull); // Avoids cascading by setting to null

            modelBuilder.Entity<Wishlist>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull); // Avoids cascading by setting to null
        }


    }
}
