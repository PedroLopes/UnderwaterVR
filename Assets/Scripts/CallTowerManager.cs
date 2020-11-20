using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class CallTowerManager : MonoBehaviour
{

    [Header("Crewmate Information")]
    public Transform[] crewmates;

    private int transmitterFrequencyRangeMin = 100;
    private int transmitterFrequencyRangeMax = 1000;

    public int[] crewmateFrequencies;

    public AudioClip wrongFrequency;

    public SimpleDial playerTransmitter;


    public int GetFrequencyMax()
    {
        return transmitterFrequencyRangeMax;
    }

    public int GetFrequencyMin()
    {
        return transmitterFrequencyRangeMin;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (crewmates.Length > 0)
            InstantiateCrewmateFrequencies();
    }


    /*
     * INSTANTIATION FUNCTIONS
     */
    void InstantiateCrewmateFrequencies()
    {
        crewmateFrequencies = new int[crewmates.Length];
        crewmateFrequencies[0] = (int)UnityEngine.Random.Range(transmitterFrequencyRangeMin, transmitterFrequencyRangeMax);
        crewmates[0].GetComponent<CrewmateBehavior>().SetFrequency(crewmateFrequencies[0]);

        for (int c = 1; c < crewmates.Length; c++)
        {
            int newFrequency = 0;
            bool uniqueNumber = false;

            // Check if we are setting to a unique frequency.
            while (!uniqueNumber)
            {
                newFrequency = (int)UnityEngine.Random.Range(transmitterFrequencyRangeMin, transmitterFrequencyRangeMax);

                uniqueNumber = true;
                foreach (int f in crewmateFrequencies)
                {
                    uniqueNumber = uniqueNumber && (newFrequency != f);
                }
            }

            // Once unique, assign.
            crewmateFrequencies[c] = newFrequency;
            crewmates[c].GetComponent<CrewmateBehavior>().SetFrequency(newFrequency);
        }
    }

    /*
     * INFORMATION FUNCTIONS
     */
    public CrewInfo[] GetCrewmatesInformation()
    {
        CrewInfo[] returnInfo = new CrewInfo[crewmates.Length];
        for(int c = 0; c < crewmates.Length; c++)
        {
            Transform currentCrewmate = crewmates[c];
            returnInfo[c] = new CrewInfo(currentCrewmate.name, currentCrewmate.position, crewmateFrequencies[c]);
        }
        return returnInfo;
    }


    /*
     * CALL FUNCTIONS
     */
    public AudioClip CallCrewmate(int frequency)
    {
        if (crewmateFrequencies.Contains(frequency))
        {
            return crewmates[Array.IndexOf(crewmateFrequencies, frequency)].GetComponent<CrewmateBehavior>().callAudioReceiver;
        }
        return wrongFrequency;
    }

    public bool CallPlayer(AudioClip call, int freq)
    {
        return playerTransmitter.IncomingCall(call, freq);
    }

    /*
    public AudioClip ReceiveRandomCrewmateCall()
    {
        return crewmates[(int)UnityEngine.Random.Range(0, crewmates.Count)].GetComponent<CrewmateBehavior>().callAudioCaller;
    }
    */
}
