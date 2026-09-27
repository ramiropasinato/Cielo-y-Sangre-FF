using UnityEngine;

public class AparicionAliado : MonoBehaviour
{
    void Start()
    {
        if (!EstadoJuego.aliadoDesbloqueado)
        {
            gameObject.SetActive(false);
        }
        else if (EstadoJuego.volverAColocarKiyomi)
        {
            // Aparece exactamente al lado de Kiyomi (X - 1) para que no se superpongan
            transform.position = new Vector3(EstadoJuego.posicionXKiyomi - 1f, EstadoJuego.posicionYKiyomi, transform.position.z);
        }
    }
}