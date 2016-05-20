
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WhenToDig98.Helpers
{
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col)
        {
            return new ObservableCollection<T>(col);

        }
    }
}
