using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visorToggle : MonoBehaviour
{

    public GameObject visor;
    public AudioSource audioSource;
    public AudioClip nightVisionOffSound;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (visor.activeInHierarchy)
            {
                audioSource.PlayOneShot(nightVisionOffSound);
            }

            visor.SetActive(!visor.activeInHierarchy);
        }
    }
}
