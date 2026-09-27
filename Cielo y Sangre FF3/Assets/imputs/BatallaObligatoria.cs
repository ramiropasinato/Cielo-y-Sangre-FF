using UnityEngine;
using UnityEngine.SceneManagement;

public class BatallaObligatoria : MonoBehaviour
{
    void Start()
    {
        // Cuando carga el bosque, la baldosa pregunta: "¿Ya me pisaron antes?"
        // Si la respuesta es sí, se autodestruye antes de que puedas tocarla.
        if (DatosGlobales.evento1Completado == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("¡Pelea guionada activada!");

            // 1. Guardamos la posición exacta de Kiyomi
            DatosGlobales.posicionKiyomi = collision.transform.position;
            // 2. Avisamos que vamos a volver de una pelea
            DatosGlobales.vengoDeBatalla = true;
            // 3. Dejamos registrado que este evento ya ocurrió
            DatosGlobales.evento1Completado = true;

            collision.GetComponent<MovimientoJugador>().enabled = false;
            SceneManager.LoadScene("Batalla 1");
        }
    }
}