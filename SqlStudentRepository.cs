using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace assignment
{
    public static class SqlStudentRepository
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
                    cmd.CommandText = @"IF OBJECT_ID('dbo.Student','U') IS NULL
CREATE TABLE dbo.Student (
    StudentId nvarchar(100) PRIMARY KEY,
    Name nvarchar(200),
    Email nvarchar(200),
    Phone nvarchar(100),
    Address nvarchar(500),
    Gender nvarchar(50),
    Module nvarchar(200),
    Month nvarchar(100),
    PaymentStatus nvarchar(50)
);";
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Student> GetAll()
        {
            try
            {
                EnsureDatabaseAndTable();
                var list = new List<Student>();
                using (var conn = new SqlConnection(DbConnectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT StudentId, Name, Email, Phone, Address, Gender, Module, Month, PaymentStatus FROM dbo.Student";
                        using (var rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                var s = new Student
                                {
                                    StudentId = rdr.IsDBNull(0) ? null : rdr.GetString(0),
                                    Name = rdr.IsDBNull(1) ? null : rdr.GetString(1),
                                    Email = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                                    Phone = rdr.IsDBNull(3) ? null : rdr.GetString(3),
                                    Address = rdr.IsDBNull(4) ? null : rdr.GetString(4),
                                    Gender = rdr.IsDBNull(5) ? null : rdr.GetString(5),
                                    Module = rdr.IsDBNull(6) ? null : rdr.GetString(6),
                                    Month = rdr.IsDBNull(7) ? null : rdr.GetString(7),
                                    PaymentStatus = rdr.IsDBNull(8) ? null : rdr.GetString(8),
                                };
                                list.Add(s);
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

        public static void SaveAll(List<Student> items)
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
                    delCmd.CommandText = "DELETE FROM dbo.Student";
                    delCmd.ExecuteNonQuery();

                    using (var insCmd = conn.CreateCommand())
                    {
                        insCmd.Transaction = tran;
                        insCmd.CommandText = "INSERT INTO dbo.Student (StudentId, Name, Email, Phone, Address, Gender, Module, Month, PaymentStatus) VALUES (@StudentId,@Name,@Email,@Phone,@Address,@Gender,@Module,@Month,@PaymentStatus)";
                        insCmd.Parameters.Add(new SqlParameter("@StudentId", SqlDbType.NVarChar, 100));
                        insCmd.Parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 200));
                        insCmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 200));
                        insCmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar, 100));
                        insCmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar, 500));
                        insCmd.Parameters.Add(new SqlParameter("@Gender", SqlDbType.NVarChar, 50));
                        insCmd.Parameters.Add(new SqlParameter("@Module", SqlDbType.NVarChar, 200));
                        insCmd.Parameters.Add(new SqlParameter("@Month", SqlDbType.NVarChar, 100));
                        insCmd.Parameters.Add(new SqlParameter("@PaymentStatus", SqlDbType.NVarChar, 50));

                        foreach (var s in items)
                        {
                            insCmd.Parameters["@StudentId"].Value = (object)s.StudentId ?? "";
                            insCmd.Parameters["@Name"].Value = (object)s.Name ?? DBNull.Value;
                            insCmd.Parameters["@Email"].Value = (object)s.Email ?? DBNull.Value;
                            insCmd.Parameters["@Phone"].Value = (object)s.Phone ?? DBNull.Value;
                            insCmd.Parameters["@Address"].Value = (object)s.Address ?? DBNull.Value;
                            insCmd.Parameters["@Gender"].Value = (object)s.Gender ?? DBNull.Value;
                            insCmd.Parameters["@Module"].Value = (object)s.Module ?? DBNull.Value;
                            insCmd.Parameters["@Month"].Value = (object)s.Month ?? DBNull.Value;
                            insCmd.Parameters["@PaymentStatus"].Value = (object)s.PaymentStatus ?? DBNull.Value;
                            insCmd.ExecuteNonQuery();
                        }
                    }

                    tran.Commit();
                }
            }
        }
    }
}
