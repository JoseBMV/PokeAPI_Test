using UnityEngine;

[System.Serializable]
public class PokemonItem
{
    public int id;
    public string name;
    public string spriteUrl;
    public string description;
    public bool isCollected;

    public PokemonItem(int id, string name, string spriteUrl, string description)
    {
        this.id = id;
        this.name = name;
        this.spriteUrl = spriteUrl;
        this.description = description;
        this.isCollected = false;
    }
}
