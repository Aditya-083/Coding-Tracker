using System;
using System.Globalization;

namespace CodingTracker
{
    internal class Validation
    {
       
        protected static int id, count;
        protected static TimeOnly StartTime, EndTime,Time;
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

        public static bool ValidateStartTime(string time)
        {
            if (TryParseTime(time, out Time))
            {
                StartTime = Time;
                return true;
            }
            return false;
        }

        public static bool ValidateEndTime(string time)
        {
            if (TryParseTime(time, out Time))
            {
                if (Time > StartTime)
                {
                    EndTime = Time;
                    return true;
                }
            }
            return false;
        }

        public static string TotalTime()
        {
            return CalculateTimeSpan(StartTime, EndTime).ToString();
        }

        public static bool ValidateDate(string date)
        {
            if (TryParseDate(UserInput, out Date))
                return true;
            return false;
        }

        public static bool ValidateInteger(string SessionId)
        {
            if (int.TryParse(SessionId, out id))
                return true;
            return false;
        }
    }
}
