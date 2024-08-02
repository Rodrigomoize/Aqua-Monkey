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
            trapParticles.Stop(); // Detener las partículas al inicio
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

                StartCoroutine(ImmobilizeEnemy(other.gameObject)); // Iniciar el proceso de inmovilización
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

            // Aquí puedes añadir efectos visuales o de sonido
            Debug.Log("Enemigo inmovilizado por " + immobilizeDuration + " segundos.");

            if (trapParticles != null)
            {
                trapParticles.Play(); // Iniciar partículas
            }

            if (trapSound != null)
            {
                trapSound.Play(); // Reproducir sonido
            }

            Debug.Log("Trampa activada. Daño aplicado: ");

            // Destruir la trampa después de usarla
            yield return new WaitForSeconds(immobilizeDuration);
            Destroy(gameObject);

            Debug.Log("Enemigo inmovilizado por " + immobilizeDuration + " segundos.");
        }
        else
        {
            Debug.LogWarning("No se encontró el script de enemigo en el objeto colisionado.");
        }


    }
}
    



