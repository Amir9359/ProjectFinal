using System.Collections.Generic;
using System.Linq;
using Application.Catalogs.CatalogItems.GetCatalogItemPDP;
using Application.Catalogs.CatalogItems.GetCatalogItemPLP;
using Application.Catalogs.CatalogItems.UriComposer;
using Application.Interfaces.Contexts;
using Domain.Banners;
using MongoDB.Driver.Linq;

namespace Application.HomepageService
{
    public interface IHomepageService
    {
        HomePageDto GetData();
    }

    public class HomepageService : IHomepageService
    {
        private readonly IDatabaseContext _context;
        private readonly IUriComposerService _uriComposer;
        private readonly IGetCatalogItemPLPService _catalogItemsService;

        public HomepageService(IUriComposerService uriComposer, IDatabaseContext context, IGetCatalogItemPLPService catalogItemsService)
        {
            _uriComposer = uriComposer;
            _context = context;
            _catalogItemsService = catalogItemsService;
        }

        public HomePageDto GetData()
        {
            var banner = _context.Banners
                .Where(s => s.IsActive == true)
                .OrderBy(s => s.Priority)
                .ThenByDescending(s => s.Id)
                .Select(d => new BannerDto()
                {
                    Id = d.Id,
                    Image = _uriComposer.ComposeImageUri(d.Image),
                    Link = d.Link,
                    Position = d.Position
                }).ToList();
            var Bestselling = _catalogItemsService.Execute(new CatlogPLPRequestDto
            {
                AvailableStock = true,
                page = 1,
                pageSize = 20,
                SortType = SortType.Bestselling
            }).Data.ToList();
            var MostPopular = _catalogItemsService.Execute(new CatlogPLPRequestDto
            {
                AvailableStock = true,
                page = 1,
                pageSize = 20,
                SortType = SortType.MostPopular
            }).Data.ToList();
            
            return new HomePageDto()
            {
                Banners = banner,
                BestSellers = Bestselling,
                MostPopular = MostPopular
            };
        }
    }

    public class HomePageDto
    {
        public List<BannerDto> Banners { get; set; }
        public List<CatalogPLPDto> MostPopular  { get; set; }
        public List<CatalogPLPDto> BestSellers  { get; set; }
    }
    public class BannerDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Link { get; set; }
        public BannerPosition Position { get; set; }
    }
}