using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GestorDeBatalla : MonoBehaviour
{
    [Header("Interfaz y Textos")]
    public TextMeshProUGUI textoTurnos;

    [Header("Personajes de Batalla")]
    public SistemaDeVida scriptKiyomi;
    public SistemaDeVida scriptAliado; //Para saber la vida del Aliado
    public SistemaDeVida scriptEnemigo1;
    public SistemaDeVida scriptEnemigo2;
    public SistemaDeVida scriptEnemigo3; 
    public GameObject spriteAliadoBatalla; 

    [Header("Estadísticas")]
    public int danoKiyomi = 10;
    public int danoAliado = 15;
    public int danoEnemigos = 8;
    public int curacion = 15;

    public int turnoActual = 0;
    
    // Separamos la defensa para saber a quién le hacen la mitad de daño
    private bool defiendeKiyomi = false;
    private bool defiendeAliado = false;

    public int enemigoSeleccionado = 0; 

    void Start()
    {
        if (scriptKiyomi != null)
        {
            scriptKiyomi.hpActual = EstadoJuego.vidaActualKiyomi;
            scriptKiyomi.hpMaximo = EstadoJuego.vidaMaximaKiyomi;
            scriptKiyomi.ActualizarUI();
        }

        if (spriteAliadoBatalla != null) spriteAliadoBatalla.SetActive(EstadoJuego.aliadoDesbloqueado);
        IniciarTurnoKiyomi();
    }

    void Update()
    {
        // Vaciamos el Update, ahora las teclas las lee el MenuCombate
    }

    void IniciarTurnoKiyomi()
    {
        turnoActual = 0;
        defiendeKiyomi = false; 
        textoTurnos.text = "Turno de Kiyomi";
    }

    void IniciarTurnoAliado()
    {
        turnoActual = 1;
        defiendeAliado = false;
        textoTurnos.text = "Turno del Aliado";
    }

    public void Atacar(int dano)
    {
        textoTurnos.text = "¡Ataque!";

        if (enemigoSeleccionado == 0 && scriptEnemigo1 != null && scriptEnemigo1.hpActual > 0)
        {
            scriptEnemigo1.RecibirDano(dano);
        }
        else if (enemigoSeleccionado == 1 && scriptEnemigo2 != null && scriptEnemigo2.hpActual > 0)
        {
            scriptEnemigo2.RecibirDano(dano);
        }
        else if (enemigoSeleccionado == 2 && scriptEnemigo3 != null && scriptEnemigo3.hpActual > 0)
        {
            scriptEnemigo3.RecibirDano(dano);
        }

        SiguienteTurno();
    }

    public void Defender()
    {
        if (turnoActual == 0)
        {
            textoTurnos.text = "¡Kiyomi se defiende!";
            defiendeKiyomi = true;
        }
        else if (turnoActual == 1)
        {
            textoTurnos.text = "¡El Aliado se defiende!";
            defiendeAliado = true;
        }
        SiguienteTurno();
    }

    public void Curar()
    {
        if (turnoActual == 0)
        {
            textoTurnos.text = "¡Kiyomi se cura!";
            scriptKiyomi.Curar(curacion);
        }
        else if (turnoActual == 1)
        {
            textoTurnos.text = "¡El Aliado se cura!";
            if (scriptAliado != null) scriptAliado.Curar(curacion);
        }
        SiguienteTurno();
    }

    void SiguienteTurno()
    {
        if (RevisarVictoria()) return;

        int turnoQuePaso = turnoActual;
        turnoActual = 99; // Bloqueamos el menú por un segundo

        if (turnoQuePaso == 0)
        {
            // Solo le damos el turno al aliado si esta desbloqueado y vivo
            if (EstadoJuego.aliadoDesbloqueado && scriptAliado != null && scriptAliado.hpActual > 0) 
            {
                Invoke("IniciarTurnoAliado", 1f);
            }
            else
            {
                textoTurnos.text = "Turno Enemigo...";
                Invoke("AtaqueEnemigo", 1.5f);
            }
        }
        else if (turnoQuePaso == 1)
        {
            textoTurnos.text = "Turno Enemigo...";
            Invoke("AtaqueEnemigo", 1.5f);
        }
    }

    void AtaqueEnemigo()
    {
        if ((scriptEnemigo1 != null && scriptEnemigo1.hpActual > 0) ||
            (scriptEnemigo2 != null && scriptEnemigo2.hpActual > 0) ||
            (scriptEnemigo3 != null && scriptEnemigo3.hpActual > 0))
        {
            // Tiramos una moneda al aire (0 o 1)
            int dadoAleatorio = Random.Range(0, 2); 
            
            // Si sale 1 y el Aliado existe, está desbloqueado y tiene vida: le pegan a él.
            if (dadoAleatorio == 1 && EstadoJuego.aliadoDesbloqueado && scriptAliado != null && scriptAliado.hpActual > 0)
            {
                int danoFinal = defiendeAliado ? danoEnemigos / 2 : danoEnemigos;
                scriptAliado.RecibirDano(danoFinal);
                textoTurnos.text = "¡Lobo ataca al Aliado!";
            }
            else
            {
                int danoFinal = defiendeKiyomi ? danoEnemigos / 2 : danoEnemigos;
                scriptKiyomi.RecibirDano(danoFinal);
                textoTurnos.text = "¡Lobo ataca a Kiyomi!";
            }
        }

        if (scriptKiyomi.hpActual > 0)
        {
            Invoke("IniciarTurnoKiyomi", 1.5f);
        }
        else
        {
            turnoActual = 4; 
            textoTurnos.text = "¡Kiyomi ha caído!";
            Invoke("PantallaGameOver", 2f);
        }
    }

    bool RevisarVictoria()
    {
        bool todosMuertos = (scriptEnemigo1 == null || scriptEnemigo1.hpActual <= 0) &&
                            (scriptEnemigo2 == null || scriptEnemigo2.hpActual <= 0) &&
                            (scriptEnemigo3 == null || scriptEnemigo3.hpActual <= 0);

        if (todosMuertos)
        {
            turnoActual = 3;
            textoTurnos.text = "¡Victoria!";
            Invoke("VolverAlBosque", 2f);
            return true;
        }
        return false;
    }

    void VolverAlBosque()
    {
        if (scriptKiyomi != null) EstadoJuego.vidaActualKiyomi = scriptKiyomi.hpActual;
        string nombreEscena = SceneManager.GetActiveScene().name;
        if (nombreEscena == "BatallaFinal") DatosGlobales.jefeDerrotado = true;
        SceneManager.LoadScene("SampleScene");
    }

    void PantallaGameOver()
    {
        EstadoJuego.vidaActualKiyomi = EstadoJuego.vidaMaximaKiyomi;
        SceneManager.LoadScene("Derrota");
    }
}