using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JsonDataClass
{
    public int systime;
    public LoadCards value;
    public string type;
}

[System.Serializable]
public class LoadCards
{
    public List<string> cards;
}
