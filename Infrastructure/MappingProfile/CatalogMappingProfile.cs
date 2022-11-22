using Application.Catalogs.CatalogItems.AddnewCatalogItem;
using Application.Catalogs.CatalogTypes;
using Application.Catalogs.CatalogItems.AddNewCatalogItem;
using Application.Catalogs.GetMenuItems;
using AutoMapper;
using Domain.Catalogs;
using Application.Catalogs.CatalogItems.CatalogItemServices;
using Application.Users;
using Domain.Users;

namespace Infrastructure.MappingProfile
{
    public class CatalogMappingProfile : Profile
    {
        public CatalogMappingProfile()
        {
            CreateMap<CatalogType, CatalogTypeDto>().ReverseMap();

            CreateMap<CatalogType, CatalogTypeListDto>().ForMember(s => s.SubTypeCount, option =>
                option.MapFrom(src => src.SubTypes.Count));

            CreateMap<CatalogType, MenuItemDto>().ForMember(s => s.Name, opt
                => opt.MapFrom(src => src.Type)).ForMember(s => s.ParentId, opt
                => opt.MapFrom(src => src.ParentCatalogTypeId)).ForMember(s => s.SUbMenus, 
                opt => opt.MapFrom(src => src.SubTypes));

            //  پروفایل افزودن دسته بندی 
            CreateMap<CatalogItemFeature, AddNewCatalogItemFeature_dto>().ReverseMap();
            CreateMap<CatalogItemImage, AddNewCatalogItemImage_Dto>().ReverseMap();
            CreateMap<CatalogItem, AddNewCatalogItemDto>().ForMember(s => s.Features
                    , opt => opt.MapFrom(d => d.CatalogItemFeatures))
                .ForMember(s => s.Images, opt => opt.MapFrom(d =>
                    d.CatalogItemImages)).ReverseMap(); ;

            //-------------------
            CreateMap<CatalogBrand, CatalogBrandDto>().ReverseMap();
            CreateMap<CatalogType, CatalogTypeDto>().ReverseMap();

            // ------------
            CreateMap<AddNewCatalogItemDto, CatalogItemItemDto>().ReverseMap();

        }
    }
}