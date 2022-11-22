using Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Catalogs;
using Domain.Baskets;
using Domain.Orders;
using Domain.Payments;
using Domain.Discounts;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;
using Domain.Banners;
using Application.Catalogs.CatalogItems.CatalogItemComments;

namespace Application.Interfaces.Contexts
{
    public interface IDatabaseContext
    {
        DbSet<CatalogBrand> CatalogBrands { get; set; }
        DbSet<CatalogType> CatalogTypes { get; set; }
        DbSet<CatalogItem> CatalogItems { get; set; }
        DbSet<Basket> Baskets { get; set; }
        DbSet<BasketItem> BasketItems { get; set; }
        DbSet<UserAddress> UserAddresses { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderItem> OrderItems { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<Discount> Discounts { get; set; }
        DbSet<DiscountUsageHistory> DiscountUsageHistories { get; set; }
        DbSet<CatalogItemFavourite> CatalogItemFavourites { get; set; }
        DbSet<Banner> Banners { get; set; }
        DbSet<CatalogItemComment> CatalogItemComments { get; set; }
        DbSet<CatalogItemFeature> CatalogItemFeature { get; set; }
        DbSet<CatalogItemImage> CatalogItemImage { get; set; }

        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken());

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
        EntityEntry Entry([NotNull] object entity);
    }
}
