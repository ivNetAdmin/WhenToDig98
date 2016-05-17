
using WhenToDig98.Data;
using WhenToDig98.Helpers;
using Xamarin.Forms;

namespace WhenToDig98.Pages
{
    public class PlantManager : ContentPage
    {
        private WTDDatabase _database;
        private IEnumerable<Plant> _plants;

        public PlantManager(WTDDatabase database)
        {
            _database = database;

            // _database.ResetPlants();

            Padding = new Thickness(10);

           // var grid = BuildForm();

            this.Content = new StackLayout
            {
                Spacing = 5,
                Padding = new Thickness(5, 10)
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PageToolBarItems.Build(_database, this);  
            UpdatePlants();
        }
        
         private void UpdatePlants()
         {
             // get plants
            _plants = _database.GetPlants();
            
            ((StackLayout)this.Content).Children.Clear();
            ((StackLayout)this.Content).Children.Add(BuildPlantTaskBar());
         }
         
         private Grid BuildPlantTaskBar()
         {
             var grid = new Grid
             {
                 VerticalOptions = LayoutOptions.Fill
             }
         }
    }
}
