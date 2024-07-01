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

    public bool underwater;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other){
        //waterFx.gameObject.SetActive(true);
        underwater=true;
        RenderSettings.fog=true;
        oxigen.isConsumingOxigen=true;
        visor.gameObject.SetActive(true);
        bubbles.gameObject.SetActive(true);
        audioSource.PlayOneShot(breathingUnderwater);
        audioSource.mute=false;


    }

    private void OnTriggerExit(Collider other){
        //waterFx.gameObject.SetActive(false);
        underwater=false;
        RenderSettings.fog=false;
        oxigen.isConsumingOxigen=false;
        visor.gameObject.SetActive(false);
        bubbles.gameObject.SetActive(false);
        audioSource.mute=true;
    }
}
