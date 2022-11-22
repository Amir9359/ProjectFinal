using Domain.Catalogs;
using System;
using System.Collections.Generic;
using System.Linq;
using Application.Interfaces.Contexts;
using Domain.Discounts;
using MongoDB.Driver.Linq;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Application.Discounts.AddNewDiscount
{
    public interface IAddNewDiscountService
    {
        void excute(AddNewDiscountDto discount);
    }
    public class AddNewDiscountService : IAddNewDiscountService
    {
        private readonly IDatabaseContext _context;

        public AddNewDiscountService(IDatabaseContext context)
        {
            _context = context;
        }
        public void excute(AddNewDiscountDto discount)
        {
            var newdiscount = new Discount()
            {
                Name = discount.Name,
                CoponeCode  = discount.CouponCode,
                DiscountAmount = discount.DiscountAmount,
                DiscountLimitationId = discount.DiscountLimitationId,
                DiscountPersentage = discount.DiscountPercentage,
                DiscountTypeId = discount.DiscountTypeId,
                EndDate = discount.EndDate,
                RequieredCoponeCode = discount.RequiresCouponCode,
                StartDate = discount.StartDate,
                UsePersentage = discount.UsePercentage,
                LimitationTimes = discount.LimitationTimes,
            };

            if (discount.appliedToCatalogItem!= null)
            {
                var CatalogItems = _context.CatalogItems.Where(s => discount.appliedToCatalogItem.Contains(s.Id))
                    .ToList();
                newdiscount.CatalogItems = CatalogItems;
            }

            _context.Discounts.Add(newdiscount);
            _context.SaveChanges();
        }
    }

    public class AddNewDiscountDto
    {
        [Display(Name = "نام تخفیف")]
        public string Name { get; set; }
        [Display(Name = "استفاده از درصد؟")]
        public bool UsePercentage { get; set; } = true;
        [Display(Name = "درصد تخفیف")]
        public int DiscountPercentage { get; set; } = 0;
        [Display(Name = "مبلغ تخفیف")]
        public int DiscountAmount { get; set; } = 0;
        [Display(Name = "زمان شروع")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "زمان انقضا")]
        public DateTime? EndDate { get; set; }
        [Display(Name = "استفاده از کوپن")]
        public bool RequiresCouponCode { get; set; }
        [Display(Name = "کد کوپن")]
        public string CouponCode { get; set; }
        [Display(Name = "نوع تخفیف")]
        public int DiscountTypeId { get; set; }
        [Display(Name = "محدودیت تخفیف")]
        public int DiscountLimitationId { get; set; }

        [Display(Name = "تعداد کد تخفیف")]
        public int LimitationTimes { get; set; } = 0;
        [Display(Name = "اعمال برای محصول")]
        public List<int> appliedToCatalogItem { get; set; }

    }
}