using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class PokeAPIManager : MonoBehaviour
{
    private const string API_BASE_URL = "https://pokeapi.co/api/v2/";

    public static PokeAPIManager Instance { get; private set; }

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

[System.Serializable]
class PokemonJsonData
{
    public string name;
    public Sprites sprites;
    public PokemonType[] types;
    public int height;
    public int weight;
    public int base_experience;
    public PokemonStat[] stats;
}

[System.Serializable]
class PokemonType
{
    public TypeInfo type;
}

[System.Serializable]
class TypeInfo
{
    public string name;
}

[System.Serializable]
class PokemonStat
{
    public int base_stat;
    public StatInfo stat;
}

[System.Serializable]
class StatInfo
{
    public string name;
}

[System.Serializable]
class Sprites
{
    public string front_default;
}
