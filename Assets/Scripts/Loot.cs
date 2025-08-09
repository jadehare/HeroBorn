using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Loot
{
    public string Name;
    public int Rarity;

    public Loot(string name, int rarity)
    {
        Name = name;
        Rarity = rarity;
    }
} 