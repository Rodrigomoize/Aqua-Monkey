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

            // Aqu� puedes a�adir cualquier l�gica adicional, como mostrar un UI de trampas
        }
    }

    public void DeployTrap()
    {
        if (trapCount > 0)
        {
            trapCount--;
            // L�gica para desplegar la trampa
        }
    }
}
