

namespace CodingTracker
{
    internal class CodingTrackerController
    {
        public static void Menu()
        {
            var isTrackerOn = true;
            do
            {
                Display.DisplayMenu();
                var selection = Console.ReadLine().ToLower().Trim();

                switch(selection)
                {
                    case "0":
                        IEnumerable<CodingSessionModel> results = Database.ViewSessionRecords();
                        Display.ViewRecords(results);
                        break;
                    case "1":
                        CodingSessionModel UserSessionInput = Display.GetRecord();
                        Database.InsertRecord(UserSessionInput);
                        break;
                    case "2":
                        int SessionDeleteId = Display.GetSessionIdToDeleteRecord();
                        bool IsGivenIdPresent = Database.IsGivenSessionIdPresent(SessionDeleteId);
                        Display.DisplayNoRecordMessage(IsGivenIdPresent);
                        Database.DeleteRecord(SessionDeleteId, IsGivenIdPresent);
                        break;
                    case "3":
                        int SessionUpdateId = Display.GetSessionIdToUpdateRecord();
                        bool IsGivenIdToUpdatePresent = Database.IsGivenSessionIdPresent(SessionUpdateId);
                        Display.DisplayNoRecordMessage(IsGivenIdToUpdatePresent);
                        CodingSessionModel UserUpdatedInput = Display.GetRecord(IsGivenIdToUpdatePresent);
                        Database.UpdateRecord(IsGivenIdToUpdatePresent, UserUpdatedInput, SessionUpdateId);
                        break;
                    case "4":
                        Display.ExitApplicationMessage();                
                        isTrackerOn = false;
                        break;
                    default:
                        Display.InvalidInputMessage();
                        break;
                }

            }while(isTrackerOn);
        }
    }
}
