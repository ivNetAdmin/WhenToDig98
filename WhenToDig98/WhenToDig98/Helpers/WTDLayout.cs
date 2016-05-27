
using Xamarin.Forms;

namespace WhenToDig98.Helpers
{
    public class WTDLayout : StackLayout
    {
        public WTDLayout()
        {
            TopStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Spacing = 5,
                Padding = new Thickness(5, 10)
            };

            BottomStack = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.End,
                Spacing = 5,
                Padding = new Thickness(5, 10)
            };

            Children.Add(TopStack);
            Children.Add(BottomStack);
        }

        public StackLayout TopStack { get; private set; }
        public StackLayout BottomStack { get; private set; }
    }
}
