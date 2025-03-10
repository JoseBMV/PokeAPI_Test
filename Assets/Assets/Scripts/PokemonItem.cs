using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PokemonItem
{
    public int id;
    public string name;
    public string spriteUrl;
    public string description;
    public bool isCollected;
    public string[] types;
    public int height;
    public int weight;
    public int baseExperience;
    public StatEntry[] statsArray;  // Cambiado de Dictionary a array

    public PokemonItem(int id, string name, string spriteUrl, string description, string[] types, 
                      int height, int weight, int baseExperience, Dictionary<string, int> stats)
    {
        this.id = id;
        this.name = name;
        this.spriteUrl = spriteUrl;
        this.description = description;
        this.isCollected = false;
        this.types = types;
        this.height = height;
        this.weight = weight;
        this.baseExperience = baseExperience;
        
        // Convertir Dictionary a Array
        this.statsArray = new StatEntry[stats.Count];
        int i = 0;
        foreach(var kvp in stats)
        {
            this.statsArray[i] = new StatEntry { statName = kvp.Key, value = kvp.Value };
            i++;
        }
    }
}

[System.Serializable]
public class StatEntry
{
    public string statName;
    public int value;
}
