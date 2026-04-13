using System;
using System.Data;
using System.IO;

namespace assignment
{
    public static class AuthManager
    {
        // default passwords
        public static string AdminPassword { get; set; } = "123";
        public static string StudentPassword { get; set; } = "123";

        // Load persisted settings on first use
        static AuthManager()
        {
            try { LoadSettings(); } catch { /* ignore errors */ }
            // Force initialization of other stores so default accounts/data are present
            try { var _ = TrainerStore.GetAll(); } catch { }
            try { var __ = StudentStore.GetAll(); } catch { }
        }

        private static void LoadSettings()
        {
            var ds = DatabaseService.ReadDataSet();
            if (!ds.Tables.Contains("Settings")) return;
            var t = ds.Tables["Settings"];
            foreach (DataRow r in t.Rows)
            {
                var k = Convert.ToString(r["Key"]);
                var v = Convert.ToString(r["Value"]);
                if (k == "AdminPassword") AdminPassword = v;
                if (k == "StudentPassword") StudentPassword = v;
            }
        }

        private static void SaveSettings()
        {
            var ds = DatabaseService.ReadDataSet();
            DataTable table;
            if (ds.Tables.Contains("Settings"))
            {
                table = ds.Tables["Settings"];
                table.Clear();
            }
            else
            {
                table = new DataTable("Settings");
                table.Columns.Add("Key", typeof(string));
                table.Columns.Add("Value", typeof(string));
                ds.Tables.Add(table);
            }

            var r1 = table.NewRow(); r1["Key"] = "AdminPassword"; r1["Value"] = AdminPassword; table.Rows.Add(r1);
            var r2 = table.NewRow(); r2["Key"] = "StudentPassword"; r2["Value"] = StudentPassword; table.Rows.Add(r2);

            DatabaseService.SaveDataSet(ds);
        }

        // Data file location and reading is handled by DatabaseService.

        public static bool ValidateAdmin(string username, string password)
        {
            username = username?.Trim();
            password = password ?? string.Empty;
            // try to validate against XML users file if present
            try
            {
                var ds = DatabaseService.ReadDataSet();
                if (ds.Tables.Contains("Users"))
                {
                    var users = ds.Tables["Users"];
                    foreach (DataRow r in users.Rows)
                    {
                        var u = Convert.ToString(r["Username"]);
                        var p = Convert.ToString(r["Password"]);
                        var role = Convert.ToString(r["Role"]);
                        if (string.Equals(u, username, StringComparison.OrdinalIgnoreCase)
                            && string.Equals(p ?? string.Empty, password, StringComparison.Ordinal)
                            && string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {
                // ignore and fall back
            }

            // fallback to built-in admin
            return string.Equals(username, "admin", StringComparison.OrdinalIgnoreCase) && string.Equals(password, AdminPassword, StringComparison.Ordinal);
        }

        public static bool ValidateStudent(string username, string password)
        {
            username = username?.Trim();
            password = password ?? string.Empty;
            try
            {
                var ds = DatabaseService.ReadDataSet();
                if (ds.Tables.Contains("Users"))
                {
                    var users = ds.Tables["Users"];
                    foreach (DataRow r in users.Rows)
                    {
                        var u = Convert.ToString(r["Username"]);
                        var p = Convert.ToString(r["Password"]);
                        var role = Convert.ToString(r["Role"]);
                        if (string.Equals(u, username, StringComparison.OrdinalIgnoreCase)
                            && string.Equals(p ?? string.Empty, password, StringComparison.Ordinal)
                            && string.Equals(role, "Student", StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {
                // ignore and fall back
            }

            return string.Equals(username, "student", StringComparison.OrdinalIgnoreCase) && string.Equals(password, StudentPassword, StringComparison.Ordinal);
        }

        public static bool ValidateTrainer(string username, string password)
        {
            username = username?.Trim();
            password = password ?? string.Empty;
            // first try XML Users table
            try
            {
                var ds = DatabaseService.ReadDataSet();
                if (ds.Tables.Contains("Users"))
                {
                    var users = ds.Tables["Users"];
                    foreach (DataRow r in users.Rows)
                    {
                        var u = Convert.ToString(r["Username"]);
                        var p = Convert.ToString(r["Password"]);
                        var role = Convert.ToString(r["Role"]);
                        if (string.Equals(u, username, StringComparison.OrdinalIgnoreCase)
                            && string.Equals(p ?? string.Empty, password, StringComparison.Ordinal)
                            && string.Equals(role, "Trainer", StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                    }
                }

                // also check Trainer table if present (some data may store credentials there)
                if (ds.Tables.Contains("Trainer"))
                {
                    var trainers = ds.Tables["Trainer"];
                    foreach (DataRow r in trainers.Rows)
                    {
                        var u = Convert.ToString(r["Username"]);
                        var p = Convert.ToString(r["Password"]);
                        if (string.Equals(u, username, StringComparison.OrdinalIgnoreCase)
                            && string.Equals(p ?? string.Empty, password, StringComparison.Ordinal))
                        {
                            return true;
                        }
                    }
                }
            }
            catch
            {
                // ignore and fall back
            }

            // fallback to in-memory trainer store
            var t = TrainerStore.GetAll();
            foreach (var tr in t)
            {
                if (string.Equals(tr.Username, username, StringComparison.OrdinalIgnoreCase)
                    && string.Equals(tr.Password ?? string.Empty, password ?? string.Empty, StringComparison.Ordinal))
                    return true;
            }
            // fallback built-in lecturer account
            if (string.Equals(username, "lecturer", StringComparison.OrdinalIgnoreCase) && password == "123")
                return true;

            return false;
        }

        public static bool UpdatePasswordForCurrent(string role, string username, string currentPassword, string newPassword)
        {
            if (role == "admin")
            {
                if (currentPassword != AdminPassword) return false;
                AdminPassword = newPassword;
                try { SaveSettings(); } catch { }
                return true;
            }

            if (role == "student")
            {
                if (currentPassword != StudentPassword) return false;
                StudentPassword = newPassword;
                try { SaveSettings(); } catch { }
                return true;
            }

            if (role == "trainer")
            {
                var trainers = TrainerStore.GetAll();
                var tr = default(Trainer);
                foreach (var x in trainers)
                {
                    if (string.Equals(x.Username, username, StringComparison.OrdinalIgnoreCase)) { tr = x; break; }
                }
                if (tr == null) return false;
                if (tr.Password != currentPassword) return false;
                tr.Password = newPassword;
                try { TrainerStore.SaveToDisk(); } catch { }
                return true;
            }

            return false;
        }
    }
}
