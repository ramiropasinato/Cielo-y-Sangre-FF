using UnityEngine;

public class SeguirCamara : MonoBehaviour
{
    [Header("¿A quién seguimos?")]
    public Transform objetivo; // Acá le vamos a decir que siga a Kiyomi

    [Header("Configuración")]
    public float velocidad = 5f; // Para que la cámara se mueva suave y no pegue tirones

    void FixedUpdate()
    {
        // Si le asignamos un objetivo (Kiyomi), la cámara la persigue
        if (objetivo != null)
        {
            // Calculamos la nueva posición (copiando la X y la Y de Kiyomi, pero dejando la Z de la cámara intacta para que no se meta adentro del piso 2D)
            Vector3 posicionDeseada = new Vector3(objetivo.position.x, objetivo.position.y, transform.position.z);

            // Movemos la cámara suavemente hacia esa posición
            transform.position = Vector3.Lerp(transform.position, posicionDeseada, velocidad * Time.deltaTime);
        }
    }
}
