using Domain.Orders;
using UnitTest.Builders;
using Xunit;

namespace UnitTest.Core.Entities.OrderTest
{
    public class OrderStatusTest 
    {
        [Fact]
        public void Order_Delivered_Change_OrderStatus_Test()
        {
            var builder = new OrderBuilder();
            var order = builder.CreateOrderWithDefaultValues();
            order.OrderDelivered();

            Assert.Equal(Address.OrderStatus.Delivered, order.OrderStatus);
        }
    }
}