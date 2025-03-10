using UnityEngine;
using UnityEngine.UI;
using TMPro; // Añadir TMPro

public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject inventoryPanel;
    public Transform itemsContainer;
    public GameObject itemPrefab;

    [Header("UI Item Components")]
    public float itemSpacing = 10f;

    public static InventoryUI Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        if (inventoryPanel == null)
            Debug.LogError("inventoryPanel no está asignado en el Inspector");
        if (itemsContainer == null)
            Debug.LogError("itemsContainer no está asignado en el Inspector");
        if (itemPrefab == null)
            Debug.LogError("itemPrefab no está asignado en el Inspector");
            
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
        if (itemsContainer == null || itemPrefab == null)
        {
            Debug.LogError("Faltan referencias necesarias en InventoryUI");
            return;
        }

        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in Inventory.Instance.collectedItems)
        {
            GameObject itemUI = Instantiate(itemPrefab, itemsContainer);
            
            // Buscar referencias usando GetComponentInChildren para ser más flexibles
            Image pokemonImage = itemUI.GetComponentInChildren<Image>();
            TextMeshProUGUI pokemonName = itemUI.transform.Find("NameText")?.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI pokemonDescription = itemUI.transform.Find("DescriptionText")?.GetComponent<TextMeshProUGUI>();

            // Verificar que tenemos todas las referencias
            if (pokemonImage == null)
                Debug.LogError($"No se encontró el componente Image en el itemPrefab");
            if (pokemonName == null)
                Debug.LogError($"No se encontró el componente NameText (TextMeshProUGUI) en el itemPrefab");
            if (pokemonDescription == null)
                Debug.LogError($"No se encontró el componente DescriptionText (TextMeshProUGUI) en el itemPrefab");

            // Asignar valores solo si tenemos las referencias
            if (pokemonName != null)
                pokemonName.text = item.name;
            if (pokemonDescription != null)
                pokemonDescription.text = item.description;
            if (pokemonImage != null)
            {
                StartCoroutine(PokeAPIManager.Instance.LoadSprite(item.spriteUrl, 
                    (sprite) => pokemonImage.sprite = sprite));
            }
        }
    }
}
