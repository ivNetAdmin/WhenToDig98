
using System;
using System.Collections.Generic;
using WhenToDig98.Data;
using WhenToDig98.Helpers;
using WhenToDig98.Models;
using Xamarin.Forms;

namespace WhenToDig98.Pages
{
    public class PlantManager : ContentPage
    {
        private WTDDatabase _database;
        private IEnumerable<Plant> _plants;
        private int _currentPlant;

        public PlantManager(WTDDatabase database)
        {
            _database = database;

            // _database.ResetPlants();

            Padding = new Thickness(10);

           // var grid = BuildForm();

            this.Content = new StackLayout
            {
                Spacing = 5,
                Padding = new Thickness(5, 10)
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PageToolBarItems.Build(_database, this);  
            UpdatePlants();
        }
        
         private void UpdatePlants()
         {
             // get plants
            _plants = _database.GetPlants();
            
            ((StackLayout)this.Content).Children.Clear();
            ((StackLayout)this.Content).Children.Add(BuildPlantTaskBar());
         }
         
         private Grid BuildPlantTaskBar()
         {
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.Fill
            };
            
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.Children.Add(new Button
            {
                VerticalOptions = LayoutOptions.Fill,
                Text = "Edit Plant",
            }, 0, 0);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += EditPlantOnButtonClicked;

            grid.Children.Add(new Button
            {
                VerticalOptions = LayoutOptions.Fill,
                Text = "New Plant",
            }, 1, 0);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += NewPlantOnButtonClicked;

            return grid;
         }

        private void EditPlantOnButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddPlant(_database, _currentPlant));
        }

        private void NewPlantOnButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddPlant(_database));
        }

    }
}
