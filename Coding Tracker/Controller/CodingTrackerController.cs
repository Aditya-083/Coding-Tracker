

namespace CodingTracker
{
    internal class CodingTrackerController
    {
        public static void Menu()
        {
            bool iSGivenSessionIdPresent;
            int id;
            CodingSessionModel model;
            IEnumerable<CodingSessionModel> results;
            var isTrackerOn = true;
            do
            {
                Display.DisplayMenu();
                var selection = Console.ReadLine().ToLower().Trim();

                switch(selection)
                {
                    case "0":
                        results = Database.ViewRecords();
                        Display.ViewRecords(results);
                        break;
                    case "1":
                        model = Display.GetRecord();
                        Database.InsertRecord(model);
                        break;
                    case "2":
                        id = Display.DeleteRecord();
                        iSGivenSessionIdPresent = Database.IsGivenSessionIdPresent(id);
                        Database.DeleteRecord(id, iSGivenSessionIdPresent);
                        break;
                    case "3":
                        id = Display.UpdateRecord();
                        iSGivenSessionIdPresent = Database.IsGivenSessionIdPresent(id);
                        model = Display.GetRecord();
                        Database.UpdateRecord(iSGivenSessionIdPresent, model);
                        break;
                    case "4":
                        Display.GoodBye();                
                        isTrackerOn = false;
                        break;
                    default:
                        Display.InvalidInput();
                        break;
                }

            }while(isTrackerOn);
        }
    }
}
