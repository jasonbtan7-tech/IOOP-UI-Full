using System.Collections.Generic;

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
            Trainer t = null;
            for (int i = 0; i < trainers.Count; i++)
            {
                if (trainers[i].Id == id)
                {
                    t = trainers[i];
                    break;
                }
            }
            if (t == null) return false;
            trainers.Remove(t);
            return true;
        }

        public static List<Trainer> GetAll()
        {
            return new List<Trainer>(trainers);
        }
    }
}
