namespace CodingTracker
{
    internal class CodingSessionModel
    {
        public int SessionId { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public DateOnly Date { get; set; }
    }
}
