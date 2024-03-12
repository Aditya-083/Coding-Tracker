using System;
using System.Data.SQLite;
using System.Xml;

namespace CodingTracker
{
    internal class Database
    {
        private static string connectionString;

        public Database(){
            CreateDatabase();
        }
        public static void ViewRecords()
        {

        }

        public static void InsertRecord()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                TimeOnly STime = TimeOnly.FromDateTime(DateTime.Now);
                Console.WriteLine(STime);

                TimeOnly ETime = STime.Add(new TimeSpan(1, 15, 0));
                Console.WriteLine(ETime);

                TimeSpan Duration = new TimeSpan(1, 15, 0);
                Console.WriteLine(Duration);

                DateOnly Date = DateOnly.FromDateTime(DateTime.Now);
                Console.WriteLine(Date);

                var cmd = new SQLiteCommand(connection);
                cmd.CommandText = "INSERT INTO CodingTracker(StartTime, EndTime, Duration, CodingDate) VALUES (@StartTime, @EndTime, @Duration, @Date)";
                cmd.Parameters.AddWithValue("@StartTime", STime.ToString()); 
                cmd.Parameters.AddWithValue("@EndTime", ETime.ToString());
                cmd.Parameters.AddWithValue("@Duration", Duration.ToString());
                cmd.Parameters.AddWithValue("@Date", Date.ToString());

                cmd.ExecuteNonQuery();
                cmd.Dispose();
                connection.Close();
            }
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
                connectionString = xmlDoc.SelectSingleNode("/database/connectionString").InnerText;
                if (!File.Exists(dbPath))
                {
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        var cmd = new SQLiteCommand(connection);

                        cmd.CommandText = @"CREATE TABLE CodingTracker(Id INTEGER PRIMARY KEY, StartTime TimeOnly, EndTime TimeOnly, Duration  TimeSpan,CodingDate DateOnly)";
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
