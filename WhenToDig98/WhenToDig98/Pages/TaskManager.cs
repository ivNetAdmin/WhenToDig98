
using System;
using System.Collections.Generic;
using WhenToDig98.Data;
using WhenToDig98.Helpers;
using WhenToDig98.Models;
using Xamarin.Forms;

namespace WhenToDig98.Pages
{
    public class TaskManager : ContentPage
    {
        private WTDDatabase _database;
        private IEnumerable<Plant> _plants;
        private IEnumerable<Task> _tasks;
        private IEnumerable<string> _taskTypes;
        private IEnumerable<string> _years;
        private IEnumerable<string> _months;

        private Grid _taskInformation;
        
        public TaskManager(WTDDatabase database)
        {
            _database = database;

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
            UpdateTasks();
        }

        private void UpdateTasks()
        {
            _plants = _database.GetPlants();
            _years = _database.GetYears();
            _months = _database.GetMonths();
            _taskTypes = new List<string>(_database.GetTaskTypes()); 

            ((StackLayout)this.Content).Children.Add(BuildSearchForm());
            ((StackLayout)this.Content).Children.Add(BuildTaskList());
            
           // _database.ResetDb();
        }
        
        private Grid BuildSearchForm()
        {

            var yearPicker = new Picker();
            yearPicker.Items.Add("All Years");
            foreach (var year in _years)
            {
                yearPicker.Items.Add(year);
            }
            yearPicker.SelectedIndex = 0;

            var monthPicker = new Picker();
            monthPicker.Items.Add("All Months");
            foreach (var month in _months)
            {
                monthPicker.Items.Add(month);
            }
            monthPicker.SelectedIndex = 0;

            var taskTypePicker = new Picker();
            taskTypePicker.Items.Add("All");
            foreach (var taskType in _taskTypes)
            {
                taskTypePicker.Items.Add(taskType);
            }
            taskTypePicker.SelectedIndex = 0;

            var plantPicker = new Picker();
            plantPicker.Items.Add("All");
            foreach (var plant in _plants)
            {
                plantPicker.Items.Add(plant.PlantDisplayName);
            }
            plantPicker.SelectedIndex = 0;

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.Fill
            };

            for (int i = 0; i < 5; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            }

            grid.Children.Add(new Label
            {
                VerticalOptions = LayoutOptions.Fill,
                Text = "Season"
            }, 0, 0);

            grid.Children.Add(yearPicker, 1, 0);
            grid.Children.Add(monthPicker, 2, 0);

            grid.Children.Add(new Label
            {
                VerticalOptions = LayoutOptions.Fill,
                Text = "Type"
            }, 0, 1);
            
            grid.Children.Add(taskTypePicker, 1, 1);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 2);

            grid.Children.Add(new Label
            {
                VerticalOptions = LayoutOptions.Fill,
                Text = "Plant"
            }, 0, 2);
            
            grid.Children.Add(plantPicker, 1, 2);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 2);

            grid.Children.Add(new Label
            {
                VerticalOptions = LayoutOptions.Fill,
                Text = "Task"
            }, 0, 3);
            
            grid.Children.Add(new Entry
            {
            }, 1, 3);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 2);

            grid.Children.Add(new Button
            {                
                Text = "Search"
            }, 1, 4);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 2);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += SearchOnButtonClicked;

            return grid;
        }

        private void SearchOnButtonClicked(object sender, EventArgs e)
        {
            var layout = (StackLayout)this.Content;
            var grid = (Grid)layout.Children[0];


            var season = ((Picker)grid.Children[1]).SelectedIndex > 0 ? ((List<string>)_years)[((Picker)grid.Children[1]).SelectedIndex - 1] : null;
            var month = ((Picker)grid.Children[2]).SelectedIndex;
            var taskType = ((Picker)grid.Children[4]).SelectedIndex > 0 ? ((List<string>)_taskTypes)[((Picker)grid.Children[4]).SelectedIndex - 1] : null;
            var plant = ((Picker)grid.Children[6]).SelectedIndex > 0 ? ((List<Plant>)_plants)[((Picker)grid.Children[6]).SelectedIndex - 1].PlantDisplayName : null;
            var task = ((Entry)grid.Children[8]).Text;

            var tasks = _database.GetTasks(season, month, taskType, plant, task);
            var cakes = tasks;
        }
        
        private ListView BuildTaskList()
        {           
            var listView =  new ListView
            {
                RowHeight=40,
                ItemsSource = _tasks,
                ItemTemplate = new DataTemplate(() =>
                {                   
                    // var menuItem = new MenuItem
                    var date = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center
                        // BackgroundColor = Color.Yellow
                    };
                    date.SetBinding(Label.TextProperty, "Day");

                    var description = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Center,
                        //BackgroundColor = Color.Red
                    };
                    description.SetBinding(Label.TextProperty, "Description");

                    var typeImage = new Image
                    {
                        //BackgroundColor = Color.Blue
                    };
                    typeImage.SetBinding(Image.SourceProperty, "TaskTypeImage");

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
                    grid.Children.Add(date, 0, 0);
                    Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 1);
                     grid.Children.Add(typeImage, 1, 0);
                    Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 1);
                    grid.Children.Add(description, 2, 0);
                    Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);
                    grid.Children.Add(deleteButton, 5, 0);
                    Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 1);
                    
                    ((Button)grid.Children[grid.Children.Count - 1]).Clicked += DeleteTaskButtonClicked;

                    var viewCell = new ViewCell
                    {
                        View = grid
                    };

                    viewCell.SetBinding(ViewCell.ClassIdProperty, "ID");

                    viewCell.Tapped += TaskRowTapped;

                    return viewCell;
                })
            };
          
            return listView;
        }
    }
}
