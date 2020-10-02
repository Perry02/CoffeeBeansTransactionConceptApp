using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CoffeeBeans.Models;
using CoffeeBeans.Views;
using CoffeeBeans.ViewModels;

namespace CoffeeBeans.Views
{
    public partial class ItemsPendingPage : ContentPage
    {
        ItemsPendingViewModel _pendingViewModel;

        public ItemsPendingPage()
        {
            InitializeComponent();

            BindingContext = _pendingViewModel = new ItemsPendingViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _pendingViewModel.OnAppearing();
        }
    }
}