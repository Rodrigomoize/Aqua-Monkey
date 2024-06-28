using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{

    public bool isActiveItem;

    public int uses=1;

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
        Cigarrete
    }

    public ItemType thisItemType;

    private void Awake()
    {
        animator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isActiveItem)
        {
            GetComponent<Outline>().enabled=false;

            if (Input.GetKeyDown(KeyCode.Mouse0) && !isUsing)
            {
                UseItem();
                isUsing = true;
            }
        }
        
    }

    private void UseItem()
    {
        animator.SetTrigger("USE");
        uses-=1;
        if (allowDestroy)
        {
            animDelay=GetDelay();
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
                animDelay = clip.length+0.1f;
                break;
            }
        }
        return animDelay;
    }

    private void DestroyItem()
    {
        print("UsedItem!");
        if(uses<1){
            Destroy(gameObject); // Destroy the game object
        }
        else{
            isUsing=false;
            allowDestroy=true;
        }
    }
}