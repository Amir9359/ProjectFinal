using System;
using Domain.Attributes;
using Domain.Orders;

namespace Domain.Payments
{
    [Audtable]
    public class Payment
    {

        public Guid Id { get; set; }
        public int Amount { get; private set; }
        public bool IsPayed { get; private set; } = false;
        public DateTime? DatePay { get; private set; } = null;
        public string Authority { get; private set; }
        public long RefId { get; private set; }
        public Order Order { get; private set; }
        public int OrderId { get; private set; }

        public Payment(int amount, int orderId)
        {
            Amount = amount;
            OrderId = orderId;
        }

        public void PaymentIsDone(string authority, long refid)
        {
            IsPayed = true;
            DatePay = DateTime.Now;
            Authority = authority;
            RefId = refid;
        }
    }
}