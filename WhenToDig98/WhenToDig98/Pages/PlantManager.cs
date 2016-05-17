
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

           // var grid = BuildForm();

            this.Content = new StackLayout
            {
                Padding = new Thickness(5, 10),
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                   // grid
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PageToolBarItems.Build(_database, this);           
        }
    }
}
