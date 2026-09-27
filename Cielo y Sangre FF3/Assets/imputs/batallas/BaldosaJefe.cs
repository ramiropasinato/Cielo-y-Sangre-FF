using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BaldosaJefe : MonoBehaviour
{
    [TextArea] public string[] dialogoBoss;
    private bool trampaActivada = false;

    void Start()
    {
        if (DatosGlobales.jefeDerrotado == true) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player") && !DatosGlobales.jefeDerrotado && !trampaActivada)
        {
            trampaActivada = true; // Evita que la baldosa se dispare dos veces
            EstadoJuego.combatesAleatoriosPausados = true;
            StartCoroutine(SecuenciaJefe());
        }
    }

    IEnumerator SecuenciaJefe()
    {
        GestorDeDialogos.instancia.IniciarDialogo(dialogoBoss);
        yield return null;

        yield return new WaitUntil(() => !GestorDeDialogos.instancia.panelDialogo.activeSelf);

        // Guardamos la posición DESPUÉS del texto, justo antes de ir a pelear
        DatosGlobales.posicionKiyomi = GameObject.FindGameObjectWithTag("Player").transform.position;
        DatosGlobales.vengoDeBatalla = true;

        SceneManager.LoadScene("BatallaFinal");
    }
}