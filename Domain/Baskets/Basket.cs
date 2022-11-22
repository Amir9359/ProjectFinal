using System.Collections.Generic;
using Domain.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Domain.Catalogs;
using Domain.Discounts;

namespace Domain.Baskets
{
    [Audtable] 
    public class Basket
    {
        public int Id { get; set; }
        public string BuyerId { get; private set; }

        private readonly List<BasketItem> _items = new List<BasketItem>();

        //  Discount Items
        public int DiscountAmount { get; private set; } = 0;
        public Discount AppliedDiscount { get; private set; } 
        public int? AppliedDiscountId { get; private set; }

        public void ApplayDiscountCode(Discount discount)
        {
            if (discount != null)
            {
                this.AppliedDiscount = discount;
                this.AppliedDiscountId = discount.Id;
                this.DiscountAmount = discount.GetDiscountAmount(GetTotalPriceWithoutDiscount());
            }
        }

        public int GetTotalPriceWithoutDiscount()
        {
            return _items.Sum(s => s.UnitPrice * s.Quantity);
        }
        public int GetTotalPrice()
        {
           var price =_items.Sum(s => s.UnitPrice * s.Quantity);
           price -= AppliedDiscount.GetDiscountAmount(price);
           return price;
        }

        public void RemoveDiscount()
        {
            this.AppliedDiscount = null;
            this.AppliedDiscountId = null;
            this.DiscountAmount = 0;
        }
        public ICollection<BasketItem> Items => _items.AsReadOnly();
        public Basket(string BuyerId)
        {
            this.BuyerId = BuyerId; 
        }

        public void AddBasketItem(int catalogItemId, int quantity, int unitPrice)
        {
            if (!Items.Any(s => s.CatalogItemId == catalogItemId))
            {
                _items.Add(new BasketItem(catalogItemId, quantity, unitPrice));
                return;
            }

            var existBasketItem = Items.FirstOrDefault(s => s.CatalogItemId == catalogItemId);
            existBasketItem.AddQuantity(quantity);
        }
    }
    [Audtable]
    public class BasketItem
    {
        public int Id { get; set; }
        public int UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public int CatalogItemId { get; private set; }
        public CatalogItem CatalogItem { get; private set; }
        public int BasketId { get; private set; }
        public BasketItem(int catalogItemId, int quantity, int unitPrice)
        {
            CatalogItemId = catalogItemId;
            UnitPrice = unitPrice;
           SetQuantity(quantity);
        }

        public void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }
        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }

    }
}