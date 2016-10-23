using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ictProject3
{
    public class Item
    {
        public string Naam { get; set; }
        public int Id { get; set; }

        public static List<Item> GetItems()
        {

            List<Item> items = new List<Item>();

            string[] lijstItems = { "test1", "test2" };
            int[] lijstId = { 1, 2 };

            //foreach(...)/while(...)
            for (int i = 0; i <= 1; i++)
            {
                Item item = new Item();
                item.Id = lijstId[i];
                item.Naam = lijstItems[i];
                items.Add(item);
            }
            return items;

        }
    }
}
