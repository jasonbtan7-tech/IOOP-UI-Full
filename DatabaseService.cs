using System;
using System.IO;
using System.Data;

namespace assignment
{
    // Minimal DatabaseService used by AuthManager to locate the default data file.
    public static class DatabaseService
    {
        // Update this filename if your application expects a different XML filename.
        public const string DefaultDataFileName = "data.xml";

        // Returns the full path to the default data file (in the user's MyDocuments folder).
        public static string GetDataFilePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), DefaultDataFileName);
        }

        // Ensure the directory that will contain the data file exists.
        public static void EnsureDataDirectoryExists()
        {
            var dir = Path.GetDirectoryName(GetDataFilePath());
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        // Read and return a DataSet from the data file if it exists; otherwise return an empty DataSet.
        public static DataSet ReadDataSet()
        {
            var ds = new DataSet();
            var path = GetDataFilePath();
            if (File.Exists(path))
            {
                ds.ReadXml(path);
            }
            return ds;
        }

        // Save the provided DataSet to the default data file (writes schema as well).
        public static void SaveDataSet(DataSet ds)
        {
            if (ds == null) throw new ArgumentNullException(nameof(ds));
            EnsureDataDirectoryExists();
            ds.WriteXml(GetDataFilePath(), System.Data.XmlWriteMode.WriteSchema);
        }
    }
}
