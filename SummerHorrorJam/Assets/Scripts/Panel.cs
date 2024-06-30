using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Panel : MonoBehaviour
{
    public Transform panel;
    Item targeta;
    public bool isOn = false;
    void Start()
    {
        targeta = GameObject.Find("Target").GetComponent<Item>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (targeta.isActiveItem && targeta.isUsing)
            {
                if (!isOn)
                {
                    isOn = true;
                }
                if (isOn)
                {
                    isOn = false;
                }
            }
        }
    }

}

