using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoffeeBeans.Models;
using Xamarin.Forms;

namespace CoffeeBeans.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        readonly List<Item> items;

        public MockDataStore()
        {
            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Spicy Coffee", Description="Description.", Price=80, ImageSource="CoffeeBeansOnPlant.jpg", Type="arabica"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Joe's Beans", Description="Description.", Price=60, ImageSource="RobustaCoffeeBeans.jpg", Type="arabica"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Bob's Coffee", Description="Description.", Price=60.5f, ImageSource="CoffeeBeans.jpg", Type="robusta"},
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}