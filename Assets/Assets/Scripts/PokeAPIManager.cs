using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using System.Collections;

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
                // Parse JSON and create PokemonItem
                // Simplified for example
                var pokemonItem = new PokemonItem(
                    pokemonId,
                    JsonUtility.FromJson<PokemonJsonData>(json).name,
                    JsonUtility.FromJson<PokemonJsonData>(json).sprites.front_default,
                    "A Pokemon item"
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
}

[System.Serializable]
class Sprites
{
    public string front_default;
}
