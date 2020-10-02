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
        readonly List<Item> itemsPending;
        readonly List<Item> itemsTransit;
        readonly List<Item> itemsHistory;


        public MockDataStore()
        {
            items = new List<Item>()
            {
                //new Item { Id = Guid.NewGuid().ToString(), Text = "Spicy Coffee", Description="Description.", Price=80, ImageSource="CoffeeBeansOnPlant.jpg", Type="arabica"},
                //new Item { Id = Guid.NewGuid().ToString(), Text = "Joe's Beans", Description="Description.", Price=60, ImageSource="RobustaCoffeeBeans.jpg", Type="arabica"},
                //new Item { Id = Guid.NewGuid().ToString(), Text = "Bob's Coffee", Description="Description.", Price=60.5f, ImageSource="CoffeeBeans.jpg", Type="robusta"},
            };
            itemsPending = new List<Item>()
            {

            };
            itemsTransit = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Surya Thaker", Description="Description.", Price=30.4f, ImageSource="arabicabeans2.jpg", Type="arabica"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Tarun Dev Dayal", Description="Description.", Price=50, ImageSource="arabicacherries1.jpg", Type="arabica"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sweta Yadav", Description="Description.", Price=46, ImageSource="robustabeans4.jpg", Type="robusta"},
            };
            itemsHistory = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "Sheetal Usman", Description="Description.", Price=62, ImageSource="CoffeeBeansOnPlant.jpg", Type="arabica"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Baldev Ram Doctor", Description="Description.", Price=76, ImageSource="robustabeans4.jpg", Type="arabica"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Daanish Pardeshi", Description="Description.", Price=54.3f, ImageSource="robustabeans6.jpg", Type="robusta"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Brock Din", Description="Description.", Price=33.2f, ImageSource="robustaCherries.jpg", Type="robusta"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Tulsi Jatin Manda", Description="Description.", Price=85.3f, ImageSource="robustabeans2.jpg", Type="robusta"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Richa Chad", Description="Description.", Price=50.4f, ImageSource="robustabeans6.jpg", Type="robusta"},
                new Item { Id = Guid.NewGuid().ToString(), Text = "Brock Din", Description="Description.", Price=61, ImageSource="robustaCherries.jpg", Type="robusta"},
            };
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> AddItemAsyncOrder(Item item)
        {
            itemsPending.Add(item);

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

        // order page
        public async Task<IEnumerable<Item>> GetItemsAsyncOrder(bool forceRefresh = false)
        {
            return await Task.FromResult(itemsPending);
        }
        public async Task<IEnumerable<Item>> GetItemsAsyncTransit(bool forceRefresh = false)
        {
            return await Task.FromResult(itemsTransit);
        }
        public async Task<IEnumerable<Item>> GetItemsAsyncHistory(bool forceRefresh = false)
        {
            return await Task.FromResult(itemsHistory);
        }




        // randomize content
        public async Task<bool> RandomizeItems()
        {
            if (items.Count <= 1)
            {
                Random rnd = new Random();
                for (int i = 0; i < ItemAttributes.maxLength; i++)
                {
                    int _type = rnd.Next(0, 2);
                    ImageSource _imageSource;
                    if (_type == 0)
                    {
                        _imageSource = ItemAttributes.imageSourcesRobusta[rnd.Next(0, ItemAttributes.imageSourcesRobusta.Length)];
                    }
                    else
                    {
                        _imageSource = ItemAttributes.imageSourcesArabica[rnd.Next(0, ItemAttributes.imageSourcesArabica.Length)];
                    }
                    Item newItem = new Item()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Text = ItemAttributes.names[rnd.Next(0, ItemAttributes.names.Length)],
                        Description = ItemAttributes.descriptions[rnd.Next(0, ItemAttributes.descriptions.Length)],
                        Price = rnd.Next(30, 45) / 10f,
                        ImageSource = _imageSource,
                        Type = ItemAttributes.types[_type]
                    };

                    await AddItemAsync(newItem);
                }
            }

            return await Task.FromResult(true);
        }
    }
}