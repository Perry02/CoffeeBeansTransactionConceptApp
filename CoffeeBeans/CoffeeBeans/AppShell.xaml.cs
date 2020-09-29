using System;
using System.Collections.Generic;
using CoffeeBeans.ViewModels;
using CoffeeBeans.Services;
using CoffeeBeans.Views;
using Xamarin.Forms;

namespace CoffeeBeans
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));

            // remove bottom navigation bar
            Shell.SetTabBarIsVisible(this, false);
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
