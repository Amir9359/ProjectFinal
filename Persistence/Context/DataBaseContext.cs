using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Catalogs.CatalogItems.CatalogItemComments;
using Application.Interfaces.Contexts;
using Domain.Attributes;
using Domain.Banners;
using Domain.Baskets;
using Domain.Catalogs;
using Domain.Discounts;
using Domain.Orders;
using Domain.Payments;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityConfiguration;
using Persistence.Seeds;

namespace Persistence.Context
{
    public class DataBaseContext : DbContext , IDatabaseContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
        }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<DiscountUsageHistory> DiscountUsageHistories { get; set; }
        public DbSet<CatalogItemFavourite> CatalogItemFavourites { get; set; } 
        public DbSet<Banner> Banners { get; set; } 
        public DbSet<CatalogItemComment> CatalogItemComments { get; set; }
        public DbSet<CatalogItemFeature> CatalogItemFeature { get; set; }
        public DbSet<CatalogItemImage> CatalogItemImage { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("dbo");
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetCustomAttributes(typeof(AudtableAttribute), true).Length > 0 )
                {
                    builder.Entity(entityType.Name).Property<DateTime>("InsertTime").HasDefaultValue(DateTime.Now);
                    builder.Entity(entityType.Name).Property<DateTime?>("EditTime");
                    builder.Entity(entityType.Name).Property<DateTime?>("RemoveTime");
                    builder.Entity(entityType.Name).Property<bool>("IsRemoved").HasDefaultValue(false);
                }
            }
            builder.Entity<CatalogType>()
                .HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);   
            builder.Entity<Basket>()
                .HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);
            builder.Entity<BasketItem>()
                .HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);
            builder.Entity<CatalogItemFavourite>()
                .HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);
            builder.Entity<CatalogItem>()
                .HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

            builder.Entity<CatalogItemFeature>()
                .HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

            builder.Entity<CatalogItemImage>()
                .HasQueryFilter(m => EF.Property<bool>(m, "IsRemoved") == false);

            builder.ApplyConfiguration(new CatalogBrandConfiguration());
            builder.ApplyConfiguration(new CatalogTypeConfiguration());
            builder.ApplyConfiguration(new CatalogItemConfiguration());

            //seed 
            //DatabaseContextSeed.CatalogSeed(builder);

            // do not create Address in Order tbl
            builder.Entity<Order>().OwnsOne(s => s.Address);

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(s => s.State == EntityState.Added ||
                            s.State == EntityState.Modified ||
                            s.State == EntityState.Deleted
                );
            foreach (var entity in modifiedEntities)
            {
                var entityType = entity.Context.Model.FindEntityType(entity.Entity.GetType());
                if (entityType != null)
                {

                    var insertTime = entityType.FindProperty("InsertTime");
                    var updateTime = entityType.FindProperty("EditTime");
                    var removeTime = entityType.FindProperty("RemoveTime");
                    var isRemoved = entityType.FindProperty("IsRemoved");

                    if (entity.State == EntityState.Added && insertTime != null)
                    {
                        entity.Property("InsertTime").CurrentValue = DateTime.Now;
                    }
                    if (entity.State == EntityState.Modified && updateTime != null)
                    {
                        entity.Property("EditTime").CurrentValue = DateTime.Now;
                    }
                    if (entity.State == EntityState.Deleted && removeTime != null && isRemoved != null)
                    {
                        entity.Property("RemoveTime").CurrentValue = DateTime.Now;
                        entity.Property("IsRemoved").CurrentValue = true;
                        entity.State = EntityState.Modified;
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}
