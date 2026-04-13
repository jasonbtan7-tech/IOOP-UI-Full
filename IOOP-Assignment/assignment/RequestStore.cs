using System;
using System.Collections.Generic;

namespace assignment
{
    public static class RequestStore
    {
        private static readonly List<CoachingRequest> requests = new List<CoachingRequest>();

        public static event Action RequestsChanged;

        public static void Add(CoachingRequest r)
        {
            requests.Add(r);
            RequestsChanged?.Invoke();
        }

        public static List<CoachingRequest> GetAll()
        {
            return new List<CoachingRequest>(requests);
        }

        public static void Clear()
        {
            requests.Clear();
            RequestsChanged?.Invoke();
        }
    }
}
