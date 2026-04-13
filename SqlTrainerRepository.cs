using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace assignment
{
    // Simple SQL Server repository for Trainer records using LocalDB.
    public static class SqlTrainerRepository
    {
        // Default connection uses LocalDB and database 'AssignmentDb'.
        private static string MasterConnectionString => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;";
        private static string DbConnectionString => "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AssignmentDb;Integrated Security=True;";

        // Ensure the database and Trainer table exist.
        private static void EnsureDatabaseAndTable()
        {
            using (var conn = new SqlConnection(MasterConnectionString))
            {
                conn.Open();
                // Create database if not exists
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
                    cmd.CommandText = @"IF OBJECT_ID('dbo.Trainer','U') IS NULL
CREATE TABLE dbo.Trainer (
    Id nvarchar(100) PRIMARY KEY,
    Name nvarchar(200),
    Gender nvarchar(50),
    Email nvarchar(200),
    Phone nvarchar(100),
    Username nvarchar(200),
    Password nvarchar(200)
);";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Trainer> GetAll()
        {
            try
            {
                EnsureDatabaseAndTable();
                var list = new List<Trainer>();
                using (var conn = new SqlConnection(DbConnectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT Id, Name, Gender, Email, Phone, Username, Password FROM dbo.Trainer";
                        using (var rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var t = new Trainer
                                {
                                    Id = rdr.GetString(0),
                                    Name = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                                    Gender = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                                    Email = rdr.IsDBNull(3) ? null : rdr.GetString(3),
                                    Phone = rdr.IsDBNull(4) ? null : rdr.GetString(4),
                                    Username = rdr.IsDBNull(5) ? null : rdr.GetString(5),
                                    Password = rdr.IsDBNull(6) ? null : rdr.GetString(6)
                                };
                                list.Add(t);
                            }
                        }
                    }
                }
                return list;
            }
            catch
            {
                return null; // signal failure
            }
        }

        public static void SaveAll(List<Trainer> trainers)
        {
            if (trainers == null) throw new ArgumentNullException(nameof(trainers));
            EnsureDatabaseAndTable();
            using (var conn = new SqlConnection(DbConnectionString))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                using (var delCmd = conn.CreateCommand())
                {
                    delCmd.Transaction = tran;
                    delCmd.CommandText = "DELETE FROM dbo.Trainer";
                    delCmd.ExecuteNonQuery();

                    using (var insCmd = conn.CreateCommand())
                    {
                        insCmd.Transaction = tran;
                        insCmd.CommandText = "INSERT INTO dbo.Trainer (Id, Name, Gender, Email, Phone, Username, Password) VALUES (@Id,@Name,@Gender,@Email,@Phone,@Username,@Password)";
                        insCmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.NVarChar, 100));
                        insCmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 200));
                        insCmd.Parameters.Add(new SqlParameter("@Gender", SqlDbType.NVarChar, 50));
                        insCmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 200));
                        insCmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 100));
                        insCmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 200));
                        insCmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 200));

                        foreach (var t in trainers)
                        {
                            insCmd.Parameters["@Id"].Value = (object)t.Id ?? "";
                            insCmd.Parameters["@Name"].Value = (object)t.Name ?? DBNull.Value;
                            insCmd.Parameters["@Gender"].Value = (object)t.Gender ?? DBNull.Value;
                            insCmd.Parameters["@Email"].Value = (object)t.Email ?? DBNull.Value;
                            insCmd.Parameters["@Phone"].Value = (object)t.Phone ?? DBNull.Value;
                            insCmd.Parameters["@Username"].Value = (object)t.Username ?? DBNull.Value;
                            insCmd.Parameters["@Password"].Value = (object)t.Password ?? DBNull.Value;
                            insCmd.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                }
            }
        }
    }
}
