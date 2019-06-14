using System;
using System.Collections.Generic;
using System.Text;

namespace test_assignment_06
{
    public static class OrderSystem
    {
        private static readonly List<Order> Orders = new List<Order>();

        public static bool EnterOrder(Pizza pizza, bool paid = false, bool pickedup = false)
        {
            return EnterOrder(pizza, DateTime.Now.AddHours(1), paid, pickedup);
        }

        public static bool EnterOrder(Pizza pizza, DateTime pickupTime, bool paid = false, bool pickedup = false)
        {
            Orders.Add(new Order
            {
                Pizza = pizza,
                OrderTime = DateTime.Now,
                PickupTime = pickupTime,
                Paid = paid,
                Pickedup = pickedup
            });

            return true;
        }

        public static List<Order> GetAllOrders()
        {
            return Orders;
        }

        public static bool RemoveOrder(Order order)
        {
            Orders.Remove(order);

            return true;
        }
    }
}
