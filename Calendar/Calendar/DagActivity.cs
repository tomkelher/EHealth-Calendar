
using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using System.Diagnostics;
using Android.Text.Format;
using System.Linq;
using Database;

namespace Calendar
{

    [Activity(Label = "Dag")]
    public class DagActivity : Activity
    {
        
        DatabaseConnectionSync database = new DatabaseConnectionSync();
        static DataHolder data = new DataHolder();
        private ListView myListView;
        private DateTime geselecteerdeDatum;
        private TextView myTextView;
        private string maand;
        private List<string> myItems;       
        private DateTime datum;
        Calendar.CalendarFunctions calendar = new Calendar.CalendarFunctions();
    
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            datum = data.getDatum();
            if (datum.Year < 0010)
            {
                datum = DateTime.Now;
            }

            switch (datum.Month)
            {
                case 1:
                    maand = "Januari";
                    break;
                case 2:
                    maand = "Februari";
                    break;
                case 3:
                    maand = "Maart";
                    break;
                case 4:
                    maand = "April";
                    break;
                case 5:
                    maand = "Mei";
                    break;
                case 6:
                    maand = "Juni";
                    break;
                case 7:
                    maand = "Juli";
                    break;
                case 8:
                    maand = "Augustus";
                    break;
                case 9:
                    maand = "September";
                    break;
                case 10:
                    maand = "Oktober";
                    break;
                case 11:
                    maand = "November";
                    break;
                case 12:
                    maand = "December";
                    break;
            }
            SetContentView(Resource.Layout.Dag);
            myListView = FindViewById<ListView>(Resource.Id.myListView);
            myTextView = FindViewById<TextView>(Resource.Id.myTextView);
            myTextView.Text = datum.Day.ToString() +" "+ maand +" "+ datum.Year.ToString();
            Button buttonAddTask = FindViewById<Button>(Resource.Id.buttonTitelTask);

            buttonAddTask.Click += ButtonTaskOnClick;


            myItems = new List<string>();
            myItems.Add("00:00");
            myItems.Add("01:00");
            myItems.Add("02:00");
            myItems.Add("03:00");
            myItems.Add("04:00");
            myItems.Add("05:00");
            myItems.Add("06:00");
            myItems.Add("07:00");
            myItems.Add("08:00");
            myItems.Add("09:00");
            myItems.Add("10:00");
            myItems.Add("11:00");
            myItems.Add("12:00");
            myItems.Add("13:00");
            myItems.Add("14:00");
            myItems.Add("15:00");
            myItems.Add("16:00");
            myItems.Add("17:00");
            myItems.Add("18:00");
            myItems.Add("19:00");
            myItems.Add("20:00");
            myItems.Add("21:00");
            myItems.Add("22:00");
            myItems.Add("23:00");
            myItems.Add("00:00");           
            

            // inladen van taken die reeds in de lijst stonden
            int i = 0;
        /*    foreach (string item in calendar.takenKort)
            {
                string taak = " " + calendar.getTitels(geselecteerdeDatum, i) + " " + calendar.getOmschrijvingen(geselecteerdeDatum, i) + " " + calendar.getPlaatsen(geselecteerdeDatum, i) + " " + "van: " + calendar.getBeginuren(geselecteerdeDatum, i) + " " + "tot: " + calendar.getEinduren(geselecteerdeDatum, i);
                myItems[Convert.ToInt32(calendar.getBeginuren(geselecteerdeDatum, i))] = myItems[Convert.ToInt32(calendar.getBeginuren(geselecteerdeDatum, i))] + taak;
                i++;
            }
            */
            // lijst myItems doorsturen naar adapter van listview
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, myItems);
            myListView.Adapter = adapter;

            

        }

        private void ButtonTaskOnClick(object sender, System.EventArgs e)
        {
            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View promptView = layoutInflater.Inflate(Resource.Layout.taakLayout, null);
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
            alertDialogBuilder.SetView(promptView);

            EditText titel = (EditText)promptView.FindViewById(Resource.Id.edittextTitel);
            EditText omschrijving = (EditText)promptView.FindViewById(Resource.Id.edittextOmschrijving);
            EditText plaats = (EditText)promptView.FindViewById(Resource.Id.edittextPlaats);

            TimePicker begin = (TimePicker)promptView.FindViewById(Resource.Id.timePickerBegin);
            begin.SetIs24HourView(Java.Lang.Boolean.True);
            TimePicker einde = (TimePicker)promptView.FindViewById(Resource.Id.timePickerEind);
            einde.SetIs24HourView(Java.Lang.Boolean.True);
            int uurBegin = 0, minutenBegin = 0;
            int uurEinde, minutenEinde = 0;
            TimeSpan beginuur, einduur;
            alertDialogBuilder.SetTitle("Taak toevoegen");
            alertDialogBuilder.SetPositiveButton("OK", (senderAlert, args) =>
            {
                begin.ClearFocus();
                einde.ClearFocus();
                uurBegin = Convert.ToInt32(begin.CurrentHour);
                minutenBegin = Convert.ToInt32(begin.CurrentMinute);
                uurEinde = Convert.ToInt32(einde.CurrentHour);
                minutenEinde = Convert.ToInt32(einde.CurrentMinute);
                beginuur = new TimeSpan(uurBegin, minutenBegin, 0);
                einduur = new TimeSpan(uurEinde, minutenEinde, 0);
               // calendar.addTaak(titel.Text, omschrijving.Text, plaats.Text, beginuur, einduur);
                
                                     
                string taak = " " + titel.Text + " " + omschrijving.Text + " " + plaats.Text + " " + "van: " + beginuur + " " + "tot: " + einduur;
                myItems[uurBegin] = myItems[uurBegin] + taak;
                    
                
                               

                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, myItems);
                myListView.Adapter = adapter;


            });
            alertDialogBuilder.SetNegativeButton("Annuleer", (senderAlert, args) =>
            {

            });
            AlertDialog alert = alertDialogBuilder.Create();


            alert.Show();



            alert.GetButton((int)DialogButtonType.Positive).Enabled = false;
            titel.TextChanged += (object sendere, Android.Text.TextChangedEventArgs tekst) =>
            {
                if (tekst.Text.ToString() == "") alert.GetButton((int)DialogButtonType.Positive).Enabled = false;
                else alert.GetButton((int)DialogButtonType.Positive).Enabled = true;
            };


        }
    }
}