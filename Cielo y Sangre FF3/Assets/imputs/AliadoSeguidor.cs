using UnityEngine;

public class AliadoSeguidor : MonoBehaviour
{
    [Header("Configuración")]
    public Transform kiyomi;
    public float velocidad = 5f;
    public float distanciaParaFrenar = 1.2f; // A qué distancia se detiene para no chocarla

    void Update()
    {
        // 1. Calculamos a qué distancia exacta está el aliado de Kiyomi
        float distancia = Vector2.Distance(transform.position, kiyomi.position);

        // 2. Si la distancia es mayor al límite, camina hacia ella
        if (distancia > distanciaParaFrenar)
        {
            transform.position = Vector3.MoveTowards(transform.position, kiyomi.position, velocidad * Time.deltaTime);
        }
    }
}
