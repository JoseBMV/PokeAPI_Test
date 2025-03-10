using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    public GameObject inventoryPanel;
    public Transform itemsContainer;
    public GameObject itemPrefab;

    [Header("UI Style")]
    [SerializeField] private Color backgroundColor = new Color(0.1f, 0.1f, 0.1f, 0.95f);
    [SerializeField] private Color textColor = new Color(1f, 1f, 1f, 1f);

    public static InventoryUI Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        inventoryPanel.SetActive(false);
        ApplyVisualStyle();
    }

    private void ApplyVisualStyle()
    {
        var panelImage = inventoryPanel.GetComponent<Image>();
        if (panelImage != null)
            panelImage.color = backgroundColor;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && inventoryPanel.activeSelf)
        {
            ToggleInventory();
        }
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
