using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Calendar
{
    [Activity(Label = "Calendar", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;
        Calendar.CalendarFunctions calendar = new Calendar.CalendarFunctions();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            CalendarView calendar = FindViewById<CalendarView>(Resource.Id.calendarView1);
            
            calendar.DateChange += CalendarOnDateChange;
            //    button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }

        private void CalendarOnDateChange(object sender, CalendarView.DateChangeEventArgs args)
        {
            DateTime datum = new DateTime(args.Year, args.Month, args.DayOfMonth);
            string[] taken =  calendar.getTaken(datum);
            ArrayAdapter adapter = new ArrayAdapter<String>(this, Resource.Layout.Main, taken);
            ListView taskList = FindViewById<ListView>(Resource.Id.listView1);
            taskList.Adapter = adapter;
           

        }   
    }
}

