using System;
using System.Collections.Generic;
using System.Linq;
using Application.Dtos;
using Application.Interfaces.Contexts;
using Domain.Discounts;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;

namespace Application.Discounts
{
    public interface IDiscountService
    {
        List<CatalogItemDto> getCatalogItems(string searchKey);
        bool ApplyDiscountInBasket(string CoponCode, int BasketId);
        bool RemoveDiscountFromBasket(int BasketId);
        BaseDto ValidateDiscount(string CoponCode, User user);
    }
    public class DiscountService : IDiscountService
    {
        private readonly IDatabaseContext _context;
        private readonly IDiscountUsageHistoryService _discountUsageHistory;

        public DiscountService(IDatabaseContext context, IDiscountUsageHistoryService discountUsageHistory)
        {
            _context = context;
            _discountUsageHistory = discountUsageHistory;
        }

        public List<CatalogItemDto> getCatalogItems(string searchKey)
        {
            if (!string.IsNullOrEmpty(searchKey))
            {
                var data = _context.CatalogItems
                    .Where(s => s.Name.Contains(searchKey))
                    .Select(d => new CatalogItemDto()
                    {
                        Id = d.Id,
                        Name = d.Name
                    }).ToList();

                return data;
            }
            else
            {
                var data = _context.CatalogItems
                    .Take(10)
                    .OrderByDescending(s => s.Name)
                    .Select(d => new CatalogItemDto()
                    {
                        Id = d.Id,
                        Name = d.Name
                    }).ToList();

                return data;
            }
        }

        public bool ApplyDiscountInBasket(string CoponCode, int BasketId)
        {
            var basket = _context.Baskets
                .Include(s => s.Items)
                .Include(s => s.AppliedDiscount)
                .FirstOrDefault(s => s.Id == BasketId);

            var Discount = _context.Discounts
                .Where(s => s.CoponeCode.Equals(CoponCode)).FirstOrDefault();
            basket.ApplayDiscountCode(Discount);
            _context.SaveChanges();
            return true;

        }

        public bool RemoveDiscountFromBasket(int BasketId)
        {
            var basket = _context.Baskets.SingleOrDefault(s => s.Id == BasketId);
            basket.RemoveDiscount();
            _context.SaveChanges();
            return true;
        }

        public BaseDto ValidateDiscount(string CoponCode, User user)
        {
            var discount = _context.Discounts
                .Where(s => s.CoponeCode == CoponCode).FirstOrDefault();

            if (discount == null)
            {
                return new BaseDto(IsSucces: false,
                    Message: new List<string> { "کد تخفیف معتبر نمی باشد" });
            }
            var now = DateTime.UtcNow;
            if (discount.StartDate.HasValue)
            {
                var startDate = DateTime.SpecifyKind(discount.StartDate.Value, DateTimeKind.Utc);
                if (startDate.CompareTo(now) > 0)
                    return new BaseDto(new List<string> { "هنوز زمان استفاده از این کد تخفیف فرا نرسیده است" }, false);
            }
            if (discount.EndDate.HasValue)
            {
                var endDate = DateTime.SpecifyKind(discount.EndDate.Value, DateTimeKind.Utc);
                if (endDate.CompareTo(now) < 0)
                    return new BaseDto(new List<string> { "کد تخفیف منقضی شده است" }, false);
            }

            var checkLimit = CheckDiscountLimitations(discount, user);
            if (checkLimit.IsSucces == false)
                return checkLimit;

            return new BaseDto(null , true);

        }


        private BaseDto CheckDiscountLimitations(Discount discount, User user)
        {
            switch (discount.DiscountLimitation)
            {
                case DiscountLimitationType.Unlimited:
                    {
                        return new BaseDto(null, true);
                    }
                case DiscountLimitationType.NTimesOnly:
                    {
                        var totalUsage = _discountUsageHistory.getAllDiscountUsageHistory(discount.Id, null, 0, 1).Data.Count();
                        if (totalUsage < discount.LimitationTimes)
                        {
                            return new BaseDto(null, true);

                        }
                        else
                        {
                            return new BaseDto(new List<string> { "ظرفیت استفاده از این کد تخفیف تکمیل شده است" }, false);

                        }
                    }
                case DiscountLimitationType.NTimesPerCustomer:
                    {
                        if (user != null)
                        {
                            var totalUsage = _discountUsageHistory.getAllDiscountUsageHistory(discount.Id, user.Id, 0, 1).Data.Count();
                            if (totalUsage < discount.LimitationTimes)
                            {
                                return new BaseDto(null, true);
                            }
                            else
                            {
                                return new BaseDto(new List<string> { "تعدادی که شما مجاز به استفاده از این تخفیف بوده اید به پایان رسیده است" }, false);
                            }
                        }
                        else
                        {
                            return new BaseDto(null, true);
                        }
                    }
                default:
                    break;
            }
            return new BaseDto(null, true);

        }

    }

    public class CatalogItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}