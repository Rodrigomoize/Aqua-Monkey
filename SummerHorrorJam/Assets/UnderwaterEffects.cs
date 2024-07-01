using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterEffects : MonoBehaviour
{
    //[SerializeField] GameObject waterFx;
    public AudioSource audioSource;
    public AudioClip breathingUnderwater;
    public OxygenSystem oxigen;
    public GameObject bubbles;
    public GameObject visor;

    public Transform waterPlane; // Reference to the water plane
    public Transform referenceObject; // Reference to the player's head or another reference point

    public bool underwater;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheckUnderwaterStatus();
    }

    private void CheckUnderwaterStatus()
    {
        if (referenceObject.position.y < waterPlane.position.y && !underwater)
        {
            EnterWater();
        }
        else if (referenceObject.position.y >= waterPlane.position.y && underwater)
        {
            ExitWater();
        }
    }

    private void EnterWater()
    {
        //waterFx.gameObject.SetActive(true);
        underwater = true;
        RenderSettings.fog = true;
        oxigen.isConsumingOxigen = true;
        visor.gameObject.SetActive(true);
        bubbles.gameObject.SetActive(true);
        audioSource.PlayOneShot(breathingUnderwater);
        audioSource.mute = false;
    }

    private void ExitWater()
    {
        //waterFx.gameObject.SetActive(false);
        underwater = false;
        RenderSettings.fog = false;
        oxigen.isConsumingOxigen = false;
        visor.gameObject.SetActive(false);
        bubbles.gameObject.SetActive(false);
        audioSource.mute = true;
    }
}