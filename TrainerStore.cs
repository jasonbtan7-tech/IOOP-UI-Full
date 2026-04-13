using System.Collections.Generic;
using System.Data;
using System;

namespace assignment
{
    public static class TrainerStore
    {
        static TrainerStore()
        {
            // Try to load trainers from SQL Server first; fall back to XML file.
            try
            {
                var fromDb = SqlTrainerRepository.GetAll();
                if (fromDb != null && fromDb.Count > 0)
                {
                    trainers.Clear();
                    trainers.AddRange(fromDb);
                    return;
                }
            }
            catch
            {
                // ignore DB load errors and fall back to disk
            }

            try { LoadFromDisk(); } catch { /* ignore load errors */ }
            // Ensure a default lecturer account exists so login works out-of-the-box
            if (trainers.Count == 0)
            {
                var def = new Trainer
                {
                    Id = "T001",
                    Name = "Default Lecturer",
                    Username = "lecturer",
                    Password = "123",
                    Email = string.Empty,
                    Phone = string.Empty,
                    Gender = string.Empty
                };
                trainers.Add(def);
                try { SaveToDisk(); } catch { }
            }
        }

        private static readonly List<Trainer> trainers = new List<Trainer>();

        public static void Add(Trainer t)
        {
            trainers.Add(t);
            try { SaveToDisk(); } catch { /* ignore save errors */ }
        }

        public static bool RemoveById(string id)
        {
            Trainer t = null;
            for (int i = 0; i < trainers.Count; i++)
            {
                if (trainers[i].Id == id)
                {
                    t = trainers[i];
                    break;
                }
            }
            if (t == null) return false;
            trainers.Remove(t);
            try { SaveToDisk(); } catch { /* ignore save errors */ }
            return true;
        }

        public static List<Trainer> GetAll()
        {
            return new List<Trainer>(trainers);
        }

        // Attempt to persist trainers to SQL, fall back to XML file
        public static void SaveToDisk()
        {
            try
            {
                SqlTrainerRepository.SaveAll(trainers);
                return;
            }
            catch
            {
                // if DB save fails, fall back to XML
            }

            // existing behavior: save to XML
            var ds = DatabaseService.ReadDataSet();
            DataTable table;
            if (ds.Tables.Contains("Trainer"))
            {
                table = ds.Tables["Trainer"];
                table.Clear();
            }
            else
            {
                table = new DataTable("Trainer");
                table.Columns.Add("Id", typeof(string));
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Gender", typeof(string));
                table.Columns.Add("Email", typeof(string));
                table.Columns.Add("Phone", typeof(string));
                table.Columns.Add("Username", typeof(string));
                table.Columns.Add("Password", typeof(string));
                ds.Tables.Add(table);
            }

            foreach (var tr in trainers)
            {
                var row = table.NewRow();
                row["Id"] = tr.Id ?? string.Empty;
                row["Name"] = tr.Name ?? string.Empty;
                row["Gender"] = tr.Gender ?? string.Empty;
                row["Email"] = tr.Email ?? string.Empty;
                row["Phone"] = tr.Phone ?? string.Empty;
                row["Username"] = tr.Username ?? string.Empty;
                row["Password"] = tr.Password ?? string.Empty;
                table.Rows.Add(row);
            }

            DatabaseService.SaveDataSet(ds);
        }

        // Load trainers from the data XML into the in-memory list
        public static void LoadFromDisk()
        {
            var ds = DatabaseService.ReadDataSet();
            if (!ds.Tables.Contains("Trainer")) return;

            var table = ds.Tables["Trainer"];
            trainers.Clear();
            foreach (DataRow r in table.Rows)
            {
                var tr = new Trainer
                {
                    Id = Convert.ToString(r["Id"]),
                    Name = Convert.ToString(r["Name"]),
                    Gender = Convert.ToString(r["Gender"]),
                    Email = Convert.ToString(r["Email"]),
                    Phone = Convert.ToString(r["Phone"]),
                    Username = Convert.ToString(r["Username"]),
                    Password = Convert.ToString(r["Password"]) 
                };
                trainers.Add(tr);
            }
        }

    }
}
