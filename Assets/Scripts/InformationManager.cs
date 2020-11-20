using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class InformationManager : MonoBehaviour
{
    [Header("User Information")]
    public Transform player;

    [Header("Water Information")]
    public Transform waterPlane;

    // Oxygen info
    private float oxygenLevelMax = 100f;
    private float currentOxygenLevel = 100f;
    private float oxygenConsumpsionRate = -0.54f; // in oxygen/second.
    private float oxygenReplenishRate = 1.66f; // in oxygen/second.

    // Battery info
    private float batteryLevelMax = 100f;
    private float currentBatteryLevel = 100f;
    private float batteryConsumptionRate = -0.417f; // in batt/second.
    
    /*
     * INFORMATION RETRIEVAL FUNCTIONS
     */
    public float GetDepth()
    {
        return player.position.y - waterPlane.position.y;
    }

    public float GetBatteryLevel()
    {
        return currentBatteryLevel;
    }

    public float GetOxygenLevel()
    {
        return currentOxygenLevel;
    }

    public float GetMaxOxygenLevel()
    {
        return oxygenLevelMax;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    /*
     * UPDATE FUNCTIONS
     */
    
    private void BatteryUpdate ()
    {
        currentBatteryLevel = Mathf.Clamp(currentBatteryLevel + batteryConsumptionRate * Time.deltaTime, 0f, batteryLevelMax);

        if (currentBatteryLevel <= 0.1f)
            GameOver();
    }

    private void OxygenUpdate ()
    {
        float factor = 0f;

        if (GetDepth() < 0f)
        {
            factor = oxygenConsumpsionRate;
        } else
        {
            factor = oxygenReplenishRate;
        }
        float oxygenDifference =  factor * Time.deltaTime;

        currentOxygenLevel = Mathf.Clamp(currentOxygenLevel + oxygenDifference, 0f, oxygenLevelMax);

        if (currentOxygenLevel <= 0.05f)
            GameOver();
    }

    // Update is called once per frame
    void Update()
    {
        OxygenUpdate();
        BatteryUpdate();
    }

    void GameOver()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
