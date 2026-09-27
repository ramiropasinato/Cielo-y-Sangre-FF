using UnityEngine;
using TMPro;
using System.Collections;

public class GestorEvento2 : MonoBehaviour
{
    [Header("Personajes")]
    public SistemaDeVida scriptKiyomi;
    public SistemaDeVida scriptEnemigo1;
    public SistemaDeVida scriptEnemigo2;
    public GameObject circuloAzul;

    [Header("Recuadro de Diálogo Simple")]
    public GameObject panelAzul;
    public TextMeshProUGUI textoAdentro;

    // Podés cambiar este número desde el Inspector si va muy rápido o muy lento
    public float tiempoDeLectura = 2.5f;

    void Start()
    {
        if (circuloAzul != null) circuloAzul.SetActive(false);
        if (panelAzul != null) panelAzul.SetActive(false);

        StartCoroutine(SecuenciaCinematica());
    }

    IEnumerator SecuenciaCinematica()
    {
        yield return new WaitForSeconds(1f);

        panelAzul.SetActive(true);
        textoAdentro.text = "¡Los enemigos son muy fuertes!";

        // Espera los segundos marcados en la variable
        yield return new WaitForSeconds(tiempoDeLectura);

        panelAzul.SetActive(false);

        if (scriptKiyomi != null) scriptKiyomi.RecibirDano(scriptKiyomi.hpActual - 1);

        yield return new WaitForSeconds(1f);

        if (circuloAzul != null) circuloAzul.SetActive(true);

        panelAzul.SetActive(true);
        textoAdentro.text = "???: ¡Yo me encargo!";

        // Vuelve a esperar los segundos marcados en la variable
        yield return new WaitForSeconds(tiempoDeLectura);

        panelAzul.SetActive(false);

        if (scriptEnemigo1 != null) scriptEnemigo1.RecibirDano(999);
        if (scriptEnemigo2 != null) scriptEnemigo2.RecibirDano(999);

        yield return new WaitForSeconds(1.5f);

        EstadoJuego.aliadoDesbloqueado = true;
        if (scriptKiyomi != null) EstadoJuego.vidaActualKiyomi = scriptKiyomi.hpActual;

        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}