
using WhenToDig98.Data;
using WhenToDig98.Helpers;
using Xamarin.Forms;

namespace WhenToDig98.Pages
{
    public class PlantManager : ContentPage
    {
        private WTDDatabase _database;

        public PlantManager(WTDDatabase database)
        {
            _database = database;

            // _database.ResetPlants();

            Padding = new Thickness(10);

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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PageToolBarItems.Build(_database, this);           
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
                Text = "Variety",
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
                Text = "Type",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 2);

            grid.Children.Add(new Editor
            {
                //Text = _task == null ? string.Empty : _task.Description
            }, 1, 2);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);

            grid.Children.Add(new Label
            {
                Text = "Plant",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 3);

            grid.Children.Add(new Editor
            {
               // Text = _task == null ? string.Empty : _task.Notes
            }, 1, 3);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);
            Grid.SetRowSpan(grid.Children[grid.Children.Count - 1], 3);

            grid.Children.Add(new Label
            {
                Text = "Harvest",
                TextColor = Color.Silver,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center
            }, 0, 6);

            grid.Children.Add(new Editor
            {
                // Text = _task == null ? string.Empty : _task.Notes
            }, 1, 6);
            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 6);
            Grid.SetRowSpan(grid.Children[grid.Children.Count - 1], 3);
            return grid;

        }
    }
}
