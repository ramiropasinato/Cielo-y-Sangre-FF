using UnityEngine;

public class GestorCaminos : MonoBehaviour
{
    [Header("Interfaz")]
    public GameObject panelEleccion;

    [Header("Paredes Invisibles")]
    public GameObject barreraTextos;
    public GameObject barreraLobos;

    private bool esperandoEleccion = false;

    void Start()
    {
        if (panelEleccion != null) panelEleccion.SetActive(false);
    }

    void Update()
    {
        // Solo escuchamos las teclas si el panel está abierto y el juego pausado
        if (esperandoEleccion == true)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                ElegirCaminoTextos();
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                ElegirCaminoLobos();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player") && esperandoEleccion == false)
        {
            Time.timeScale = 0f; // Pausa el juego
            panelEleccion.SetActive(true); // Muestra la UI
            esperandoEleccion = true; // Activa la lectura de teclas
        }
    }

    void ElegirCaminoTextos()
    {
        barreraTextos.SetActive(false);
        barreraLobos.SetActive(true);

        // Apagamos los combates aleatorios temporalmente
        EstadoJuego.combatesAleatoriosPausados = true;

        CerrarPanel();

    }

    void ElegirCaminoLobos()
    {
        barreraLobos.SetActive(false);
        barreraTextos.SetActive(true);

        EstadoJuego.combatesAleatoriosPausados = true;

        CerrarPanel();
    }

    void CerrarPanel()
    {
        panelEleccion.SetActive(false);
        Time.timeScale = 1f; // Despausamos
        esperandoEleccion = false;
        gameObject.SetActive(false); // Apagamos esta trampa para que no vuelva a saltar
    }
}
