using System.ComponentModel;
using Xamarin.Forms;
using CoffeeBeans.ViewModels;

namespace CoffeeBeans.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}