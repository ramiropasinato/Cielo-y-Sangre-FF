using UnityEngine;
using UnityEngine.UI; // Necesario para controlar las imagenes

public class HUDMapa : MonoBehaviour
{
    public Image barraVerde; 

    void Update()
    {
        // Revisamos que la barra verde este conectada para que no tire errores
        if (barraVerde != null)
        {
            // La matematica para sacar el porcentaje (ej: 50 / 100 = 0.5f)
            // Le ponemos (float) adelante para que Unity no se confunda con los decimales
            float porcentaje = (float)EstadoJuego.vidaActualKiyomi / (float)EstadoJuego.vidaMaximaKiyomi;
            
            // Le pasamos ese porcentaje a la barrita
            barraVerde.fillAmount = porcentaje;
        }
    }
}
