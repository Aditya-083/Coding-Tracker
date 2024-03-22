using System;
using System.Globalization;

namespace CodingTracker
{
    internal class Validation
    {
        private static int UserInputSessionId;
        private static TimeOnly StartSessionTime, EndSessionTime;
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
                if (parsedDate <= DateTime.Now.Date) // Check if parsed date is not greater than current date
                {
                    date = DateOnly.FromDateTime(parsedDate);
                    return true;
                }                                     
                
            }
            return false;
        }

        public static TimeSpan CalculateTimeSpan(TimeOnly StartSessionTime, TimeOnly EndSessionTime)
        {
            // Convert TimeOnly to DateTime for calculation
            DateTime startDateTime = DateTime.Today.Add(StartSessionTime.ToTimeSpan());
            DateTime endDateTime = DateTime.Today.Add(EndSessionTime.ToTimeSpan());

            // Calculate TimeSpan
            TimeSpan duration = endDateTime - startDateTime;
            return duration;
        }

        public static bool ValidateStartSessionTime(string time)
        {
            if (TryParseTime(time, out TimeOnly Time))
            {
                StartSessionTime = Time;
                return true;
            }
            return false;
        }

        public static bool ValidateEndSessionTime(string time)
        {
            if (TryParseTime(time, out TimeOnly Time))
            {
                if (Time > StartSessionTime)
                {
                    EndSessionTime = Time;
                    return true;
                }
            }
            return false;
        }

        public static string SessionTotalTime()
        {
            return CalculateTimeSpan(StartSessionTime, EndSessionTime).ToString();
        }

        public static bool ValidateInputDate(string date)
        {
            if (TryParseDate(date, out DateOnly Date))
                return true;
            return false;
        }

        public static bool ValidateInputInteger(string SessionId)
        {
            if (int.TryParse(SessionId, out UserInputSessionId)) 
                return true;
            return false;
        }

        public static int ReturnSessionID()
        {
            return UserInputSessionId;
        }
    }
}
