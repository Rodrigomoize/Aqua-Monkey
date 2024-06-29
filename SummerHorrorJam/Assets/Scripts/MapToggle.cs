using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToggle : MonoBehaviour
{

    public GameObject map;
    public AudioSource audioSource;
    public AudioClip mapOffSound;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (map.activeInHierarchy)
            {
                audioSource.PlayOneShot(mapOffSound);
            }

            map.SetActive(!map.activeInHierarchy);
        }
    }
}
