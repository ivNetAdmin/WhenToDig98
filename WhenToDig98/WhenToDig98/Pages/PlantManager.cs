
using System;
using System.Collections.Generic;
using WhenToDig98.Data;
using WhenToDig98.Helpers;
using WhenToDig98.Entities;
using Xamarin.Forms;

namespace WhenToDig98.Pages
{
    public class PlantManager : ContentPage
    {
        private WTDDatabase _database;
        private IEnumerable<Plant> _plants;
        private IEnumerable<Variety> _varieties;
        private Plant _currentPlant;
        private Grid _varietyInformation;
        private StackLayout _topStack;
        private StackLayout _bottomStack;

        public PlantManager(WTDDatabase database)
        {
            _database = database;

            this.Content = new WTDLayout();

            _topStack = ((WTDLayout)this.Content).TopStack;
            _bottomStack = ((WTDLayout)this.Content).BottomStack;

            PageToolBarItems.Build(_database, this);
            PageToolBarItems.Build(_bottomStack);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing(); 
            UpdatePlants();
        }
        
         private void UpdatePlants()
         {
             // get plants
            _plants = _database.GetPlants();

            foreach (var plant in _plants)
            {
                _currentPlant = plant;
                break;
            }

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.Fill
            };
            
            for(int i = 0; i < 5; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            _topStack.Children.Clear();
            _topStack.Children.Add(DisplayPlantInformation(grid));
            _topStack.Children.Add(BuildPlantTaskBar());
            _topStack.Children.Add(BuildPlantList());
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
                        //HorizontalTextAlignment = TextAlignment.Center,
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
        
        private Grid DisplayPlantInformation(Grid grid)
        {            
            grid.Children.Clear();
            if (_currentPlant == null) return grid;

            grid.Children.Add(new Button
            {
                VerticalOptions = LayoutOptions.Start,
                Text = "Varieties +",
            }, 0, 0);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += ShowVarietiesOnButtonClicked;
            
            grid.Children.Add(new Label
            {
                TextColor=Color.Aqua,
                VerticalOptions = LayoutOptions.Fill,
                Text = _currentPlant.PlantDisplayName,
            }, 0, 1);

            grid.Children.Add(new Label
            {
                VerticalOptions = LayoutOptions.Fill,
                Text = string.Format("Planting Time: {0}", _currentPlant.PlantingTime)
            }, 0, 2);
                    
            grid.Children.Add(new Label
            {
                VerticalOptions = LayoutOptions.Fill,
                Text = string.Format("Harvesting Time: {0}", _currentPlant.HarvestingTime)
            }, 0, 3);
            
            _varietyInformation = new Grid
            {
                VerticalOptions = LayoutOptions.Fill,
                IsVisible = false
            };

            grid.Children.Add(_varietyInformation, 0, 4);
        
            return grid;
        }

        private void ShowVarietiesOnButtonClicked(object sender, EventArgs e)
        {
            if(((Button)sender).Text == "Varieties -")
            {
                ((Button)sender).Text = "Varieties +";
            } else
            {
                ((Button)sender).Text = "Varieties -";
            }

            _varietyInformation.IsVisible = !_varietyInformation.IsVisible;
            
            if(_varietyInformation.IsVisible)
            {
                _varietyInformation.Children.Clear();
                _varieties = _database.GetPlantVarieties(_currentPlant.ID);
                
                var counter=0;
                foreach(var variety in _varieties)
                {
                    _varietyInformation.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    _varietyInformation.Children.Add(new Label
                    {
                        TextColor = Color.Aqua,
                        VerticalOptions = LayoutOptions.Fill,
                        Text = variety.Name
                    }, 0, counter);
                    counter++;

                    _varietyInformation.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    _varietyInformation.Children.Add(new Label
                    {
                        VerticalOptions = LayoutOptions.Fill,
                        Text = string.Format("Planting Notes: {0}",variety.PlantingNotes)
                    }, 0, counter);
                    counter++;

                    _varietyInformation.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    _varietyInformation.Children.Add(new Label
                    {
                        VerticalOptions = LayoutOptions.Fill,
                        Text = string.Format("Harvesting Notes: {0}",variety.HarvestingNotes)
                    }, 0, counter);
                    counter++;
                }
                
            }
        }
        
        private void EditPlantOnButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddPlant(_database, _currentPlant == null ? 0 : _currentPlant.ID));
        }

        private void NewPlantOnButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddPlant(_database));
        }
        
        private void PlantRowTapped(object sender, EventArgs e)
        {
            var id = Convert.ToInt32(((ViewCell)sender).ClassId);
            
            foreach(var plant in _plants)
            {
               if(plant.ID == id) 
               {
                  _currentPlant = plant;
                  var grid = (Grid)_topStack.Children[0];
                  DisplayPlantInformation(grid);
                  break;
               }
            }
        }

    }
}
