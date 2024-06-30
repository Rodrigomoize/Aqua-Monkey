using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door;
    public Transform panel;
    private bool isOn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (panel.GetComponent<Panel>().isOn && !isOn)
            {
                door.position += new Vector3(0, 5, 0);
                isOn = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isOn)
        {
            door.position += new Vector3(0, -5, 0);
            isOn = false;
        }
    }
}
