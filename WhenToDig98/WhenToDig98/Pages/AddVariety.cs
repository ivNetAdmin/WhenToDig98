
using System;
using System.Collections.Generic;
using WhenToDig98.Data;
using WhenToDig98.Models;
using Xamarin.Forms;

namespace WhenToDig98.Pages
{
    public class AddVariety : ContentPage
    {
        private WTDDatabase _database;
        private Variety _variety;

        public AddVariety(WTDDatabase database, int varietyId = 0)
        {
            _database = database;

            if (varietyId == 0)
            {
                _variety = null;
            }
            else
            {
                _variety = _database.GetVariety(varietyId);
            }

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
           Grid grid = new Grid
            {
                VerticalOptions = LayoutOptions.Fill
            };
            
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            
            return grid;
        }
    }
}
