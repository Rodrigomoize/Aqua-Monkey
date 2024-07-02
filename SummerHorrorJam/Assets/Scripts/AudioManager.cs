using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; set; }

    GameObject currentAudioReverbZone;
    
    public GameObject AudioUnderwater;

    public GameObject AudioAboveWater;

    public AudioSource audioSourcePlayerMovement;
    public AudioSource cigarreteUsingSound;
    public AudioSource vodkaUsingSound;
    public AudioSource targetUsingSound;
    public AudioSource mapOnSound;
    public AudioSource mapOffSound;
    public AudioSource breathingUnderwaterSound;
    public AudioSource emergeSound;
    public AudioSource submergeSound;

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
    
    }

    private void Update()
    {

    }

    public void PlayUseItemSound(Item.ItemType item)
    {
        switch (item){
            case Item.ItemType.Cigarrete:
                cigarreteUsingSound.Play();
                break;
            case Item.ItemType.Vodka:
                vodkaUsingSound.Play();
                break;
            case Item.ItemType.Target:
                targetUsingSound.Play();
                break;
        }
    }

    public void SetCurrentReverbZone(GameObject reverbZone)
    {
        // Iterate through all child objects of this GameObject
        foreach (Transform child in transform)
        {
            if (child.gameObject == reverbZone)
            {
                // Activate the specified reverb zone
                child.gameObject.SetActive(true);
                currentAudioReverbZone = reverbZone;
            }
            else
            {
                // Deactivate all other reverb zones
                child.gameObject.SetActive(false);
            }
        }
    }
}