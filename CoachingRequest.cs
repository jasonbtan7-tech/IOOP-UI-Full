using System;

namespace assignment
{
    public class CoachingRequest
    {
        public Guid Id { get; set; }
        public string Grade { get; set; }
        public string Lecturer { get; set; }
        public string Status { get; set; }
        public DateTime RequestedAt { get; set; }
    }
}
