using UnityEngine;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Sistema de inventario que gestiona la colección de ítems Pokémon.
/// Implementa el patrón Singleton para asegurar una única instancia en el juego.
/// </summary>
public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    public List<PokemonItem> collectedItems = new List<PokemonItem>();

    /// <summary>
    /// Se ejecuta al iniciar el objeto. Implementa el patrón Singleton y carga el inventario guardado.
    /// </summary>
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("Inventory system initialized");
        }
        else
        {
            Debug.Log("Duplicate Inventory found, destroying object");
            Destroy(gameObject);
        }
        LoadInventory();
    }

    /// <summary>
    /// Añade un nuevo ítem Pokémon al inventario y guarda los cambios.
    /// </summary>
    /// <param name="item">El ítem Pokémon que se va a añadir al inventario</param>
    public void AddItem(PokemonItem item)
    {
        item.isCollected = true;
        collectedItems.Add(item);
        SaveInventory();
    }

    /// <summary>
    /// Ruta donde se guardará el archivo del inventario.
    /// </summary>
    private string SavePath => Path.Combine(Application.persistentDataPath, "pokemoninventory.json");

    /// <summary>
    /// Guarda el inventario actual en un archivo JSON.
    /// El archivo se guarda en la ruta especificada por SavePath.
    /// </summary>
    private void SaveInventory()
    {
        string inventoryJson = JsonUtility.ToJson(new SerializableInventory(collectedItems), true); // true for pretty print
        
        try
        {
            File.WriteAllText(SavePath, inventoryJson);
            Debug.Log($"Inventory saved to: {SavePath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error saving inventory: {e.Message}");
        }
    }

    /// <summary>
    /// Carga el inventario desde un archivo JSON si existe.
    /// Si no existe el archivo, inicializa un nuevo inventario vacío.
    /// </summary>
    private void LoadInventory()
    {
        try
        {
            if (File.Exists(SavePath))
            {
                string inventoryJson = File.ReadAllText(SavePath);
                SerializableInventory serializableInventory = JsonUtility.FromJson<SerializableInventory>(inventoryJson);
                collectedItems = serializableInventory.items;
                Debug.Log($"Inventory loaded from: {SavePath}");
            }
            else
            {
                Debug.Log("No saved inventory found, starting fresh");
                collectedItems = new List<PokemonItem>();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error loading inventory: {e.Message}");
            collectedItems = new List<PokemonItem>();
        }
    }

    /// <summary>
    /// Muestra en la consola la ubicación del archivo de guardado del inventario.
    /// Útil para depuración y verificación del sistema de guardado.
    /// </summary>
    public void PrintSavePath()
    {
        Debug.Log($"Save file location: {SavePath}");
    }
}

/// <summary>
/// Clase auxiliar para serializar el inventario a formato JSON.
/// Necesaria para el guardado y carga de datos.
/// </summary>
[System.Serializable]
public class SerializableInventory
{
    public List<PokemonItem> items;

    public SerializableInventory(List<PokemonItem> items)
    {
        this.items = items;
    }
}
