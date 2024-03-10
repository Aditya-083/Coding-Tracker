namespace CodingTracker
{
    internal class CodingSessionModel
    {
        public int SessionId { get; set; }
        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public TimeSpan Duration { get; set; }


    }
}
