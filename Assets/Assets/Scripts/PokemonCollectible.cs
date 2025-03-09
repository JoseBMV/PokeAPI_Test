using UnityEngine;

public class PokemonCollectible : MonoBehaviour
{
    public int pokemonId;
    private bool isCollected = false;

    private void Start()
    {
        // Al iniciar, obtener los datos del Pokémon de la API
        StartCoroutine(PokeAPIManager.Instance.FetchPokemonData(pokemonId, OnPokemonDataReceived));
    }

    private void OnPokemonDataReceived(PokemonItem pokemonData)
    {
        // Aquí puedes configurar la apariencia del objeto coleccionable
        // Por ejemplo, cargar la sprite del Pokémon
    }

    public void Collect()
    {
        if (!isCollected)
        {
            isCollected = true;
            StartCoroutine(PokeAPIManager.Instance.FetchPokemonData(pokemonId, (pokemonItem) => {
                Inventory.Instance.AddItem(pokemonItem);
                // Desactivar el objeto coleccionable
                gameObject.SetActive(false);
            }));
        }
    }
}
