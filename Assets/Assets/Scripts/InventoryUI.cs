using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Clase que maneja la interfaz de usuario del inventario de Pokémon
/// </summary>
public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    // Panel principal que contiene toda la interfaz del inventario
    public GameObject inventoryPanel;
    // Contenedor donde se generarán los elementos del inventario
    public Transform itemsContainer;
    // Prefab que se usará como plantilla para cada elemento del inventario
    public GameObject itemPrefab;

    [Header("UI Style")]
    // Color de fondo del panel del inventario
    [SerializeField] private Color backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.95f);
    // Color del texto para los nombres de los Pokémon
    [SerializeField] private Color textColor = new Color(1f, 1f, 1f, 1f);

    // Instancia singleton para acceder a este componente desde otros scripts
    public static InventoryUI Instance { get; private set; }

    /// <summary>
    /// Se ejecuta al iniciar el objeto. Configura la instancia y oculta el inventario
    /// </summary>
    void Awake()
    {
        Instance = this;
        inventoryPanel.SetActive(false);
        ApplyVisualStyle();
    }

    /// <summary>
    /// Aplica los estilos visuales configurados al panel del inventario
    /// </summary>
    private void ApplyVisualStyle()
    {
        var panelImage = inventoryPanel.GetComponent<Image>();
        if (panelImage != null)
            panelImage.color = backgroundColor;
    }

    /// <summary>
    /// Se ejecuta cada frame. Detecta si se presiona ESC para cerrar el inventario
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && inventoryPanel.activeSelf)
        {
            ToggleInventory();
        }
    }

    /// <summary>
    /// Alterna la visibilidad del inventario y configura el cursor según corresponda
    /// </summary>
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

    /// <summary>
    /// Actualiza la visualización del inventario, creando un elemento UI para cada Pokémon recolectado
    /// </summary>
    void RefreshInventory()
    {
        // Elimina todos los elementos UI existentes
        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }

        // Crea un nuevo elemento UI para cada Pokémon en el inventario
        foreach (var item in Inventory.Instance.collectedItems)
        {
            GameObject itemUI = Instantiate(itemPrefab, itemsContainer);
            
            Image pokemonImage = itemUI.transform.Find("PokemonImage").GetComponent<Image>();
            TextMeshProUGUI pokemonName = itemUI.transform.Find("NameText").GetComponent<TextMeshProUGUI>();

            if (pokemonName != null)
            {
                pokemonName.text = item.name;
                pokemonName.color = textColor;
            }

            if (pokemonImage != null)
            {
                pokemonImage.preserveAspect = true;
                StartCoroutine(PokeAPIManager.Instance.LoadSprite(item.spriteUrl, 
                    (sprite) => pokemonImage.sprite = sprite));
            }
        }
    }
}
