using UnityEngine;
using TMPro;

public class SistemaDeVida : MonoBehaviour
{
    public string nombrePersonaje;
    public int hpMaximo = 50;
    public int hpActual;
    public TextMeshProUGUI textoVidaUI;

    [Header("Requisito TP1: Instantiate")]
    public GameObject prefabEfectoGolpe; // Acá arrastrás tu cubito azul (el cuadradito rojo)

    void Start()
    {
        hpActual = hpMaximo;
        ActualizarUI();
    }

    public void RecibirDano(int cantidad)
    {
        hpActual -= cantidad;
        if (hpActual < 0) hpActual = 0;

        // CUMPLE REQUISITO: Debug.Log de daño
        Debug.Log(nombrePersonaje + " recibió " + cantidad + " de daño. HP restante: " + hpActual);

        // CUMPLE REQUISITO: Instantiate con posición aleatoria
        if (prefabEfectoGolpe != null)
        {
            // 1. Calculamos un desfase al azar (entre -0.8 y 0.8 unidades) para X e Y
            float offsetX = Random.Range(-0.8f, 0.8f);
            float offsetY = Random.Range(-0.8f, 0.8f);

            // 2. Se lo sumamos a la posición central del personaje
            Vector3 posicionAleatoria = transform.position + new Vector3(offsetX, offsetY, 0);

            // 3. Creamos la mancha roja en esa posición corrida
            GameObject efecto = Instantiate(prefabEfectoGolpe, posicionAleatoria, Quaternion.identity);

            // 4. La destruimos a los 0.5 segundos para limpiar la pantalla
            Destroy(efecto, 0.5f);
        }

        ActualizarUI();
    }

    public void Curar(int cantidad)
    {
        hpActual += cantidad;
        if (hpActual > hpMaximo) hpActual = hpMaximo;

        // CUMPLE REQUISITO: Debug.Log de curación
        Debug.Log(nombrePersonaje + " se ha curado " + cantidad + " de HP. HP actual: " + hpActual);

        ActualizarUI();
    }

    public void ActualizarUI()
    {
        if (textoVidaUI != null)
        {
            textoVidaUI.text = nombrePersonaje + "\n" + hpActual + " / " + hpMaximo;
        }
    }
}