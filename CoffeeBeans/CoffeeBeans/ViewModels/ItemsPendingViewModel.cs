using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using MvvmHelpers;
using CoffeeBeans.Models;
using CoffeeBeans.Views;
using CoffeeBeans.Services;

namespace CoffeeBeans.ViewModels
{
    public class ItemsPendingViewModel : ObservableRangeCollection<ItemViewModel>, INotifyPropertyChanged
    {
        private Item _selectedItem;
        private bool _hasItems = false;
        private bool _expanded;
        private ObservableRangeCollection<ItemViewModel> itemCollenctions = new ObservableRangeCollection<ItemViewModel>();

        public ObservableCollection<Item> Items { get; }
        public Command LoadOrdersCommand { get; }
        public Command<Item> ItemTapped { get; }

        public Command SearchAll { get; }
        public Command SearchArabica { get; }
        public Command SearchRobusta { get; }

   

        public ItemsPendingViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadOrdersCommand = new Command(async () => await ExecuteLoadOrdersCommand());

            ItemTapped = new Command<Item>(OnItemSelected);
        }
        #region Item list view
        public ItemsPendingViewModel(ItemCollection itemCollection, bool expanded = false)
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadOrdersCommand = new Command(async () => await ExecuteLoadOrdersCommand());

            ItemTapped = new Command<Item>(OnItemSelected);


            ItemCollection = itemCollection;
            _expanded = expanded;
            foreach (Item item in itemCollection.Items)
            {
                Add(new ItemViewModel(item));
            }
            if (expanded) AddRange(itemCollenctions);
        }

        public bool Expanded
        {
            get
            {
                return _expanded;
            }
            set
            {
                if (_expanded != value)
                {
                    _expanded = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Expanded"));
                    OnPropertyChanged(new PropertyChangedEventArgs("StateIcon"));
                    if (_expanded)
                    {
                        AddRange(itemCollenctions);
                    }
                    else
                    {
                        Clear();
                    }
                }
            }
        }

        public string StateIcon
        {
            get
            {
                if (Expanded)
                {
                    return "arrow_up.png";
                }
                else
                {
                    return "arrow_down.png";
                }
            }
        }
        public string Name
        {
            get
            {
                return ItemCollection.Name;
            }
        }

        public ItemCollection ItemCollection
        {
            get;
            set;
        }
        #endregion

        async Task ExecuteLoadOrdersCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                    
                foreach (var item in items)                    
                {           
                    Items.Add(item);
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


        #region BaseViewModel
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
        [CallerMemberName] string propertyName = "",
        Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #endregion
    }
}