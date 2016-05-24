
using WhenToDig98.Data;
using WhenToDig98.Helpers;
using Xamarin.Forms;

namespace WhenToDig98.Pages
{
    public class TaskManager : ContentPage
    {
        private WTDDatabase _database;
        private IEnumerable<Plant> _plants;
        private IEnumerable<Task> _tasks;

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

           // _database.ResetDb();
        }
    }
}
