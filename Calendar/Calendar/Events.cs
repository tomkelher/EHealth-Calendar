using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Calendar
{
    class Events
    {
            public int id { get; set; }
            public string titel { get; set; }
            public string omschrijving { get; set; }
            public string locatie { get; set; }
            public TimeSpan beginuur { get; set; }
            public TimeSpan einduur { get; set; }

            public Events(int id, string titel, string omschrijving, string locatie, TimeSpan beginuur, TimeSpan einduur)
            {
                this.id = id;
                this.titel = titel;
                this.omschrijving = omschrijving;
                this.locatie = locatie;
                this.beginuur = beginuur;
                this.einduur = einduur;
            }
        }
    }
