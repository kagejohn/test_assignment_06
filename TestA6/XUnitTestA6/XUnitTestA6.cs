using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using TestA6;
using Xunit;

namespace XUnitTestA6
{
    public class XUnitTestA6
    {
        private readonly List<Pizza> _pizzaMenu = new List<Pizza>();
        readonly Mock<IList<Order>> _orderMock = new Mock<IList<Order>>();
        private readonly List<Order> _orderList = new List<Order>();
        private readonly List<Pizza> _staticMenu = Menu.Read();

        public XUnitTestA6()
        {
            #region Mockist

            _orderMock.Setup(o => o.GetEnumerator()).Returns(() => _orderList.GetEnumerator());

            for (int i = 0; i < 100; i++)
            {
                Pizza pizza = new Pizza
                {
                    Name = "Pizza" + i,
                    Description = "Pizza description " + i,
                    Price = 50 + i
                };
                _pizzaMenu.Add(pizza);
            }

            for (int i = 0; i < 10; i++)
            {
                Random random = new Random();

                Order order = new Order
                {
                    Pizza = _pizzaMenu[random.Next(0, _pizzaMenu.Count)],
                    OrderTime = DateTime.Now,
                    PickupTime = DateTime.Now.AddHours(1),
                    Paid = true,
                    Pickedup = false
                };

                _orderList.Add(order);
            }

            #endregion
        }

        [Fact]
        public void EnterOrderMockist()
        {
            _orderList.Add(new Order { Pizza = _pizzaMenu[0], OrderTime = DateTime.Now, PickupTime = DateTime.Now.AddHours(2), Paid = false, Pickedup = true });

            using (IEnumerator<Order> mock = _orderMock.Object.GetEnumerator())
            {
                var last = mock.Current;
                while (mock.MoveNext())
                {
                    // for some reason it is not possible to get the last element directly,
                    // so i have to iterate over the collection until the current element is the last.
                    last = mock.Current;
                }
                Assert.Same(last.Pizza, _orderList.Last().Pizza);
                Assert.Equal(last.OrderTime, _orderList.Last().OrderTime);
                Assert.Equal(last.PickupTime, _orderList.Last().PickupTime);
                Assert.Equal(last.Paid, _orderList.Last().Paid);
                Assert.Equal(last.Pickedup, _orderList.Last().Pickedup);
            }
        }

        [Fact]
        public void GetAllOrdersMockist()
        {
            using (IEnumerator<Order> mock = _orderMock.Object.GetEnumerator())
            {
                foreach (Order order in _orderList)
                {
                    mock.MoveNext();
                    Assert.Same(mock.Current.Pizza, order.Pizza);
                    Assert.Equal(mock.Current.OrderTime, order.OrderTime);
                    Assert.Equal(mock.Current.PickupTime, order.PickupTime);
                    Assert.Equal(mock.Current.Paid, order.Paid);
                    Assert.Equal(mock.Current.Pickedup, order.Pickedup);
                }
            }
        }

        [Fact]
        public void RemoveOrderMockist()
        {
            var elementToRemove = _orderList[0];

            _orderList.Remove(elementToRemove);

            using (IEnumerator<Order> mock = _orderMock.Object.GetEnumerator())
            {
                mock.MoveNext();// because it starts at index -1 for some reason.

                Assert.NotSame(mock.Current.Pizza, elementToRemove.Pizza);
                Assert.NotEqual(mock.Current.OrderTime, elementToRemove.OrderTime);
                Assert.NotEqual(mock.Current.PickupTime, elementToRemove.PickupTime);
            }
        }

        [Fact]
        public void EnterOrderClassic()
        {
            Assert.True(OrderSystem.EnterOrder(_staticMenu[0], DateTime.Now.AddHours(1), true));
        }

        [Fact]
        public void GetAllOrdersClassic()
        {
            OrderSystem.EnterOrder(_staticMenu[0], DateTime.Now.AddHours(1), true);

            Assert.NotEmpty(OrderSystem.GetAllOrders());
        }

        [Fact]
        public void RemoveOrderClassic()
        {
            Assert.True(OrderSystem.RemoveOrder(OrderSystem.GetAllOrders()[0]));
        }
    }
}
