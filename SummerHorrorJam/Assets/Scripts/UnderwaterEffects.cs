using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnderwaterEffects : MonoBehaviour
{
    [SerializeField] GameObject waterFx;
    public OxygenSystem oxigen;
    public GameObject bubbles;
    public GameObject visor;

    public Transform waterPlane; // Reference to the water plane
    public Transform referenceObject; // Reference to the player's head or another reference point

    public bool underwater;
    private bool isPlayingBreathingSound = false;

    void Start()
    {

    }

    void Update()
    {
        CheckUnderwaterStatus();

         if (!isPlayingBreathingSound && underwater)
        {
            StartCoroutine(PlayBreathingSound());
        }

    }

    private void CheckUnderwaterStatus()
    {
        if (referenceObject.position.y < waterPlane.position.y && !underwater)
        {
            EnterWater();
        }
        else if (referenceObject.position.y >= waterPlane.position.y && underwater)
        {
            ExitWater();
        }
    }

    private void EnterWater()
    {
        AudioManager.Instance.submergeSound.Play();
        waterFx.SetActive(true);
        underwater = true;
        RenderSettings.fog = true;
        oxigen.isConsumingOxigen = true;
        visor.SetActive(true);
        bubbles.SetActive(true);
        AudioManager.Instance.SetCurrentReverbZone(AudioManager.Instance.AudioUnderwater);
    }

    private void ExitWater()
    {
        AudioManager.Instance.emergeSound.Play();
        waterFx.SetActive(false);
        underwater = false;
        RenderSettings.fog = false;
        oxigen.isConsumingOxigen = false;
        visor.SetActive(false);
        bubbles.SetActive(false);
        AudioManager.Instance.SetCurrentReverbZone(AudioManager.Instance.AudioAboveWater);
    }

    private IEnumerator PlayBreathingSound()
    {
        isPlayingBreathingSound = true;

        while (underwater)
        {
            AudioManager.Instance.breathingUnderwaterSound.pitch = Random.Range(0.9f, 1f);
            AudioManager.Instance.breathingUnderwaterSound.volume = Random.Range(0.65f, 0.85f);
            AudioManager.Instance.breathingUnderwaterSound.Play();

            // Adjust interval as necessary; here, it's every 1 second
            yield return new WaitForSeconds(3.646f);
        }

        isPlayingBreathingSound = false;
    }
}