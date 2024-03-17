using System;

namespace CodingTracker
{
    internal class Validation
    {
        protected static string connectionString;
        protected static int id, count;
        protected static TimeOnly StartTime, EndTime;
        protected static TimeSpan Duration;
        protected static DateOnly Date;
        protected static string UserInput;
        public static bool TryParseTime(string input, out TimeOnly time)
        {
            time = default;

            if (DateTime.TryParseExact(input, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime parsedTime))
            {
                time = TimeOnly.FromDateTime(parsedTime);
                return true;
            }
            return false;
        }

        public static bool TryParseDate(string input, out DateOnly date)
        {
            date = default;

            if (DateTime.TryParseExact(input, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                date = DateOnly.FromDateTime(parsedDate);
                return true;
            }

            return false;
        }

        public static TimeSpan CalculateTimeSpan(TimeOnly startTime, TimeOnly endTime)
        {
            // Convert TimeOnly to DateTime for calculation
            DateTime startDateTime = DateTime.Today.Add(startTime.ToTimeSpan());
            DateTime endDateTime = DateTime.Today.Add(endTime.ToTimeSpan());

            // Calculate TimeSpan
            TimeSpan duration = endDateTime - startDateTime;
            return duration;
        }

        public static void ValidateInput()
        {
            while (true)
            {
                Console.WriteLine("Enter the Start-time in the format HH:mm (e.g., 12:30):");
                UserInput = Console.ReadLine();
                if (TryParseTime(UserInput, out StartTime))
                    break;
                Console.WriteLine("Invalid time format. Please try again.");
            }

            while (true)
            {
                Console.WriteLine("Enter the End-time in the format HH:mm (e.g., 12:45):");
                UserInput = Console.ReadLine();
                if (TryParseTime(UserInput, out EndTime))
                {
                    if (EndTime > StartTime)
                        break;
                    else
                        Console.WriteLine("End Time should be greater than start time. Please try again.");
                }
                else
                    Console.WriteLine("Invalid time format. Please try again.");
            }

            Duration = CalculateTimeSpan(StartTime, EndTime);

            while (true)
            {
                Console.WriteLine("Enter a date in the format yyyy-MM-dd (e.g., 2024-03-14):");
                UserInput = Console.ReadLine();

                if (TryParseDate(UserInput, out Date))
                    break;

                Console.WriteLine("Invalid date format. Please try again.");
            }
        }        
    }
}
