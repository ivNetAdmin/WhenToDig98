
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
                    Name = "New"
                });

                _varietyList.Add(new Variety
                {
                    Name = "Early"
                });
            }
            else
            {
                _currentPlant = _database.GetPlant(plantId);
                _varietyList = _database.GetPlantVarieties(plantId);
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
                //Text = _task == null ? string.Empty : _task.Description
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
                //Text = _task == null ? string.Empty : _task.Description
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
                //Text = _task == null ? string.Empty : _task.Description
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
                //Text = _task == null ? string.Empty : _task.Description
            }, 1, 3);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 5);
          
            grid.Children.Add(new Label
            {
                Text = "Variety",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 4);

            grid.Children.Add(GetVarietyButtonList(), 1, 4);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 5);

            grid.Children.Add(new Button { Text = "Save" }, 0, 5);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 2);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += SavePlantOnButtonClicked;

            // grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.Children.Add(new Button { Text = "Cancel" }, 4, 5);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 2);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += SavePlantOnButtonClicked;

            return grid;
        }

        private Gird GetVarietyButtonList()
        {
            var rows = _varietyList.Count/3;
            
            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.StartAndExpand
            };
            
            for(int i=0;i<rows;i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }
            
            var currentRow = 0
            var currentColumn = 0
            for(int i=0;i<_varietyList.Count;i++)
            {
                var button = new Button{
                    Text =_varietyList[i].Name
                };
                grid.Children.Add(button), currentColumn, currentRow);
                currentColumn = i;
                if(currentColumn>3)
                {
                currentColumn++;
                currentRow = 0;
                }
            }
        }

        //private ListView GetVarietyButtonList()
        //{
                     
        //    ListView listView = new ListView
        //    {
                // Source of data items.
        //        ItemsSource = _varietyList,
        //        RowHeight=40,
                
               // BackgroundColor=Color.Red,

                // Define template for displaying each item (Argument of DataTemplate constructor is called for each item; it must return a Cell derivative.)
         //       ItemTemplate = new DataTemplate(() => {

                    // Create views with bindings for displaying each property.
        //            Button button = new Button();
        //            button.SetBinding(Button.TextProperty, "Name");
        //            button.HorizontalOptions = LayoutOptions.Start;
                    
               
                   // BoxView boxView = new BoxView();
        

        //            return new ViewCell
        //            {
             
        //                View = new StackLayout
        //                {
        //                    Orientation=StackOrientation.Horizontal,
        //                    BackgroundColor=Color.Blue,
        //                    HorizontalOptions=LayoutOptions.Start,
        //                    Children = {
        //                       button
        //                    }
        //                }
        //            };
        //        })
        //    };
        //    return listView;
        //}

        //private ListView GetVarietyList()
        //{
        //    var listView = new ListView
        //    {
        //        RowHeight = 40,
        //        ItemsSource = _varietyList,
        //        ItemTemplate = new DataTemplate(() =>
        //        {
        //            // var menuItem = new MenuItem
        //            var name = new Label
        //            {
        //                HorizontalTextAlignment = TextAlignment.Center,
        //                VerticalTextAlignment = TextAlignment.Center
        //                // BackgroundColor = Color.Yellow
        //            };
        //            name.SetBinding(Label.TextProperty, "Name");

        //            //var description = new Label
        //            //{
        //            //    HorizontalTextAlignment = TextAlignment.Start,
        //            //    VerticalTextAlignment = TextAlignment.Center,
        //            //    //BackgroundColor = Color.Red
        //            //};
        //            //description.SetBinding(Label.TextProperty, "Description");

        //            //var typeImage = new Image
        //            //{
        //            //    //BackgroundColor = Color.Blue
        //            //};
        //            //typeImage.SetBinding(Image.SourceProperty, "TaskTypeImage");

        //            Grid grid = new Grid
        //            {
        //                VerticalOptions = LayoutOptions.Fill
        //            };

        //            grid.RowDefinitions.Add(new RowDefinition
        //            {
        //                Height = GridLength.Auto
        //            });

        //            var deleteButton = new Button
        //            {
        //                Text = "X",
        //                TextColor = Color.Red
        //            };
        //            deleteButton.SetBinding(Button.ClassIdProperty, "ID");

        //            // grid.Children.Add(id,-1,0);
        //            grid.Children.Add(name, 0, 0);
        //            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 1);
        //            //grid.Children.Add(typeImage, 1, 0);
        //            //Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 1);
        //            //grid.Children.Add(description, 2, 0);
        //            //Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);
        //            grid.Children.Add(deleteButton, 5, 0);
        //            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 1);

        //         //   ((Button)grid.Children[grid.Children.Count - 1]).Clicked += DeleteTaskButtonClicked;

        //            var viewCell = new ViewCell
        //            {
        //                View = grid
        //            };

        //            viewCell.SetBinding(ViewCell.ClassIdProperty, "ID");

        //          //  viewCell.Tapped += TaskRowTapped;

        //            return viewCell;
        //        })
        //    };

        //    return listView;

        //}

        private void AddVarietyOnButtonClicked(object sender, EventArgs e)
        {
            //var addVariety = new AddVariety(_database);
            MessagingCenter.Subscribe<AddVariety, Variety>(this, "new variety", (page, variety) => {

                _varietyList.Add(variety);
                BuildForm();
            });
            Navigation.PushAsync(new AddVariety(_database));
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

                    _database.AddPlant(_currentPlant.ID, name, plantType, plantTime, harvestTime);

                    Navigation.PopToRootAsync();
                    break;
                case "Cancel":
                    Navigation.PopToRootAsync();
                    break;
            }
        }
    }

   
}
