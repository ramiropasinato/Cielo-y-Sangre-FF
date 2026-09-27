using UnityEngine;

public class ArbolDeLaVida : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D otro)
    {
        // Si Kiyomi toca la zona del árbol
        if (otro.CompareTag("Player"))
        {
            // Curamos la vida en la memoria global
            EstadoJuego.vidaActualKiyomi = EstadoJuego.vidaMaximaKiyomi;
            Debug.Log("¡Kiyomi ha descansado! HP restaurado al máximo.");
        }
    }
}
