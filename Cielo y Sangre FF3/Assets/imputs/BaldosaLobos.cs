using UnityEngine;
using UnityEngine.SceneManagement;

public class BaldosaLobos : MonoBehaviour
{
    void Start()
    {
        if (DatosGlobales.baldosaLobosRota == true) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            DatosGlobales.baldosaLobosRota = true;

            // Atrapamos la posición exacta de Kiyomi justo antes de irnos
            DatosGlobales.posicionKiyomi = GameObject.FindGameObjectWithTag("Player").transform.position;
            DatosGlobales.vengoDeBatalla = true;

            SceneManager.LoadScene("BatallaLobos");
        }
    }
}