using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.IO;
using CoffeeBeans.Models;
using Xamarin.Forms;

namespace CoffeeBeans.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private string text = "None selected";
        private string description;
        private float price;
        private List<string> speciesList = new List<string> { "arabica", "robusta" };
        private ImageSource imageSource;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
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

        public List<string> SpeciesList
        {
            get => speciesList;
            set => SetProperty(ref speciesList, value);
        }

        public ImageSource ImageSource
        {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }


        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Text = Text,
                Description = Description,
                Price = Price,
                ImageSource = ImageSource
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
