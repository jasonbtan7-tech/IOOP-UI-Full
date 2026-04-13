using System;

namespace assignment
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime SubmittedAt { get; set; }
        // Optional: the Id of the trainer who submitted this feedback (or null/empty if not applicable)
        public string TrainerId { get; set; }
    }
}
