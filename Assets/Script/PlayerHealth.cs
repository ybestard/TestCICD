using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Estadísticas del Jugador")]
    [SerializeField] private float vidaMaxima = 100f;
    [SerializeField] private float armaduraMaxima = 50f;

    private float vidaActual;
    private float armaduraActual;

    // Propiedades públicas para acceder a los valores (solo lectura)
    public float VidaActual => vidaActual;
    public float VidaMaxima => vidaMaxima;
    public float ArmaduraActual => armaduraActual;
    public float ArmaduraMaxima => armaduraMaxima;

 
 

    void Start()
    {
        Init();
    }

    /// <summary>
    /// Inicializa los valores de vida y armadura a sus máximos
    /// </summary>
    public void Init()
    {
        vidaActual = vidaMaxima;
        armaduraActual = armaduraMaxima;

        
        Debug.Log($"Jugador inicializado - Vida: {vidaActual}/{vidaMaxima}, Armadura: {armaduraActual}/{armaduraMaxima}");
    }

    /// <summary>
    /// Incrementa la vida actual sin exceder la vida máxima
    /// </summary>
    /// <param name="cantidad">Cantidad de vida a incrementar</param>
    public void IncrementarVida(float cantidad)
    {
        if (cantidad <= 0) return;

        vidaActual = Mathf.Min(vidaActual + cantidad, vidaMaxima);

        Debug.Log($"Vida incrementada. Vida actual: {vidaActual}/{vidaMaxima}");
    }

    /// <summary>
    /// Incrementa la armadura actual sin exceder la armadura máxima
    /// </summary>
    /// <param name="cantidad">Cantidad de armadura a incrementar</param>
    public void IncrementarArmadura(float cantidad)
    {
        if (cantidad <= 0) return;

        armaduraActual = Mathf.Min(armaduraActual + cantidad, armaduraMaxima);

        Debug.Log($"Armadura incrementada. Armadura actual: {armaduraActual}/{armaduraMaxima}");
    }

    /// <summary>
    /// Decrementa primero la armadura y luego la vida
    /// </summary>
    /// <param name="daño">Cantidad de daño a aplicar</param>
    public void RecibirDaño(float daño)
    {
        if (daño <= 0 || EstaMuerto()) return;

        float dañoRestante = daño;

        // Primero reducir armadura
        if (armaduraActual > 0)
        {
            float dañoArmadura = Mathf.Min(dañoRestante, armaduraActual);
            armaduraActual -= dañoArmadura;
            dañoRestante -= dañoArmadura;

            Debug.Log($"Armadura dañada: -{dañoArmadura}. Armadura actual: {armaduraActual}/{armaduraMaxima}");
        }

        // Si queda daño después de romper la armadura, aplicar a la vida
        if (dañoRestante > 0)
        {
            vidaActual = Mathf.Max(0, vidaActual - dañoRestante);

            Debug.Log($"Vida dañada: -{dañoRestante}. Vida actual: {vidaActual}/{vidaMaxima}");

            // Verificar si murió
            if (EstaMuerto())
            {
                Debug.Log("¡El jugador ha muerto!");
            }
        }
    }

    /// <summary>
    /// Verifica si el jugador está muerto
    /// </summary>
    /// <returns>True si está muerto, false si está vivo</returns>
    public bool EstaMuerto()
    {
        return vidaActual <= 0;
    }

    /// <summary>
    /// Verifica si el jugador está vivo
    /// </summary>
    /// <returns>True si está vivo, false si está muerto</returns>
    public bool EstaVivo()
    {
        return !EstaMuerto();
    }

    /// <summary>
    /// Restaura completamente la vida y armadura
    /// </summary>
    public void RestaurarCompleto()
    {
        Init();
        Debug.Log("Jugador completamente restaurado");
    }

    /// <summary>
    /// Obtiene el porcentaje de vida actual (0-1)
    /// </summary>
    public float GetPorcentajeVida()
    {
        return vidaMaxima > 0 ? vidaActual / vidaMaxima : 0f;
    }

    /// <summary>
    /// Obtiene el porcentaje de armadura actual (0-1)
    /// </summary>
    public float GetPorcentajeArmadura()
    {
        return armaduraMaxima > 0 ? armaduraActual / armaduraMaxima : 0f;
    }
}