using Dapper;
using System.Data.SQLite;
using System.Xml;

namespace CodingTracker 
{
    internal class Database
    {
        protected static string connectionString;
        public static IEnumerable<CodingSessionModel> ViewSessionRecords()
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
                connection.Execute("INSERT INTO CodingTracker(SessionStartTime, SessionEndTime, SessionDuration, SessionCodingDate) VALUES (@SessionStartTime, @SessionEndTime, @SessionDuration, @SessionCodingDate)",
                new { SessionStartTime = record.SessionStartTime.ToString(), SessionEndTime = record.SessionEndTime.ToString(), SessionDuration = record.SessionDuration.ToString(), SessionCodingDate = record.SessionCodingDate.ToString() });

                connection.Close();
            }
        }

        public static void DeleteRecord(int SessionIdToBeDeleted, bool IsSessionValidated)
        {
            if (IsSessionValidated)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    connection.Execute("DELETE FROM CodingTracker WHERE SessionId = @SessionIdToBeDeleted", new { SessionIdToBeDeleted });
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
                    string sql = @"UPDATE CodingTracker SET SessionStartTime = @SessionStartTime, SessionEndTime = @SessionEndTime, SessionDuration = @SessionDuration, SessionCodingDate = @SessionCodingDate WHERE SessionId = @SessionId";

                    int rowsAffected = connection.Execute(sql, new
                    {
                        record.SessionStartTime,
                        record.SessionEndTime,
                        record.SessionDuration,
                        record.SessionCodingDate,
                        record.SessionId
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
                  
                        connection.Execute(@"CREATE TABLE CodingTracker (SessionId INTEGER PRIMARY KEY, SessionStartTime TEXT, SessionEndTime TEXT, SessionDuration TEXT,SessionCodingDate TEXT)");

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

        public static bool IsGivenSessionIdPresent(int SessionId)

        {
            int count;
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM CodingTracker WHERE SessionId = @Id", new { Id = SessionId });
                connection.Close();
            }
            if (count <= 0)
            {
                return false;
            }
            return true;
        }
    }
}
