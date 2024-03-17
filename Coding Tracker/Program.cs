using Spectre.Console;

namespace CodingTracker
{
    public class Program
    {
        static void Main(string[] args)
        {
            Database.CreateDatabase();
            CodingTrackerController.Menu();
        }
    }
   
}
