using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onButtonPressed;

    public bool pressedInProgress = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (!pressedInProgress && (other.name.Substring(0, 4) == "Hand") && (other.tag == "Player"))
        {
            pressedInProgress = true;
            onButtonPressed?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            pressedInProgress = false;
            onButtonPressed?.Invoke();
        }
    }
}
