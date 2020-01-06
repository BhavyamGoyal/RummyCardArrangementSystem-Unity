using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CardAssets", menuName = "CustomObjects/Cards/CardAssets", order = 0)]
public class CardAssets : ScriptableObject
{
    //scriptable object to get the sprite of respective card

    public List<Suits> Cards;

    public Sprite GetCardSprite(SUITS s,int num)
    {
        return Cards[(int)s].cards[num - 1];
    }
}

