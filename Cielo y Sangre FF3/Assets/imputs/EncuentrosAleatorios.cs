using UnityEngine;
using UnityEngine.SceneManagement;

public class EncuentrosAleatorios : MonoBehaviour
{
    public float probabilidadDeBatalla = 10f;
    public float distanciaPorPaso = 1.5f;

    private Vector3 posicionAnterior;
    private float distanciaAcumulada = 0f;

    void Start()
    {
        // 1. Si la memoria dice que venimos de una batalla, acomodamos a Kiyomi
        if (EstadoJuego.volverAColocarKiyomi == true)
        {
            transform.position = new Vector3(EstadoJuego.posicionXKiyomi, EstadoJuego.posicionYKiyomi, transform.position.z);
        }

        posicionAnterior = transform.position;
    }

    void Update()
    {
        if (!EstadoJuego.aliadoDesbloqueado) return;
        if (EstadoJuego.combatesAleatoriosPausados == true) return; // Si elegimos lobos

        float distanciaRecorrida = Vector3.Distance(transform.position, posicionAnterior);
        distanciaAcumulada += distanciaRecorrida;
        posicionAnterior = transform.position;

        if (distanciaAcumulada >= distanciaPorPaso)
        {
            distanciaAcumulada = 0f;
            TirarDadosDeBatalla();
        }
    }

    void TirarDadosDeBatalla()
    {
        float dado = Random.Range(0f, 100f);

        if (dado <= probabilidadDeBatalla)
        {
            // 2. Guardamos las coordenadas exactas de este instante en la memoria global
            EstadoJuego.posicionXKiyomi = transform.position.x;
            EstadoJuego.posicionYKiyomi = transform.position.y;
            EstadoJuego.volverAColocarKiyomi = true; // Avisamos que hay que reacomodarla al volver

            GetComponent<MovimientoJugador>().enabled = false;
            SceneManager.LoadScene("Batalla");
        }
    }
}