using UnityEngine;

/// <summary>
/// Clase que controla las funcionalidades principales del jugador.
/// Maneja la interacción con coleccionables y la interfaz del inventario.
/// </summary>
public class Player : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerLook look;

    /// <summary>
    /// Se ejecuta cuando se inicializa el objeto.
    /// Configura los componentes necesarios y bloquea el cursor del mouse.
    /// </summary>
    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        look = GetComponent<PlayerLook>();
        
        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// Se ejecuta una vez al inicio antes de la primera actualización.
    /// Actualmente no tiene implementación.
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Se ejecuta en cada frame del juego.
    /// Controla las entradas del jugador:
    /// - Tecla E: Revisa y recoge coleccionables cercanos
    /// - Tecla I: Alterna la visibilidad del inventario
    /// </summary>
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

    /// <summary>
    /// Busca objetos coleccionables en un radio de 2 unidades alrededor del jugador.
    /// Si encuentra un coleccionable de tipo Pokémon, lo recoge.
    /// </summary>
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
