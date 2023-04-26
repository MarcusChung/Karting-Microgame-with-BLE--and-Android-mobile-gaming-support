using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    public GameObject warningDisplay;
    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player kart
        if (FindObjectOfType<GameFlowManager>().bluetoothConnected == false)
        {
            ShowWarning();
            gameObject.SetActive(false);
        }
        else
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

    private void ShowWarning(){
        warningDisplay.SetActive(true);
        Invoke("HideWarning", 0.5f);
    }

    private void HideWarning(){
        warningDisplay.SetActive(false);
    }
}
