using UnityEngine;
using TMPro;
using System.Collections;

public class GestorDeDialogos : MonoBehaviour
{
    // Esta línea mágica permite que cualquier objeto del mapa encuentre este script sin conectarlo a mano
    public static GestorDeDialogos instancia;

    [Header("Interfaz")]
    public GameObject panelDialogo;
    public TextMeshProUGUI textoDialogo;

    private string[] lineasActuales;
    private int indiceLinea = 0;
    private bool escribiendo = false;

    void Awake()
    {
        instancia = this;
        if (panelDialogo != null) panelDialogo.SetActive(false);
    }

    // Cualquier personaje del mapa llama a esta función y le pasa sus textos
    public void IniciarDialogo(string[] lineas)
    {
        lineasActuales = lineas;
        indiceLinea = 0;
        Time.timeScale = 0f; // Pausa el mundo
        panelDialogo.SetActive(true);
        StartCoroutine(EscribirLinea());
    }

    IEnumerator EscribirLinea()
    {
        escribiendo = true;
        textoDialogo.text = "";

        foreach (char letra in lineasActuales[indiceLinea])
        {
            textoDialogo.text += letra;
            yield return new WaitForSecondsRealtime(0.03f); // Velocidad de escritura
        }

        escribiendo = false;
    }

    void Update()
    {
        // Solo escuchamos la barra si el panel está abierto
        if (panelDialogo.activeSelf && Input.GetKeyDown(KeyCode.H))
        {
            if (escribiendo)
            {
                // Si estaba escribiendo, completamos la frase de golpe
                StopAllCoroutines();
                textoDialogo.text = lineasActuales[indiceLinea];
                escribiendo = false;
            }
            else
            {
                // Si ya terminó de escribir, pasamos a la siguiente línea
                SiguienteLinea();
            }
        }
    }

    void SiguienteLinea()
    {
        indiceLinea++;

        if (indiceLinea < lineasActuales.Length)
        {
            StartCoroutine(EscribirLinea());
        }
        else
        {
            // Si no hay más líneas, cerramos todo
            panelDialogo.SetActive(false);
            Time.timeScale = 1f; // Despausa el mundo
        }
    }
}
