
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
                Text = "P-Time",
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
                Text = "H-Time",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 3);

            grid.Children.Add(new Editor
            {
                //Text = _task == null ? string.Empty : _task.Description
            }, 1, 3);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);
            
            grid.Children.Add(new Label
            {
                Text = "P-Note",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 4);

            grid.Children.Add(new Editor
            {
               // Text = _task == null ? string.Empty : _task.Notes
            }, 1, 4);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);
            Grid.SetRowSpan(grid.Children[grid.Children.Count - 1], 2);

            grid.Children.Add(new Label
            {
                Text = "H-Note",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 6);

            grid.Children.Add(new Editor
            {
                // Text = _task == null ? string.Empty : _task.Notes
            }, 1, 6);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);
            Grid.SetRowSpan(grid.Children[grid.Children.Count - 1], 2);
            return grid;
        }
}
