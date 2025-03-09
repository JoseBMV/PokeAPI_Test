using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }
    public List<PokemonItem> collectedItems = new List<PokemonItem>();

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

    public void AddItem(PokemonItem item)
    {
        item.isCollected = true;
        collectedItems.Add(item);
        SaveInventory();
    }

    private string SavePath => Path.Combine(Application.persistentDataPath, "pokemoninventory.json");

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

    // Método útil para encontrar el archivo de guardado
    public void PrintSavePath()
    {
        Debug.Log($"Save file location: {SavePath}");
    }
}

[System.Serializable]
public class SerializableInventory
{
    public List<PokemonItem> items;

    public SerializableInventory(List<PokemonItem> items)
    {
        this.items = items;
    }
}
