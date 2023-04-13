using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownVibration : MonoBehaviour
{
    [SerializeField] private Text oneText;
    [SerializeField] private Text twoText;
    [SerializeField] private Text threeText;
    [SerializeField] private Text goText;

    private int functionCalled = 0;
    void FixedUpdate()
    {
        if (threeText.IsActive() && functionCalled == 0)
        {
            functionCalled = 3;
            FindObjectOfType<GameFlowManager>().QuickVibrate();
            Debug.Log("Vibrating 3");
        }

        if (twoText.IsActive() && !threeText.IsActive() && functionCalled == 3)
        {
            functionCalled = 2;
            Debug.Log("Vibrating 2");
            FindObjectOfType<GameFlowManager>().QuickVibrate();
        }

        if (oneText.IsActive() && !twoText.IsActive() && functionCalled == 2)
        {
            functionCalled = 1;
            Debug.Log("Vibrating 1");
            FindObjectOfType<GameFlowManager>().QuickVibrate();
        }

        if (goText.IsActive() && !oneText.IsActive() && functionCalled == 1)
        {
            functionCalled = 0;
            Debug.Log("Vibrating!!");
            FindObjectOfType<GameFlowManager>().Vibrate();
        }
    }
}
