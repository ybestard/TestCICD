using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TestingPlayMode
{
    private GameObject playerGameObject;
    private PlayerHealth playerHealth;

    // Test para probar el sistema de vida del jugador - exactamente lo que pediste
    [Test]
    public void TestPlayerHealthSystemPlayMode()
    {
        // Crear el GameObject y componente
        playerGameObject = new GameObject("TestPlayer");
        playerHealth = playerGameObject.AddComponent<PlayerHealth>();

        // 1. Agregar vida y armadura inicial al player
        playerHealth.Init();

        // Verificar inicialización
        Debug.Log($"Estado inicial - Vida: {playerHealth.VidaActual}/{playerHealth.VidaMaxima}, Armadura: {playerHealth.ArmaduraActual}/{playerHealth.ArmaduraMaxima}");
        Assert.AreEqual(100f, playerHealth.VidaActual, "La vida inicial debe ser 100");
        Assert.AreEqual(50f, playerHealth.ArmaduraActual, "La armadura inicial debe ser 50");

        // 2. Restar 20 de vida (en realidad se resta de armadura primero)
        playerHealth.RecibirDaño(20f);

        Debug.Log($"Después de 20 de daño - Vida: {playerHealth.VidaActual}/{playerHealth.VidaMaxima}, Armadura: {playerHealth.ArmaduraActual}/{playerHealth.ArmaduraMaxima}");
        Assert.AreEqual(100f, playerHealth.VidaActual, "La vida debe seguir siendo 100");
        Assert.AreEqual(30f, playerHealth.ArmaduraActual, "La armadura debe ser 30 (50-20)");

        // 3. Incrementar 50 de vida
        playerHealth.IncrementarVida(50f);

        Debug.Log($"Después de incrementar 50 vida - Vida: {playerHealth.VidaActual}/{playerHealth.VidaMaxima}, Armadura: {playerHealth.ArmaduraActual}/{playerHealth.ArmaduraMaxima}");
        Assert.AreEqual(100f, playerHealth.VidaActual, "La vida debe mantenerse en 100 (máximo)");

        // 4. Saber si está muerto o vivo
        bool estaVivo = playerHealth.EstaVivo();
        bool estaMuerto = playerHealth.EstaMuerto();

        Debug.Log($"Estado del jugador: {(estaVivo ? "VIVO" : "MUERTO")}");
        Assert.IsTrue(estaVivo, "El jugador debe estar vivo");
        Assert.IsFalse(estaMuerto, "El jugador NO debe estar muerto");

        // Limpiar
        Object.DestroyImmediate(playerGameObject);

        Debug.Log("¡Test completado exitosamente!");
    }

    // Test adicional para verificar muerte
    [Test]
    public void TestPlayerDeathPlayMode()
    {
        // Crear el GameObject y componente
        playerGameObject = new GameObject("TestPlayer");
        playerHealth = playerGameObject.AddComponent<PlayerHealth>();

        // Inicializar
        playerHealth.Init();

        // Aplicar daño letal (más de 150 total: 50 armadura + 100 vida)
        playerHealth.RecibirDaño(200f);

        // Verificar muerte
        Debug.Log($"Después de daño letal - Vida: {playerHealth.VidaActual}, Armadura: {playerHealth.ArmaduraActual}");
        Debug.Log($"Estado del jugador: {(playerHealth.EstaVivo() ? "VIVO" : "MUERTO")}");

        Assert.AreEqual(0f, playerHealth.VidaActual, "La vida debe ser 0");
        Assert.IsTrue(playerHealth.EstaMuerto(), "El jugador debe estar muerto");
        Assert.IsFalse(playerHealth.EstaVivo(), "El jugador NO debe estar vivo");

        // Limpiar
        Object.DestroyImmediate(playerGameObject);

        Debug.Log("Test de muerte completado!");
    }

    // Test original que ya tenías
    [Test]
    public void NewTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
        Assert.IsTrue(true);
    }

    // Test original convertido a test síncrono
    [Test]
    public void NewTestScriptSyncPasses()
    {
        // Test síncrono simple
        Assert.IsTrue(true);
        Debug.Log("Test síncrono completado");
    }
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}