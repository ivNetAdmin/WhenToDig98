
using WhenToDig98.Data;
using WhenToDig98.Helpers;
using Xamarin.Forms;

namespace WhenToDig98.Pages
{
    public class Review : ContentPage
    {
        private WTDDatabase _database;

        public Review(WTDDatabase database)
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
        }

    }
}
