using UnityEngine;
using UnityEngine.UI;

public class MenuCombate : MonoBehaviour
{
    public RectTransform flechita;
    public RectTransform[] opciones;
    
    private int indiceActual = 0; 

    void Start()
    {
        MoverFlechita();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            indiceActual++;
            if (indiceActual >= opciones.Length) indiceActual = 0;
            MoverFlechita();
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            indiceActual--;
            if (indiceActual < 0) indiceActual = opciones.Length - 1;
            MoverFlechita();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            // Usamos el comando actualizado para Unity 6
            GestorDeBatalla gestor = FindFirstObjectByType<GestorDeBatalla>();

            if (indiceActual == 0) 
            {
                // Le pasamos el daño que tenés configurado en el Gestor
                gestor.Atacar(gestor.danoKiyomi); 
            }
            else if (indiceActual == 1) 
            {
                gestor.Defender();
            }
            else if (indiceActual == 2) 
            {
                gestor.Curar();
            }
        }
    }

    void MoverFlechita()
    {
        flechita.position = new Vector3(flechita.position.x, opciones[indiceActual].position.y, flechita.position.z);
    }
}