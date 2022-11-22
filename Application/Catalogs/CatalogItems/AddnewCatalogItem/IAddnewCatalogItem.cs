using System.Collections.Generic;
using System.Linq;
using Application.Catalogs.CatalogItems.AddNewCatalogItem;
using Application.Dtos;
using Application.Interfaces.Contexts;
using AutoMapper;
using Domain.Catalogs;
using Microsoft.EntityFrameworkCore;

namespace Application.Catalogs.CatalogItems.AddnewCatalogItem
{
    public interface IAddnewCatalogItem
    {
        BaseDto<int> Execute(AddNewCatalogItemDto itemDto);
        BaseDto<int> Edite(AddNewCatalogItemDto itemDto, int id);
    }

    public class AddnewCatalogItem : IAddnewCatalogItem
    { 
        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;


        public AddnewCatalogItem(IDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BaseDto<int> Execute(AddNewCatalogItemDto itemDto)
        {
           var catalogItem = _mapper.Map<CatalogItem>(itemDto);
 
            _context.CatalogItems.Add(catalogItem);
            _context.SaveChanges();
            return new BaseDto<int>(catalogItem.Id, new List<string> {"با موفقیت ثبت شد."}, true);
        }

        public BaseDto<int> Edite(AddNewCatalogItemDto NewitemDto, int id)
        {
            var oldCatalog = _context.CatalogItems
                .Include(d => d.CatalogItemImages)
                .Include(d => d.CatalogItemFeatures)
                .SingleOrDefault(s => s.Id == id);



            var catalogItem = _mapper.Map<CatalogItem>(NewitemDto);
 

            //oldCatalog.CatalogItemImages = null;
            //oldCatalog.CatalogItemFeatures = null;
            _context.CatalogItemFeature.RemoveRange(oldCatalog.CatalogItemFeatures);
            _context.CatalogItemImage.RemoveRange(oldCatalog.CatalogItemImages);
            _context.SaveChanges();
            foreach (var item in NewitemDto.Features)   
            {
                oldCatalog.CatalogItemFeatures.Add(new CatalogItemFeature()
                {
                    Group = item.Group,
                    Key = item.Key,
                    Value = item.Value,
                    CatalogItemId = oldCatalog.Id
                });
            }

            foreach (var item in NewitemDto.Images)
            {
                oldCatalog.CatalogItemImages.Add(new CatalogItemImage()
                {
                    Src = item.Src,
                    CatalogItemId = oldCatalog.Id
                });
            }

            oldCatalog.Price = catalogItem.Price;
            oldCatalog.Name = catalogItem.Name;
            oldCatalog.CatalogBrandId = catalogItem.CatalogBrandId;
            oldCatalog.CatalogTypeId= catalogItem.CatalogTypeId;
            oldCatalog.AvailableStock= catalogItem.AvailableStock;
            oldCatalog.RestockThreshold = catalogItem.RestockThreshold;
            oldCatalog.MaxStockThreshold= catalogItem.MaxStockThreshold;
            oldCatalog.Slug = catalogItem.Slug;

            _context.SaveChanges();



            return new BaseDto<int>( id, new List<string> { "با موفقیت آپدیت شد." }, true);

        }
    }
    public class AddNewCatalogItemFeature_dto
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Group { get; set; }
    }

    public class AddNewCatalogItemImage_Dto
    {
        public string Src { get; set; }
    }
}