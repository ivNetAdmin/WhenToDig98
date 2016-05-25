
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
            _taskTypes = _database.GetTaskTypes();

            ((StackLayout)this.Content).Children.Add(BuildSearchForm());
            
           // _database.ResetDb();
        }
        
        private Grid BuildSearchForm()
        {

            var yearPicker = new Picker();
            foreach (var year in _years)
            {
                yearPicker.Items.Add(year);
            }

            var monthPicker = new Picker();
            foreach (var month in _months)
            {
                monthPicker.Items.Add(month);
            }

            var taskTypePicker = new Picker();
            foreach (var taskType in _taskTypes)
            {
                taskTypePicker.Items.Add(taskType);
            }
            
            var plantPicker = new Picker();
            foreach (var plant in _plants)
            {
                plantPicker.Items.Add(plant.DisplayName);
            }

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.Fill
            };

            for (int i = 0;i<3;i++)
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
            
            grid.Children.Add(new Label
            {
                VerticalOptions = LayoutOptions.Fill,
                Text = "Plant"
            }, 0, 2);
            
            grid.Children.Add(plantPicker, 1, 2);
            
            return grid;
        }
    }
}
