
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
            _currentPlant = _plants.FirstOrDefault();
            
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.Fill
            };
            
            for(int i = 0; i < 6; i++;)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }  
            
            ((StackLayout)this.Content).Children.Clear();
            ((StackLayout)this.Content).Children.Add(DisplayPlantInformation(grid));
            ((StackLayout)this.Content).Children.Add(BuildPlantTaskBar());
            ((StackLayout)this.Content).Children.Add(BuildPlantList());
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
                Text = "New Plant",
            }, 0, 0);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += EditPlantOnButtonClicked;

            grid.Children.Add(new Button
            {
                VerticalOptions = LayoutOptions.Fill,
                Text = "Edit Plant",
            }, 1, 0);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += NewPlantOnButtonClicked;

            return grid;
         }

        private ListView BuildPlantList()
        {
            var listView =  new ListView
            {
                RowHeight=40,
                ItemsSource = _plants,
                ItemTemplate = new DataTemplate(() =>
                {
                    var name = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center
                    };
                    name.SetBinding(Label.TextProperty, "PlantDisplayName");
                    
                    var viewCell = new ViewCell
                    {
                        View = name
                    };
                    viewCell.SetBinding(ViewCell.ClassIdProperty, "ID");

                    viewCell.Tapped += PlantRowTapped;

                    return viewCell;
                })
            };
            
            return listView;
        }
        
        private Grid DisplayPlantInformation(grid)
        {
            grid.Children.Clear();
            
            grid.Children.Add(new Label
            {
                VerticalOptions = LayoutOptions.Fill,
                Text = _currentPlant.PlantDisplayName,
            }, 0, 0);
            
            grid.Children.Add(new Label
            {
                VerticalOptions = LayoutOptions.Fill,
                Text = "Planting Time",
            }, 0, 1);
            
            grid.Children.Add(new Label
            {
                VerticalOptions = LayoutOptions.Fill,
                Text = _currentPlant.PlantingTime ,
            }, 0, 2);
            
            grid.Children.Add(new Label
            {
                VerticalOptions = LayoutOptions.Fill,
                Text = "Harvesting Time",
            }, 0, 3);
            
            grid.Children.Add(new Label
            {
                VerticalOptions = LayoutOptions.Fill,
                Text = _currentPlant.HarvestingTime  ,
            }, 0, 4);
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
