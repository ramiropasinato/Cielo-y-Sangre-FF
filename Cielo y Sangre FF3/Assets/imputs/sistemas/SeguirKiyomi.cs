using UnityEngine;

public class SeguirKiyomi : MonoBehaviour
{
    public Transform kiyomi; // Acá tiene que estar asignada la Prota
    public float velocidad = 4f;
    public float distanciaParaFrenar = 1.2f;

    void FixedUpdate()
    {
        if (kiyomi != null)
        {
            // Calculamos a qué distancia está
            if (Vector2.Distance(transform.position, kiyomi.position) > distanciaParaFrenar)
            {
                // Lerp hace que el movimiento sea resbaladizo y fluido
                transform.position = Vector2.Lerp(transform.position, kiyomi.position, velocidad * Time.fixedDeltaTime);
            }
        }
    }
}