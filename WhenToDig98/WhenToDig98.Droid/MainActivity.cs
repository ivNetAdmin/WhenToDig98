using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace WhenToDig98.Droid
{
    [Activity(Label = "WhenToDig98", Theme="@android:style/Theme.Holo", 
ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
public class MainActivity : AndroidActivity
{
    protected override void OnCreate(Bundle bundle)
    {
        base.OnCreate(bundle);

        Xamarin.Forms.Forms.Init(this, bundle);
        SetPage(App.GetMainPage());
    }
}
