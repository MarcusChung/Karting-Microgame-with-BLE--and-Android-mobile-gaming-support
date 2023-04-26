using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningMessagePopup : MonoBehaviour
{

    public GameObject warningDisplay;


    private void OnTriggerEnter(Collider other)
    {
        if (FindObjectOfType<GameFlowManager>().bluetoothConnected == false)
        {
            ShowWarning();
            gameObject.SetActive(false);
        }
    }
    private void ShowWarning()
    {
        if (warningDisplay == null) return;
        warningDisplay.SetActive(true);
        Invoke("HideWarning", 0.5f);
    }

    private void HideWarning()
    {
        warningDisplay.SetActive(false);
    }
}
