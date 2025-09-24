using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Estad�sticas del Jugador")]
    [SerializeField] private float vidaMaxima = 100f;
    [SerializeField] private float armaduraMaxima = 50f;

    private float vidaActual;
    private float armaduraActual;

    // Propiedades p�blicas para acceder a los valores (solo lectura)
    public float VidaActual => vidaActual;
    public float VidaMaxima => vidaMaxima;
    public float ArmaduraActual => armaduraActual;
    public float ArmaduraMaxima => armaduraMaxima;

 
 

    void Start()
    {
        Init();
    }

    /// <summary>
    /// Inicializa los valores de vida y armadura a sus m�ximos
    /// </summary>
    public void Init()
    {
        vidaActual = vidaMaxima;
        armaduraActual = armaduraMaxima;

        
        Debug.Log($"Jugador inicializado - Vida: {vidaActual}/{vidaMaxima}, Armadura: {armaduraActual}/{armaduraMaxima}");
    }

    /// <summary>
    /// Incrementa la vida actual sin exceder la vida m�xima
    /// </summary>
    /// <param name="cantidad">Cantidad de vida a incrementar</param>
    public void IncrementarVida(float cantidad)
    {
        if (cantidad <= 0) return;

        vidaActual = Mathf.Min(vidaActual + cantidad, vidaMaxima);

        Debug.Log($"Vida incrementada. Vida actual: {vidaActual}/{vidaMaxima}");
    }

    /// <summary>
    /// Incrementa la armadura actual sin exceder la armadura m�xima
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
    /// <param name="da�o">Cantidad de da�o a aplicar</param>
    public void RecibirDa�o(float da�o)
    {
        if (da�o <= 0 || EstaMuerto()) return;

        float da�oRestante = da�o;

        // Primero reducir armadura
        if (armaduraActual > 0)
        {
            float da�oArmadura = Mathf.Min(da�oRestante, armaduraActual);
            armaduraActual -= da�oArmadura;
            da�oRestante -= da�oArmadura;

            Debug.Log($"Armadura da�ada: -{da�oArmadura}. Armadura actual: {armaduraActual}/{armaduraMaxima}");
        }

        // Si queda da�o despu�s de romper la armadura, aplicar a la vida
        if (da�oRestante > 0)
        {
            vidaActual = Mathf.Max(0, vidaActual - da�oRestante);

            Debug.Log($"Vida da�ada: -{da�oRestante}. Vida actual: {vidaActual}/{vidaMaxima}");

            // Verificar si muri�
            if (EstaMuerto())
            {
                Debug.Log("�El jugador ha muerto!");
            }
        }
    }

    /// <summary>
    /// Verifica si el jugador est� muerto
    /// </summary>
    /// <returns>True si est� muerto, false si est� vivo</returns>
    public bool EstaMuerto()
    {
        return vidaActual <= 0;
    }

    /// <summary>
    /// Verifica si el jugador est� vivo
    /// </summary>
    /// <returns>True si est� vivo, false si est� muerto</returns>
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