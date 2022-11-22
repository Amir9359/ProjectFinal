using System.Collections;
using Application.Interfaces.Contexts;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Application.Catalogs.CatalogItems.UriComposer;
using Microsoft.EntityFrameworkCore;
using Application.Dtos;
using Common;
using Domain.Catalogs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Application.Catalogs.CatalogItems.CatalogItemServices
{
    public interface ICatalogItemServices
    {
        List<CatalogBrandDto> GetBrand();
        List<ListCatalogTypeDto> GetCatalogType();
        PaginatedItemDto<CatalogItemListItemDto> GetCatalogList(int page, int pageSize);
        CatalogItemItemDto GetCatalogItem(int Id);
        void AddToFavorite(string UserId, int CatalogItemId);
        PaginatedItemDto<FavouriteCatalogItemDto> GetMayFavourite(string UserId, int PageIndex = 1, int PageSize = 20);
        void RemoveFavorite(string UserId, int CatalogItemId);
        void RemoveCatalogItem(int id);

    }

    public class CatalogItemServices : ICatalogItemServices
    {
        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUriComposerService _composerService;

        public CatalogItemServices(IDatabaseContext context, IMapper mapper, IUriComposerService composerService)
        {
            _context = context;
            _mapper = mapper;
            _composerService = composerService;
        }
        public List<CatalogBrandDto> GetBrand()
        {
           var brands = _context.CatalogBrands.OrderBy(p => p.Brand).Take(500).ToList();
            var data = _mapper.Map<List<CatalogBrandDto>>(brands);
            return data;
        }

        public List<ListCatalogTypeDto> GetCatalogType()
        {
            var types = _context.CatalogTypes
                .Include(p => p.ParentCatalogType)
                .Include(p => p.ParentCatalogType)
                .ThenInclude(p => p.ParentCatalogType.ParentCatalogType)
                .Include(p => p.SubTypes)
                .Where(p => p.ParentCatalogTypeId != null)
                .Where(p => p.SubTypes.Count == 0)
                .Select(p => new { p.Id, p.Type, p.ParentCatalogType, p.SubTypes })
                .ToList()
                .Select(p => new ListCatalogTypeDto
                {
                    Id = p.Id,
                    Type = $"{p?.Type ?? ""} - {p?.ParentCatalogType?.Type ?? ""} - {p?.ParentCatalogType?.ParentCatalogType?.Type ?? ""}"
                }).ToList();

            return types;
        }

        public PaginatedItemDto<CatalogItemListItemDto> GetCatalogList(int page, int pageSize)
        {
            var rowCount = 0;
            var catalogItem = _context.CatalogItems.Include(s => s.CatalogBrand)
                .Include(s => s.CatalogType)
                .ToPaged(page, pageSize, out rowCount)
                .OrderByDescending(s => s.Id)
                .Select(p => new CatalogItemListItemDto
                {
                    Id = p.Id,
                    Brand = p.CatalogBrand.Brand,
                    Type = p.CatalogType.Type,
                    AvailableStock = p.AvailableStock,
                    MaxStockThreshold = p.MaxStockThreshold,
                    RestockThreshold = p.RestockThreshold,
                    Name = p.Name,
                    Price = p.Price,
                    Slug = p.Slug,
                }).ToList(); ;
            return new PaginatedItemDto<CatalogItemListItemDto>(page, pageSize, rowCount, catalogItem);
        }

        public CatalogItemItemDto GetCatalogItem(int Id)
        {
            var catalogItem = _context.CatalogItems
                .Include(s=> s.CatalogBrand)
                .Include(s=> s.CatalogType)
                .SingleOrDefault(s => s.Id == Id);
            if (catalogItem != null)
            {
               var res =  new CatalogItemItemDto()
                {
                    Id = catalogItem.Id,
                    Brand = catalogItem.CatalogBrand,
                    CatalogType = catalogItem.CatalogType,
                    AvailableStock = catalogItem.AvailableStock,
                    MaxStockThreshold = catalogItem.MaxStockThreshold,
                    RestockThreshold = catalogItem.RestockThreshold,
                    Name = catalogItem.Name,
                    Price = catalogItem.Price,
                    Slug = catalogItem.Slug,
 
                };



                return res;
            }
            

            return null;
        }

        public void AddToFavorite(string UserId, int CatalogItemId)
        {
            var CatalogItem = _context.CatalogItems.Find(CatalogItemId);
            if (CatalogItem != null)
            {
                var NewFav = new CatalogItemFavourite()
                {
                    CatalogItem = CatalogItem,
                    UserId = UserId
                };
                _context.CatalogItemFavourites.Add(NewFav);
                _context.SaveChanges();
            }
        }

        public PaginatedItemDto<FavouriteCatalogItemDto> GetMayFavourite(string UserId, int PageIndex = 1, int PageSize = 20)
        {
            var model = _context.CatalogItems
                .Include(s => s.CatalogItemImages)
                .Include(s => s.Discounts)
                .Include(s => s.CatalogItemFavourites)
                .Where(s => s.CatalogItemFavourites.Any(d => d.UserId == UserId))
                .OrderByDescending(s => s.Id)
                .AsQueryable();

            int rowcount = 0;
            var data = model.PagedResult(PageIndex, PageSize, out rowcount)
                .ToList()
                .Select(p => new FavouriteCatalogItemDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Rate = 4,
                    AvailableStock = p.AvailableStock,
                    Image = _composerService
                        .ComposeImageUri(p.CatalogItemImages.FirstOrDefault().Src),
                });
            return new PaginatedItemDto<FavouriteCatalogItemDto>(PageIndex, PageSize, rowcount, data);
        }

        public void RemoveFavorite(string UserId, int CatalogItemId)
        {
            var CatalogItem = _context.CatalogItems.Find(CatalogItemId);
            if (CatalogItem != null)
            {
                var fav = _context.CatalogItemFavourites.FirstOrDefault(d => d.UserId == UserId &&
                                                                              d.CatalogItemId == CatalogItemId );
                if (fav != null)
                {
                    _context.CatalogItemFavourites.Remove(fav);
                    _context.SaveChanges();
                }
            }
        }

        public void RemoveCatalogItem(int id)
        {
            var catalogItem = _context.CatalogItems.Find(id);
            if (catalogItem != null)
            {
                _context.CatalogItems.Remove(catalogItem);
                _context.SaveChanges();
            }
        }
    }
    public class FavouriteCatalogItemDto
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int Rate { get; set; }
        public int AvailableStock { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }

    public class CatalogBrandDto
    {
        public int Id { get; set; }
        public string Brand { get; set; }
    }
    public class ListCatalogTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
    public class CatalogItemListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Slug { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
    }
    public class CatalogItemItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Type { get; set; }
        public CatalogBrand Brand { get; set; }
        public CatalogType CatalogType { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
    }
}