using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    private ExampleBleInteractor bleInteractor;

    private void Start()
    {
        bleInteractor = FindObjectOfType<ExampleBleInteractor>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && gameObject.name.StartsWith("InvisiblePreTrigger"))
        {
            //unused
            bleInteractor.VstrongTap();
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("PhysicsBall") && gameObject.name.StartsWith("GoalNet"))
        {
            bleInteractor.Vgoal();
        }
        else if (other.gameObject.CompareTag("Player") && gameObject.name.StartsWith("InvisibleRampTrigger"))
        {
            bleInteractor.Vwarning();
        }
        else if (other.gameObject.CompareTag("Player") && gameObject.name.StartsWith("InvisibleTrackTrigger"))
        {
            bleInteractor.Vtap();
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Player") && gameObject.name.StartsWith("Checkpoint"))
        {
            bleInteractor.Vtap();
        }
    }
}
