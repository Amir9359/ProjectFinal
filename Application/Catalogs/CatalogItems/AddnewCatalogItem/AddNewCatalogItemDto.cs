﻿
using System.Collections.Generic;
using Application.Catalogs.CatalogItems.AddnewCatalogItem;
using FluentValidation;

namespace Application.Catalogs.CatalogItems.AddNewCatalogItem
{
    public class AddNewCatalogItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }
        public int AvailableStock { get; set; }
        public int RestockThreshold { get; set; }
        public int MaxStockThreshold { get; set; }
        public string Slug { get; set; }
        public List<AddNewCatalogItemFeature_dto> Features { get; set; }
        public List<AddNewCatalogItemImage_Dto> Images { get; set; }

    }

    public class AddNewCatalogItemDtoValidator : AbstractValidator<AddNewCatalogItemDto>
    {
        public AddNewCatalogItemDtoValidator()
        {
            RuleFor(s => s.Name).NotNull().WithMessage("نام کاتالوگ اجباری است");
            RuleFor(s => s.Name).Length(2, 100);
            RuleFor(s => s.Description).NotNull().WithMessage("توضیحات اجباری است");
            RuleFor(x => x.AvailableStock).InclusiveBetween(0, int.MaxValue);
            RuleFor(x => x.Price).InclusiveBetween(0, int.MaxValue);
            RuleFor(x => x.Price).NotNull();
            RuleFor(s => s.Slug).NotNull();
        }
    }
 
}
