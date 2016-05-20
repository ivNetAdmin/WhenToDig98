using System;
using System.Collections.Generic;
using WhenToDig98.Data;
using WhenToDig98.Helpers;
using WhenToDig98.Models;
using Xamarin.Forms;

namespace WhenToDig98.Pages
{
    public class Calendar : ContentPage            
    {
        private string[] _taskImages;
        private string[] _weekDays;
        private DateTime _currentCallendarDate;
        private WTDDatabase _database;
        private IEnumerable<Task> _tasks;

        public Calendar(WTDDatabase database)
        {
            _database = database;
            _taskImages = new[] { "x.png", "a.png", "b.png", "c.png", "d.png", "e.png", "f.png", "g.png" };
            _weekDays = new[] { "Mo", "Tu", "We", "Th", "Fr", "Sa", "Su" };
            _currentCallendarDate = DateTime.Now;           

             _database.ResetDb();

            Padding = new Thickness(10);            

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
            UpdateCalendar();
        }

        private void UpdateCalendar()
        {
            // get tasks
            _tasks = _database.GetTasksByMonth(_currentCallendarDate);

            ((StackLayout)this.Content).Children.Clear();
            ((StackLayout)this.Content).Children.Add(BuildCalendarNavigationBar());
            ((StackLayout)this.Content).Children.Add(BuildCalendarHeaderBar());
            ((StackLayout)this.Content).Children.Add(BuildCalendar());
            ((StackLayout)this.Content).Children.Add(BuildCalendarTaskBar());
            ((StackLayout)this.Content).Children.Add(BuildTaskList());
        }

        private Grid BuildCalendarNavigationBar()
        {

            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.Fill
            };
            // Height = GridLength.Auto
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // build calendar
            var month = _currentCallendarDate.ToString("MMM yyyy");

            grid.Children.Add(new Button
            {
                // FontSize=12,
                // HeightRequest = 400,
                Text = "<<",
            }, 0, 0);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += CalendarNavOnButtonClicked;

            grid.Children.Add(new Button
            {
                // HeightRequest = 400,
                Text = "<"
            }, 1, 0);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += CalendarNavOnButtonClicked;

            grid.Children.Add(new Label
            {
                Text = month,
                TextColor = Color.Silver,
                BackgroundColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HeightRequest = 40
            }, 2, 0);

            Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);

            grid.Children.Add(new Button
            {
                // HeightRequest = 400,
                Text = ">"
            }, 5, 0);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += CalendarNavOnButtonClicked;

            grid.Children.Add(new Button
            {
                // HeightRequest = 400,
                Text = ">>"
            },6, 0);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += CalendarNavOnButtonClicked;

