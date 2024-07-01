using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flashlightToggle : MonoBehaviour
{

    public GameObject flashlight;
    public AudioSource audioSource;
    public AudioClip flashlightOffSound;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (flashlight.activeInHierarchy)
            {
                audioSource.PlayOneShot(flashlightOffSound);
            }

            flashlight.SetActive(!flashlight.activeInHierarchy);
        }
    }
}
