using UnityEngine;

public class InterruptorCombates : MonoBehaviour
{
    [Header("Pausa los combates al pisar")]
    public bool pausarCombates;

    void OnTriggerEnter2D(Collider2D otro)
    {
        if (otro.CompareTag("Player"))
        {
            EstadoJuego.combatesAleatoriosPausados = pausarCombates;
        }
    }
}