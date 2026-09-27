using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // Sirve para cambiar de escena al ganar

public class NPCFinal : MonoBehaviour
{
    [TextArea] public string[] lineasDialogo;
    public GameObject cartelHablar; // Para conectar el texto flotante de la [H]

    private bool enRango = false;

    void Update()
    {
        // Si estamos cerca, apretamos H y el panel NO está ya abierto
        if (enRango && Input.GetKeyDown(KeyCode.H) && !GestorDeDialogos.instancia.panelDialogo.activeSelf)
        {
            StartCoroutine(TerminarElJuego());
        }
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            enRango = true;
            if (cartelHablar != null) cartelHablar.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            enRango = false;
            if (cartelHablar != null) cartelHablar.SetActive(false);
        }
    }

    IEnumerator TerminarElJuego()
    {
        // Ocultamos el cartel de "Hablar [H]"
        if (cartelHablar != null) cartelHablar.SetActive(false);

        // Disparamos los textos finales del Rey
        GestorDeDialogos.instancia.IniciarDialogo(lineasDialogo);
        yield return null;

        // Pausamos el código hasta que el jugador cierre el cuadro de diálogo
        yield return new WaitUntil(() => !GestorDeDialogos.instancia.panelDialogo.activeSelf);

        // Carga la pantalla de victoria real
        SceneManager.LoadScene("Victory");
    }
}
