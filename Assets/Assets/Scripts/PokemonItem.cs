using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Clase que representa un Pokémon individual con sus características básicas.
/// Esta clase es serializable para poder guardar/cargar datos fácilmente.
/// </summary>
[System.Serializable]
public class PokemonItem
{
    /// <summary>
    /// Número identificador único del Pokémon en la Pokédex
    /// </summary>
    public int id;

    /// <summary>
    /// Nombre del Pokémon
    /// </summary>
    public string name;

    /// <summary>
    /// URL de la imagen sprite del Pokémon
    /// </summary>
    public string spriteUrl;

    /// <summary>
    /// Descripción del Pokémon según la Pokédex
    /// </summary>
    public string description;

    /// <summary>
    /// Indica si el Pokémon ha sido coleccionado por el jugador
    /// </summary>
    public bool isCollected;

    /// <summary>
    /// Array de tipos elementales del Pokémon (puede tener uno o dos tipos)
    /// </summary>
    public string[] types;

    /// <summary>
    /// Altura del Pokémon en decímetros
    /// </summary>
    public int height;

    /// <summary>
    /// Peso del Pokémon en hectogramos
    /// </summary>
    public int weight;

    /// <summary>
    /// Experiencia base que otorga el Pokémon al ser derrotado
    /// </summary>
    public int baseExperience;

    /// <summary>
    /// Array de estadísticas base del Pokémon
    /// </summary>
    public StatEntry[] statsArray;

    /// <summary>
    /// Constructor que inicializa un nuevo Pokémon con todos sus atributos
    /// </summary>
    /// <param name="id">ID del Pokémon</param>
    /// <param name="name">Nombre del Pokémon</param>
    /// <param name="spriteUrl">URL de la imagen del Pokémon</param>
    /// <param name="description">Descripción del Pokémon</param>
    /// <param name="types">Tipos elementales del Pokémon</param>
    /// <param name="height">Altura del Pokémon</param>
    /// <param name="weight">Peso del Pokémon</param>
    /// <param name="baseExperience">Experiencia base</param>
    /// <param name="stats">Diccionario de estadísticas</param>
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

/// <summary>
/// Clase auxiliar que representa una entrada de estadística del Pokémon.
/// Contiene el nombre de la estadística y su valor numérico.
/// </summary>
[System.Serializable]
public class StatEntry
{
    /// <summary>
    /// Nombre de la estadística (HP, Ataque, Defensa, etc.)
    /// </summary>
    public string statName;

    /// <summary>
    /// Valor numérico de la estadística
    /// </summary>
    public int value;
}
