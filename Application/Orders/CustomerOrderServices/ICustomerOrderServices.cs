using System.Collections.Generic;
using System.Linq;
using Application.Interfaces.Contexts;
using Domain.Orders;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;

namespace Application.Orders.CustomerOrderServices
{
    public interface ICustomerOrderServices
    {
        List<MyOrderDto> getMyOrder(string UserId);
    }
    public class CustomerOrderServices : ICustomerOrderServices
    {
        private readonly IDatabaseContext _context;

        public CustomerOrderServices(IDatabaseContext context)
        {
            _context = context;
        }

        public List<MyOrderDto> getMyOrder(string UserId)
        {
            var Orders = _context.Orders
                .Include(s => s.OrderItems)
                .Where(s => s.BuyerId == UserId)
                .OrderByDescending(s => s.Id)
                .Select(d => new MyOrderDto()
                {
                    Id = d.Id,
                    Price = d.TotalPrice(),
                    PaymentStatus = d.PaymentStatus,
                    OrderStatus = d.OrderStatus,
                    Date = _context.Entry(d).Property("InsertTime").CurrentValue.ToString()

                }).ToList();

            return Orders;
        }
    }

    public class MyOrderDto
    {
        public int Id { get; set; }     
        public string Date { get; set; }
        public int Price { get; set; }
        public Address.OrderStatus OrderStatus { get; set; }
        public Address.PaymentStatus PaymentStatus { get; set; }
    }
}