using UnityEngine;

/// <summary>
/// Controla la rotación de la cámara del jugador basada en el movimiento del mouse.
/// </summary>
public class PlayerLook : MonoBehaviour
{
    /// <summary>
    /// Sensibilidad del movimiento del mouse. Un valor más alto hace que la cámara gire más rápido.
    /// </summary>
    [SerializeField] private float mouseSensitivity = 2f;

    /// <summary>
    /// Referencia a la transformación de la cámara del jugador.
    /// </summary>
    [SerializeField] private Transform playerCamera;
    
    /// <summary>
    /// Almacena la rotación vertical actual de la cámara.
    /// </summary>
    private float verticalRotation = 0f;

    /// <summary>
    /// Inicializa la cámara y configura el cursor del mouse.
    /// </summary>
    private void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main.transform;
            
        // Reset rotations to look forward
        verticalRotation = 0f;
        playerCamera.localRotation = Quaternion.Euler(0f, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        
        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// Se ejecuta cada frame para actualizar la rotación de la cámara.
    /// </summary>
    private void Update()
    {
        HandleMouseLook();
    }

    /// <summary>
    /// Procesa el movimiento del mouse y actualiza la rotación de la cámara.
    /// La rotación vertical está limitada entre -90 y 90 grados para evitar que la cámara de vueltas completas.
    /// El movimiento horizontal rota todo el objeto del jugador.
    /// </summary>
    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
