using UnityEngine;

public class ControlDeZonas : MonoBehaviour
{
    [Header("Tildar si es el Pueblo/Camino Seguro")]
    public bool esZonaSegura = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Le avisa al juego en qué tipo de zona estamos
            DatosGlobales.enZonaSegura = esZonaSegura;
        }
    }
}