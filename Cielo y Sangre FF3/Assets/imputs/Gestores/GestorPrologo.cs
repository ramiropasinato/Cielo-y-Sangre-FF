using UnityEngine;
using TMPro;
using System.Collections;

public class GestorPrologo : MonoBehaviour
{
    [Header("Conexiones")]
    public TextMeshProUGUI textoPantalla;
    public CanvasGroup panelFade;

    [Header("Textos de la Historia")]
    [TextArea] public string texto1 = "El reino ha caído. Kiyomi logró escapar...";
    [TextArea] public string texto2 = "Ahora, debe encontrar respuestas en el norte...";

    void Start()
    {
        if (EstadoJuego.vioPrologo)
        {
            Time.timeScale = 1f;
            gameObject.SetActive(false);
            return;
        }

        Time.timeScale = 0f;
        StartCoroutine(SecuenciaPrologo());
    }

    IEnumerator SecuenciaPrologo()
    {
        textoPantalla.text = ".";
        yield return new WaitForSecondsRealtime(1f);
        textoPantalla.text = "..";
        yield return new WaitForSecondsRealtime(1f);
        textoPantalla.text = "...";
        yield return new WaitForSecondsRealtime(1.5f);

        textoPantalla.text = "";
        foreach (char letra in texto1)
        {
            textoPantalla.text += letra;
            yield return new WaitForSecondsRealtime(0.05f);
        }

        textoPantalla.text += "\n\n(Presiona ESPACIO)";
        while (!Input.GetKeyDown(KeyCode.Space)) { yield return null; }

        textoPantalla.text = "";
        foreach (char letra in texto2)
        {
            textoPantalla.text += letra;
            yield return new WaitForSecondsRealtime(0.05f);
        }

        textoPantalla.text += "\n\n(Presiona ESPACIO)";
        while (!Input.GetKeyDown(KeyCode.Space)) { yield return null; }

        textoPantalla.text = "";
        float tiempoFade = 3f;
        float timer = 0f;

        while (timer < tiempoFade)
        {
            timer += Time.unscaledDeltaTime;
            panelFade.alpha = 1f - (timer / tiempoFade);
            yield return null;
        }

        EstadoJuego.vioPrologo = true; // Guardamos que ya se vio la historia
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}