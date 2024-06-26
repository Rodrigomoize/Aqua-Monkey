using System.Collections;
using UnityEngine;

public class PlayerBleeding : MonoBehaviour
{
    public GameObject bloodVFXPrefab;  // Assign the blood VFX prefab in the inspector
    public float spawnInterval = 0.5f; // Time interval between VFX spawns

    private bool isBleeding = false;
    private Coroutine bleedingCoroutine;

    void Update()
    {
        // Activate bleeding
        if (Input.GetKeyDown(KeyCode.B))
        {
            StartBleeding();
        }

        // Stop bleeding
        if (Input.GetKeyDown(KeyCode.H))
        {
            StopBleeding();
        }
    }

    void StartBleeding()
    {
        if (!isBleeding)
        {
            isBleeding = true;
            bleedingCoroutine = StartCoroutine(SpawnBloodVFX());
        }
    }

    void StopBleeding()
    {
        if (isBleeding)
        {
            isBleeding = false;
            if (bleedingCoroutine != null)
            {
                StopCoroutine(bleedingCoroutine);
            }
        }
    }

    IEnumerator SpawnBloodVFX()
    {
        while (isBleeding)
        {
            // Spawn the blood VFX at a specific Y position relative to the player's position
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 1.063327f, transform.position.z);
            GameObject bloodVFX = Instantiate(bloodVFXPrefab, spawnPosition, Quaternion.identity);

            // Ensure the VFX is enabled
            bloodVFX.SetActive(true);

            // Determine the lifetime of the VFX from the ParticleSystem component
            ParticleSystem particleSystem = bloodVFX.GetComponent<ParticleSystem>();
            float vfxLifetime = particleSystem != null ? particleSystem.main.duration + particleSystem.main.startLifetime.constantMax : 10.0f;

            Destroy(bloodVFX, vfxLifetime);

            // Wait for the next spawn interval
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}