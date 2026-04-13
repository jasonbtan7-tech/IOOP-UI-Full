using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace assignment
{
    // Minimal SQL persistence for Feedback using LocalDB.
    public static class SqlFeedbackRepository
    {
        private static string MasterConnectionString => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;";
        private static string DbConnectionString => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AssignmentDb;Integrated Security=True;";

        private static void EnsureDatabaseAndTable()
        {
            using (var conn = new SqlConnection(MasterConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "IF DB_ID('AssignmentDb') IS NULL CREATE DATABASE [AssignmentDb];";
                    cmd.ExecuteNonQuery();
                }
            }

            using (var conn = new SqlConnection(DbConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"IF OBJECT_ID('dbo.Feedback','U') IS NULL
CREATE TABLE dbo.Feedback (
    Id nvarchar(100) PRIMARY KEY,
    Text nvarchar(max),
    SubmittedAt datetime,
    TrainerId nvarchar(100)
);";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Feedback> GetAll()
        {
            try
            {
                EnsureDatabaseAndTable();
                var list = new List<Feedback>();
                using (var conn = new SqlConnection(DbConnectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT Id, Text, SubmittedAt, TrainerId FROM dbo.Feedback";
                        using (var rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var idStr = rdr.IsDBNull(0) ? Guid.NewGuid().ToString() : rdr.GetString(0);
                                Guid id;
                                if (!Guid.TryParse(idStr, out id)) id = Guid.NewGuid();
                                var f = new Feedback
                                {
                                    Id = id,
                                    Text = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                                    SubmittedAt = rdr.IsDBNull(2) ? DateTime.Now : rdr.GetDateTime(2),
                                    TrainerId = rdr.IsDBNull(3) ? null : rdr.GetString(3)
                                };
                                list.Add(f);
                            }
                        }
                    }
                }
                return list;
            }
            catch
            {
                return null;
            }
        }

        public static void SaveAll(List<Feedback> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            EnsureDatabaseAndTable();
            using (var conn = new SqlConnection(DbConnectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                using (var delCmd = conn.CreateCommand())
                {
                    delCmd.Transaction = tran;
                    delCmd.CommandText = "DELETE FROM dbo.Feedback";
                    delCmd.ExecuteNonQuery();

                    using (var insCmd = conn.CreateCommand())
                    {
                        insCmd.Transaction = tran;
                        insCmd.CommandText = "INSERT INTO dbo.Feedback (Id, Text, SubmittedAt, TrainerId) VALUES (@Id,@Text,@SubmittedAt,@TrainerId)";
                        insCmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar, 100));
                        insCmd.Parameters.Add(new SqlParameter("@Text", SqlDbType.NVarChar, -1));
                        insCmd.Parameters.Add(new SqlParameter("@SubmittedAt", SqlDbType.DateTime));
                        insCmd.Parameters.Add(new SqlParameter("@TrainerId", SqlDbType.NVarChar, 100));

                        foreach (var f in items)
                        {
                            insCmd.Parameters["@Id"].Value = (object)f.Id.ToString();
                            insCmd.Parameters["@Text"].Value = (object)f.Text ?? DBNull.Value;
                            insCmd.Parameters["@SubmittedAt"].Value = f.SubmittedAt;
                            insCmd.Parameters["@TrainerId"].Value = (object)f.TrainerId ?? DBNull.Value;
                            insCmd.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                }
            }
        }
    }
}
