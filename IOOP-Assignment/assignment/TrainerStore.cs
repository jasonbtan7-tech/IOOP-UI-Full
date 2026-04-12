using System.Collections.Generic;
using System.Linq;

namespace assignment
{
    public static class TrainerStore
    {
        private static readonly List<Trainer> trainers = new List<Trainer>();

        public static void Add(Trainer t)
        {
            trainers.Add(t);
        }

        public static bool RemoveById(string id)
        {
            var t = trainers.FirstOrDefault(x => x.Id == id);
            if (t == null) return false;
            trainers.Remove(t);
            return true;
        }

        public static IReadOnlyList<Trainer> GetAll()
        {
            return trainers.AsReadOnly();
        }
    }
}
