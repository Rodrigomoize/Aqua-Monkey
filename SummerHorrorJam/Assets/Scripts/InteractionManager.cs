using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance {get; set;}

    public Item hoveredItem = null;

    private void Awake()
    {
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else{
            Instance=this;
        }
    }

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)){
            GameObject objectHitByRaycast = hit.transform.gameObject;

            if(objectHitByRaycast.GetComponent<Item>() && objectHitByRaycast.GetComponent<Item>().isActiveItem == false){
                hoveredItem = objectHitByRaycast.gameObject.GetComponent<Item>();
                hoveredItem.GetComponent<Outline>().enabled=true;

                if(Input.GetKeyDown(KeyCode.F)){
                    ItemManager.Instance.PickUpItem(objectHitByRaycast);
                }
            }
            else{
                if(hoveredItem){
                hoveredItem.GetComponent<Outline>().enabled=false;
                }
            }
        }
    }
}
