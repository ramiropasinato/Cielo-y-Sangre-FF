using UnityEngine;
using TMPro;

public class UIMapa : MonoBehaviour
{
    public TextMeshProUGUI textoVida;

    void Update()
    {
        // Actualiza el texto en tiempo real con los datos de la memoria global
        if (textoVida != null)
        {
            textoVida.text = "HP: " + EstadoJuego.vidaActualKiyomi + " / " + EstadoJuego.vidaMaximaKiyomi;
        }
    }
}
