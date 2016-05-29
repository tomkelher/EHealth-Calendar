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
using Database;

namespace Calendar
{
    [Activity(Label = "Maand")]
    public class MonthActivity : Activity
    {
       // DataHolder dataholder = new DataHolder();
        DatabaseConnectionSync database = new DatabaseConnectionSync();
        static DateTime datum;
        static DataHolder data = new DataHolder();
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Maand);
           

            // Get our CalenderView from the layout resource,
            // and attach an event to it
            CalendarView calendar = FindViewById<CalendarView>(Resource.Id.calendarView1);
            ListView taskList = FindViewById<ListView>(Resource.Id.listView1);
            Button buttonAddTask = FindViewById<Button>(Resource.Id.buttonTitelTask);
            Button moveToToday = FindViewById<Button>(Resource.Id.buttonToday);

            
          //  
            buttonAddTask.Click += ButtonTaskOnClick;
            calendar.DateChange += CalendarOnDateChange;
            taskList.ItemClick += ListTaskOnClick;
            taskList.ItemLongClick += ListTaskOnLongClick;
            moveToToday.Click += ButtonTodayOnClick;
            ChangeDate();

        }
        private void ButtonTodayOnClick(object sender, System.EventArgs args)
        {
            ChangeDate();
        }

        /**
         * Als je calendar.Date gebruikt als getter zal deze alle miliseconden vanaf 1 januari 1970 om 00:00:00 weergeven
         * DateTime.Today.Ticks, deze geld 00:00:00.0000000 UTC, January 1, 0001 en telt elke 100ns --> /10 000 = ms
         * We nemen de ms van vandaag, en trekken deze van 1970 ervanaf -> de miliseconden van vandaag tot 1970 blijven over
         * en dit kun je dan naar setDate wegschrijven om de huidige dag weer te geven.
         * 2*3600*1000 uur, seconden, miliseconden. België = UTC +2.
         * Op de huidige datum zit een onnauwkeurigheid van 13 minuten, geen idee waarom
         */
        private void ChangeDate()
        {
            CalendarView calendar = FindViewById<CalendarView>(Resource.Id.calendarView1);
            ListView taskList = FindViewById<ListView>(Resource.Id.listView1);
            DateTime mindate = new DateTime(1970,1,1,0,0,0);
            long date = (DateTime.Today.Ticks/10000)-(mindate.Ticks/10000)+ 2*3600*1000;
            calendar.Date = date;
            DateTime setDate = DateTime.Now;
            try { datum = new DateTime(setDate.Year, setDate.Month, setDate.Day); }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            data.setDatum(datum);
            System.Diagnostics.Debug.WriteLine(datum);
            List<string> taken = database.getFromTable(datum);
            ArrayAdapter adapter = new ArrayAdapter<String>(this, Resource.Layout.TextViewItem, taken);
            taskList.Adapter = adapter;
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
            int uurBegin =0 , minutenBegin = 0;
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
                    database.addToTable(datum,titel.Text, plaats.Text, omschrijving.Text, beginuur, einduur);
                    ListView taskList = FindViewById<ListView>(Resource.Id.listView1);
                    List<string> taken = database.getFromTable(datum);
                    ArrayAdapter adapter = new ArrayAdapter<String>(this, Resource.Layout.TextViewItem, taken);
                    taskList.Adapter = adapter;

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

        private void CalendarOnDateChange(object sender, CalendarView.DateChangeEventArgs args)
        {
            
            ListView taskList = FindViewById<ListView>(Resource.Id.listView1);
            DateTime dataset = new DateTime(args.Year, args.Month+1, args.DayOfMonth);
            try { datum = dataset; }
            catch(Exception e)
            {
                System.Diagnostics.Debug.Write("the error is:" + e.ToString());
                System.Diagnostics.Debug.Write("the year: " + args.Year.ToString());
                System.Diagnostics.Debug.Write("the month: " + args.Month.ToString());
                System.Diagnostics.Debug.Write("the day: " + args.DayOfMonth.ToString());
            }
            data.setDatum(datum);
            List<string> taken = database.getFromTable(datum);
            ArrayAdapter adapter = new ArrayAdapter<String>(this, Resource.Layout.TextViewItem, taken);
            taskList.Adapter = adapter;
        }
        
        private void ListTaskOnClick(object sender, AdapterView.ItemClickEventArgs arg)
        {
            
            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View promptView = layoutInflater.Inflate(Resource.Layout.listShowAllLayout, null);
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
            alertDialogBuilder.SetView(promptView);
            alertDialogBuilder.SetTitle("Omschrijving van de taak");

            EditText toonTitel = (EditText)promptView.FindViewById<EditText>(Resource.Id.edittextShowTitel);
            EditText toonOmschrijving = (EditText)promptView.FindViewById<EditText>(Resource.Id.edittextShowOmschrijving);
            EditText toonPlaats = (EditText)promptView.FindViewById<EditText>(Resource.Id.edittextShowPlaats);
            TimePicker toonBegin = (TimePicker)promptView.FindViewById<TimePicker>(Resource.Id.timePickerShowBegin);
            TimePicker toonEinde = (TimePicker)promptView.FindViewById<TimePicker>(Resource.Id.timePickerShowEind);
            toonBegin.SetIs24HourView(Java.Lang.Boolean.True);
            toonEinde.SetIs24HourView(Java.Lang.Boolean.True);
    
            TimeSpan beginuren = database.getBeginuren(arg.Position);
            TimeSpan einduren = database.getEinduren(arg.Position);
            toonTitel.Text = database.getTitels(arg.Position);
            toonOmschrijving.Text = database.getOmschrijvingen(arg.Position);
            toonPlaats.Text = database.getPlaatsen(arg.Position);

            toonBegin.Enabled = false;
            toonEinde.Enabled = false;
            System.Diagnostics.Debug.Write(datum);
            toonBegin.CurrentHour = (Java.Lang.Integer)(beginuren.Hours);
            toonBegin.CurrentMinute = (Java.Lang.Integer)(beginuren.Minutes);

            toonEinde.CurrentHour = (Java.Lang.Integer)(einduren.Hours);
            toonEinde.CurrentMinute = (Java.Lang.Integer)(einduren.Minutes);


            alertDialogBuilder.SetPositiveButton("OK", (senderAlert, args) =>
            {
           
            });

            alertDialogBuilder.SetNegativeButton("Bewerk", (senderAlert, args) =>
            {

                EditTask(arg.Position, toonTitel.Text, toonOmschrijving.Text, toonPlaats.Text, beginuren, einduren);

            });

            AlertDialog alert = alertDialogBuilder.Create();
            alert.Show();

        }
        private void ListTaskOnLongClick(object sender, AdapterView.ItemLongClickEventArgs arg)
        {

            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View promptView = layoutInflater.Inflate(Resource.Layout.TaskMenu, null);
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
            alertDialogBuilder.SetView(promptView);
            alertDialogBuilder.SetTitle("Menu");
            int item = database.getId(arg.Position);
            Button verwijder = (Button)promptView.FindViewById<Button>(Resource.Id.buttonMenuDel);
            Button annuleer = (Button)promptView.FindViewById<Button>(Resource.Id.buttonMenuCancel);
            AlertDialog alert = alertDialogBuilder.Create();
            verwijder.Click += (object senderVerw, System.EventArgs verw) =>
            {
                database.DeleteFromTable(item);
                alert.Cancel();
                ListView taskList = FindViewById<ListView>(Resource.Id.listView1);
                List<string> taken = database.getFromTable(datum);
                ArrayAdapter adapter = new ArrayAdapter<String>(this, Resource.Layout.TextViewItem, taken);
                taskList.Adapter = adapter;
            };

            annuleer.Click += (object senderVerw, System.EventArgs verw) =>
            {
                alert.Cancel();
            };
            alert.Show();


        }
        private void EditTask(int index,String titel, String omschrijving, String plaats, TimeSpan beginUur, TimeSpan eindUur)
        {
            int item = database.getId(index);
            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View promptView = layoutInflater.Inflate(Resource.Layout.taakLayout, null);
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
            alertDialogBuilder.SetView(promptView);
            alertDialogBuilder.SetTitle("Wijzigen van de taak");

            EditText toonTitel = (EditText)promptView.FindViewById(Resource.Id.edittextTitel);
            EditText toonOmschrijving = (EditText)promptView.FindViewById(Resource.Id.edittextOmschrijving);
            EditText toonPlaats = (EditText)promptView.FindViewById(Resource.Id.edittextPlaats);

            TimePicker toonBegin = (TimePicker)promptView.FindViewById(Resource.Id.timePickerBegin);
            toonBegin.SetIs24HourView(Java.Lang.Boolean.True);
            TimePicker toonEinde = (TimePicker)promptView.FindViewById(Resource.Id.timePickerEind);
            toonEinde.SetIs24HourView(Java.Lang.Boolean.True);

            toonTitel.Text = titel;
            toonOmschrijving.Text = omschrijving;
            toonPlaats.Text = plaats;
         
            toonBegin.CurrentHour = (Java.Lang.Integer)(beginUur.Hours);
            toonBegin.CurrentMinute = (Java.Lang.Integer)(beginUur.Minutes);

            toonEinde.CurrentHour = (Java.Lang.Integer)(eindUur.Hours);
            toonEinde.CurrentMinute = (Java.Lang.Integer)(eindUur.Minutes);
              

            alertDialogBuilder.SetPositiveButton("Opslaan", (senderAlert, args) =>
            {
                toonBegin.ClearFocus();
                toonEinde.ClearFocus();
                TimeSpan beginuur = new TimeSpan(Convert.ToInt32(toonBegin.CurrentHour), Convert.ToInt32(toonBegin.CurrentMinute), 0);
                TimeSpan einduur = new TimeSpan(Convert.ToInt32(toonEinde.CurrentHour), Convert.ToInt32(toonEinde.CurrentMinute), 0);
                database.ChangeToTable(item, datum, toonTitel.Text, toonPlaats.Text, toonOmschrijving.Text, beginuur, einduur);
                ListView taskList = FindViewById<ListView>(Resource.Id.listView1);
                List<string> taken = database.getFromTable(datum);
                ArrayAdapter adapter = new ArrayAdapter<String>(this, Resource.Layout.TextViewItem, taken);
                taskList.Adapter = adapter;
            });

            alertDialogBuilder.SetNegativeButton("Annuleren", (senderAlert, args) =>
            {

            });

            AlertDialog alert = alertDialogBuilder.Create();
            alert.Show();
            toonTitel.TextChanged += (object sendere, Android.Text.TextChangedEventArgs tekst) =>
            {
                if (tekst.Text.ToString() == "") alert.GetButton((int)DialogButtonType.Positive).Enabled = false;
                else alert.GetButton((int)DialogButtonType.Positive).Enabled = true;
            };

        }
        public static DateTime getDatum()
        {
            return datum;
        }
        
    }
}

