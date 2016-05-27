
using System;
using System.Threading.Tasks;
using WhenToDig98.Data;
using WhenToDig98.Pages;
using Xamarin.Forms;

namespace WhenToDig98.Helpers
{
   public static class PageToolBarItems
    {
        private static ContentPage _currentPage;
        private static WTDDatabase _database;

        public static void Build(WTDDatabase database, ContentPage currentPage)
        {
            _database = database;

            _currentPage = currentPage;

            _currentPage.ToolbarItems.Clear();

            _currentPage.ToolbarItems.Add(new ToolbarItem
            {          
                Text = "Calendar",
                Icon = new FileImageSource
                {                  
                    File = _currentPage.GetType().ToString().IndexOf("Calendar") != -1 ? "calendar.png" : "calendar_low.png"
                }                                          
            });
            _currentPage.ToolbarItems[_currentPage.ToolbarItems.Count - 1].Clicked += MenuItemActivated;
            _currentPage.ToolbarItems.Add(new ToolbarItem
            {
                Text = "TaskManager",
                Icon = new FileImageSource
                {
                    File = _currentPage.GetType().ToString().IndexOf("TaskManager") != -1 ? "task.png" : "task_low.png"
                }
            });
            _currentPage.ToolbarItems[_currentPage.ToolbarItems.Count - 1].Clicked += MenuItemActivated;
            _currentPage.ToolbarItems.Add(new ToolbarItem
            {
                Text = "Review",
                Icon = new FileImageSource
                {
                    File = _currentPage.GetType().ToString().IndexOf("Review") != -1 ? "review.png" : "review_low.png"
                }
            });
            _currentPage.ToolbarItems[_currentPage.ToolbarItems.Count - 1].Clicked += MenuItemActivated;
            _currentPage.ToolbarItems.Add(new ToolbarItem
            {
                Text = "PlantManager",
                Icon = new FileImageSource
                {
                    File = _currentPage.GetType().ToString().IndexOf("PlantManager") != -1 ? "plant.png" : "plant_low.png"
                }
            });
            _currentPage.ToolbarItems[_currentPage.ToolbarItems.Count - 1].Clicked += MenuItemActivated;
        }

        public static void Build(WTDDatabase database, StackLayout layout, INavigation nav)
        {
            _database = database;

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.StartAndExpand
            };

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            grid.Children.Add(new Image
            {
                Source = "frost.png",
                HorizontalOptions = LayoutOptions.Start
            }, 0, 0);
            ((Image)grid.Children[grid.Children.Count - 1]).GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => OnFooterButtonClicked(nav, "frost"))
            });

            grid.Children.Add(new Image
            {
                Source = "location.png",
                HorizontalOptions = LayoutOptions.End
            }, 1, 0);
            ((Image)grid.Children[grid.Children.Count - 1]).GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => OnFooterButtonClicked(nav, "location"))
            });

            layout.Children.Add(grid);
        }

        private static void OnFooterButtonClicked(INavigation nav, string item)
        {
            switch (item)
            {
                case "frost":
                    nav.PushAsync(new AddFrost(_database));
                    break;
                case "location":
                   
                    break;

            }
        }

        private static Task<bool> DisplayAlert(string v1, string v2, string v3, string v4)
        {
            throw new NotImplementedException();
        }

        private static void MenuItemActivated(object sender, EventArgs e)
        {
            switch(((ToolbarItem)sender).Text)
            {
                case "Calendar":
                    App.Current.MainPage =new NavigationPage(new Calendar(_database)); 
                    break;
                case "TaskManager":
                    App.Current.MainPage = new NavigationPage(new TaskManager(_database));
                    break;
                case "Review":
                    App.Current.MainPage = new NavigationPage(new Review(_database));
                    break;
                case "PlantManager":
                    App.Current.MainPage = new NavigationPage(new PlantManager(_database));
                    break;
            }

            //if (_currentPage.GetType() == typeof(Calendar))
            //{
            //    var x = PageNav.Calendar.ToString();
            //    if (((ToolbarItem)sender).Text != PageNav.Calendar.ToString())
            //    {
            //       // _currentPage.Navigation.PushModalAsync(new Calendar(_database));
                        
            //           // .PushAsync(new AddTask(_database, Convert.ToInt32(((ViewCell)sender).ClassId)));
            //    }

            //}
           // throw new NotImplementedException();
        }
    }
}
