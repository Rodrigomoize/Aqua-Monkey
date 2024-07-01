using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenStation : MonoBehaviour
{
    public float oxygenAmount = 50.0f;
    public float oxygenRecoveryRate = 1.0f;
    public float oxygenRecovered;
    public float range = 2.0f;
    public bool isRecoveringOxygen = false;
    private OxygenSystem oxygenSystem;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        oxygenSystem = GameObject.FindObjectOfType<OxygenSystem>();      
        playerMovement = GameObject.FindObjectOfType<PlayerMovement>();  
        oxygenRecovered = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is within range of the oxygen station
        if (oxygenSystem.currentoxigeno >= oxygenSystem.oxigenoMax || oxygenRecovered >= oxygenAmount)
        {  
            oxygenSystem.isRecoveringOxigen = false;
        }
        if (Vector3.Distance(transform.position, playerMovement.transform.position) < range)
        {
            //press F to add oxygen
            if (Input.GetKey(KeyCode.F) && oxygenSystem.currentoxigeno < oxygenSystem.oxigenoMax && oxygenRecovered <= oxygenAmount)
            {
                // Add oxygen to player
                oxygenSystem.currentoxigeno += oxygenRecoveryRate * Time.deltaTime;
                oxygenSystem.isRecoveringOxigen = true;
                oxygenRecovered += oxygenRecoveryRate * Time.deltaTime;                
            }
            else
            {
                oxygenSystem.isRecoveringOxigen = false;
            }
        }
    }
}