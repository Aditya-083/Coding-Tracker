using Dapper;
using System.Data.SQLite;
using System.Xml;

namespace CodingTracker 
{
    internal class Database
    {
        public static int count;
        protected static string connectionString;
        public static IEnumerable<CodingSessionModel> ViewRecords()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // Query the database and map the results to CodingSessionModel
                IEnumerable<CodingSessionModel> sessions = connection.Query<CodingSessionModel>("SELECT * FROM CodingTracker");
                connection.Close();
                return sessions;
            }
        }
        public static void InsertRecord(CodingSessionModel record)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                connection.Execute("INSERT INTO CodingTracker(StartTime, EndTime, Duration, CodingDate) VALUES (@StartTime, @EndTime, @Duration, @Date)",
                new { StartTime = record.StartTime.ToString(), EndTime = record.EndTime.ToString(), Duration = record.Duration.ToString(), Date = record.CodingDate.ToString() });

                connection.Close();
            }
        }

        public static void DeleteRecord(int id, bool IsSessionValidated)
        {
            if (IsSessionValidated)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    connection.Execute("DELETE FROM CodingTracker WHERE Id = @id", new { id });
                    connection.Close();
                }
            }
        }

        public static void UpdateRecord(bool IsSessionValidated, CodingSessionModel record)
        {

            if (IsSessionValidated)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sql = @"UPDATE CodingTracker SET StartTime = @StartTime, EndTime = @EndTime, Duration = @Duration, CodingDate = @CodingDate WHERE Id = @Id";

                    int rowsAffected = connection.Execute(sql, new
                    {
                        record.StartTime,
                        record.EndTime,
                        record.Duration,
                        record.CodingDate,
                        record.Id
                    });
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

        public static bool IsDatabaseEmpty()
        {
         
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                int rowCount = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM CodingTracker");

                if (rowCount == 0)         
                    return true;                   
                else                    
                    return false;                   
                connection.Close();
            }
            return false;
           
        }

        public static bool IsGivenSessionIdPresent(int id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM CodingTracker WHERE Id = @Id", new { Id = id });
                connection.Close();
            }
            if (count <= 0)
            {
                //Console.WriteLine("The given id is not present in the database. Press any key to continue.");
                //Console.ReadLine();
                return false;
            }
            return true;
        }
    }
}
