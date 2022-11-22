using Admin.EndPoint.ViewModels.Catalogs;
using Application.Catalogs.CatalogTypes;
using AutoMapper;

namespace Admin.Endpoint.MappingProfiles
{
    public class CatalogMappingProfile : Profile
    {
        public CatalogMappingProfile()
        {
            CreateMap<CatalogTypeDto, CatalogTypeViewModel>().ReverseMap();
        }
    }
}