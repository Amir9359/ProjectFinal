using System.Collections.Generic;
using System.Linq;
using Application.Interfaces.Contexts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Catalogs.GetMenuItems
{
    public interface IGetMenuItemService
    {
        List<MenuItemDto> Excute();
    }

    public class GetMenuItemService : IGetMenuItemService
    {
        private readonly IDatabaseContext dbContext;
        private readonly IMapper mapper;

        public GetMenuItemService(IDatabaseContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public List<MenuItemDto> Excute()
        {
            var catalogType = dbContext.CatalogTypes.Include(s =>
                s.ParentCatalogType).ToList();
            var data = mapper.Map<List<MenuItemDto>>(catalogType);
            
            return data;
        }
    }
    public class MenuItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public List<MenuItemDto> SUbMenus { get; set; }
    }
}