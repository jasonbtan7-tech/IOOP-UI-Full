using System;
using System.Collections.Generic;

namespace assignment
{
    public static class FeedbackStore
    {
        private static readonly List<Feedback> items = new List<Feedback>();

        public static void Add(Feedback f)
        {
            items.Add(f);
        }

        public static IReadOnlyList<Feedback> GetAll()
        {
            return items.AsReadOnly();
        }

        public static void Clear()
        {
            items.Clear();
        }
    }
}
