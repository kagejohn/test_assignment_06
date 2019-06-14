using System;
using System.Collections.Generic;
using System.Text;

namespace test_assignment_06
{
    public static class Menu
    {
        private static readonly List<Pizza> PizzaMenu = new List<Pizza>();

        static Menu()
        {
            for (int i = 0; i < 100; i++)
            {
                Pizza pizza = new Pizza
                {
                    Name = "Pizza" + i,
                    Description = "Pizza description " + i,
                    Price = 50 + i
                };
                PizzaMenu.Add(pizza);
            }
        }

        public static List<Pizza> Read()
        {
            return PizzaMenu;
        }
    }
}
