using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject inventoryPanel;    // Panel principal del inventario
    public Transform itemsContainer;     // Contenedor de items
    public GameObject itemPrefab;        // Prefab del item

    [Header("UI Item Components")]
    public float itemSpacing = 10f;     // Espacio entre items

    public static InventoryUI Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        inventoryPanel.SetActive(false);
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf)
        {
            RefreshInventory();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void RefreshInventory()
    {
        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in Inventory.Instance.collectedItems)
        {
            GameObject itemUI = Instantiate(itemPrefab, itemsContainer);
            
            // Configurar la imagen del Pokémon
            Image pokemonImage = itemUI.transform.Find("PokemonImage").GetComponent<Image>();
            Text pokemonName = itemUI.transform.Find("NameText").GetComponent<Text>();
            Text pokemonDescription = itemUI.transform.Find("DescriptionText").GetComponent<Text>();

            pokemonName.text = item.name;
            pokemonDescription.text = item.description;

            // Cargar la sprite del Pokémon
            StartCoroutine(PokeAPIManager.Instance.LoadSprite(item.spriteUrl, 
                (sprite) => pokemonImage.sprite = sprite));
        }
    }
}
