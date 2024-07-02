using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapToggle : MonoBehaviour
{
    public GameObject map;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (map.activeInHierarchy)
            {
                AudioManager.Instance.mapOffSound.Play();
            }
            else{
                AudioManager.Instance.mapOnSound.Play();
            }

            map.SetActive(!map.activeInHierarchy);
        }
    }
}
