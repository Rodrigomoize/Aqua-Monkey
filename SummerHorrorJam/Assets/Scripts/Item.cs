using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    public bool isActiveItem;
    public bool isSpecial;

    public int uses;
    public bool canBeUsedUnderwater;
    private UnderwaterEffects underwater;
    public PlayerMovement player;
    public Camera playerCamera;

    public bool isUsing = false;
    private bool allowDestroy = true;

    public Animator animator;
    private float animDelay;

    public Vector3 spawnPosition;
    public Vector3 spawnRotation;

    public enum ItemType
    {
        Vodka,
        Cigarette,
        Target
    }

    public ItemType thisItemType;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        switch (thisItemType)
        {
            case ItemType.Vodka:
                uses = 1;
                canBeUsedUnderwater = false;
                break;
            case ItemType.Cigarette:
                uses = 3;
                canBeUsedUnderwater = false;
                break;
            case ItemType.Target:
                canBeUsedUnderwater = true;
                break;
        }

        // Find the Water_System GameObject
        GameObject waterSystemObject = GameObject.Find("WaterSystem");
        if (waterSystemObject != null)
        {
            // Find the underwater child GameObject and get its UnderwaterEffects component
            Transform underwaterTransform = waterSystemObject.transform.Find("Underwater");
            if (underwaterTransform != null)
            {
                underwater = underwaterTransform.GetComponent<UnderwaterEffects>();
                if (underwater == null)
                {
                    Debug.LogWarning("UnderwaterEffects component not found on the 'underwater' child GameObject.");
                }
            }
            else
            {
                Debug.LogWarning("'Underwater' child GameObject not found under 'Water_System'.");
            }
        }
        else
        {
            Debug.LogWarning("WaterSystem GameObject not found in the hierarchy.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActiveItem)
        {
            GetComponent<Outline>().enabled = false;

            if (Input.GetKeyDown(KeyCode.Mouse0) && !isUsing)
            {
                if (underwater == null || !underwater.underwater || (underwater.underwater && canBeUsedUnderwater))
                {
                    UseItem();
                    isUsing = true;
                }
            }
        }
    }

    private void UseItem()
    {
        AudioManager.Instance.PlayUseItemSound(thisItemType);
        animator.SetTrigger("USE");
        if (!isSpecial)
        {
            uses -= 1;
        }
        if (allowDestroy)
        {
            animDelay = GetDelay();
            Invoke("DestroyItem", animDelay);
            allowDestroy = false;
        }
    }

    private float GetDelay()
    {
        // Initialize the animation delay based on the animation clip length
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "Use")
            {
                animDelay = clip.length + 0.1f;
                break;
            }
        }
        return animDelay;
    }

    private void DestroyItem()
    {
        print("UsedItem!");
        if (uses < 1 && !isSpecial)
        {
            Destroy(gameObject); // Destroy the game object
        }
        else
        {
            isUsing = false;
            allowDestroy = true;
        }
    }
}