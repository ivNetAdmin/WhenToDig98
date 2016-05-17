using WhenToDig98.Data;
using WhenToDig98.Pages;
using Xamarin.Forms;

namespace WhenToDig98
{
    public class App : Application
    { 
        public App()
        {
            MainPage = GetMainPage();            
        }

        public static Page GetMainPage() {
            var database = new WTDDatabase();
            return new NavigationPage(new Calendar(database)); }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
