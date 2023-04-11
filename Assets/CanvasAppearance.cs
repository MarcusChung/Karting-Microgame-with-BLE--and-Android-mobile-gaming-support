using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAppearance : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    public void ShowCanva(){
        Debug.Log("ShowCanvas");
        // canvas.SetActive(true);
        canvas.enabled = true;
       
    }

    // public void HideCanva(){
    //     Debug.Log("HideCanvas");
    //     canvas.SetActive(false);
   
    // }

    public void ToggleCanva(){
        canvas.enabled = !canvas.enabled;
    }
}
