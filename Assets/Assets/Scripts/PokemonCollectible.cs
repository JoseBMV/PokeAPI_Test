using UnityEngine;

/// <summary>
/// Clase que maneja los objetos coleccionables de Pokémon en el juego.
/// Este script debe ser adjuntado a los GameObjects que representan Pokémon coleccionables.
/// </summary>
public class PokemonCollectible : MonoBehaviour
{
    /// <summary>
    /// ID único del Pokémon que será usado para obtener sus datos de la PokeAPI.
    /// Este valor debe ser configurado en el Inspector de Unity.
    /// </summary>
    public int pokemonId;

    /// <summary>
    /// Indica si el Pokémon ya ha sido recolectado por el jugador.
    /// </summary>
    private bool isCollected = false;

    /// <summary>
    /// Se ejecuta al iniciar el objeto en la escena.
    /// Realiza una llamada a la PokeAPI para obtener los datos del Pokémon.
    /// </summary>
    private void Start()
    {
        // Al iniciar, obtener los datos del Pokémon de la API
        StartCoroutine(PokeAPIManager.Instance.FetchPokemonData(pokemonId, OnPokemonDataReceived));
    }

    /// <summary>
    /// Callback que se ejecuta cuando se reciben los datos del Pokémon de la API.
    /// Aquí se puede personalizar la apariencia del coleccionable basado en los datos recibidos.
    /// </summary>
    /// <param name="pokemonData">Datos del Pokémon obtenidos de la API</param>
    private void OnPokemonDataReceived(PokemonItem pokemonData)
    {
        // Aquí puedes configurar la apariencia del objeto coleccionable
        // Por ejemplo, cargar la sprite del Pokémon
    }

    /// <summary>
    /// Método público para recolectar el Pokémon.
    /// Cuando se llama a este método:
    /// 1. Verifica si el Pokémon no ha sido recolectado previamente
    /// 2. Obtiene los datos actualizados del Pokémon de la API
    /// 3. Añade el Pokémon al inventario del jugador
    /// 4. Desactiva el objeto coleccionable en la escena
    /// </summary>
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
