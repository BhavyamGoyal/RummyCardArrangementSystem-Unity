using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CardAssets", menuName = "CustomObjects/Cards/CardAssets", order = 0)]
public class CardAssets : ScriptableObject
{
    public List<Suits> Cards;  
}

[System.Serializable]
public class Suits
{
    public SUITS suit;
    public List<Sprite> cards;
}