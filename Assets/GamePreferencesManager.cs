using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;
public class GamePreferencesManager : MonoBehaviour
{
    const string key = "collisions";
    // Start is called before the first frame update
    void Start()
    {
        LoadPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetInt(key, FindObjectOfType<ArcadeKart>().collisionCount);
        PlayerPrefs.Save();
    }

    public void LoadPrefs()
    {
        var collisions = PlayerPrefs.GetInt(key, 0);
        
    }
}
