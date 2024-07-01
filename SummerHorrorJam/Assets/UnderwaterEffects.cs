using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderwaterEffects : MonoBehaviour
{
    //[SerializeField] GameObject waterFx;
    public OxygenSystem oxigen;
    public GameObject visor;

    private void OnTriggerEnter(Collider other){
        //waterFx.gameObject.SetActive(true);
        RenderSettings.fog=true;
        oxigen.isConsumingOxigen=true;
        visor.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other){
        //waterFx.gameObject.SetActive(false);
        RenderSettings.fog=false;
        oxigen.isConsumingOxigen=false;
        visor.gameObject.SetActive(false);
    }
}
