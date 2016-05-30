
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
        List<string> taken = new List<string>();
        private TextView myTextView;
        private List<string> myItems;
        private DateTime datum;
        Calendar.CalendarFunctions calendar = new Calendar.CalendarFunctions();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            datum = data.getDatum();
            /* if (datum.Year < 0010)
             {
                 datum = DateTime.Now;
             }*/


            SetContentView(Resource.Layout.Dag);
            myListView = FindViewById<ListView>(Resource.Id.myListView);
            myTextView = FindViewById<TextView>(Resource.Id.myTextView);
            myTextView.Text = DateToString(datum);
            Button buttonAddTask = FindViewById<Button>(Resource.Id.buttonTitelTask);
            Button buttonPreviousDay = FindViewById<Button>(Resource.Id.buttonPreviousDay);
            Button buttonNextDay = FindViewById<Button>(Resource.Id.buttonNextDay);
            Button moveToToday = FindViewById<Button>(Resource.Id.buttonToday);

            buttonAddTask.Click += ButtonTaskOnClick;

            buttonPreviousDay.Click += ButtonPreviousOnClick;
            buttonNextDay.Click += ButtonNextOnClick;
            moveToToday.Click += ButtonTodayOnClick;

            myListView.ItemClick += ListTaskOnClick;
            myListView.ItemLongClick += ListTaskOnLongClick;




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
            taken = database.getFromTable(datum);
            foreach (string item in taken)
            {
                int index = Convert.ToInt32(database.getBeginuren(i).Hours);
                myItems[index] = item;
                i++;
            }         

            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, myItems);
            myListView.Adapter = adapter;

        }

        private void ButtonTodayOnClick(object sender, System.EventArgs args)
        {
            ChangeDate();
        }
                
        private void ChangeDate()
        {
            DateTime mindate = new DateTime(1970, 1, 1, 0, 0, 0);
            long date = (DateTime.Today.Ticks / 10000) - (mindate.Ticks / 10000) + 2 * 3600 * 1000;            
            DateTime setDate = DateTime.Now;
            try { datum = new DateTime(setDate.Year, setDate.Month, setDate.Day); }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }            
            System.Diagnostics.Debug.WriteLine(datum);
            myTextView.Text = DateToString(datum);
            int i = 0;
            taken = database.getFromTable(datum);
            foreach (string item in taken)
            {
                int index = Convert.ToInt32(database.getBeginuren(i).Hours);
                myItems[index] =  item;
                i++;
            }
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, myItems);
            myListView.Adapter = adapter;
            data.setDatum(datum);
        }


        private void ButtonNextOnClick(object sender, System.EventArgs e)
        {
                datum=datum.AddDays(1);
                myTextView.Text = DateToString(datum);
                int i = 0;
                myItems = ResetAndFillUpList(myItems);
                taken = database.getFromTable(datum);
                foreach (string item in taken)
                {
                    int index = Convert.ToInt32(database.getBeginuren(i).Hours);
                    myItems[index] = item;
                    i++;
                }
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, myItems);
            myListView.Adapter = adapter;
            data.setDatum(datum);

        }
        private void ButtonPreviousOnClick(object sender, System.EventArgs e)
        {
            datum = datum.AddDays(-1);
            myTextView.Text = DateToString(datum);
            int i = 0;
            myItems = ResetAndFillUpList(myItems);
            taken = database.getFromTable(datum);
            foreach (string item in taken)
            {
                int index = Convert.ToInt32(database.getBeginuren(i).Hours);
                myItems[index] = item;
                i++;
            }
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, myItems);
            myListView.Adapter = adapter;
            data.setDatum(datum);

        }
        private string DateToString(DateTime datum)
        {
            string maand="";
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
            string returnString = datum.Day.ToString() + " " + maand + " " + datum.Year.ToString();
            return returnString;


        }

        public List<string> ResetAndFillUpList(List<string> lijst)
        {
            lijst.Clear();
            lijst.Add("00:00");
            lijst.Add("01:00");
            lijst.Add("02:00");
            lijst.Add("03:00");
            lijst.Add("04:00");
            lijst.Add("05:00");
            lijst.Add("06:00");
            lijst.Add("07:00");
            lijst.Add("08:00");
            lijst.Add("09:00");
            lijst.Add("10:00");
            lijst.Add("11:00");
            lijst.Add("12:00");
            lijst.Add("13:00");
            lijst.Add("14:00");
            lijst.Add("15:00");
            lijst.Add("16:00");
            lijst.Add("17:00");
            lijst.Add("18:00");
            lijst.Add("19:00");
            lijst.Add("20:00");
            lijst.Add("21:00");
            lijst.Add("22:00");
            lijst.Add("23:00");
            lijst.Add("00:00");
            return lijst;


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

                /* string taak = " " + titel.Text + " " + omschrijving.Text + " " + plaats.Text + " " + "van: " + beginuur + " " + "tot: " + einduur;
                 myItems[uurBegin] = myItems[uurBegin] + taak;*/
                database.addToTable(datum, titel.Text, plaats.Text, omschrijving.Text, beginuur, einduur);
                myItems = ResetAndFillUpList(myItems);
                taken = database.getFromTable(datum);
                int i = 0;
                foreach (string item in taken)
                {
                    int index = Convert.ToInt32(database.getBeginuren(i).Hours);
                    myItems[index] = item;
                    i++;
                }
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
            String zoeken = myItems[arg.Position];
            int positie=0;
            foreach (string item in taken)
            {
                if (item == zoeken) break;
                positie++;

            }
            TimeSpan beginuren = database.getBeginuren(positie);
            TimeSpan einduren = database.getEinduren(positie);
            toonTitel.Text = database.getTitels(positie);
            toonOmschrijving.Text = database.getOmschrijvingen(positie);
            toonPlaats.Text = database.getPlaatsen(positie);

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

                EditTask(positie, toonTitel.Text, toonOmschrijving.Text, toonPlaats.Text, beginuren, einduren);

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
            int positie = 0;
            String zoeken = myItems[arg.Position];
            foreach (string test in taken)
            {
                if (test == zoeken) break;
                positie++;

            }
            int item = database.getId(positie);
            Button verwijder = (Button)promptView.FindViewById<Button>(Resource.Id.buttonMenuDel);
            Button annuleer = (Button)promptView.FindViewById<Button>(Resource.Id.buttonMenuCancel);
            AlertDialog alert = alertDialogBuilder.Create();
            verwijder.Click += (object senderVerw, System.EventArgs verw) =>
            {
                database.DeleteFromTable(item);
                alert.Cancel();
                myItems = ResetAndFillUpList(myItems);
                taken = database.getFromTable(datum);
                int i = 0;
                foreach (string test in taken)
                {
                    int indextest = Convert.ToInt32(database.getBeginuren(i).Hours);
                    myItems[indextest] = test;
                    i++;
                }
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, myItems);
                myListView.Adapter = adapter;
            };

            annuleer.Click += (object senderVerw, System.EventArgs verw) =>
            {
                alert.Cancel();
            };
            alert.Show();


        }
        private void EditTask(int index, String titel, String omschrijving, String plaats, TimeSpan beginUur, TimeSpan eindUur)
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
                myItems = ResetAndFillUpList(myItems);
                taken = database.getFromTable(datum);
                int i = 0;
                foreach (string test in taken)
                {
                    int indextest = Convert.ToInt32(database.getBeginuren(i).Hours);
                    myItems[indextest] = test;
                    i++;
                }
                ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, myItems);
                myListView.Adapter = adapter;


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
    }
}