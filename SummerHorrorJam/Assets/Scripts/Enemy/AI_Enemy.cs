using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform[] objetivos; // Array de todos los objetivos que el enemigo puede visitar
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

    private bool isImmobilized = false; // Estado para verificar si el enemigo está inmovilizado
    private float immobilizeEndTime = 0f; // Tiempo hasta el que el enemigo estará inmovilizado

    void Start()
    {
        // Start by defining the first objective to go to
        CambiarObjetivo();
    }

    void Update()
    {
        if (isImmobilized)
        {
            // Revisar si el tiempo de inmovilización ha terminado
            if (Time.time >= immobilizeEndTime)
            {
                isImmobilized = false;
                malomalote.isStopped = false; // Reanudar movimiento del NavMeshAgent
                Debug.Log("Enemigo se ha recuperado de la inmovilización.");
            }
            else
            {
                // Permanecer inmovilizado
                return; // Salir de Update mientras esté inmovilizado
            }
        }

        malomalote.speed = velocidad;

        lookForPlayer();

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
                if (hit.transform.CompareTag("Player")) // Usar CompareTag para mejorar rendimiento
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
            CambiarObjetivo(); // Llamar a la función que cambia el objetivo
            objetivoAlcanzado = false; // Establecer en falso para que no haga loop
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
        } while (objetivoActual == objetivoAnterior); // Do while, el ciclo eterno olvidado

        objetivoAnterior = objetivoActual;
        malomalote.SetDestination(objetivoActual.position);
    }

    public void Immobilize(float duration)
    {
        if (!isImmobilized)
        {
            isImmobilized = true;
            immobilizeEndTime = Time.time + duration;
            malomalote.isStopped = true; // Detener el movimiento del NavMeshAgent

            // Aquí puedes añadir efectos visuales o de sonido
            Debug.Log("Enemigo inmovilizado por " + duration + " segundos.");
        }
    }
}