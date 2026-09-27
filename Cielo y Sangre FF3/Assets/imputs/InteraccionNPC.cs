using UnityEngine;

public class InteraccionNPC : MonoBehaviour
{
    [Header("Diálogo")]
    [TextArea] public string[] lineasDeDialogo;

    [Header("Interfaz Flotante")]
    public GameObject cartelHablar; // El textito de "Hablar [H]"

    private bool jugadorCerca = false;

    void Start()
    {
        // El cartel arranca invisible
        if (cartelHablar != null) cartelHablar.SetActive(false);
    }

    void Update()
    {
        // Si el jugador está cerca, aprieta H y el panel de diálogo principal NO está abierto
        if (jugadorCerca && Input.GetKeyDown(KeyCode.H))
        {
            if (!GestorDeDialogos.instancia.panelDialogo.activeSelf)
            {
                GestorDeDialogos.instancia.IniciarDialogo(lineasDeDialogo);
                if (cartelHablar != null) cartelHablar.SetActive(false); // Oculta el cartelito mientras habla
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = true;
            if (cartelHablar != null) cartelHablar.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorCerca = false;
            if (cartelHablar != null) cartelHablar.SetActive(false);
        }
    }
}