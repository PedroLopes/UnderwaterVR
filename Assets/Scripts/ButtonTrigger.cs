using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onButtonPressed;

    public bool pressedInProgress = false;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(!pressedInProgress)
        {
            pressedInProgress = true;
            onButtonPressed?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
            pressedInProgress = false;
            onButtonPressed?.Invoke();
    }
}
