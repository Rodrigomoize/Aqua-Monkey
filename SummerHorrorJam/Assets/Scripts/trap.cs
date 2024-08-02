using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int immobilizeDuration = 5;
    public GameObject trampa;
    public GameObject enemy;
    public ParticleSystem trapParticles; 
    public AudioSource trapSound;
    public bool active = true;


    void Start()
    {
        if (trapParticles != null)
        {
            trapParticles.Stop(); // Detener las part�culas al inicio
        }
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == ("Enemy"))
        {
           if (active)
            {
                trampa.SetActive(true);
                Debug.Log("Trampa activada por el enemigo.");

                StartCoroutine(ImmobilizeEnemy(other.gameObject)); // Iniciar el proceso de inmovilizaci�n
                active = false;
            }
            
        }
    }

    private IEnumerator ImmobilizeEnemy(GameObject enemy)
    {
        NewBehaviourScript enemyScript = enemy.GetComponent<NewBehaviourScript>();

        if (enemyScript != null)
        {
            enemyScript.Immobilize(immobilizeDuration); // Inmovilizar al enemigo

            // Aqu� puedes a�adir efectos visuales o de sonido
            Debug.Log("Enemigo inmovilizado por " + immobilizeDuration + " segundos.");

            if (trapParticles != null)
            {
                trapParticles.Play(); // Iniciar part�culas
            }

            if (trapSound != null)
            {
                trapSound.Play(); // Reproducir sonido
            }

            Debug.Log("Trampa activada. Da�o aplicado: ");

            // Destruir la trampa despu�s de usarla
            yield return new WaitForSeconds(immobilizeDuration);
            Destroy(gameObject);

            Debug.Log("Enemigo inmovilizado por " + immobilizeDuration + " segundos.");
        }
        else
        {
            Debug.LogWarning("No se encontr� el script de enemigo en el objeto colisionado.");
        }


    }
}
    



