namespace assignment
{
    public static class AuthManager
    {
        // default passwords
        public static string AdminPassword { get; set; } = "123";
        public static string StudentPassword { get; set; } = "123";

        public static bool ValidateAdmin(string username, string password)
        {
            return username == "admin" && password == AdminPassword;
        }

        public static bool ValidateStudent(string username, string password)
        {
            return username == "student" && password == StudentPassword;
        }

        public static bool ValidateTrainer(string username, string password)
        {
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
