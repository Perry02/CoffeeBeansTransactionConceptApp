using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CoffeeBeans.Models;
using Xamarin.Forms;

namespace CoffeeBeans.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private string itemId;
        private string text;
        private string description;
        private string type;
        private ImageSource imageSource;
        private float price;
        private float priceOrder;
        private float priceCal;
        public string Id { get; set; }

        public ItemDetailViewModel ()
        {
            OrderAmountCommand = new Command(OnOrderAmount);
        }

        public Command OrderAmountCommand { get; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public float Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public float PriceOrder
        {
            get => priceOrder;
            set => SetProperty(ref priceOrder, value);
        }

        public float PriceCal
        {
            get => priceCal;
            set => SetProperty(ref priceCal, value);
        }

        public ImageSource ImageSource
        {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
                Price = item.Price;
                ImageSource = item.ImageSource;
                Type = item.Type;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        private void OnOrderAmount()
        {
            PriceCal = priceOrder * price;

            //await Shell.Current.GoToAsync("..");
        }
    }
}
