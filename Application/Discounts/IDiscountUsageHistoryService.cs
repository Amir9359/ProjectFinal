using System;
using System.Linq;
using Application.Dtos;
using Application.Interfaces.Contexts;
using Common;
using Domain.Discounts;
using Domain.Users;

namespace Application.Discounts
{
    public interface IDiscountUsageHistoryService
    {
        void InsertDiscountUsageHistory(int DiscountId, int OrderId);
        DiscountUsageHistory getDiscountUsageHistory(int DiscountHistoryId);
        PaginatedItemDto<DiscountUsageHistory> getAllDiscountUsageHistory(int? DiscountId, 
            string UserId , int PageIndex , int PageSize);
    }
    public  class DiscountUsageHistoryService : IDiscountUsageHistoryService
    {
        private readonly IDatabaseContext _context;

        public DiscountUsageHistoryService(IDatabaseContext context)
        {
            _context = context;
        }

        public void InsertDiscountUsageHistory(int DiscountId, int OrderId)
        {
            var Discount = _context.Discounts.Find( DiscountId);
            var order = _context.Orders.SingleOrDefault(s => s.Id == OrderId);

            var newDiscountUsage = new DiscountUsageHistory()
            {
                Order = order,
                Discount = Discount,
                CreatedOn = DateTime.Now,
            };
            _context.DiscountUsageHistories.Add(newDiscountUsage);
            _context.SaveChanges();
        }
        public DiscountUsageHistory getDiscountUsageHistory(int DiscountHistoryId)
        {
            if (DiscountHistoryId == null)
            {
                return null;
            }

            var discountUsage = _context.DiscountUsageHistories.Find(DiscountHistoryId);
            return discountUsage;
        }

        public PaginatedItemDto<DiscountUsageHistory> getAllDiscountUsageHistory(int? DiscountId, string UserId, int PageIndex, int PageSize)
        {
            var query = _context.DiscountUsageHistories.AsQueryable();

            if (DiscountId.HasValue && DiscountId.Value > 0)
                query = query.Where(p => p.DiscountId == DiscountId.Value);

            if (!string.IsNullOrEmpty(UserId))
                query = query.Where(p => p.Order != null && p.Order.BuyerId == UserId);

            query = query.OrderByDescending(c => c.CreatedOn);
            var pagedItems = query.PagedResult(PageIndex, PageSize, out int rowCount);
            return new PaginatedItemDto<DiscountUsageHistory>(PageIndex, PageSize, rowCount, query.ToList());

        }
    }
}