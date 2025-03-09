using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerLook look;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Check for item collection
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckForCollectible();
        }

        // Toggle inventory UI
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryUI.Instance.ToggleInventory();
        }
    }

    void CheckForCollectible()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<PokemonCollectible>(out var collectible))
            {
                collectible.Collect();
            }
        }
    }
}
