using UnityEngine;

/// <summary>
/// Clase que permite rotar un objeto de forma continua alrededor del eje Y.
/// </summary>
public class ObjectRotator : MonoBehaviour
{
    /// <summary>
    /// Velocidad de rotación en grados por segundo.
    /// El valor predeterminado es 50 grados por segundo.
    /// </summary>
    public float rotationSpeed = 50f;

    /// <summary>
    /// Se ejecuta una vez por frame para actualizar la rotación del objeto.
    /// Rota el objeto alrededor del eje Y (Vector3.up) multiplicado por 
    /// la velocidad de rotación y el tiempo transcurrido entre frames.
    /// </summary>
    private void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
