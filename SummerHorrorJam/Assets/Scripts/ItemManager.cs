using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml.Schema;
using Unity.VisualScripting;
using System.Diagnostics;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance {get; set;}

    public List<GameObject> itemSlots;

    public GameObject activeItemSlot;

    private void Awake()
    {
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        }
        else{
            Instance=this;
        }
    }

    private void Start()
    {
        activeItemSlot = itemSlots[0];
    }

    private void Update()
    {
        foreach (GameObject itemSlot in itemSlots){
            if(itemSlot==activeItemSlot){
                itemSlot.SetActive(true);
            }
            else{
                itemSlot.SetActive(false);
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            SwitchACtiveSlot(0);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)){
            SwitchACtiveSlot(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchACtiveSlot(2);
        }
    }

    public void PickUpItem(GameObject pickedUpItem)
    {
        AddItemIntoActiveSlot(pickedUpItem); 
    }

    private void AddItemIntoActiveSlot(GameObject pickedUpItem)
    {

        DropCurrentItem(pickedUpItem);

        pickedUpItem.transform.SetParent(activeItemSlot.transform,false);

        Item item = pickedUpItem.GetComponent<Item>();

        pickedUpItem.transform.localPosition = new Vector3(item.spawnPosition.x,item.spawnPosition.y,item.spawnPosition.z);
        pickedUpItem.transform.localRotation = Quaternion.Euler(item.spawnRotation.x,item.spawnRotation.y,item.spawnRotation.z);
  
        item.isActiveItem = true;
        item.animator.enabled = true;
    }

    private void DropCurrentItem(GameObject pickedUpItem)
    {
        if (activeItemSlot.transform.childCount > 0){
            var itemToDrop = activeItemSlot.transform.GetChild(0).gameObject;

            itemToDrop.GetComponent<Item>().isActiveItem=false;
            itemToDrop.GetComponent<Item>().animator.enabled=false;

            itemToDrop.transform.SetParent(pickedUpItem.transform.parent);
            itemToDrop.transform.localPosition = pickedUpItem.transform.localPosition;
            itemToDrop.transform.localRotation = pickedUpItem.transform.localRotation;
        }
    }

    private void SwitchACtiveSlot(int slotNumber)
    {
        if(activeItemSlot.transform.childCount>0){
            Item currentItem = activeItemSlot.transform.GetChild(0).GetComponent<Item>();
            currentItem.isActiveItem=false;
        }

        activeItemSlot = itemSlots[slotNumber];

        if(activeItemSlot.transform.childCount>0){
            Item newCurrentItem = activeItemSlot.transform.GetChild(0).GetComponent<Item>();
            newCurrentItem.isActiveItem=true;
        }
    }
}
