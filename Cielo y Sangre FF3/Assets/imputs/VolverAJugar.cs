using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverAlInicio : MonoBehaviour
{
    void Update()
    {
        // Si tocamos la H en la pantalla final o de Game Over
        if (Input.GetKeyDown(KeyCode.H))
        {
            // 1. Reiniciamos todas las variables para que el juego arranque de cero
            EstadoJuego.vidaActualKiyomi = EstadoJuego.vidaMaximaKiyomi;
            EstadoJuego.aliadoDesbloqueado = false;

            // (Si alguna de estas variables te tira error en rojo, borrá esa línea. 
            // Depende de si las guardaste en EstadoJuego o DatosGlobales)
            DatosGlobales.jefeDerrotado = false;
            DatosGlobales.vengoDeBatalla = false;
            DatosGlobales.lobosDerrotados = false;
            DatosGlobales.baldosaLobosRota = false;
            

            // 2. Cargamos la escena principal del bosque
            SceneManager.LoadScene("SampleScene");
        }
    }
}
