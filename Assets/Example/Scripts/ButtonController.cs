using Android.BLE;
using Android.BLE.Commands;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public void HideCanvas()
    {
        GameObject.Find("BleCanvas").SetActive(false);
    }

    public void ShowCanvas()
    {
        GameObject.Find("BleCanvas").SetActive(true);
    }

}
