using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Clase principal que gestiona las peticiones a la PokeAPI.
/// Implementa el patrón Singleton para asegurar una única instancia.
/// </summary>
public class PokeAPIManager : MonoBehaviour
{
    /// <summary>
    /// URL base de la API de Pokémon
    /// </summary>
    private const string API_BASE_URL = "https://pokeapi.co/api/v2/";

    /// <summary>
    /// Instancia única del manager (Singleton)
    /// </summary>
    public static PokeAPIManager Instance { get; private set; }

    /// <summary>
    /// Se ejecuta al iniciar el objeto, configura el Singleton
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Obtiene los datos de un Pokémon específico mediante su ID
    /// </summary>
    /// <param name="pokemonId">ID del Pokémon a buscar</param>
    /// <param name="callback">Función que se ejecutará cuando se obtengan los datos</param>
    public IEnumerator FetchPokemonData(int pokemonId, System.Action<PokemonItem> callback)
    {
        string url = $"{API_BASE_URL}pokemon/{pokemonId}";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string json = www.downloadHandler.text;
                PokemonJsonData pokemonData = JsonUtility.FromJson<PokemonJsonData>(json);
                
                // Procesar tipos
                string[] types = pokemonData.types.Select(t => t.type.name).ToArray();
                
                // Procesar stats
                Dictionary<string, int> stats = new Dictionary<string, int>();
                foreach (var stat in pokemonData.stats)
                {
                    stats[stat.stat.name] = stat.base_stat;
                }

                var pokemonItem = new PokemonItem(
                    pokemonId,
                    pokemonData.name,
                    pokemonData.sprites.front_default,
                    "A Pokemon item",
                    types,
                    pokemonData.height,
                    pokemonData.weight,
                    pokemonData.base_experience,
                    stats
                );
                callback(pokemonItem);
            }
        }
    }

    /// <summary>
    /// Carga un sprite desde una URL
    /// </summary>
    /// <param name="url">URL de la imagen a cargar</param>
    /// <param name="callback">Función que se ejecutará cuando se cargue el sprite</param>
    public IEnumerator LoadSprite(string url, System.Action<Sprite> callback)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                callback(sprite);
            }
        }
    }
}

/// <summary>
/// Clase que representa la estructura de datos JSON recibida de la API
/// </summary>
[System.Serializable]
class PokemonJsonData
{
    /// <summary>
    /// Nombre del Pokémon
    /// </summary>
    public string name;
    /// <summary>
    /// Sprites (imágenes) del Pokémon
    /// </summary>
    public Sprites sprites;
    /// <summary>
    /// Tipos del Pokémon
    /// </summary>
    public PokemonType[] types;
    /// <summary>
    /// Altura del Pokémon
    /// </summary>
    public int height;
    /// <summary>
    /// Peso del Pokémon
    /// </summary>
    public int weight;
    /// <summary>
    /// Experiencia base del Pokémon
    /// </summary>
    public int base_experience;
    /// <summary>
    /// Estadísticas base del Pokémon
    /// </summary>
    public PokemonStat[] stats;
}

/// <summary>
/// Clase que representa el tipo de un Pokémon
/// </summary>
[System.Serializable]
class PokemonType
{
    public TypeInfo type;
}

/// <summary>
/// Información detallada del tipo de Pokémon
/// </summary>
[System.Serializable]
class TypeInfo
{
    public string name;
}

/// <summary>
/// Clase que representa una estadística del Pokémon
/// </summary>
[System.Serializable]
class PokemonStat
{
    /// <summary>
    /// Valor base de la estadística
    /// </summary>
    public int base_stat;
    public StatInfo stat;
}

/// <summary>
/// Información detallada de la estadística
/// </summary>
[System.Serializable]
class StatInfo
{
    public string name;
}

/// <summary>
/// Clase que contiene las URLs de los sprites del Pokémon
/// </summary>
[System.Serializable]
class Sprites
{
    /// <summary>
    /// URL de la imagen frontal por defecto
    /// </summary>
    public string front_default;
}