            return grid;
        }

        private Grid BuildCalendarHeaderBar()
        {
            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.Fill
            };

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var month = _currentCallendarDate.ToString("MMM yyyy");
            for (var wd = 0; wd < _weekDays.Length; wd++)
            {
                grid.Children.Add(new Label
                {
                    Text = _weekDays[wd],
                    TextColor = Color.Aqua,
                    BackgroundColor = Color.Black,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                  // VerticalOptions= LayoutOptions.Fill
                }, wd, 0);
            }

            return grid;
        }

        private Grid BuildCalendar()
        {
            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.Fill
            };

            // build calendar
            var month = _currentCallendarDate.ToString("MMM yyyy");

            var fill = new int[] { 6, 0, 1, 2, 3, 4, 5 };

            var firstDayofMonth = new DateTime(_currentCallendarDate.Year, _currentCallendarDate.Month, 1).DayOfWeek;
            var calendarStartDate = new DateTime(_currentCallendarDate.Year, _currentCallendarDate.Month, 1)
                .AddDays(-1 * (fill[(int)firstDayofMonth]));

            var days = DateTime.DaysInMonth(_currentCallendarDate.Year, _currentCallendarDate.Month);

            var rowCount = days + fill[(int)firstDayofMonth] > 35 ? 6 : 5;

            var dates = new List<DateTime>();
            for (var d = 0; d < rowCount * 7; d++)
            {
                dates.Add(calendarStartDate.AddDays(d));
            }

            for (var r = 0; r < rowCount; r++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                BuildCalendarCells(grid, _weekDays, dates, r);
            }

            return grid;
        }

        private Grid BuildCalendarTaskBar()
        {
            Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.Fill
            };

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var text = "View";
            var year = _currentCallendarDate.AddYears(-1).Year;

            if (_currentCallendarDate.Year < DateTime.Now.Year)
            {
                text = "Add to";
                year = DateTime.Now.Year;
            }

            var tasksButtonLabel = string.Format("{0} {1}", text, year);

            grid.Children.Add(new Button
            {
                // HeightRequest = 400,
                Text = tasksButtonLabel,
            }, 0, 0);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += PreviousYearTasksOnButtonClicked;
           // Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);

            grid.Children.Add(new Button
            {
                VerticalOptions = LayoutOptions.Fill,
                // HeightRequest = 400,
                Text = "New Task",
            }, 1, 0);
            ((Button)grid.Children[grid.Children.Count - 1]).Clicked += NewTaskOnButtonClicked;
           // Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);

            return grid;
        }

        private ListView BuildTaskList()
        {           
            var listView =  new ListView
            {
                RowHeight=40,
                ItemsSource = _tasks,
                ItemTemplate = new DataTemplate(() =>
                {                   
                    // var menuItem = new MenuItem
                    var date = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center
                        // BackgroundColor = Color.Yellow
                    };
                    date.SetBinding(Label.TextProperty, "Day");

                    var description = new Label
                    {
                        HorizontalTextAlignment = TextAlignment.Start,
                        VerticalTextAlignment = TextAlignment.Center,
                        //BackgroundColor = Color.Red
                    };
                    description.SetBinding(Label.TextProperty, "Description");

                    var typeImage = new Image
                    {
                        //BackgroundColor = Color.Blue
                    };
                    typeImage.SetBinding(Image.SourceProperty, "TaskTypeImage");

                    Grid grid = new Grid
                    {
                        VerticalOptions = LayoutOptions.Fill
                    };

                    grid.RowDefinitions.Add(new RowDefinition
                    {
                        Height = GridLength.Auto
                    });

                    var deleteButton = new Button
                    {
                        Text = "X",
                        TextColor = Color.Red
                    };
                    deleteButton.SetBinding(Button.ClassIdProperty, "ID");

                    // grid.Children.Add(id,-1,0);
                    grid.Children.Add(date, 0, 0);
                    Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 1);
                     grid.Children.Add(typeImage, 1, 0);
                    Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 1);
                    grid.Children.Add(description, 2, 0);
                    Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 3);
                    grid.Children.Add(deleteButton, 5, 0);
                    Grid.SetColumnSpan(grid.Children[grid.Children.Count - 1], 1);
                    
                    ((Button)grid.Children[grid.Children.Count - 1]).Clicked += DeleteTaskButtonClicked;

                    var viewCell = new ViewCell
                    {
                        View = grid
                    };

                    viewCell.SetBinding(ViewCell.ClassIdProperty, "ID");

                    viewCell.Tapped += TaskRowTapped;

                    return viewCell;
                })
            };
          
            return listView;
        }

        private void BuildCalendarCells(
            Grid grid, string[] weekDays, 
            List<DateTime> dates, int r)
        {
            bool lowlight = r == 0 ? true : false;            
            for (var wd = 0; wd < weekDays.Length; wd++)
            {                               
                var dateIndex = (r) * weekDays.Length + wd;
                var dateStr = dateIndex < dates.Count ? Convert.ToString(dates[dateIndex].Day.ToString("D2")) : string.Empty;
                var today = ((DateTime)dates[dateIndex]).ToString("ddMMyyyy") == DateTime.Now.ToString("ddMMyyyy");
                var taskImage = GetTaskImage(dates[dateIndex]);

                if (dateStr == "01")
                {
                    if (lowlight == true)
                    {
                        lowlight = false;
                    }
                    else
                    {
                        lowlight = true;
                    }
                }

                var relativeLayout = new RelativeLayout
                {
                    BackgroundColor = Color.Black
                };

                var backgroundImage = new Image()
                {
                    Source = taskImage,
                  //  Aspect = Aspect.AspectFill,
                    IsOpaque = true,
                    Opacity = 1.0,
                };

                var label = new Label
                {
                    Text = dateStr,
                    TextColor = lowlight == true ? Color.FromRgb(51, 51, 51) : today ? Color.Aqua : Color.Silver,
                    BackgroundColor = Color.Black//,
                   // HorizontalTextAlignment = TextAlignment.Center,
                   // VerticalTextAlignment = TextAlignment.Center
                };

                label.GestureRecognizers.Add(new TapGestureRecognizer
                {
                    Command = new Command(() => OnLabelClicked(dates[dateIndex])),
                });

                relativeLayout.Children.Add(
                    backgroundImage,
                    Constraint.Constant(0),
                Constraint.Constant(0)
                );
                relativeLayout.Children.Add(

                label, Constraint.RelativeToParent((parent) => {
                    return parent.Width / 3;
                }),
                     Constraint.Constant(0));

                grid.Children.Add(relativeLayout, wd, r);
               
            }
        }

        private FileImageSource GetTaskImage(DateTime date)
        {
            var imageId = 0;
            foreach (var task in _tasks)
            {
                if (task.Date.ToString("ddMMyyyy") == date.ToString("ddMMyyyy"))
                {
                    imageId = imageId + task.Type;
                }
            }

            //var rnd = new Random();
         
            return new FileImageSource() { File = _taskImages[imageId] };
        }       

        private void AddTasks(DateTime _currentCallendarDate)
        {
            
            var tasks = _database.GetTasksByMonth(_currentCallendarDate);

            foreach(var task in tasks)
            {
                AdjustDate(task);
                _database.AddTask(0, task.Description, task.Notes, task.Type, task.Date, task.Plant);
            }
        }

        private void AdjustDate(Task task)
        {
            //L->N -1 N->L -2 N->N -1 N->N 0

            if (task.Date.AddYears(1).Year % 4 == 0)
            {
                // leap year to leap year
                if (task.Date.Year % 4 == 0)
                {
                    task.Date = new DateTime(DateTime.Now.Year, task.Date.Month, task.Date.Day);
                }
                else
                {
                    task.Date = new DateTime(DateTime.Now.Year, task.Date.Month, task.Date.AddDays(-2).Day);
                }
            }
            else
            {
                task.Date = new DateTime(DateTime.Now.Year, task.Date.Month, task.Date.AddDays(-1).Day);
            }
        }

        async void PreviousYearTasksOnButtonClicked(object sender, EventArgs e)
        {
            var txt = ((Button)sender).Text;
            if (txt.Contains("View"))
            {
                _currentCallendarDate = _currentCallendarDate.AddYears(-1);
            }
            else
            {
                if (await DisplayAlert("Copy Tasks", "Would you like to add all the tasks from this month to the same month this year?", "Yes", "No"))
                {
                    AddTasks(_currentCallendarDate);
                    _currentCallendarDate = DateTime.Now;
                }
            }
            UpdateCalendar();

        }

        async void DeleteTaskButtonClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Delete Task", "Are you sure you want to delete this task?", "Yes", "No"))
                {
                var id = Convert.ToInt32(((Button)sender).ClassId);
                    _database.DeleteTask(id);
                    UpdateCalendar();
                }
        }
         
        private void CalendarNavOnButtonClicked(object sender, EventArgs e)
        {
            switch (((Button)sender).Text)
            {
                case "<<":
                    _currentCallendarDate = _currentCallendarDate.AddYears(-1);
                    break;
                case ">>":
                    _currentCallendarDate = _currentCallendarDate.AddYears(1);
                    break;
                case "<":
                    _currentCallendarDate = _currentCallendarDate.AddMonths(-1);
                    break;
                case ">":
                    _currentCallendarDate = _currentCallendarDate.AddMonths(1);
                    break;

            }

            UpdateCalendar();

        }

        private void NewTaskOnButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTask(_database));
        }

        private void TaskRowTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTask(_database, Convert.ToInt32(((ViewCell)sender).ClassId)));
        }
        
        private void OnLabelClicked(DateTime date)
        {
            var dateStr = date.ToString("dd-MMM-yyyy");
            var cakes = dateStr;
        }

    }
}
