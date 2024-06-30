using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public Transform panel;
    public Item targeta;
    public bool isOn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (targeta.isActiveItem && targeta.isUsing)
            {
                isOn = true;
            }
        }
    }
}
