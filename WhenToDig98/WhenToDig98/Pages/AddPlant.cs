
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WhenToDig98.Data;
using WhenToDig98.Models;
using Xamarin.Forms;

namespace WhenToDig98.Pages
{
    public class AddPlant : ContentPage
    {
        private WTDDatabase _database;
        private Plant _currentPlant;
        private ObservableCollection<Variety> _varietyList;

        public AddPlant(WTDDatabase database, int plantId = 0)
        {
            _database = database;

            if (plantId == 0)
            {
                _currentPlant = null;
                _varietyList = new ObservableCollection<Variety>();
                _varietyList.Add(new Variety
                {
                    ID = 0,
                    Name = "New"
                });
            }
            else
            {
                _currentPlant = _database.GetPlant(plantId);
                _varietyList = _database.GetPlantVarieties(plantId);
            }            

            this.Content = new StackLayout
            {
                Padding = new Thickness(5, 10),
                VerticalOptions = LayoutOptions.StartAndExpand
            };

            BuildForm();
        }

        private void BuildForm()
        {
            if (this.Content != null)
            {
                ((StackLayout)this.Content).Children.Clear();
            }

            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.StartAndExpand
            };

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            grid.Children.Add(new Label
            {
                Text = "Name",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 0);

            grid.Children.Add(new Editor
            {
                Text = _currentPlant == null ? string.Empty : _currentPlant.Name

            }, 1, 0);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 5);

            grid.Children.Add(new Label
            {
                Text = "Plant",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 1);

            grid.Children.Add(new Editor
            {
                Text = _currentPlant == null ? string.Empty : _currentPlant.PlantingTime
            }, 1, 1);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 5);

            grid.Children.Add(new Label
            {
                Text = "Harvest",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 2);

            grid.Children.Add(new Editor
            {
                Text = _currentPlant == null ? string.Empty : _currentPlant.HarvestingTime
            }, 1, 2);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 5);

            grid.Children.Add(new Label
            {
                Text = "Type",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 3);

            grid.Children.Add(new Editor
            {
                Text = _currentPlant == null ? string.Empty : _currentPlant.Type
            }, 1, 3);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 5);

            grid.Children.Add(new Label
            {
                Text = "Variety",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 4);

            var varietyButtonListGrid = new Grid
            {
                VerticalOptions = LayoutOptions.Start
            };

            GetVarietyButtonList(varietyButtonListGrid);

            grid.Children.Add(varietyButtonListGrid, 1, 4);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 5);

            grid.Children.Add(new Button { Text = "Save" }, 0, 5);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 2);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += SavePlantOnButtonClicked;

            grid.Children.Add(new Button { Text = "Cancel" }, 4, 5);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 2);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += SavePlantOnButtonClicked;

            ((StackLayout)this.Content).Children.Add(grid);
        }

        private void BuildVarietyList()
        {
            var contentGrid = ((Grid)((StackLayout)this.Content).Children[0]);

            var buttonGrid = ((Grid)contentGrid.Children[9]);

            buttonGrid.Children.Clear();

            GetVarietyButtonList(buttonGrid);

        }

        private void GetVarietyButtonList(Grid grid)
        {

            var rows = _varietyList.Count / 3;

            if (_varietyList.Count % 3 != 0) rows++;


            for (int i = 0; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            var currentRow = 0;
            var currentColumn = 0;
            for (var i = 0; i < _varietyList.Count; i++)
            {
                var button = new Button
                {
                    Text = _varietyList[i].Name,
                    ClassId = _varietyList[i].ID.ToString()
                };
                button.Clicked += AddVarietyOnButtonClicked;

                if (currentColumn > 2)
                {
                    currentColumn = 0;
                    currentRow++;
                }

                grid.Children.Add(button, currentColumn, currentRow);
                currentColumn++;
            }

            
        }

        private void AddVarietyOnButtonClicked(object sender, EventArgs e)
        {
            //var addVariety = new AddVariety(_database);
            MessagingCenter.Subscribe<AddVariety, Variety>(this, "new variety", (page, variety) => {

                _varietyList.Add(variety);
                BuildVarietyList();
            });

            var varietyIdString = ((Button)sender).ClassId;

            Navigation.PushAsync(new AddVariety(_database, Convert.ToInt32(varietyIdString)));
        }
      
        private void SavePlantOnButtonClicked(object sender, EventArgs e)
        {
            switch (((Button)sender).Text)
            {
                case "Save":
                    var layout = (StackLayout)this.Content;
                    var grid = (Grid)layout.Children[0];

                    var name = ((Editor)grid.Children[1]).Text;
                    
                    var plantTime = ((Editor)grid.Children[3]).Text;
                    var harvestTime = ((Editor)grid.Children[5]).Text;
                    var plantType = ((Editor)grid.Children[7]).Text;

                    var plantId = _database.AddPlant(_currentPlant == null ? 0 : _currentPlant.ID, name, plantType, plantTime, harvestTime);

                    foreach(var variety in _varietyList)
                    {
                        _database.AddVariety(variety.ID, variety.Name, variety.PlantingNotes, variety.HarvestingNotes, plantId);
                    }

                    Navigation.PopToRootAsync();
                    break;
                case "Cancel":
                    Navigation.PopToRootAsync();
                    break;
            }
        }
    }

   
}
