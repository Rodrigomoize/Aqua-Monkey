using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform[] objetivos; // Array of all the objectives the enemy can go to
    public Transform player;
    public float velocidad;
    public float velocidadRoaming;
    public float velocidadPursuit;
    public float velocidadRagePursuit;
    bool isRoaming, isInPursuit, isInRagePursuit;
    public float distanciaObjetivo;
    public NavMeshAgent malomalote;

    private Transform objetivoActual;
    private Transform objetivoAnterior;
    private bool objetivoAlcanzado = true;
    public bool playerSpotted;
    private Coroutine resetPlayerSpottedCoroutine;

    private bool isImmobilized = false; // Estado para verificar si el enemigo est� inmovilizado
    private float immobilizeEndTime = 0f; // Tiempo hasta el que el enemigo estar� inmovilizado

    void Start()
    {
        // Start by defining the first objective to go to
        CambiarObjetivo();
    }

    void Update()
    {
        malomalote.speed = velocidad;

        lookForPlayer();

        if (isImmobilized)
        {
            // Revisar si el tiempo de inmovilizaci�n ha terminado
            if (Time.time >= immobilizeEndTime)
            {
                isImmobilized = false;
                malomalote.isStopped = false; // Reanudar movimiento del NavMeshAgent
            }
            else
            {
                return; // Salir de Update mientras est� inmovilizado
            }
        }

        if (playerSpotted)
        {
            pursuit();
        }
        else if (player.GetComponent<PlayerBleeding>().isBleeding)
        {
            ragePursuit();
        }
        else
        {
            roaming();
        }
    }

    void lookForPlayer()
    {
        bool playerDetected = false;
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float maxDistance = distanciaObjetivo;

        // Cast multiple rays in a fan shape in front of the enemy
        for (float angle = -30; angle <= 30; angle += 10)
        {
            Vector3 rayDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
            Ray ray = new Ray(transform.position, rayDirection);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    playerDetected = true;
                    break;
                }
            }
        }

        if (playerDetected)
        {
            playerSpotted = true;
            if (resetPlayerSpottedCoroutine != null)
            {
                StopCoroutine(resetPlayerSpottedCoroutine);
            }
            resetPlayerSpottedCoroutine = StartCoroutine(ResetPlayerSpotted());
        }
    }

    IEnumerator ResetPlayerSpotted()
    {
        yield return new WaitForSeconds(5f);
        playerSpotted = false;
    }

    void pursuit()
    {
        malomalote.SetDestination(player.position);
        velocidad = velocidadPursuit;
        isInPursuit = true;
        isInRagePursuit = false;
        isRoaming = false;
    }

    void ragePursuit()
    {
        malomalote.SetDestination(player.position);
        velocidad = velocidadRagePursuit;
        isInPursuit = false;
        isInRagePursuit = true;
        isRoaming = false;
    }

    void roaming()
    {
        velocidad = velocidadRoaming;
        if (!isRoaming)
        {
            CambiarObjetivo();
        }
        if (objetivoAlcanzado)
        {
            CambiarObjetivo(); // Call the function that changes the objective
            objetivoAlcanzado = false; // Set to false so it doesn't loop
        }
        else
        {
            if (objetivoActual != null && Vector3.Distance(transform.position, objetivoActual.position) < distanciaObjetivo)
            {
                objetivoAlcanzado = true;
            }
        }
        isInPursuit = false;
        isInRagePursuit = false;
        isRoaming = true;
    }

    void CambiarObjetivo()
    {
        do
        {
            objetivoActual = objetivos[Random.Range(0, objetivos.Length)];
        } while (objetivoActual == objetivoAnterior); // Do while, the eternal forgotten loop

        objetivoAnterior = objetivoActual;
        malomalote.SetDestination(objetivoActual.position);
    }

    public void Immobilize(float duration)
    {
        isImmobilized = true; // Marcar como inmovilizado
        immobilizeEndTime = Time.time + duration; // Establecer el tiempo de fin de inmovilizaci�n
        malomalote.isStopped = true; // Detener el movimiento del NavMeshAgent

        // Aqu� puedes a�adir efectos visuales o de sonido
        Debug.Log("Enemigo inmovilizado.");
    }
}