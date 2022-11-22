using System.Collections.Generic;
using Application.BasketService;
using Application.Users;

namespace WebSite.Endpoint.Models.ViewModels.Baskets
{
    public class ShippingPaymentViewModel
    {
        public BasketDto basket { get; set; }
        public List<UserAddressDto> UserAddress { get; set; }
    }
}