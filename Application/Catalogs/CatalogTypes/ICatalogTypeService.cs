using System.Collections.Generic;
using System.Linq;
using Application.Dtos;
using Application.Interfaces.Contexts;
using AutoMapper;
using Common;
using Domain.Catalogs;

namespace Application.Catalogs.CatalogTypes
{
    public interface ICatalogTypeService
    {
        BaseDto<CatalogTypeDto> Add(CatalogTypeDto catalog);
        BaseDto Remove(int id);
        BaseDto<CatalogTypeDto> Edit(CatalogTypeDto catalog);
        BaseDto<CatalogTypeDto> FindById(int catalogId);
        PaginatedItemDto<CatalogTypeListDto> GetList(int? parentId, int page, int pageSize);
    }

    public class CatalogTypeService : ICatalogTypeService
    {
        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;

        public CatalogTypeService(IDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BaseDto<CatalogTypeDto> Add(CatalogTypeDto catalog)
        {
            var result = _mapper.Map<CatalogType>(catalog);
            _context.CatalogTypes.Add(result);
            _context.SaveChanges();

            return new BaseDto<CatalogTypeDto>(_mapper.Map<CatalogTypeDto>(result),
                new List<string> { $"تایپ {result.Type}  با موفقیت در سیستم ثبت شد" },true);
        }

        public BaseDto Remove(int id)
        {
            var catalog = _context.CatalogTypes.Find(id);
            var result = _context.CatalogTypes.Remove(catalog);
            _context.SaveChanges();

            return new BaseDto(new List<string> { $"ایتم با موفقیت حذف شد" } , true);
        }

        public BaseDto<CatalogTypeDto> Edit(CatalogTypeDto catalog)
        {
            var model = _context.CatalogTypes.SingleOrDefault(p => p.Id == catalog.Id);
            _mapper.Map(catalog, model);
            _context.SaveChanges();
            return new BaseDto<CatalogTypeDto>
            (
                _mapper.Map<CatalogTypeDto>(model),
                new List<string> { $"تایپ {model.Type} با موفقیت ویرایش شد" },
                true
            );
        }

        public BaseDto<CatalogTypeDto> FindById(int catalogId)
        {
            var Catalog = _context.CatalogTypes.Find(catalogId);
            var result = _mapper.Map<CatalogTypeDto>(Catalog);

            return new BaseDto<CatalogTypeDto>(result, null, true);
        }

        public PaginatedItemDto<CatalogTypeListDto> GetList(int? parentId, int page, int pageSize)
        {
            int totalCount = 0;
            var model = _context.CatalogTypes.Where(s => s.ParentCatalogTypeId == parentId)
                .PagedResult(page, pageSize, out totalCount);
            var result = _mapper.ProjectTo<CatalogTypeListDto>(model).ToList();

            return new PaginatedItemDto<CatalogTypeListDto>(page, pageSize, totalCount, result);
        }
    }

    public class CatalogTypeDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int? ParentCatalogTypeId { get; set; }
    }
    public class CatalogTypeListDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int SubTypeCount { get; set; }

    }

    }