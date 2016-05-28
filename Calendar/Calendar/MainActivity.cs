using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Calendar
{

    [Activity(Label = "Agenda", MainLauncher = true, Icon = "@drawable/calendar2")]
    public class MainActivity : TabActivity
    {
        DataHolder dataholder = new DataHolder();
        DateTime datum;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
            // Create an Intent to launch an Activity for the tab (to be reused)  
            //var intent = new Intent(this, typeof(MyActivityGroup));
            var intent = new Intent(this, typeof(MonthActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var spec = TabHost.NewTabSpec("Maand");
            spec.SetIndicator("Maand", null);
            spec.SetContent(intent);
            TabHost.AddTab(spec);
            
            intent = new Intent(this, typeof(DagActivity));
       
            intent.AddFlags(ActivityFlags.ClearTop);
            spec = TabHost.NewTabSpec("Dag");
            spec.SetIndicator("Dag", null);
            spec.SetContent(intent);
            TabHost.AddTab(spec);
            TabHost.CurrentTab = 0;
        }
    }
}







