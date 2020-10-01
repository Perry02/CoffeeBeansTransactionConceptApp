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
    public partial class ItemsPage2 : ContentPage
    {
        ItemsViewModel2 _viewModel2;

        public ItemsPage2()
        {
            InitializeComponent();

            BindingContext = _viewModel2 = new ItemsViewModel2();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel2.OnAppearing();
        }
    }
}