using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GestorDeBatalla : MonoBehaviour
{
    [Header("Interfaz y Textos")]
    public TextMeshProUGUI textoTurnos;

    [Header("Personajes de Batalla")]
    public SistemaDeVida scriptKiyomi;
    public SistemaDeVida scriptEnemigo1;
    public SistemaDeVida scriptEnemigo2;
    public SistemaDeVida scriptEnemigo3; // Acá va conectado el BossFinal
    public GameObject spriteAliadoBatalla;

    [Header("Estadísticas")]
    public int danoKiyomi = 10;
    public int danoAliado = 15;
    public int danoEnemigos = 8;
    public int curacion = 15;

    private int turnoActual = 0;
    private bool estaDefendiendo = false;

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
        // En el Update solo leemos las teclas normales
        if (turnoActual == 0)
        {
            if (Input.GetKeyDown(KeyCode.H)) { Atacar(danoKiyomi); }
            else if (Input.GetKeyDown(KeyCode.J)) { Defender(); }
            else if (Input.GetKeyDown(KeyCode.K)) { Curar(); }
        }
        else if (turnoActual == 1)
        {
            if (Input.GetKeyDown(KeyCode.H)) { Atacar(danoAliado); }
        }
    }

    void IniciarTurnoKiyomi()
    {
        turnoActual = 0;
        estaDefendiendo = false;
        textoTurnos.text = "Tu turno";
    }

    void IniciarTurnoAliado()
    {
        turnoActual = 1;
        textoTurnos.text = "Turno Aliado [H] Atacar";
    }

    void Atacar(int dano)
    {
        textoTurnos.text = "¡Ataque!";

        // Pega al primero vivo que encuentre en orden: 1, 2 o 3.
        if (scriptEnemigo1 != null && scriptEnemigo1.hpActual > 0) scriptEnemigo1.RecibirDano(dano);
        else if (scriptEnemigo2 != null && scriptEnemigo2.hpActual > 0) scriptEnemigo2.RecibirDano(dano);
        else if (scriptEnemigo3 != null && scriptEnemigo3.hpActual > 0) scriptEnemigo3.RecibirDano(dano);

        SiguienteTurno();
    }

    void Defender()
    {
        textoTurnos.text = "¡Kiyomi se defiende!";
        estaDefendiendo = true;
        SiguienteTurno();
    }

    void Curar()
    {
        textoTurnos.text = "¡Kiyomi se cura!";
        scriptKiyomi.Curar(curacion);
        SiguienteTurno();
    }

    void SiguienteTurno()
    {
        if (RevisarVictoria()) return;

        // Guardamos de quién era el turno antes de bloquearlo
        int turnoQuePaso = turnoActual;

        // Bloqueamos el teclado poniendo el turno en 99 para evitar el spam
        turnoActual = 99;

        if (turnoQuePaso == 0)
        {
            if (EstadoJuego.aliadoDesbloqueado) Invoke("IniciarTurnoAliado", 1f);
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
        // Los enemigos atacan si hay alguno vivo
        if ((scriptEnemigo1 != null && scriptEnemigo1.hpActual > 0) ||
            (scriptEnemigo2 != null && scriptEnemigo2.hpActual > 0) ||
            (scriptEnemigo3 != null && scriptEnemigo3.hpActual > 0))
        {
            int danoFinal = estaDefendiendo ? danoEnemigos / 2 : danoEnemigos;
            scriptKiyomi.RecibirDano(danoFinal);
        }

        // Si Kiyomi sobrevive, sigue peleando. Si no, ¡Game Over!
        if (scriptKiyomi.hpActual > 0)
        {
            Invoke("IniciarTurnoKiyomi", 1.5f);
        }
        else
        {
            turnoActual = 4; // Bloqueamos los turnos de nuevo
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

        // --- GUARDADO DE VICTORIA DEL JEFE ---
        string nombreEscena = SceneManager.GetActiveScene().name;
        if (nombreEscena == "BatallaFinal")
        {
            DatosGlobales.jefeDerrotado = true;
        }
        // -------------------------------------

        SceneManager.LoadScene("SampleScene");
    }

    void PantallaGameOver()
    {
        // Reiniciamos las variables globales por si el jugador vuelve a empezar el juego
        EstadoJuego.vidaActualKiyomi = EstadoJuego.vidaMaximaKiyomi;

        // Cargamos la pantalla de Game Over
        SceneManager.LoadScene("PantallaDerrota");
    }
}