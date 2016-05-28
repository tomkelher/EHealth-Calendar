using System;
using System.Diagnostics;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using SQLite;
using SQLite.Net;


namespace Database
{

    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public DateTime Datum { get; set; }
        public string Taak { get; set; }
        public string Plaats { get; set; }
        public string Omschrijving { get; set; }
        public TimeSpan BeginUur { get; set; }
        public TimeSpan EindUur { get; set; }
        public override string ToString()
        {
            return string.Format("[item: Id={0}, FirstName={1}, Datum={2},Taak={3}, Plaats={4}, Omschrijving={5}, BeginUur={6}, EindUur={7}]", Id, Datum, Taak, Plaats, Omschrijving, BeginUur, EindUur);
        }
    }

     public class DatabaseConnectionAsync
      {
          private string databaseFileName;
          private string applicationFolderPath;
          private List<string> titels = new List<string>();
          private List<int> ids = new List<int>();
          private List<string> omschrijvingen = new List<string>();
          private List<string> plaatsen = new List<string>();
          private List<TimeSpan> beginuren = new List<TimeSpan>();
          private List<TimeSpan> einduren = new List<TimeSpan>();
      
          public DatabaseConnectionAsync()
          {
              try
              {
                  databaseFileName = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DataBase.db");
                  // Create the folder path.
                  //System.IO.Directory.CreateDirectory(applicationFolderPath);

                  //this.databaseFileName = System.IO.Path.Combine(applicationFolderPath, "DataBase.db");


                 var conn = new SQLiteAsyncConnection(databaseFileName);
                  conn.CreateTableAsync<Item>().ContinueWith((results) =>
                  {
                      System.Diagnostics.Debug.WriteLine("Table created!");
                  });
              }
              catch
              {
                  applicationFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                  System.IO.Directory.CreateDirectory(applicationFolderPath);
                  databaseFileName = System.IO.Path.Combine(applicationFolderPath, "DataBase.db");
                  var conn = new SQLiteAsyncConnection(databaseFileName);
                  conn.CreateTableAsync<Item>().ContinueWith((results) =>
                  {
                      System.Diagnostics.Debug.WriteLine("Table created!");
                  });



              }
          }

          public void addToTable(DateTime datum, string taak, string plaats, string omschrijving, TimeSpan beginuur, TimeSpan einduur)
          {
              try
              {
                  Item item = new Item()
                  {
                      Datum = datum,
                      Taak = taak,
                      Plaats = plaats,
                      Omschrijving = omschrijving,
                      BeginUur = beginuur,
                      EindUur = einduur,
                  };

                  var conn = new SQLiteAsyncConnection(databaseFileName);
                  conn.InsertAsync(item).ContinueWith((t) =>
                  {
                      System.Diagnostics.Debug.WriteLine("New item ID: {0}", item.Id);

                  });
              }
              catch (Exception e)
              {
                  System.Diagnostics.Debug.WriteLine("getexception: " + e);
                  System.Diagnostics.Debug.WriteLine("add exception: ", databaseFileName);
              }

          }

          public void ChangeToTable(int id, DateTime datum, string taak, string plaats, string omschrijving, TimeSpan beginuur, TimeSpan einduur)
          {
              try
              {
                  Item item = new Item()
                  {
                      Id = id,
                      Datum = datum,
                      Taak = taak,
                      Plaats = plaats,
                      Omschrijving = omschrijving,
                      BeginUur = beginuur,
                      EindUur = einduur,
                  };

                  var conn = new SQLiteAsyncConnection(databaseFileName);
                  conn.UpdateAsync(item).ContinueWith((t) =>
                  {
                      System.Diagnostics.Debug.WriteLine("changed item ID: {0}", item.Id);
                  });
              }
              catch (Exception e)
              {
                  System.Diagnostics.Debug.WriteLine("getexception: " + e);
                  System.Diagnostics.Debug.WriteLine("change exception: ", databaseFileName);
              }

          }

          public void DeleteFromTable(int id)
          {
              try
              {
                  Item item = new Item()
                  {
                      Id = id,
                      
                  };

                  var conn = new SQLiteAsyncConnection(databaseFileName);
                  conn.DeleteAsync(item).ContinueWith((t) =>
                  {
                      System.Diagnostics.Debug.WriteLine("deleted item ID: {0}", item.Id);
                  });
              }
              catch (Exception e)
              {
                  System.Diagnostics.Debug.WriteLine("getexception: " + e);
                  System.Diagnostics.Debug.WriteLine("delet exception: ", databaseFileName);
              }

          }

          public List<string> getFromTable(DateTime datum)
          {
            List<string> takenKort = new List<string>();
            ids.Clear();
            titels.Clear();
            omschrijvingen.Clear();
            plaatsen.Clear();
            beginuren.Clear();
            einduren.Clear();

            try
              {
                  var conn = new SQLiteAsyncConnection(databaseFileName);
                  var query = conn.Table<Item>().Where(v => v.Datum.Equals(datum));
                  query.ToListAsync().ContinueWith((t) =>
                  {
                      foreach (var item in t.Result)
                      {
                          System.Diagnostics.Debug.WriteLine("ID: " + item.Id);
                          System.Diagnostics.Debug.WriteLine("Datum: " + item.Datum);
                          System.Diagnostics.Debug.WriteLine("taak: " + item.Taak);
                          System.Diagnostics.Debug.WriteLine("plaats: " + item.Plaats);
                          System.Diagnostics.Debug.WriteLine("omschrijving: " + item.Omschrijving);
                          System.Diagnostics.Debug.WriteLine("begin: " + item.BeginUur);
                          System.Diagnostics.Debug.WriteLine("einde: " + item.EindUur);
                          takenKort.Add(item.BeginUur.ToString() + " - " + item.EindUur.ToString() + ": " + item.Taak);
                          titels.Add(item.Taak);
                          ids.Add(item.Id);
                          omschrijvingen.Add(item.Omschrijving);
                          plaatsen.Add(item.Plaats);
                          beginuren.Add(item.BeginUur);
                          einduren.Add(item.EindUur);

                      }
                      
                  });
                return takenKort;
            }
              catch (Exception e)
              {
                takenKort.Clear();
                takenKort.Add("error");
                System.Diagnostics.Debug.WriteLine("getexception: " + e);
                System.Diagnostics.Debug.WriteLine("getfrom exception: ", databaseFileName);
                return takenKort;
              }
          }

        public string getTitels(int index)
        {
            return titels[index];
        }
        public string getOmschrijvingen(int index)
        {
            return omschrijvingen[index];
        }
        public string getPlaatsen(int index)
        {
            return plaatsen[index];
        }
        public TimeSpan getBeginuren(int index)
        {
            return beginuren[index];
        }
        public TimeSpan getEinduren(int index)
        {
            return einduren[index];
        }
        public int getId(int index)
        {
            return ids[index];
        }

    }

    public class DatabaseConnectionSync
    {
        private string databaseFileName;
        private string applicationFolderPath;
        private List<string> titels = new List<string>();
        private List<int> ids = new List<int>();
        private List<string> omschrijvingen = new List<string>();
        private List<string> plaatsen = new List<string>();
        private List<TimeSpan> beginuren = new List<TimeSpan>();
        private List<TimeSpan> einduren = new List<TimeSpan>();
        public DatabaseConnectionSync()
        {
            try
            {
                databaseFileName = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "DataBase.db");
                var conn = new SQLite.SQLiteConnection(databaseFileName);
                conn.CreateTable<Item>();
            }
            catch
            {
                applicationFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                System.IO.Directory.CreateDirectory(applicationFolderPath);
                databaseFileName = System.IO.Path.Combine(applicationFolderPath, "DataBase.db");
                var conn = new SQLite.SQLiteConnection(databaseFileName);
                conn.CreateTable<Item>();
            }
            
        }

        public void addToTable(DateTime datum, string taak, string plaats, string omschrijving, TimeSpan beginuur, TimeSpan einduur)
        {
            try
            {
                Item item = new Item()
                {
                    Datum = datum,
                    Taak = taak,
                    Plaats = plaats,
                    Omschrijving = omschrijving,
                    BeginUur = beginuur,
                    EindUur = einduur,
                };

                var conn = new SQLite.SQLiteConnection(databaseFileName);
                conn.Insert(item);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("getexception: " + e);
                System.Diagnostics.Debug.WriteLine("add exception: ", databaseFileName);
            }

        }

        public void ChangeToTable(int id, DateTime datum, string taak, string plaats, string omschrijving, TimeSpan beginuur, TimeSpan einduur)
        {
            try
            {
                Item item = new Item()
                {
                    Id = id,
                    Datum = datum,
                    Taak = taak,
                    Plaats = plaats,
                    Omschrijving = omschrijving,
                    BeginUur = beginuur,
                    EindUur = einduur,
                };

                var conn = new SQLite.SQLiteConnection(databaseFileName);
                conn.Update(item);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("getexception: " + e);
                System.Diagnostics.Debug.WriteLine("change exception: ", databaseFileName);
            }

        }

        public void DeleteFromTable(int id)
        {
            try
            {
                Item item = new Item()
                {
                    Id = id,
                };

                var conn = new SQLite.SQLiteConnection(databaseFileName);
                conn.Delete(item);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("getexception: " + e);
                System.Diagnostics.Debug.WriteLine("delet exception: ", databaseFileName);
            }

        }

        public List<string> getFromTable(DateTime datum)
        {
            List<string> takenKort = new List<string>();
            ids.Clear();
            titels.Clear();
            omschrijvingen.Clear();
            plaatsen.Clear();
            beginuren.Clear();
            einduren.Clear();
            try
            {
                var conn = new SQLite.SQLiteConnection(databaseFileName);
                var query = conn.Table<Item>().Where(v => v.Datum.Equals(datum));
                foreach (var item in query)
                    {
                        System.Diagnostics.Debug.WriteLine("ID: " + item.Id);
                        System.Diagnostics.Debug.WriteLine("Datum: " + item.Datum);
                        System.Diagnostics.Debug.WriteLine("taak: " + item.Taak);
                        System.Diagnostics.Debug.WriteLine("plaats: " + item.Plaats);
                        System.Diagnostics.Debug.WriteLine("omschrijving: " + item.Omschrijving);
                        System.Diagnostics.Debug.WriteLine("begin: " + item.BeginUur);
                        System.Diagnostics.Debug.WriteLine("einde: " + item.EindUur);
                        takenKort.Add(item.BeginUur.ToString() + " - " + item.EindUur.ToString() + ": " + item.Taak);
                        titels.Add(item.Taak);
                        ids.Add(item.Id);
                        omschrijvingen.Add(item.Omschrijving);
                        plaatsen.Add(item.Plaats);
                        beginuren.Add(item.BeginUur);
                        einduren.Add(item.EindUur);

                }

                return takenKort;
            }
            catch (Exception e)
            {
                takenKort.Clear();
                takenKort.Add("error");
                System.Diagnostics.Debug.WriteLine("getexception: " + e);
                System.Diagnostics.Debug.WriteLine("getfrom exception: ", databaseFileName);
                return takenKort;
            }


        }

        public string getTitels(int index)
        {
            return titels[index];
        }
        public string getOmschrijvingen(int index)
        {
            return omschrijvingen[index];
        }
        public string getPlaatsen(int index)
        {
            return plaatsen[index];
        }
        public TimeSpan getBeginuren(int index)
        {
            return beginuren[index];
        }
        public TimeSpan getEinduren(int index)
        {
            return einduren[index];
        }
        public int getId(int index)
        {
            return ids[index];
        }

    }
}