using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickableobject : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isPickable = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag== "PlayerInteractionZone")// Esto lo que hace es que lea el tag de nuestro collider other y va a buscar en este el script pickupobjects para que busque object to pick up y lo asocie al player

        {
            other.GetComponentInParent<PickUpObject>().ObjectToPickUp = this.gameObject;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerInteractionZone")

        {
            other.GetComponentInParent<PickUpObject>().ObjectToPickUp = null;
        }
    }
}
