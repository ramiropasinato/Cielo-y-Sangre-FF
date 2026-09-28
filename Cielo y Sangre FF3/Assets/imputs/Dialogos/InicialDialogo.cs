using UnityEngine;

public class IniciadorDeDialogo : MonoBehaviour
{
    [Header("Textos")]
    [TextArea]
    public string[] lineasDeTexto;

    [Header("Configuración")]
    public bool leerConTeclaE = false; // Tildar para NPCs. Destildar para que salte solo al pisar.
    public bool seLeeUnaSolaVez = true;

    private bool jugadorCerca = false;
    private bool yaLeido = false;

    void Update()
    {
        // Si hay que apretar E y estamos cerca
        if (leerConTeclaE && jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            LanzarDialogo();
        }
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            jugadorCerca = true;
            // Si es automático, arranca de una
            if (!leerConTeclaE) LanzarDialogo();
        }
    }

    void OnTriggerExit2D(Collider2D otro)
    {
        if (otro.CompareTag("Player")) jugadorCerca = false;
    }

    void LanzarDialogo()
    {
        if (seLeeUnaSolaVez && yaLeido) return;
        yaLeido = true;

        //Le manda las frases al Cerebro sin tener cables conectados
        GestorDeDialogos.instancia.IniciarDialogo(lineasDeTexto);
    }
}
