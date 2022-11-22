using System.Linq;
using System.Net.NetworkInformation;
using Application.Catalogs.CatalogItems.UriComposer;
using Application.Dtos;
using Application.Interfaces.Contexts;
using Common;
using Microsoft.EntityFrameworkCore;

namespace Application.Catalogs.CatalogItems.GetCatalogItemPLP
{
    public interface IGetCatalogItemPLPService
    {
        PaginatedItemDto<CatalogPLPDto> Execute(CatlogPLPRequestDto dto);
    }

    public class GetCatalogItemPLPService : IGetCatalogItemPLPService
    {
        private readonly IDatabaseContext _context;
        private readonly IUriComposerService _uriComposerService;

        public GetCatalogItemPLPService(IDatabaseContext context, IUriComposerService uriComposerService)
        {
            _context = context;
          _uriComposerService = uriComposerService;
        }

        public PaginatedItemDto<CatalogPLPDto> Execute(CatlogPLPRequestDto request)
        {
            int rowCount = 0;
            var query = _context.CatalogItems
                .Include(s => s.CatalogItemImages)
                .Include(s => s.Discounts)
                .OrderByDescending(s => s.Id)
                .AsQueryable();

            if (request.brandId != null)
            {
                query = query.Where(s => request.brandId.Any(d => d == s.CatalogBrandId));
            }

            if (request.CatalogTypeId != null)
            {
                query = query.Where(s => s.CatalogTypeId == request.CatalogTypeId);
            }
            if (!string.IsNullOrEmpty(request.SearchKey))
            {
                query = query.Where(p => p.Name.Contains(request.SearchKey)
                                         || p.Description.Contains(request.SearchKey));
            }

            if (request.AvailableStock)
            {
                query = query.Where(s => s.AvailableStock > 0);
            }

            if (request.SortType == SortType.Bestselling)
            {
                query = query.Include(s => s.OrderItems)
                    .OrderByDescending(s => s.OrderItems.Count());
            }
            if (request.SortType == SortType.MostPopular)
            {
                query = query.Include(s => s.CatalogItemFavourites)
                    .OrderByDescending(s => s.CatalogItemFavourites.Count());
            }
            if (request.SortType == SortType.MostVisited)
            {
                query = query.OrderByDescending(s => s.VisitCount);
            }
            if (request.SortType == SortType.newest)
            {
                query = query
                    .OrderByDescending(p => p.Id);
            }

            if (request.SortType == SortType.cheapest)
            {
                query = query.Include(s => s.Discounts)
                    .OrderBy(p => p.Price);
            }

            if (request.SortType == SortType.mostExpensive)
            {
                query = query.Include(s => s.Discounts)
                    .OrderByDescending(p => p.Price);
            }

            var data = query.PagedResult(request.page, request.pageSize, out rowCount)
                .ToList()
                .Select(p =>  new CatalogPLPDto()
                {
                    Id = p.Id,
                    Rate = 4,
                    Name = p.Name,
                    Price = p.Price,
                    Image = _uriComposerService
                        .ComposeImageUri(p.CatalogItemImages?.FirstOrDefault()?.Src  ?? "" ),
                    Slug = p.Slug,
                    AvalableStock = p.AvailableStock
                }).ToList();
            return new PaginatedItemDto<CatalogPLPDto>(request.page, request.pageSize, rowCount, data);
        }
    }
    public class CatlogPLPRequestDto
    {
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public int? CatalogTypeId { get; set; }
        public int[] brandId { get; set; }
        public bool AvailableStock { get; set; }
        public string SearchKey { get; set; }
        public SortType SortType { get; set; }
    }


    public enum SortType
    {
        /// <summary>
        /// بدونه مرتب سازی
        /// </summary>
        None = 0,
        /// <summary>
        /// پربازدیدترین
        /// </summary>
        MostVisited = 1,
        /// <summary>
        /// پرفروش‌ترین
        /// </summary>
        Bestselling = 2,
        /// <summary>
        /// محبوب‌ترین
        /// </summary>
        MostPopular = 3,
        /// <summary>
        ///  ‌جدیدترین
        /// </summary>
        newest = 4,
        /// <summary>
        /// ارزان‌ترین
        /// </summary>
        cheapest = 5,
        /// <summary>
        /// گران‌ترین
        /// </summary>
        mostExpensive = 6,
    }

    public class CatalogPLPDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public byte Rate { get; set; }
        public int AvalableStock { get; set; }
    }
}