using UnityEngine;

/// <summary>
/// Controla el movimiento del jugador utilizando el CharacterController de Unity.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Velocidad de movimiento del jugador. Modificable desde el Inspector.
    /// </summary>
    [SerializeField] private float moveSpeed = 5f;

    /// <summary>
    /// Referencia al componente CharacterController que maneja las colisiones.
    /// </summary>
    [SerializeField] private CharacterController controller;

    /// <summary>
    /// Vector que almacena la dirección del movimiento.
    /// </summary>
    private Vector3 moveDirection;

    /// <summary>
    /// Se ejecuta al iniciar el script. Obtiene la referencia al CharacterController.
    /// </summary>
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Se ejecuta cada frame. Gestiona la actualización del movimiento.
    /// </summary>
    private void Update()
    {
        HandleMovement();
    }

    /// <summary>
    /// Procesa las entradas del jugador y aplica el movimiento.
    /// Utiliza los ejes horizontal (A,D o flechas) y vertical (W,S o flechas)
    /// para determinar la dirección del movimiento.
    /// </summary>
    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * horizontalInput + transform.forward * verticalInput;
        controller.Move(move.normalized * moveSpeed * Time.deltaTime);
    }
}
