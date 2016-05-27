
using System;
using WhenToDig98.Data;
using Xamarin.Forms;

namespace WhenToDig98.Pages
{
    class AddFrost : ContentPage
    {
        private WTDDatabase _database;

        public AddFrost(WTDDatabase database)
        {
            _database = database;

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
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.StartAndExpand
            };

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            grid.Children.Add(new Label
            {
                Text = "Was there a frost today?",
                FontSize = 20,
                TextColor = Color.Teal,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 0);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 7);

            grid.Children.Add(new Label
            {
                Text = "Date",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 1);

            grid.Children.Add(new DatePicker
            {
                Format = "dd/MMM/yyyy",
                Date = DateTime.Now
            }, 1, 1);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);


            grid.Children.Add(new Button { Text = "Save" }, 0, 3);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += SaveFrostOnButtonClicked;

            grid.Children.Add(new Button { Text = "Cancel" }, 4, 3);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += SaveFrostOnButtonClicked;


            return grid;
        }

        private void SaveFrostOnButtonClicked(object sender, EventArgs e)
        {
            switch (((Button)sender).Text)
            {
                case "Save":
                    var layout = (StackLayout)this.Content;
                    var grid = (Grid)layout.Children[0];
                    var date = ((DatePicker)grid.Children[2]).Date;
                
                    _database.AddFrost(date);

                    Navigation.PopAsync();
                    break;
                case "Cancel":
                    Navigation.PopAsync();
                    break;
            }
        }
    }
}
