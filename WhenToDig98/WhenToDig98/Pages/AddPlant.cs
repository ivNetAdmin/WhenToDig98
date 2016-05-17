
﻿
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

        public AddTask(WTDDatabase database, int plantId = 0)
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
                VerticalOptions = LayoutOptions.StartAndExpand
            };

            // date entry
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            grid.Children.Add(new Label
            {
                Text = "Date",
                TextColor = Color.Silver,
                //BackgroundColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 0);

            grid.Children.Add(new DatePicker
            {
                Format = "dd/MMM/yyyy",
                Date = _task == null ? DateTime.Now : _task.Date
            }, 1, 0);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);

            // type entry
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var typePicker = new Picker();
            var selectedTypeIndex = -1;
            for (int i = 0; i < _taskTypes.Length; i++)
            {
                if (_task!=null && _task.Type == i + 1) selectedTypeIndex = i;
                typePicker.Items.Add(_taskTypes[i]);
            }
            if (_task != null && _task.Type == 4) selectedTypeIndex = 2;
            if (selectedTypeIndex > -1)
                typePicker.SelectedIndex = selectedTypeIndex;

            grid.Children.Add(new Label
            {
                Text = "Type",
                TextColor = Color.Silver,
                //BackgroundColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 1);

            grid.Children.Add(typePicker, 1, 1);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);

            // plant entry
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var plantPicker = new Picker();
            var selectedPlantIndex = -1;
            foreach(var plant in _plants)
            {
                if (_task != null)
                {
                    if (plant == _task.Plant) selectedPlantIndex = Array.FindIndex(_plants, index => index.Contains(plant));
                }
                plantPicker.Items.Add(plant);
            }
            if(selectedPlantIndex>-1)
            plantPicker.SelectedIndex = selectedPlantIndex;

            grid.Children.Add(new Label
            {
                Text = "Plant",
                TextColor = Color.Silver,
                //BackgroundColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 2);

            grid.Children.Add(plantPicker, 1, 2);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);

            grid.Children.Add(new Label
            {
                Text = "Task",
                TextColor = Color.Silver,
                //BackgroundColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 3);


            grid.Children.Add(new Editor
            {
                Text = _task == null ? string.Empty : _task.Description
            }, 1, 3);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);

            grid.Children.Add(new Label
            {
                Text = "Notes",
                TextColor = Color.Silver,
                //BackgroundColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 4);

            grid.Children.Add(new Editor
            {
                Text = _task == null ? string.Empty : _task.Notes
            }, 1, 4);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);
            Grid.SetRowSpan(grid.Children[grid.Children.Count - 1], 3);

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.Children.Add(new Button { Text = "Save" }, 0, 7);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += SaveTaskOnButtonClicked;

           // grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.Children.Add(new Button { Text = "Cancel" }, 4, 7);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += SaveTaskOnButtonClicked;

            return grid;
        }

        private void SaveTaskOnButtonClicked(object sender, EventArgs e)
        {
            switch (((Button)sender).Text)
            {
                case "Save":
                    var layout = (StackLayout)this.Content;
                    var grid = (Grid)layout.Children[0];

                    var date = ((DatePicker)grid.Children[1]).Date;
                    var taskType = ((Picker)grid.Children[3]).SelectedIndex + 1; // to make the background images work 
                    var plant = _plants[((Picker)grid.Children[5]).SelectedIndex];
                    var description = ((Editor)grid.Children[7]).Text;
                    var notes = ((Editor)grid.Children[9]).Text;

                    // to make the images work 
                    if (taskType == 3) taskType = 4;

                    var taskId = _task == null ? 0 : _task.ID;

                    _database.AddTask(taskId, description, notes, taskType, date, plant);

                    Navigation.PopToRootAsync();
                    break;
                case "Cancel":
                    Navigation.PopToRootAsync();
                    break;
            }
        }
    }
}
