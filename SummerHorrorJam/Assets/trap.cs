using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{


    void OnTriggerEnter(Collider other)
    {
            Destroy(gameObject);
    }
}

