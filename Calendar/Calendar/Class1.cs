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


namespace Calendar
{
    
    class DataHolder
    {
        static DateTime datum = new DateTime();
        public DataHolder()
        {
    
        }

        public void setDatum(DateTime datum)
        {
            DataHolder.datum = datum;
        }

        public DateTime getDatum()
        {
            return datum;
        }

    }
}