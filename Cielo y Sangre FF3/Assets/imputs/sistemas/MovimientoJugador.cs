using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections; // Sumamos esto para poder usar tiempos y esperas

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private InputActionReference moveAction;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (DatosGlobales.vengoDeBatalla == true)
        {
            // En vez de moverla de golpe, disparamos esta secuencia especial
            StartCoroutine(ForzarTeletransporte());
        }
    }

    IEnumerator ForzarTeletransporte()
    {
        // MAGIA: Esperamos exactamente 1 frame. 
        // Esto permite que el motor de físicas haga su carga inicial tranquilo.
        yield return null;

        // Al frame siguiente, la forzamos a ir a donde queremos
        transform.position = DatosGlobales.posicionKiyomi;
        rb.position = DatosGlobales.posicionKiyomi;

        // Apagamos la variable para que no siga teletransportándose
        DatosGlobales.vengoDeBatalla = false;
    }

    private void FixedUpdate()
    {
        Vector2 moveValue = moveAction.action.ReadValue<Vector2>();
        Vector2 movimiento = Vector2.ClampMagnitude(moveValue, 1f) * speed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movimiento);
    }
}