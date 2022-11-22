using System.Collections.Generic;
using System.Linq;
using Application.Catalogs.CatalogItems.UriComposer;
using Application.Interfaces.Contexts;
using Domain.Catalogs;
using Domain.Discounts;
using Microsoft.EntityFrameworkCore;

namespace Application.Catalogs.CatalogItems.GetCatalogItemPDP
{
    public interface IGetCatalogItemPDPService
    {
        CatalogItemPDPDto Execute(string Slug);
    }

    public class GetCatalogItemPDPService : IGetCatalogItemPDPService
    {
        private readonly IDatabaseContext _context;
        private readonly IUriComposerService _uriComposer;

        public GetCatalogItemPDPService(IDatabaseContext context, IUriComposerService uriComposer)
        {
            _context = context;
            _uriComposer = uriComposer;
        }

        public CatalogItemPDPDto Execute(string Slug)
        {
            var catalogItem = _context.CatalogItems
                .Include(s => s.CatalogBrand)
                .Include(s => s.CatalogType)
                .Include(s => s.CatalogItemImages)
                .Include(s => s.CatalogItemFeatures)
                .Include(s => s.Discounts)
                .SingleOrDefault(s => s.Slug == Slug);
            if (catalogItem != null)
            {
                catalogItem.VisitCount++;
                _context.SaveChanges();
            }

            var feature = catalogItem.CatalogItemFeatures
                .Select(p => new PDPFeaturesDto
                {
                    Group = p.Group,
                    Key = p.Key,
                    Value = p.Value,
                }).ToList()
                .GroupBy(s => s.Group);

            var SimilarCatalog = _context.CatalogItems
                .Include(s => s.CatalogItemImages)
                .Where(d => d.CatalogTypeId == catalogItem.CatalogTypeId)
                .Take(10)
                .Select(s => new SimilarCatalogItemDto()
                {
                    Id = s.Id ,
                    Name = s.Name,
                    Images = _uriComposer.ComposeImageUri(s.CatalogItemImages.FirstOrDefault().Src ?? ""),
                    Price = s.Price,
                }).ToList();

            if (catalogItem.Discounts != null)
            {
                catalogItem = GetPrice(catalogItem);

            }
    

            return new CatalogItemPDPDto()
            {
                Features = feature,
                SimilarCatalogs = SimilarCatalog,
                Id = catalogItem.Id,
                Name = catalogItem.Name,
                Brand = catalogItem.CatalogBrand.Brand,
                Type = catalogItem.CatalogType.Type,
                Price = catalogItem.Price,
                Description = catalogItem.Description,
                Images = catalogItem.CatalogItemImages.Select(s => _uriComposer.ComposeImageUri(s.Src)).ToList(),
                OldPrice = catalogItem.OldPrice,
                DiscountPercent = catalogItem.Discountpersent
            };

        }
        private CatalogItem GetPrice(CatalogItem catalogItem)
        {
            var dis = GetPreferredDiscount(catalogItem.Discounts, catalogItem.Price);
            if (dis != null)
            {
                var disCountPrice = dis.GetDiscountAmount(catalogItem.Price);
                int newPrice = catalogItem.Price - disCountPrice;
                catalogItem.Discountpersent = (disCountPrice * 100) / catalogItem.Price;

                catalogItem.OldPrice = catalogItem.Price;
                catalogItem.Price = newPrice;

                _context.SaveChanges();
                return catalogItem;
            }

            return catalogItem;
        }
        /// دریافت بیشترین تخفیف
        private Discount GetPreferredDiscount(ICollection<Discount> discounts, int price)
        {
            Discount preferredDiscount = null;
            decimal? maximumDiscountValue = null;
            if (discounts != null)
            {
                foreach (var discount in discounts)
                {
                    var currentDiscountValue = discount.GetDiscountAmount(price);
                    if (currentDiscountValue != decimal.Zero)
                    {
                        if (!maximumDiscountValue.HasValue || currentDiscountValue > maximumDiscountValue)
                        {
                            maximumDiscountValue = currentDiscountValue;
                            preferredDiscount = discount;
                        }
                    }
                }
            }

            return preferredDiscount;
        }
    }
  
    public class CatalogItemPDPDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public int Price { get; set; }
        public int? OldPrice { get; set; }  
        public int? DiscountPercent { get; set; }
        public List<string> Images { get; set; }
        public string Description { get; set; }
        public IEnumerable<IGrouping<string, PDPFeaturesDto>> Features { get; set; }
        public List<SimilarCatalogItemDto> SimilarCatalogs { get; set; }

    }

    public class PDPFeaturesDto
    {
        public string Group { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }


    public class SimilarCatalogItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string? Images { get; set; }
    }
}