
using System;
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
                    File = _currentPage.GetType().ToString().IndexOf("Calendar") != -1 ? "Calendar.png" : "CalendarLow.png"
                }
                                          
            });
            _currentPage.ToolbarItems[_currentPage.ToolbarItems.Count - 1].Clicked += MenuItemActivated;
            _currentPage.ToolbarItems.Add(new ToolbarItem
            {
                Text = "TaskManager",
                Icon = new FileImageSource
                {
                    File = _currentPage.GetType().ToString().IndexOf("TaskManager") != -1 ? "Task.png" : "TaskLow.png"
                }
            });
            _currentPage.ToolbarItems[_currentPage.ToolbarItems.Count - 1].Clicked += MenuItemActivated;
            _currentPage.ToolbarItems.Add(new ToolbarItem
            {
                Text = "Review",
                Icon = new FileImageSource
                {
                    File = _currentPage.GetType().ToString().IndexOf("Review") != -1 ? "Review.png" : "ReviewLow.png"
                }
            });
            _currentPage.ToolbarItems[_currentPage.ToolbarItems.Count - 1].Clicked += MenuItemActivated;
            _currentPage.ToolbarItems.Add(new ToolbarItem
            {
                Text = "PlantManager",
                Icon = new FileImageSource
                {
                    File = _currentPage.GetType().ToString().IndexOf("PlantManager") != -1 ? "Plant.png" : "PlantLow.png"
                }
            });
            _currentPage.ToolbarItems[_currentPage.ToolbarItems.Count - 1].Clicked += MenuItemActivated;
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
