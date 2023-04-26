using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && gameObject.name.StartsWith("InvisiblePreTrigger"))
        {
            FindObjectOfType<ExampleBleInteractor>().VstrongTap();
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("PhysicsBall") && gameObject.name.StartsWith("GoalNet"))
        {
            FindObjectOfType<ExampleBleInteractor>().Vgoal();
        }
        else if (other.gameObject.CompareTag("Player") && gameObject.name.StartsWith("InvisibleRampTrigger"))
        {
            FindObjectOfType<ExampleBleInteractor>().Vwarning();
        }
        else if (other.gameObject.CompareTag("Player") && gameObject.name.StartsWith("InvisibleTrackTrigger"))
        {
            FindObjectOfType<ExampleBleInteractor>().Vtap();
            gameObject.SetActive(false);
        }
    }
}
