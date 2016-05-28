using Android.App;
using Android.Content;
using Android.OS;


namespace Calendar
{

    [Activity(Label = "Week")]
    public class WeekActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

           SetContentView(Resource.Layout.Week);
        }
    }
}