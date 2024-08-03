using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Xml.Schema;
using Unity.VisualScripting;
using System.Diagnostics;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; set; }

    public List<GameObject> itemSlots;
    public GameObject activeItemSlot;
    public float dropDistance = 2f; // Distancia para colocar el objeto frente al jugador
    public float dropHeightOffset = 1f; // Desplazamiento vertical para que caiga correctamente

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        activeItemSlot = itemSlots[0];
    }

    private void Update()
    {
        foreach (GameObject itemSlot in itemSlots)
        {
            if (itemSlot == activeItemSlot)
            {
                itemSlot.SetActive(true);
            }
            else
            {
                itemSlot.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchActiveSlot(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchActiveSlot(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchActiveSlot(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchActiveSlot(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchActiveSlot(4);
        }
    }

    public void PickUpItem(GameObject pickedUpItem)
    {
        AddItemIntoActiveSlot(pickedUpItem);
    }

    private void AddItemIntoActiveSlot(GameObject pickedUpItem)
    {
        DropCurrentItem(pickedUpItem);

        pickedUpItem.transform.SetParent(activeItemSlot.transform, false);

        Item item = pickedUpItem.GetComponent<Item>();

        pickedUpItem.transform.localPosition = new Vector3(item.spawnPosition.x, item.spawnPosition.y, item.spawnPosition.z);
        pickedUpItem.transform.localRotation = Quaternion.Euler(item.spawnRotation.x, item.spawnRotation.y, item.spawnRotation.z);

        item.isActiveItem = true;
        item.animator.enabled = true;
    }

    private void DropCurrentItem(GameObject pickedUpItem)
    {
        if (activeItemSlot.transform.childCount > 0)
        {
            var itemToDrop = activeItemSlot.transform.GetChild(0).gameObject;

            itemToDrop.GetComponent<Item>().isActiveItem = false;
            itemToDrop.GetComponent<Item>().animator.enabled = false;

            itemToDrop.transform.SetParent(pickedUpItem.transform.parent);
            itemToDrop.transform.localPosition = pickedUpItem.transform.localPosition;
            itemToDrop.transform.localRotation = pickedUpItem.transform.localRotation;
        }
    }

    private void SwitchActiveSlot(int slotNumber)
    {
        if (activeItemSlot.transform.childCount > 0)
        {
            Item currentItem = activeItemSlot.transform.GetChild(0).GetComponent<Item>();
            currentItem.isActiveItem = false;
        }

        activeItemSlot = itemSlots[slotNumber];

        if (activeItemSlot.transform.childCount > 0)
        {
            Item newCurrentItem = activeItemSlot.transform.GetChild(0).GetComponent<Item>();
            newCurrentItem.isActiveItem = true;
        }
    }

    public void DropActiveItem()
    {
        // Check if there is an active item in the slot
        if (activeItemSlot.transform.childCount > 0)
        {
            var itemToDrop = activeItemSlot.transform.GetChild(0).gameObject;
            var itemComponent = itemToDrop.GetComponent<Item>();

            // Set item as inactive
            itemComponent.isActiveItem = false;
            itemComponent.animator.enabled = false;

            // Detach the item from the slot
            itemToDrop.transform.SetParent(null);

            // Enable collider and physics
            Collider itemCollider = itemToDrop.GetComponent<Collider>();
            if (itemCollider != null)
            {
                itemCollider.enabled = true;
            }

            Rigidbody itemRigidbody = itemToDrop.GetComponent<Rigidbody>();
            if (itemRigidbody != null)
            {
                itemRigidbody.isKinematic = false;
            }

            // Position the item in front of the player
            Transform playerTransform = Camera.main.transform;
            Vector3 dropPosition = playerTransform.position + playerTransform.forward * dropDistance;
            dropPosition.y = playerTransform.position.y - dropHeightOffset; // Adjust for ground level
            itemToDrop.transform.position = dropPosition;

            // Add a slight forward force
            if (itemRigidbody != null)
            {
                itemRigidbody.AddForce(playerTransform.forward * 2f, ForceMode.Impulse);
            }
        }
    }
}
