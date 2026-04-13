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

        private static string GetDataFilePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DatabaseService.DefaultDataFileName);
        }

        public static bool ValidateAdmin(string username, string password)
        {
            // try to validate against XML users file if present
            try
            {
                var path = GetDataFilePath();
                if (File.Exists(path))
                {
                    var ds = new DataSet();
                    ds.ReadXml(path);
                    if (ds.Tables.Contains("Users"))
                    {
                        var users = ds.Tables["Users"];
                        foreach (DataRow r in users.Rows)
                        {
                            var u = Convert.ToString(r["Username"]);
                            var p = Convert.ToString(r["Password"]);
                            var role = Convert.ToString(r["Role"]);
                            if (string.Equals(u, username, StringComparison.OrdinalIgnoreCase)
                                && p == password
                                && string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch
            {
                // ignore and fall back
            }

            // fallback to built-in admin
            return username == "admin" && password == AdminPassword;
        }

        public static bool ValidateStudent(string username, string password)
        {
            try
            {
                var path = GetDataFilePath();
                if (File.Exists(path))
                {
                    var ds = new DataSet();
                    ds.ReadXml(path);
                    if (ds.Tables.Contains("Users"))
                    {
                        var users = ds.Tables["Users"];
                        foreach (DataRow r in users.Rows)
                        {
                            var u = Convert.ToString(r["Username"]);
                            var p = Convert.ToString(r["Password"]);
                            var role = Convert.ToString(r["Role"]);
                            if (string.Equals(u, username, StringComparison.OrdinalIgnoreCase)
                                && p == password
                                && string.Equals(role, "Student", StringComparison.OrdinalIgnoreCase))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch
            {
                // ignore and fall back
            }

            return username == "student" && password == StudentPassword;
        }

        public static bool ValidateTrainer(string username, string password)
        {
            // first try XML Users table
            try
            {
                var path = GetDataFilePath();
                if (File.Exists(path))
                {
                    var ds = new DataSet();
                    ds.ReadXml(path);
                    if (ds.Tables.Contains("Users"))
                    {
                        var users = ds.Tables["Users"];
                        foreach (DataRow r in users.Rows)
                        {
                            var u = Convert.ToString(r["Username"]);
                            var p = Convert.ToString(r["Password"]);
                            var role = Convert.ToString(r["Role"]);
                            if (string.Equals(u, username, StringComparison.OrdinalIgnoreCase)
                                && p == password
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
                                && p == password)
                            {
                                return true;
                            }
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
                if (tr.Username == username && tr.Password == password)
                    return true;
            }
            return false;
        }

        public static bool UpdatePasswordForCurrent(string role, string username, string currentPassword, string newPassword)
        {
            if (role == "admin")
            {
                if (currentPassword != AdminPassword) return false;
                AdminPassword = newPassword;
                return true;
            }

            if (role == "student")
            {
                if (currentPassword != StudentPassword) return false;
                StudentPassword = newPassword;
                return true;
            }

            if (role == "trainer")
            {
                var trainers = TrainerStore.GetAll();
                var tr = default(Trainer);
                foreach (var x in trainers)
                {
                    if (x.Username == username) { tr = x; break; }
                }
                if (tr == null) return false;
                if (tr.Password != currentPassword) return false;
                tr.Password = newPassword;
                return true;
            }

            return false;
        }
    }
}
