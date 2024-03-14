using Dapper;
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
                TimeOnly ETime = STime.Add(new TimeSpan(1, 15, 0));
                TimeSpan Duration = new TimeSpan(1, 15, 0);
                DateOnly Date = DateOnly.FromDateTime(DateTime.Now);
                connection.Execute("INSERT INTO CodingTracker(StartTime, EndTime, Duration, CodingDate) VALUES (@StartTime, @EndTime, @Duration, @Date)",
                new { StartTime = STime.ToString(), EndTime = ETime.ToString(), Duration = Duration.ToString(), Date = Date.ToString() });

                connection.Close();
            }
        }

        public static void DeleteRecord()
        {
            
        }

        public static void UpdateRecord()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                int id;
                Console.WriteLine("Enter the id of record you want to Update:");
                var input = Console.ReadLine();
                while (!int.TryParse(input, out id))
                {
                    Console.WriteLine("Invalid input pls enter integer value.");
                    input = Console.ReadLine();
                }
               
                int count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM CodingTracker WHERE Id = @Id", new { Id = id });

                if (count <= 0)
                {
                    Console.WriteLine("The given id is not present in the database. Press any key to continue.");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine(count);
                Console.ReadLine();

                connection.Close();
            }
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
                  
                        connection.Execute(@"CREATE TABLE CodingTracker (Id INTEGER PRIMARY KEY, StartTime TimeOnly, EndTime TimeOnly, Duration TimeSpan,CodingDate DateOnly)");

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
