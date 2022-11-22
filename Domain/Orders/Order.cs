using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Attributes;
using Domain.Catalogs;
using Domain.Discounts;
using static Domain.Orders.Address;
using static Domain.Orders.Order;

namespace Domain.Orders
{
    [Audtable]
    public class Order
    {
        public int Id { get; set; }
        public string BuyerId { get; private set; }
        public DateTime OrderDate { get; private set; } = DateTime.Now;
        public Address Address { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public PaymentStatus PaymentStatus { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        private readonly List<OrderItem> _OrderItems = new List<OrderItem>();
        public IReadOnlyCollection<OrderItem> OrderItems => _OrderItems.AsReadOnly();

        // discount
        public decimal DiscountAmount { get; private set; }
        public Discount AppliedDiscount { get; private set; }
        public int? AppliedDiscountId { get; private set; }

        public Order(string buyerId, Address address, List<OrderItem> orderItems
            , PaymentMethod paymentMethod, Discount discount)
        {
            BuyerId = buyerId;
            Address = address;
            PaymentMethod = paymentMethod;
            _OrderItems = orderItems;
            if (discount != null)
            {
                ApplyDiscountCode(discount);
            }

        }

        public Order()
        {

        }


        public int TotalPrice()
        {

            int totalPrice = OrderItems.Sum(p => p.UnitPrice * p.Units);
            if (AppliedDiscount != null)
            {
                totalPrice -= AppliedDiscount.GetDiscountAmount(totalPrice);
            }

            return totalPrice;
        }

        /// دریافت مبلغ کل بدونه در نظر گرفتن کد تخفیف
        public int TotalPriceWithOutDiescount()
        {
            int totalPrice = OrderItems.Sum(p => p.UnitPrice * p.Units);
            return totalPrice;
        }
        public void ApplyDiscountCode(Discount discount)
        {
            this.AppliedDiscount = discount;
            this.AppliedDiscountId = discount.Id;
            this.DiscountAmount = discount.GetDiscountAmount(TotalPrice());
        }

        public void PaymentDone()
        {
            PaymentStatus = PaymentStatus.Paid;
        }
        /// <summary>
        /// کالا تحویل داده شد
        /// </summary>
        public void OrderDelivered()
        {
            OrderStatus = OrderStatus.Delivered;
        }

        /// <summary>
        /// ثبت مرجوعی کالا
        /// </summary>
        public void OrderReturned()
        {
            OrderStatus = OrderStatus.Returned;
        }


        /// <summary>
        /// لغو سفارش
        /// </summary>
        public void OrderCancelled()
        {
            OrderStatus = OrderStatus.Cancelled;
        }

    }

    [Audtable]
    public class OrderItem
    {
        public int Id { get; set; }
        public int CatalogItemId { get; private set; }
        public CatalogItem CatalogItem { get; private set; }
        public string ProductName { get; private set; }
        public string PictureUri { get; private set; }
        public int UnitPrice { get; private set; }
        public int Units { get; private set; }
        public OrderItem(int catalogItemId, string productName, string pictureUri, int unitPrice, int units)
        {
            CatalogItemId = catalogItemId;
            ProductName = productName;
            PictureUri = pictureUri;
            UnitPrice = unitPrice;
            Units = units;
        }
        //ef core
        public OrderItem()
        {

        }
    }
    public class Address
    {
        public string State { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
        public string PostalAddres { get; private set; }
        public string ReciverName { get; private set; }

        public Address(string state, string city, string zipCode,
            string postalAddres, string reciverName)
        {
            State = state;
            City = city;
            ZipCode = zipCode;
            PostalAddres = postalAddres;
            ReciverName = reciverName;
        }


        public enum PaymentMethod
        {
            /// <summary>
            /// پرداخت آنلاین
            /// </summary>
            OnlinePaymnt = 0,
            /// <summary>
            /// پرداخت در محل
            /// </summary>
            PaymentOnTheSpot = 1,
        }

        public enum PaymentStatus
        {
            /// <summary>
            /// منتظر پرداخت
            /// </summary>
            WaitingForPayment = 0,
            /// <summary>
            /// پرداخت انجام شد
            /// </summary>
            Paid = 1,
        }

        public enum OrderStatus
        {

            /// <summary>
            /// در حال پردازش
            /// </summary>
            Processing = 0,
            /// <summary>
            /// تحویل داده شد
            /// </summary>
            Delivered = 1,
            /// <summary>
            /// مرجوعی
            /// </summary>
            Returned = 2,
            /// <summary>
            /// لغو شد
            /// </summary>
            Cancelled = 3,
        }

    }
}