using System.Linq;
using Application.Catalogs.CatalogItems.UriComposer;
using Application.Discounts;
using Application.Exceptions;
using Application.Interfaces.Contexts;
using AutoMapper;
using Domain.Orders;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Orders
{
    public interface IOrderService
    {
        int CreateOrder(int BasketId , int UserAddressId, Address.PaymentMethod PaymentMethod);
    }
    public class OrderService : IOrderService
    {
        private readonly IDatabaseContext _context;
        private readonly IMapper _mapper;
        private readonly IUriComposerService _uriComposer;
        private readonly IDiscountUsageHistoryService _discountUsageService;

        public OrderService(IDatabaseContext context, IMapper mapper,
            IUriComposerService uriComposer, IDiscountUsageHistoryService discountUsageService)
        {
            _context = context;
            _mapper = mapper;
            _uriComposer = uriComposer;
            _discountUsageService = discountUsageService;
        }

        public int CreateOrder(int BasketId, int UserAddressId, Address.PaymentMethod PaymentMethod)
        {
            var basket = _context.Baskets
                .Include(s => s.Items)
                .Include(s => s.AppliedDiscount)
                .SingleOrDefault(s => s.Id == BasketId);
            if (basket == null)
                throw new NotFoundException("Baskets", BasketId);

            int[] ids = basket.Items.Select(s => s.CatalogItemId).ToArray();
            var CatalogItems = _context.CatalogItems
                .Include(s => s.CatalogItemImages)
                .Where(s => ids.Contains(s.Id));

            var OrderItems = basket.Items.Select(s =>
            {
                var catalogItem = CatalogItems.First(c => c.Id == s.CatalogItemId);
                var Orderitem = new OrderItem(catalogItem.Id, catalogItem.Name,
                    _uriComposer.ComposeImageUri(catalogItem?.CatalogItemImages?.FirstOrDefault()?.Src ?? ""),
                    catalogItem.Price, s.Quantity);
                return Orderitem;
            }).ToList();

            var userAddress = _context.UserAddresses.FirstOrDefault(s => s.Id == UserAddressId);
            var address = _mapper.Map<Address>(userAddress);

            var Order = new Order(basket.BuyerId, address, OrderItems, PaymentMethod, basket.AppliedDiscount);
            _context.Orders.Add(Order);
            _context.Baskets.Remove(basket);

            if (basket.AppliedDiscount != null)
            { 
                _discountUsageService.InsertDiscountUsageHistory(basket.AppliedDiscount.Id , Order.Id);
            }
            _context.SaveChanges();
            return Order.Id;
        }
    }
}