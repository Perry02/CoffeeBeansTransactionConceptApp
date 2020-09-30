using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Diagnostics;

using Xamarin.Forms;

using CoffeeBeans.Models;
using CoffeeBeans.Views;

namespace CoffeeBeans.ViewModels
{
    public class ItemsPendingViewModel : BaseViewModel
    {
        private Item _selectedItem;
        private string _itemSearchKeyword = null;
        private bool _hasItems = false;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public Command SearchPending { get; }
        public Command SearchInTransit { get; }
        public Command SearchHistory { get; }



        public ItemsPendingViewModel()
        {
            Title = "Orders";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            SearchPending = new Command(OnSearchPending);
            SearchInTransit = new Command(OnSearchInTransit);
            SearchHistory = new Command(OnSearchHistory);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                if (_itemSearchKeyword != null)
                {
                    foreach (var item in items)
                    {
                        if (item.Type == _itemSearchKeyword)
                        {
                            Items.Add(item);
                        }
                    }
                } else
                {
                    foreach (var item in items)
                    {
                        Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }


        // TODO simplify
        private void OnSearchPending()
        {
            _itemSearchKeyword = null;
            ExecuteLoadItemsCommand();
        }

        private void OnSearchInTransit()
        {
            _itemSearchKeyword = "arabica";
            ExecuteLoadItemsCommand();
        }

        private void OnSearchHistory()
        {
            _itemSearchKeyword = "robusta";
            ExecuteLoadItemsCommand();
        }
    }
}