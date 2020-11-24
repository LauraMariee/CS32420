using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TimeTravel : MonoBehaviour
{

    public List<Tuple<string, Vector2>> playerPosition;

    private int frameLimit;
    private string currentTime;

    // Start is called before the first frame update
    void Start()
    {
        frameLimit = 1200;
    }

    private string GetTimeStamp()
    {
        UnityEngine.Debug.Log("TimeTravel GetTimeStamp");
        currentTime = Time.time.ToString("f6");
        return currentTime; 
    }

    public void AddPlayerPosition(Vector2 position)
    {
        //if less than or equal to 1200 frames
        if (playerPosition.Count < frameLimit)
        {
            UnityEngine.Debug.Log("PlayerController AddPlayerPosition");
            playerPosition.Add(new Tuple<string, Vector2>(GetTimeStamp(), position)); 
        }
    }

    public void ShowPlayerPositions()
    {
        for(int i = 0; i < playerPosition.Count; i++)
        {
            UnityEngine.Debug.Log(playerPosition[i]);
        }
    }
}
