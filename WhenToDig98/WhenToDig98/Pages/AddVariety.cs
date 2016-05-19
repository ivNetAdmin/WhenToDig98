
using System;
using System.Collections.Generic;
using WhenToDig98.Data;
using WhenToDig98.Models;
using Xamarin.Forms;

namespace WhenToDig98.Pages
{
    public class AddVariety : ContentPage
    {
        private WTDDatabase _database;
        private Variety _variety;

        public AddVariety(WTDDatabase database, int varietyId = 0)
        {
            _database = database;

            if (varietyId == 0)
            {
                _variety = null;
            }
            else
            {
                _variety = _database.GetVariety(varietyId);
            }

            var grid = BuildForm();

            this.Content = new StackLayout
            {
                Padding = new Thickness(5, 10),
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    grid
                }
            };
        }
        
        private Grid BuildForm()
        {
           Grid grid = new Grid
            {
               VerticalOptions = LayoutOptions.StartAndExpand
           };

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition {  });
            grid.RowDefinitions.Add(new RowDefinition {  });
            grid.RowDefinitions.Add(new RowDefinition {  });
            grid.RowDefinitions.Add(new RowDefinition {  });
            grid.RowDefinitions.Add(new RowDefinition {  });
            grid.RowDefinitions.Add(new RowDefinition {  });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            grid.Children.Add(new Label
            {
                Text = "Name",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 0);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 1);

            grid.Children.Add(new Editor
            {
                Text = _variety == null ? string.Empty : _variety.Name
            }, 1, 0);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 5);

            grid.Children.Add(new Label
            {
                Text = "Plant",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 1);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 1);
            Grid.SetRowSpan(grid.Children[grid.Children.Count - 1], 1);

            grid.Children.Add(new Editor
            {
                Text = _variety == null ? string.Empty : _variety.PlantingNotes
            }, 1, 1);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 5);
            Grid.SetRowSpan(grid.Children[grid.Children.Count - 1], 3);
           
            grid.Children.Add(new Label
            {
                Text = "Harvest",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 4);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 1);

            grid.Children.Add(new Editor
            {
                Text = _variety == null ? string.Empty : _variety.HarvestingNotes
            }, 1, 4);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 5);
            Grid.SetRowSpan(grid.Children[grid.Children.Count - 1], 3);

            grid.Children.Add(new Button { Text = "Save" }, 0, 7);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += SaveVarietyOnButtonClicked;

            grid.Children.Add(new Button { Text = "Cancel" }, 3, 7);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += SaveVarietyOnButtonClicked;

            return grid;
        }
        
        private void SaveVarietyOnButtonClicked(object sender, EventArgs e)
        {
            switch (((Button)sender).Text)
            {
                case "Save":
                
                    var layout = (StackLayout)this.Content;
                    var grid = (Grid)layout.Children[0];

                    var name = ((Editor)grid.Children[1]).Text;
                    var plantNotes = ((Editor)grid.Children[3]).Text;
                    var harvestNotes = ((Editor)grid.Children[5]).Text;

                    var variety = new Variety {
                        Name = name,
                        PlantingNotes = plantNotes,
                        HarvestingNotes = harvestNotes
                    };

                    MessagingCenter.Send<AddVariety, Variety>(this, "new variety", variety);

                    Navigation.PopAsync();
                    break;
                case "Cancel":
                    Navigation.PopAsync();
                    break;
            }
        }
    }
}
