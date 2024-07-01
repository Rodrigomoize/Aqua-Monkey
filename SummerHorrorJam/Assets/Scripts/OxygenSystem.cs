using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OxygenSystem : MonoBehaviour
{
    public AudioSource audioSource;
    private PlayerMovement playerMovement;
    public OxygenStation oxygenStation;
    public float currentoxigeno = 100;
    public float oxigenoMax = 100;
    public float oxigenoMin = 0;
    public float NoConsumo = 0.0f;
    public float ConsumoOxigenoStatic = 0.1f;
    public float ConsumoOxigenoRun = 0.5f;
    public float ConsumoOxigenoWalk = 0.2f;

    public float CurrentConsume = 0;

    public bool isRecoveringOxigen = false;
    public bool isConsumingOxigen = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        oxygenStation = GetComponent<OxygenStation>();

        currentoxigeno = oxigenoMax;
        CurrentConsume = ConsumoOxigenoStatic;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentoxigeno > oxigenoMin)
        {
            isConsumingOxigen = true;
            if (playerMovement.currentSpeed == playerMovement.speed && playerMovement.isMoving == true && isRecoveringOxigen == false)
            {
                CurrentConsume = ConsumoOxigenoWalk;
            }
            else if (playerMovement.currentSpeed == playerMovement.sprintSpeed && playerMovement.isMoving == true && isRecoveringOxigen == false)
            {
                CurrentConsume = ConsumoOxigenoRun;
            }
            else if (playerMovement.isMoving == false && isRecoveringOxigen == false)
            {
                CurrentConsume = ConsumoOxigenoStatic;
            }
            else if (isRecoveringOxigen == true)
            {
                CurrentConsume = NoConsumo;
            }
            //check if AddOxygen function is called
            
            currentoxigeno -= CurrentConsume * Time.deltaTime;
        }
        else if (currentoxigeno <= oxigenoMin)
        {
            print("You are dead");
        }
    }
}