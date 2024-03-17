using Dapper;
using System.Data.SQLite;
using System.Xml;

namespace CodingTracker 
{
    internal class Database : Validation
    {
        public static void ViewRecords()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Query the database and map the results to CodingSessionModel
                IEnumerable<CodingSessionModel> sessions = connection.Query<CodingSessionModel>("SELECT * FROM CodingTracker");

                foreach (var session in sessions)
                {
                    Console.WriteLine($"Session ID: {session.Id}");
                    Console.WriteLine($"Start Time: {session.StartTime}");
                    Console.WriteLine($"End Time: {session.EndTime}");
                    Console.WriteLine($"Duration: {session.Duration}");
                    Console.WriteLine($"Date: {session.CodingDate}");
                    Console.WriteLine();
                }

                connection.Close();
            }
        }
        public static void InsertRecord()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                ValidateInput();
                connection.Execute("INSERT INTO CodingTracker(StartTime, EndTime, Duration, CodingDate) VALUES (@StartTime, @EndTime, @Duration, @Date)",
                new { StartTime = StartTime.ToString(), EndTime = EndTime.ToString(), Duration = Duration.ToString(), Date = Date.ToString() });

                connection.Close();
            }
        }

        public static void DeleteRecord()
        {
            Console.WriteLine("Enter the id of record you want to Delete:");
            if (ValidateSessionId())
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    connection.Execute("DELETE FROM CodingTracker WHERE Id = @id", new { id });
                    connection.Close();
                }
            }
        }

        public static void UpdateRecord()
        {
            Console.WriteLine("enter the id of record you want to update:");
            if (ValidateSessionId())
            {
                ValidateInput();
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"UPDATE CodingTracker SET StartTime = @StartTime, EndTime = @EndTime, Duration = @Duration, CodingDate = @CodingDate WHERE Id = @Id";

                    int rowsAffected = connection.Execute(sql, new
                    { 
                        StartTime = StartTime.ToString(),
                        EndTime = EndTime.ToString(),
                        Duration = Duration.ToString(),
                        CodingDate = Date.ToString(),
                        Id = id
                    });

                    Console.WriteLine($"{rowsAffected} rows updated.");

                    connection.Close();
                }
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
                  
                        connection.Execute(@"CREATE TABLE CodingTracker (Id INTEGER PRIMARY KEY, StartTime TEXT, EndTime TEXT, Duration TEXT,CodingDate TEXT)");

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

        public static bool ValidateSessionId()
        {
            var input = Console.ReadLine();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                while (!int.TryParse(input, out id))
                {
                    Console.WriteLine("Invalid input pls enter integer value.");
                    input = Console.ReadLine();
                }
                count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM CodingTracker WHERE Id = @Id", new { Id = id });
                connection.Close();
            }
            if (count <= 0)
            {
                Console.WriteLine("The given id is not present in the database. Press any key to continue.");
                Console.ReadLine();
                return false;
            }
            return true;

        }
    }
}
