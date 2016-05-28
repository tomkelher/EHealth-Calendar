using System.Text;
using System;
using System.Collections.Generic;
/*
 * Code no longer in use, functionality has been replaced to SQLliteConn
 * */
namespace Calendar
{
    public class CalendarFunctions
    {
        List<string> takenKort = new List<string>();
        //List<string> takenLang = new List<string>();
        List<string> titels = new List<string>();
        List<string> omschrijvingen = new List<string>();
        List<string> plaatsen = new List<string>();
        List<TimeSpan> beginuren = new List<TimeSpan>();
        List<TimeSpan> einduren = new List<TimeSpan>();


        public CalendarFunctions()
        {
            takenKort.Clear();
            titels.Clear();
            omschrijvingen.Clear();
            plaatsen.Clear();
            beginuren.Clear();
            einduren.Clear();
        }

        public void LoadTaak(String titel, String omschrijving, String plaats, TimeSpan begin, TimeSpan einde)
        {
            takenKort.Add(begin.ToString() + " - " + einde.ToString() + ": " + titel);
            titels.Add(titel);
            omschrijvingen.Add(omschrijving);
            plaatsen.Add(plaats);
            beginuren.Add(begin);
            einduren.Add(einde);
            // takenLang.Add(begin.ToString() + " - " + einde.ToString() + ": " + titel + ", " + omschrijving + " te " + plaats);
        }
        public void adjustTask(DateTime date, int index, string titel, string omschrijving, string plaats, TimeSpan begin, TimeSpan einde)
        {
            titels[index] = titel;
        
            omschrijvingen[index] = omschrijving;
       
            plaatsen[index] = plaats;
       
            beginuren[index] = begin;
        
            einduren[index] = einde;
            takenKort[index] = (begin.ToString() + " - " + einde.ToString() + ": " + titel);
        }
        public void removeTaak(DateTime date, int taakID)
        {
            takenKort.RemoveAt(taakID);
            titels.RemoveAt(taakID);
            omschrijvingen.RemoveAt(taakID);
            plaatsen.RemoveAt(taakID);
            beginuren.RemoveAt(taakID);
            einduren.RemoveAt(taakID);

        }
        public List<string> getTakenKort()
        {
            return takenKort;
        }
        /* public String getTakenLang(DateTime date, int index)
         {
             return takenLang[index];
         }*/

        public String getTitels(DateTime date, int index)
        {
            return titels[index];
        }
        public String getOmschrijvingen(DateTime date, int index)
        {
            return omschrijvingen[index];
        }
        public String getPlaatsen(DateTime date, int index)
        {
            return plaatsen[index];
        }
        public TimeSpan getBeginuren(DateTime date, int index)
        {
   
                return beginuren[index];
          
        }
        public TimeSpan getEinduren(DateTime date, int index)
        {
            return einduren[index];
        }




        public void setTitels(DateTime date, int index,  string tekst)
        {
             titels[index] = tekst;
        }
        public void setOmschrijvingen(DateTime date, int index, string tekst)
        {
             omschrijvingen[index] = tekst;
        }
        public void setPlaatsen(DateTime date, int index, string tekst)
        {
             plaatsen[index] = tekst;
        }
        public void setBeginuren(DateTime date, int index, TimeSpan uur)
        {
             beginuren[index]= uur;
        }
        public void setEinduren(DateTime date, int index, TimeSpan uur)
        {
             einduren[index] = uur;
        }

    }
}