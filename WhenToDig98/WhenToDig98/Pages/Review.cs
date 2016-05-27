
using System;
using System.Collections.Generic;
using WhenToDig98.Data;
using WhenToDig98.Helpers;
using WhenToDig98.Entities;
using Xamarin.Forms;
using WhenToDig98.Models;

namespace WhenToDig98.Pages
{
    public class Review : ContentPage
    {
        private WTDDatabase _database;
        private IEnumerable<Plant> _plants;
        private IEnumerable<string> _notes;
        private Grid _noteGrid;

        public Review(WTDDatabase database)
        {
            _database = database;
            _noteGrid = new Grid();

            Padding = new Thickness(10);

            this.Content = new StackLayout
            {
                Spacing = 5,
                Padding = new Thickness(5, 10),
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PageToolBarItems.Build(_database, this);
            UpdateReview();
            //_database.ResetDb();
        }

        private void UpdateReview()
        {
            _plants = _database.GetPlants();
          
            ((StackLayout)this.Content).Children.Clear();
            ((StackLayout)this.Content).Children.Add(BuildSearchForm());
            ((StackLayout)this.Content).Children.Add(_noteGrid);
        }

        private Grid BuildSearchForm()
        {
            var plantPicker = new Picker();
            plantPicker.Items.Add("All Plants");
            foreach (var plant in _plants)
            {
                plantPicker.Items.Add(plant.PlantDisplayName);
            }
            plantPicker.SelectedIndex = 0;

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.Fill
            };

            for (int i = 0; i < 3; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }
           
            grid.Children.Add(plantPicker, 0, 0);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);
         
            grid.Children.Add(new Button
            {
                Text = "Variety Notes"
            }, 0, 1);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += SearchNotesOnButtonClicked;

            grid.Children.Add(new Button
            {
                Text = "Sowing"
            }, 0, 2);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += SearchNotesOnButtonClicked;

            grid.Children.Add(new Button
            {
                Text = "Harvesting"
            }, 1, 2);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += SearchNotesOnButtonClicked;

            grid.Children.Add(new Button
            {
                Text = "Cultivating"
            }, 2, 2);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += SearchNotesOnButtonClicked;

            return grid;
        }

        private void SearchNotesOnButtonClicked(object sender, EventArgs e)
        {

            var layout = (StackLayout)this.Content;
            var grid = (Grid)layout.Children[0];

            var plant = ((Picker)grid.Children[0]).SelectedIndex > 0 ? ((List<Plant>)_plants)[((Picker)grid.Children[0]).SelectedIndex - 1] : null;
            var plantId = plant == null ? 0 : plant.ID;            

            switch (((Button)sender).Text)
            {
                case "Variety Notes":
                    _notes = _database.GetNotes("variety", plantId);
                    break;
                case "Sowing":
                    _notes = _database.GetNotes("sow", plantId);
                    break;
                case "Harvesting":
                    _notes = _database.GetNotes("harvest", plantId);
                    break;
                case "Cultivating":
                    _notes = _database.GetNotes("cultivate", plantId);
                    break;
            }

            _noteGrid.Children.Clear();

            var rowCounter = 0;
            foreach(var note in _notes)
            {
                _noteGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                var notes = note.Split("-");
                
                _noteGrid.Children.Add(new Label
                {
                    Text = notes[0],
                    TextColor = Color.Teal,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center
                }, 0, rowCounter);
                _noteGrid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);
                
                _noteGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                _noteGrid.Children.Add(new Label
                {
                    Text = notes[1],
                    TextColor = Color.Silver,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalTextAlignment = TextAlignment.Center
                }, 0, rowCounter);
                _noteGrid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);
                
                rowCounter++;

            }
        }
    }
}
