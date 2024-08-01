using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Trap : MonoBehaviour
{
    public float disableDuration = 10.0f; // Tiempo que el enemigo queda atrapado
    public bool isEnemyTrapped = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isEnemyTrapped)
        {
            // Si el enemigo entra en la trampa y no está atrapado, activar la trampa
            StartCoroutine(TrapEnemy(other));
        }
    }

    private IEnumerator TrapEnemy(Collider enemy)
    {
        isEnemyTrapped = true;
        Debug.Log("Enemigo atrapado");

        // Desactivar el movimiento del enemigo
        NavMeshAgent enemyAgent = enemy.GetComponent<NavMeshAgent>();
        if (enemyAgent != null)
        {
            enemyAgent.isStopped = true;
            enemy.GetComponent<Renderer>().material.color = Color.blue; // Cambia el color del enemigo (opcional)
        }

        // Esperar la duración de la desactivación
        yield return new WaitForSeconds(disableDuration);

        // Reactivar el movimiento del enemigo
        if (enemyAgent != null)
        {
            enemyAgent.isStopped = false;
            enemy.GetComponent<Renderer>().material.color = Color.red; // Restaurar el color original
        }

        Debug.Log("Enemigo liberado");
        Destroy(gameObject); // Destruir la trampa después de su uso
    }
}