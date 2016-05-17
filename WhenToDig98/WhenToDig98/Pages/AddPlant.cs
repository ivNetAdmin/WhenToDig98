
using System;
using System.Collections.Generic;
using WhenToDig98.Data;
using WhenToDig98.Models;
using Xamarin.Forms;

namespace WhenToDig98.Pages
{
    public class AddPlant : ContentPage
    {
        private WTDDatabase _database;
        private Plant _plant;

        public AddPlant(WTDDatabase database, int plantId = 0)
        {
            _database = database;

            if (plantId == 0)
            {
                _plant = null;
            }
            else
            {
                _plant = _database.GetPlant(plantId);
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
                VerticalOptions = LayoutOptions.Fill
            };

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
                //Text = _task == null ? string.Empty : _task.Description
            }, 1, 0);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.Children.Add(new Label
            {
                Text = "Type",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 1);

            grid.Children.Add(new Editor
            {
                //Text = _task == null ? string.Empty : _task.Description
            }, 1, 1);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.Children.Add(new Label
            {
                Text = "Sow",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 2);

            grid.Children.Add(new Editor
            {
                //Text = _task == null ? string.Empty : _task.Description
            }, 1, 2);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.Children.Add(new Label
            {
                Text = "Reap",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 3);

            grid.Children.Add(new Editor
            {
                //Text = _task == null ? string.Empty : _task.Description
            }, 1, 3);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);

            //grid.Children.Add(new Label
            //{
            //    Text = "Sow Note",
            //    TextColor = Color.Silver,
            //    HorizontalTextAlignment = TextAlignment.Start,
            //    VerticalTextAlignment = TextAlignment.Center
            //}, 0, 4);

            //grid.Children.Add(new Editor
            //{
            //    // Text = _task == null ? string.Empty : _task.Notes
            //}, 1, 4);
            //Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);
            //Grid.SetRowSpan(grid.Children[grid.Children.Count - 1], 3);

            //grid.Children.Add(new Label
            //{
            //    Text = "Reap Note",
            //    TextColor = Color.Silver,
            //    HorizontalTextAlignment = TextAlignment.Start,
            //    VerticalTextAlignment = TextAlignment.Center
            //}, 0, 7);

            //grid.Children.Add(new Editor
            //{
            //    // Text = _task == null ? string.Empty : _task.Notes
            //}, 1, 7);
            //Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);
            //Grid.SetRowSpan(grid.Children[grid.Children.Count - 1], 3);

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.Children.Add(new Button { Text = "Add Variety" }, 0, 4);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += SaveVarietyOnButtonClicked;

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.Children.Add(GetVarietyList(), 0, 5);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.Children.Add(new Button { Text = "Save" }, 0, 6);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += SavePlantOnButtonClicked;

            // grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.Children.Add(new Button { Text = "Cancel" }, 4, 6);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += SavePlantOnButtonClicked;

            return grid;
        }

        private ListView GetVarietyList()
        {
            var listView = new ListView
            {
                RowHeight = 40,
                ItemsSource = new List<Variety>(),
                ItemTemplate = new DataTemplate(() =>
                {
                    // var menuItem = new MenuItem
                    var name = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center
                        // BackgroundColor = Color.Yellow
                    };
                    name.SetBinding(Label.TextProperty, "Name");

                    var description = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Center,
                        //BackgroundColor = Color.Red
                    };
                    //description.SetBinding(Label.TextProperty, "Description");

                    //var typeImage = new Image
                    //{
                    //    //BackgroundColor = Color.Blue
                    //};
                    //typeImage.SetBinding(Image.SourceProperty, "TaskTypeImage");

                    Grid grid = new Grid
                    {
                        VerticalOptions = LayoutOptions.Fill
                    };

                    grid.RowDefinitions.Add(new RowDefinition
                    {
                        Height = GridLength.Auto
                    });

                    var deleteButton = new Button
                    {
                        Text = "X",
                        TextColor = Color.Red
                    };
                    deleteButton.SetBinding(Button.ClassIdProperty, "ID");

                    // grid.Children.Add(id,-1,0);
                    grid.Children.Add(name, 0, 0);
                    Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 1);
                    //grid.Children.Add(typeImage, 1, 0);
                    //Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 1);
                    //grid.Children.Add(description, 2, 0);
                    //Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);
                    grid.Children.Add(deleteButton, 5, 0);
                    Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 1);

                 //   ((Button)grid.Children[grid.Children.Count - 1]).Clicked += DeleteTaskButtonClicked;

                    var viewCell = new ViewCell
                    {
                        View = grid
                    };

                    viewCell.SetBinding(ViewCell.ClassIdProperty, "ID");

                  //  viewCell.Tapped += TaskRowTapped;

                    return viewCell;
                })
            };

            return listView;

        }

        private void SaveVarietyOnButtonClicked(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void SavePlantOnButtonClicked(object sender, EventArgs e)
        {
            switch (((Button)sender).Text)
            {
                case "Save":
                    var layout = (StackLayout)this.Content;
                    var grid = (Grid)layout.Children[0];

                    var name = ((Editor)grid.Children[1]).Text;
                    var plantType = ((Editor)grid.Children[3]).Text;
                    var plantTime = ((Editor)grid.Children[5]).Text;
                    var harvestTime = ((Editor)grid.Children[7]).Text;                        

                    _database.AddPlant(_plant.ID, name, plantType, plantTime, harvestTime);

                    Navigation.PopToRootAsync();
                    break;
                case "Cancel":
                    Navigation.PopToRootAsync();
                    break;
            }
        }
    }
}
