using System.Collections.Generic;

namespace Model
{
    public class Type
    {
        public string Name { get; }
        public List<string> NoDamageTo { get; }
        public List<string> NoDamageFrom { get; }
        public List<string> SuperEffectiveTo { get; }
        public List<string> NotVeryEffectiveTo { get; }
        public List<string> StrongAgainst { get; }
        public List<string> WeakAgainst { get; }

        public Type(
            string name,
            List<string> noDamageTo,
            List<string> noDamageFrom,
            List<string> superEffectiveTo,
            List<string> notVeryEffectiveTo,
            List<string> strongAgainst,
            List<string> weakAgainst)
        {
            Name = name;
            NoDamageTo = noDamageTo;
            NoDamageFrom = noDamageFrom;
            SuperEffectiveTo = superEffectiveTo;
            NotVeryEffectiveTo = notVeryEffectiveTo;
            StrongAgainst = strongAgainst;
            WeakAgainst = weakAgainst;
        }
    }
}