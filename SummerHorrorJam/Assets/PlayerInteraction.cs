using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public int trapCount = 0;
    public int maxTraps = 3;

    public void UseTrap()
    {
        if (trapCount < maxTraps)
        {
            trapCount++;
            Debug.Log("Tienes tantas trampas: " + trapCount);

            // Aquí puedes añadir cualquier lógica adicional, como mostrar un UI de trampas
        }
    }

    public void DeployTrap()
    {
        if (trapCount > 0)
        {
            trapCount--;
            // Lógica para desplegar la trampa
        }
    }
}
