using System;
using System.Collections.Generic;

namespace assignment
{
    public static class RequestStore
    {
        private static readonly List<CoachingRequest> requests = new List<CoachingRequest>();

        public static event Action RequestsChanged;

        static RequestStore()
        {
            try { LoadFromDisk(); } catch { }
            if (requests.Count == 0)
            {
                requests.Add(new CoachingRequest { Id = Guid.NewGuid(), Grade = "Foundation", Lecturer = "Default Lecturer", Status = "Pending", RequestedAt = DateTime.Now });
                try { SaveToDisk(); } catch { }
            }
        }

        public static void Add(CoachingRequest r)
        {
            requests.Add(r);
            try { SaveToDisk(); } catch { }
            RequestsChanged?.Invoke();
        }

        public static List<CoachingRequest> GetAll()
        {
            return new List<CoachingRequest>(requests);
        }

        public static void Clear()
        {
            requests.Clear();
            try { SaveToDisk(); } catch { }
            RequestsChanged?.Invoke();
        }

        // Load from XML (fallback) using DatabaseService
        public static void LoadFromDisk()
        {
            var ds = DatabaseService.ReadDataSet();
            if (!ds.Tables.Contains("CoachingRequest")) return;
            var table = ds.Tables["CoachingRequest"];
            requests.Clear();
            foreach (System.Data.DataRow r in table.Rows)
            {
                var idStr = Convert.ToString(r["Id"]);
                Guid id;
                if (!Guid.TryParse(idStr, out id)) id = Guid.NewGuid();
                var grade = Convert.ToString(r["Grade"]);
                var lecturer = Convert.ToString(r["Lecturer"]);
                var status = Convert.ToString(r["Status"]);
                DateTime requestedAt;
                try { requestedAt = Convert.ToDateTime(r["RequestedAt"]); } catch { requestedAt = DateTime.Now; }
                requests.Add(new CoachingRequest { Id = id, Grade = grade, Lecturer = lecturer, Status = status, RequestedAt = requestedAt });
            }
        }

        // Save to XML via DatabaseService (SQL not implemented for requests)
        public static void SaveToDisk()
        {
            var ds = DatabaseService.ReadDataSet();
            System.Data.DataTable table;
            if (ds.Tables.Contains("CoachingRequest"))
            {
                table = ds.Tables["CoachingRequest"];
                table.Clear();
            }
            else
            {
                table = new System.Data.DataTable("CoachingRequest");
                table.Columns.Add("Id", typeof(string));
                table.Columns.Add("Grade", typeof(string));
                table.Columns.Add("Lecturer", typeof(string));
                table.Columns.Add("Status", typeof(string));
                table.Columns.Add("RequestedAt", typeof(DateTime));
                ds.Tables.Add(table);
            }

            foreach (var r in requests)
            {
                var row = table.NewRow();
                row["Id"] = r.Id.ToString();
                row["Grade"] = r.Grade ?? string.Empty;
                row["Lecturer"] = r.Lecturer ?? string.Empty;
                row["Status"] = r.Status ?? string.Empty;
                row["RequestedAt"] = r.RequestedAt;
                table.Rows.Add(row);
            }

            DatabaseService.SaveDataSet(ds);
        }

        // Update the status of a request and notify listeners
        public static bool UpdateStatus(Guid id, string status)
        {
            var r = requests.Find(x => x.Id == id);
            if (r == null) return false;
            r.Status = status;
            try { SaveToDisk(); } catch { }
            RequestsChanged?.Invoke();
            return true;
        }
    }
}
