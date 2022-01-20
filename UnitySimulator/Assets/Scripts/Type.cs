using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type
{
    public string Name { get; private set; }
    public List<string> NoDamageTo { get; private set; }
    public List<string> NoDamageFrom { get; private set; }
    public List<string> SuperEffectiveTo { get; private set; }
    public List<string> NotVeryEffectiveTo { get; private set; }
    public List<string> StrongAgainst { get; private set; }
    public List<string> WeakAgainst { get; private set; }
    public Type(string name, List<string> noDamageTo,List<string> noDamageFrom,List<string> superEffectiveTo,List<string> notVeryEffectiveTo,List<string> strongAgainst,List<string> weakAgainst)
    {
        Name=name;
    }
    
}
