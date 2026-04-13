using System;
using System.Collections.Generic;
using System.Data;

// Note: Feedback persistence will attempt SQL Server LocalDB first and fall back to XML file.

namespace assignment
{
    public static class FeedbackStore
    {
        static FeedbackStore()
        {
            try
            {
                var fromDb = SqlFeedbackRepository.GetAll();
                if (fromDb != null && fromDb.Count > 0)
                {
                    items.Clear();
                    items.AddRange(fromDb);
                    return;
                }
            }
            catch
            {
                // ignore DB errors
            }

            try { LoadFromDisk(); } catch { }
            // ensure at least one feedback exists so UI grids aren't empty
            if (items.Count == 0)
            {
                var firstTrainer = TrainerStore.GetAll();
                var trainerId = firstTrainer.Count > 0 ? firstTrainer[0].Id : null;
                items.Add(new Feedback { Id = Guid.NewGuid(), Text = "Welcome — sample feedback.", SubmittedAt = DateTime.Now, TrainerId = trainerId });
                try { SaveToDisk(); } catch { }
            }
        }

        private static readonly List<Feedback> items = new List<Feedback>();

        public static void Add(Feedback f)
        {
            items.Add(f);
            try { SaveToDisk(); } catch { }
        }

        public static List<Feedback> GetAll()
        {
            return new List<Feedback>(items);
        }

        public static void Clear()
        {
            items.Clear();
            try { SaveToDisk(); } catch { }
        }

        // Load from XML file (fallback)
        public static void LoadFromDisk()
        {
            var ds = DatabaseService.ReadDataSet();
            if (!ds.Tables.Contains("Feedback")) return;
            var table = ds.Tables["Feedback"];
            items.Clear();
            foreach (DataRow r in table.Rows)
            {
                var idStr = Convert.ToString(r["Id"]);
                Guid id;
                if (!Guid.TryParse(idStr, out id)) id = Guid.NewGuid();
                var text = Convert.ToString(r["Text"]);
                DateTime submitted;
                try { submitted = Convert.ToDateTime(r["SubmittedAt"]); } catch { submitted = DateTime.Now; }
                var trainerId = Convert.ToString(r["TrainerId"]);
                items.Add(new Feedback { Id = id, Text = text, SubmittedAt = submitted, TrainerId = trainerId });
            }
        }

        // Save to SQL if possible; otherwise write to XML via DatabaseService
        public static void SaveToDisk()
        {
            try
            {
                SqlFeedbackRepository.SaveAll(items);
                return;
            }
            catch
            {
                // fall back to XML
            }

            var ds = DatabaseService.ReadDataSet();
            DataTable table;
            if (ds.Tables.Contains("Feedback"))
            {
                table = ds.Tables["Feedback"];
                table.Clear();
            }
            else
            {
                table = new DataTable("Feedback");
                table.Columns.Add("Id", typeof(string));
                table.Columns.Add("Text", typeof(string));
                table.Columns.Add("SubmittedAt", typeof(DateTime));
                table.Columns.Add("TrainerId", typeof(string));
                ds.Tables.Add(table);
            }

            foreach (var f in items)
            {
                var row = table.NewRow();
                row["Id"] = f.Id.ToString();
                row["Text"] = f.Text ?? string.Empty;
                row["SubmittedAt"] = f.SubmittedAt;
                row["TrainerId"] = f.TrainerId ?? string.Empty;
                table.Rows.Add(row);
            }

            DatabaseService.SaveDataSet(ds);
        }
    }
}
