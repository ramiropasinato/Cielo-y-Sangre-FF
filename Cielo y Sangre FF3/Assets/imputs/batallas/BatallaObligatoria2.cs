using UnityEngine;
using UnityEngine.SceneManagement;

public class BatallaObligatoria2 : MonoBehaviour
{
    void Start()
    {
        // Si ya nos salvó el caballero, esta baldosa no existe más
        if (DatosGlobales.evento2Completado == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("¡Emboscada 2 activada!");

            DatosGlobales.posicionKiyomi = collision.transform.position;
            DatosGlobales.vengoDeBatalla = true;
            DatosGlobales.evento2Completado = true;

            collision.GetComponent<MovimientoJugador>().enabled = false;

            // Nos manda a la escena de la emboscada final
            SceneManager.LoadScene("Batalla 2");
        }
    }
}
