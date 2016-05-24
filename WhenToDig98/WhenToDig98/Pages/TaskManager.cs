
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
        private Grid _taskInformation;
        
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

            ((StackLayout)this.Content).Children.Add(BuildSearchForm());
            
           // _database.ResetDb();
        }
        
        private Grid BuildSearchForm()
        {
           return new TableView {
                Root = new TableRoot {
                    new TableSection{
                        new EntryCell{
                            Text = "Season"
                        }, new EntryCell{
                            Text = ""
                        }
                    },
                    new TableSection{
                         new EntryCell{
                            Text = "Type"
                        }
                    }
                },
                Intent = TableIntent.Settings
            };
           
        }
    }
}
