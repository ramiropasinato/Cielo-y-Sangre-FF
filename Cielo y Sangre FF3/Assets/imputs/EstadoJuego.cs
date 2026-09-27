public static class EstadoJuego
{
    public static bool vioPrologo = false;
    public static bool aliadoDesbloqueado = false;

    public static int vidaActualKiyomi = 100;
    public static int vidaMaximaKiyomi = 100;

    // NUEVO: Variables para recordar dónde estaba parada Kiyomi
    public static bool volverAColocarKiyomi = false;
    public static float posicionXKiyomi = 0f;
    public static float posicionYKiyomi = 0f;
    public static bool combatesAleatoriosPausados = false;
}
