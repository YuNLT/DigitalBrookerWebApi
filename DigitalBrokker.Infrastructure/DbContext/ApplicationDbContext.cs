using DigitalBrooker.Domain.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DigitalBrokker.Infrastructure.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<PasswordReset> PasswordResets { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<UserPayment> UserPayment { get; set; }
        public DbSet<PropertyDetail> PropertyDetail { get; set; }
        public DbSet<Documentation> Documentations { get; set; }
        public DbSet<SellerRequest> SellerRequests { get; set; }
        public DbSet<BuyerRequest> BuyerRequests { get; set; }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .Property(u=>u.FirstName).HasMaxLength(256);
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .Property(u => u.LastName).HasMaxLength(256);

            base.OnModelCreating(builder);
            builder.Entity<Property>(entity =>
            {
                entity.Property(p => p.PropertyViewId).HasMaxLength(36);
                entity.Property(p => p.Address).HasMaxLength(256);
                entity.Property(p => p.Price).HasColumnType("decimal(18,2)");
                entity.Property(p => p.Township).HasMaxLength(100);
                entity.Property(p => p.Title).HasMaxLength(256);
                entity.Property(p => p.IsActive).HasDefaultValue(true);
                entity.Property(p => p.IsVerify).HasDefaultValue(false);
                entity.Property(p=>p.TourService).HasDefaultValue(false);

                entity.Property(p => p.PropertyTypeValue)
                .HasColumnName("PropertyType").HasMaxLength(20);
                entity.ToTable(tb =>
                {
                    tb.HasCheckConstraint("CK_Property_PropertyType",
                        "[PropertyType] IS NULL OR [PropertyType] IN ('House', 'Yard', 'HouseAndYard')");
                });
            });

            base.OnModelCreating(builder);
            builder.Entity<UserPayment>(entity =>
            {
                entity.Property(up => up.Amount)
                    .HasColumnType("decimal(18,2)");

                entity.Property(up => up.PaymentTypeValue)
                .HasColumnName("PaymentType")
                .HasMaxLength(30);

                entity.ToTable(tb =>
                {
                    tb.HasCheckConstraint("CK_UserPayment_PaymentType",
                        "[PaymentType] IS NULL OR [PaymentType] IN ('ListingFee','DepositeFee', 'ServiceFee', 'TourServiceFee')");
                });

                entity.Property(up=>up.PaidAt).HasDefaultValueSql("GETUTCDATE()"); ;
            }) ;

           base.OnModelCreating(builder);
            builder.Entity<SellerRequest>(entity=>
            {
                entity.Property(sr=>sr.RequestedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(sr=>sr.RequestStatusValue).HasColumnName("RequestStatus")
                .HasMaxLength(10);
                entity.ToTable(tb =>
                {
                    tb.HasCheckConstraint("CK_SellerRequest_RequestStatus",
                        "[RequestStatus] IN ('Pending','Accepted','Rejected')");
                });
            });

            base.OnModelCreating(builder);
            builder.Entity<BuyerRequest>(entity =>
            {
                entity.Property(p => p.PropertyViewId).HasMaxLength(36);
                entity.Property(p => p.ComfirmCode).HasDefaultValue(100);
                entity.Property(p => p.BuyerRequestTypeValue)
                      .HasColumnName("BuyerRequestType") 
                      .HasMaxLength(20);

                entity.ToTable(tb =>
                {
                    tb.HasCheckConstraint("CK_BuyerRequest_BuyerRequestType",
                        "[BuyerRequestType] IS NULL OR [BuyerRequestType] IN ('Tour','Purchase')");
                });

                entity.Property(p=>p.RequestedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            base.OnModelCreating(builder);
            builder.Entity<PropertyDetail>(entity =>
            {
                entity.Property(p=>p.Area).HasMaxLength(50);
                entity.Property(p=>p.HouseType).HasMaxLength(50);
                entity.HasMany(pd=>pd.Documentations)
                .WithOne(d=>d.PropertyDetail)
                .HasForeignKey(d=>d.PropertyDetailId)
                .OnDelete(DeleteBehavior.NoAction);
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            builder.Entity<Documentation>(entity =>
            {
                entity.Property(d => d.Document).HasColumnType("varbinary(max)");
            });
        }
    }
}
