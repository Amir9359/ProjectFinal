using System;
using System.Collections.Generic;
using System.Linq;
using Application.Catalogs.CatalogItems.UriComposer;
using Application.Interfaces.Contexts;
using Domain.Baskets;
using Microsoft.EntityFrameworkCore;

namespace Application.BasketService
{
    public interface IBasketService
    {
        BasketDto GetOrCreateBasket(string ByuerId);
        BasketDto GetBasket(string ByuerId);
        void AddBasketItem(int basketId, int CatalogItemId, int quantity = 1);
        bool RemoveItemFromBasket(int ItemId);
        bool SetQuantities(int ItemId , int quantity);
        void transferBasket(string anonymousId ,string UserId);
    }

    public class BasketService : IBasketService
    {
        private readonly IDatabaseContext _context;
        private readonly IUriComposerService _composerService;

        public BasketService(IDatabaseContext context, IUriComposerService composerService)
        {
            _context = context;
            _composerService = composerService;
        }

        public BasketDto GetOrCreateBasket(string ByuerId)
        {
            var basket = _context.Baskets
                .Include(d => d.Items)
                .ThenInclude(d => d.CatalogItem)
                .ThenInclude(d => d.CatalogItemImages)
                .Include(d => d.Items)
                .ThenInclude(d => d.CatalogItem)
                .ThenInclude(d => d.Discounts)
                
                .SingleOrDefault(s => s.BuyerId == ByuerId);
            if (basket == null)
            {
                // ایجاد سبد خرید
                return CreateBasketForUser(ByuerId);   
            }

            return new BasketDto()
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id,
                DiscountAmount = basket.DiscountAmount,
                Items = basket.Items.Select(s => new BasketItemDto()
                {
                    Id = s.Id,
                    CatalogItemId = s.CatalogItemId,
                    Quantity = s.Quantity,
                    CatalogName = s.CatalogItem.Name,
                    UnitPrice = s.CatalogItem.Price,
                    ImageUrl = _composerService.ComposeImageUri(s?.CatalogItem?.CatalogItemImages?.FirstOrDefault()?.Src ?? "")
                }).ToList()
            };
        }

        public BasketDto GetBasket(string ByuerId)
        {
            var basket = _context.Baskets
                .Include(d => d.Items)
                .ThenInclude(d => d.CatalogItem)
                .ThenInclude(d => d.CatalogItemImages)
                .SingleOrDefault(s => s.BuyerId == ByuerId);

            if (basket == null)
            {
                return null;
            }
            return new BasketDto()
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id,
                DiscountAmount = basket.DiscountAmount,
                Items = basket.Items.Select(s => new BasketItemDto()
                {
                    Id = s.Id,
                    CatalogItemId = s.CatalogItemId,
                    Quantity = s.Quantity,
                    CatalogName = s.CatalogItem.Name,
                    UnitPrice = s.UnitPrice,
                    ImageUrl = _composerService.ComposeImageUri(
                        s?.CatalogItem?.CatalogItemImages?.FirstOrDefault()?.Src ?? ""),
                }).ToList()
            };
        }

        public void AddBasketItem(int basketId, int CatalogItemId, int quantity = 1)
        {
            var basket = _context.Baskets
                .FirstOrDefault(s => s.Id == basketId);
            if (basket == null)
            {
                throw new Exception("");
            }

            var catalogItem = _context.CatalogItems.Find(CatalogItemId);
            basket.AddBasketItem(CatalogItemId , quantity , catalogItem.Price );
            _context.SaveChanges();
        }

        public bool RemoveItemFromBasket(int ItemId)
        {
            var item = _context.BasketItems.SingleOrDefault(s => s.Id == ItemId);
            _context.BasketItems.Remove(item);
            _context.SaveChanges();
            return true;
        }

        public bool SetQuantities(int ItemId, int quantity)
        {
            var item = _context.BasketItems.SingleOrDefault(s => s.Id == ItemId);
            var catalogItem = _context.CatalogItems.FirstOrDefault(s => s.Id == item.CatalogItemId);
            if (catalogItem.AvailableStock > quantity)
            {
                item.SetQuantity(quantity);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public void transferBasket(string anonymousId, string UserId)
        {
            var anonymousBasket = _context.Baskets.Include(s => s.Items)
                .Include(s => s.AppliedDiscount)
                .SingleOrDefault(s => s.BuyerId == anonymousId);
            if(anonymousBasket == null) return;

            var UserBasket = _context.Baskets.SingleOrDefault(s => s.BuyerId == UserId);
            if (UserBasket == null)
            {
                UserBasket = new Basket(UserId);
                _context.Baskets.Add(UserBasket);
            }

            foreach (var item in anonymousBasket.Items)
            {
                UserBasket.AddBasketItem(item.CatalogItemId , item.Quantity , item.UnitPrice);
            }

            if (anonymousBasket.AppliedDiscount != null)
            {
             UserBasket.ApplayDiscountCode(anonymousBasket.AppliedDiscount);

            }
            _context.Baskets.Remove(anonymousBasket);
            _context.SaveChanges();
        }

        private BasketDto CreateBasketForUser(string BuyerId)
        {
            Basket basket = new Basket(BuyerId);
            _context.Baskets.Add(basket);
            _context.SaveChanges();
            return new BasketDto
            {
                BuyerId = basket.BuyerId,
                Id = basket.Id,
            };
        }
    }
    public class BasketDto
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
        public int DiscountAmount { get; set; }
        public int Total()
        {
            if (Items.Count > 0 )
            {
                var price = Items.Sum(s => s.UnitPrice * s.Quantity);
                price -=  DiscountAmount;
                return price;
            }
            return 0;
        }
        public int TotalWithOutDiscount()
        {
            if (Items.Count > 0 )
            {
                var price = Items.Sum(s => s.UnitPrice * s.Quantity);
                return price;
            }
            return 0;
        }

    }

    public class BasketItemDto
    {
        public int Id { get; set; }
        public int CatalogItemId { get; set; }
        public string CatalogName { get; set; }
        public int UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }
    }
}