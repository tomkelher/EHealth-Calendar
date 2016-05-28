using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;
using SQLite.Net;

public class DBConnection
{
    SQLiteConnection sqlconn;
    string connsqlstring;
    
    public DBConnection()
    {
        try
        {
            connsqlstring = "db_agenda_app";
            // 
            SQLiteConnection sqlconn = new SQLiteConnection(connsqlstring);
            
            List<Calendar.Events> events = new List<Calendar.Events>();
        }
        catch (Exception e){ System.Diagnostics.Debug.Write("error catched: " + e.ToString()); }
    }



    public void addEvent(string titel, string omschrijving, string locatie, TimeSpan van, TimeSpan tot)
    {

        try
        {
            System.Diagnostics.Debug.Write("SQLconnenction = "+sqlconn.ToString());
            string sql = "INSERT INTO Events (Titel, Omschrijving, Locatie, Van, Tot) values(titel, omschrijving, locatie, values, tot)";
            SQLiteCommand command = new SQLiteCommand(sql, sqlconn);
            System.Diagnostics.Debug.Write("Command = " + command.ToString());
            command.ExecuteNonQuery();
            System.Diagnostics.Debug.Write("Addevent reached, try ");
        }
        catch (Exception e)
        {
            System.Diagnostics.Debug.Write("Addevent reached, catch ");
            System.Diagnostics.Debug.Write("error catched: " + e.ToString());
        }

    }
     /*   public Calendar.Events getTitels()
        {
            try 
            {
                string queryString = "SELECT Titel FROM Event";
                MySqlCommand sqlcmd = new MySqlCommand(queryString, sqlconn);
                Console.WriteLine ("Showing all event titels");
            }
            catch (Exception e)
            {
                Console.WriteLine ("Query failed");
            }
        }*/

 }

    

 


