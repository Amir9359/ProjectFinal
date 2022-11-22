using System;
using System.Linq;
using Application.Interfaces.Contexts;
using Domain.Payments;
using Microsoft.EntityFrameworkCore;
using static Domain.Orders.Address;

namespace Application.Payments
{
    public interface IPaymentService
    {
        PaymentOfOrderDto PaymentOfOrder(int orderId);
        PaymentDto getPayment(Guid PayId);
        bool verifyPayment(Guid PayId, string authority, long RefId);

    }

    public class PaymentService : IPaymentService
    {
        private readonly IIdentityDbContext _identityContext;
        private readonly IDatabaseContext _context;

        public PaymentService(IIdentityDbContext identityDbContext, IDatabaseContext context)
        {
            _identityContext = identityDbContext;
            _context = context;
        }

        public PaymentOfOrderDto PaymentOfOrder(int orderId)
        {
            var order = _context.Orders
                .Include(s => s.OrderItems)
                .Include(s => s.AppliedDiscount)
                .SingleOrDefault(d => d.Id == orderId);
            if (order == null)
                throw new Exception("");

            var Payment = _context.Payments
                .SingleOrDefault(s => s.OrderId == orderId);
            if (Payment == null)
            {
                
                Payment = new Payment(order.TotalPrice(), order.Id);

                _context.Payments.Add(Payment);
                _context.SaveChanges();
            }
            return new PaymentOfOrderDto()
            {
                Amount = Payment.Amount,
                PaymentId = Payment.Id,
                PaymentMethod = order.PaymentMethod,
            };
        }

        public PaymentDto getPayment(Guid PayId)
        {
            var payment = _context.Payments
                .Include(s => s.Order)
                .ThenInclude(s => s.OrderItems)
                .Include(s => s.Order)
                .ThenInclude(s => s.AppliedDiscount)
                .SingleOrDefault(s => s.Id == PayId);

            var user = _identityContext.Users.SingleOrDefault(s => s.Id == payment.Order.BuyerId);

            string description = $"پرداخت سفارش شماره {payment.OrderId} " + Environment.NewLine;
            description += "محصولات" + Environment.NewLine;
            foreach (var item in payment.Order.OrderItems.Select(p => p.ProductName))
            {
                description += $" -{item}";
            }

            PaymentDto paymentDto = new PaymentDto
            {
                Amount = payment.Order.TotalPrice(),
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Userid = user.Id,
                Id = payment.Id,
                Description = description,
            };
            return paymentDto;
        }

        public bool verifyPayment(Guid PayId, string Authority, long RefId)
        {
            var Payment = _context.Payments
                .Include(s => s.Order)
                .SingleOrDefault(d => d.Id == PayId);

            if (Payment == null)
                throw new Exception("payment not found");

            Payment.Order.PaymentDone();
            Payment.PaymentIsDone(Authority , RefId);
            _context.SaveChanges();
            return true;
        }
    }

    public class PaymentOfOrderDto
    {
        public Guid PaymentId { get; set; }
        public int Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }

    public class PaymentDto
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public int Amount { get; set; }
        public string Userid { get; set; }
    }
}