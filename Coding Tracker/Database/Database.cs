using System.Data.SQLite;
using System.Xml;

namespace CodingTracker
{
    internal class Database
    {
       
        public static void ViewRecords()
        {

        }

        public static void InsertRecord()
        {
            
        }

        public static void DeleteRecord()
        {
            
        }

        public static void UpdateRecord()
        {

        }

        public static void CreateDatabase()
        {
            try
            {

                string filePath = "./Appsettings.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);
                string dbPath = xmlDoc.SelectSingleNode("/database/name").InnerText;
                string connectionString = xmlDoc.SelectSingleNode("/database/connectionString").InnerText;
                Console.WriteLine(connectionString);
                if (!File.Exists(dbPath))
                {
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        var cmd = new SQLiteCommand(connection);

                        cmd.CommandText = @"CREATE TABLE CodingTracker(Id INTEGER PRIMARY KEY, )";
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadLine();
            }
        }
    }
}
