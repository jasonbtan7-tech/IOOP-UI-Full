using System;
using System.Collections.Generic;
using System.Data;

namespace assignment
{
    public static class StudentStore
    {
        static StudentStore()
        {
            try
            {
                var fromDb = SqlStudentRepository.GetAll();
                if (fromDb != null && fromDb.Count > 0)
                {
                    items.Clear();
                    items.AddRange(fromDb);
                    return;
                }
            }
            catch
            {
                // ignore db
            }

            try { LoadFromDisk(); } catch { }
            if (items.Count == 0)
            {
                items.Add(new Student
                {
                    StudentId = "S100",
                    Name = "Sample Student",
                    Email = "sample@student.local",
                    Phone = "0000000000",
                    Address = "Sample Address",
                    Gender = "Not specified",
                    Module = "Sample Module",
                    Month = "Jan",
                    PaymentStatus = "Unpaid"
                });
                try { SaveToDisk(); } catch { }
            }
        }

        private static readonly List<Student> items = new List<Student>();

        public static void Add(Student s)
        {
            items.Add(s);
            try { SaveToDisk(); } catch { }
        }

        public static List<Student> GetAll()
        {
            return new List<Student>(items);
        }

        public static bool RemoveById(string id)
        {
            var s = items.Find(x => x.StudentId == id);
            if (s == null) return false;
            items.Remove(s);
            try { SaveToDisk(); } catch { }
            return true;
        }

        public static void LoadFromDisk()
        {
            var ds = DatabaseService.ReadDataSet();
            if (!ds.Tables.Contains("Student")) return;
            var table = ds.Tables["Student"];
            items.Clear();
            foreach (DataRow r in table.Rows)
            {
                items.Add(new Student
                {
                    StudentId = Convert.ToString(r["StudentId"]),
                    Name = Convert.ToString(r["Name"]),
                    Email = Convert.ToString(r["Email"]),
                    Phone = Convert.ToString(r["Phone"]),
                    Address = Convert.ToString(r["Address"]),
                    Gender = Convert.ToString(r["Gender"]),
                    Module = Convert.ToString(r["Module"]),
                    Month = Convert.ToString(r["Month"]),
                    PaymentStatus = Convert.ToString(r["PaymentStatus"])
                });
            }
        }

        public static void SaveToDisk()
        {
            try
            {
                SqlStudentRepository.SaveAll(items);
                return;
            }
            catch { }

            var ds = DatabaseService.ReadDataSet();
            DataTable table;
            if (ds.Tables.Contains("Student"))
            {
                table = ds.Tables["Student"];
                table.Clear();
            }
            else
            {
                table = new DataTable("Student");
                table.Columns.Add("StudentId", typeof(string));
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Email", typeof(string));
                table.Columns.Add("Phone", typeof(string));
                table.Columns.Add("Address", typeof(string));
                table.Columns.Add("Gender", typeof(string));
                table.Columns.Add("Module", typeof(string));
                table.Columns.Add("Month", typeof(string));
                table.Columns.Add("PaymentStatus", typeof(string));
                ds.Tables.Add(table);
            }

            foreach (var s in items)
            {
                var row = table.NewRow();
                row["StudentId"] = s.StudentId ?? string.Empty;
                row["Name"] = s.Name ?? string.Empty;
                row["Email"] = s.Email ?? string.Empty;
                row["Phone"] = s.Phone ?? string.Empty;
                row["Address"] = s.Address ?? string.Empty;
                row["Gender"] = s.Gender ?? string.Empty;
                row["Module"] = s.Module ?? string.Empty;
                row["Month"] = s.Month ?? string.Empty;
                row["PaymentStatus"] = s.PaymentStatus ?? string.Empty;
                table.Rows.Add(row);
            }

            DatabaseService.SaveDataSet(ds);
        }
    }
}
