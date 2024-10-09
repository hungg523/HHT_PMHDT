using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NhaThuocOnline.Domain.Configurations;
using NhaThuocOnline.Domain.Entities;
using NhaThuocOnline.Domain.SeedDomain;

namespace NhaThuocOnline.Domain.EF
{
    public class NhaThuocOnlineDbContext : IdentityDbContext<AppStaffAccount, AppStaffRole, int>
    {
        public NhaThuocOnlineDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new CartConfiguration());

            modelBuilder.ApplyConfiguration(new CartItemConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CouponConfiguration());

            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerAddressConfiguration());

            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCouponConfiguration());
            modelBuilder.ApplyConfiguration(new ProductReturnConfiguration());

            modelBuilder.ApplyConfiguration(new ProductInCategoryConfiguration());

            modelBuilder.ApplyConfiguration(new BatchConfiguration());
            modelBuilder.ApplyConfiguration(new PrescriptionConfiguration());


            modelBuilder.ApplyConfiguration(new AppStaffAccountConfiguration());
            modelBuilder.Entity<AppStaffRole>().ToTable("AppStaffRoles").HasNoKey();

            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("AppUserClaims");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("AppUserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityRoleClaim<int>>().ToTable("AppRoleClaims");
            modelBuilder.Entity<IdentityUserToken<int>>().ToTable("AppUserTokens").HasKey(x => x.UserId);
            //Domain seeding
            modelBuilder.SeedDomainGenerate();

        }

        public DbSet<AppStaffAccount> AppStaffAccounts { get; set; }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCoupon> ProductCoupons { get; set; }
        public DbSet<ProductInCategory> ProductInCategories { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<ProductReturn> ProductReturns { get; set; }

        public DbSet<Prescription> Prescriptions { get; set; }


    }
}
