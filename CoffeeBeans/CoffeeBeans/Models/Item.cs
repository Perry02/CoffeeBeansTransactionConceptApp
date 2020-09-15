using System;
using Xamarin.Forms;

namespace CoffeeBeans.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public ImageSource ImageSource { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
    }
}