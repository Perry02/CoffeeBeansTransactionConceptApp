using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using CoffeeBeans.Models;
using CoffeeBeans.Views;

namespace CoffeeBeans.ViewModels
{
    public class ItemsViewModel2 : BaseViewModel
    {
        private Item _selectedItem;
        private string _itemSearchKeyword;
        private bool _hasItems = false;

        public ObservableCollection<Item> Items2 { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }

        public Command SearchAll { get; }
        public Command SearchArabica { get; }
        public Command SearchRobusta { get; }

   

        public ItemsViewModel2()
        {
            Title = "Browse";
            Items2 = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            SearchAll = new Command(OnSearchAll);
            SearchArabica = new Command(OnSearchArabica);
            SearchRobusta = new Command(OnSearchRobusta);
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            if (!_hasItems)
            {
                await DataStore.RandomizeItems();
                _hasItems = true;
            }

            try
            {
                Items2.Clear();
                var items = await DataStore.GetItemsAsync(true);
                if (_itemSearchKeyword != null)
                {
                    foreach (var item in items)
                    {
                        if (item.Type == _itemSearchKeyword)
                        {
                            Items2.Add(item);
                        }
                    }
                } else
                {
                    foreach (var item in items)
                    {
                        Items2.Add(item);
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

        private async void OnAddItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewItemPage));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}");
        }


        // TODO simplify
        private void OnSearchAll()
        {
            _itemSearchKeyword = null;
            ExecuteLoadItemsCommand();
        }

        private void OnSearchArabica()
        {
            _itemSearchKeyword = "arabica";
            ExecuteLoadItemsCommand();
        }

        private void OnSearchRobusta()
        {
            _itemSearchKeyword = "robusta";
            ExecuteLoadItemsCommand();
        }
    }
}