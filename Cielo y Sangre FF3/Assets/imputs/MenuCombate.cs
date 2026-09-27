using UnityEngine;
using UnityEngine.UI;

public class MenuCombate : MonoBehaviour
{
    [Header("Cosas visuales del Menu")]
    public RectTransform flechita;
    public RectTransform[] opciones;
    
    [Header("Textos de Enemigos en el HUD")]
    public RectTransform[] textosEnemigos; 
    
    [Header("Ajustes de Flecha")]
    public float separacionXAcciones = 40f; 
    public float separacionXEnemigos = 80f; 

    private int indiceActual = 0; 
    private int indiceEnemigo = 0; 
    private bool eligiendoEnemigo = false; 

    void Start()
    {
        MoverFlechita();
    }

    void Update()
    {
        GestorDeBatalla gestor = FindFirstObjectByType<GestorDeBatalla>();

        // ACA ESTA LA MAGIA: El menú funciona en el turno 0 (Kiyomi) y el 1 (Aliado)
        if (gestor.turnoActual != 0 && gestor.turnoActual != 1) return; 

        if (eligiendoEnemigo)
        {
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                indiceEnemigo++;
                if (indiceEnemigo >= textosEnemigos.Length) indiceEnemigo = 0;
                MoverFlechitaEnemigo();
            }
            else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                indiceEnemigo--;
                if (indiceEnemigo < 0) indiceEnemigo = textosEnemigos.Length - 1;
                MoverFlechitaEnemigo();
            }

            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                gestor.enemigoSeleccionado = indiceEnemigo; 
                
                // Elegimos con cuánta fuerza pegar dependiendo de quién ataca
                int danoParaPegar = 0;
                if (gestor.turnoActual == 0) danoParaPegar = gestor.danoKiyomi;
                if (gestor.turnoActual == 1) danoParaPegar = gestor.danoAliado;

                gestor.Atacar(danoParaPegar); 
                
                eligiendoEnemigo = false;
                MoverFlechita(); 
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                eligiendoEnemigo = false;
                MoverFlechita(); 
            }
            return; 
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            indiceActual++;
            if (indiceActual >= opciones.Length) indiceActual = 0;
            MoverFlechita();
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            indiceActual--;
            if (indiceActual < 0) indiceActual = opciones.Length - 1;
            MoverFlechita();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (indiceActual == 0) 
            {
                eligiendoEnemigo = true; 
                indiceEnemigo = 0;
                MoverFlechitaEnemigo();
            }
            else if (indiceActual == 1) 
            {
                gestor.Defender();
            }
            else if (indiceActual == 2) 
            {
                gestor.Curar();
            }
        }
    }

    void MoverFlechita()
    {
        flechita.position = new Vector3(opciones[indiceActual].position.x - separacionXAcciones, opciones[indiceActual].position.y, flechita.position.z);
    }

    void MoverFlechitaEnemigo()
    {
        flechita.position = new Vector3(textosEnemigos[indiceEnemigo].position.x - separacionXEnemigos, textosEnemigos[indiceEnemigo].position.y, flechita.position.z);
    }
}