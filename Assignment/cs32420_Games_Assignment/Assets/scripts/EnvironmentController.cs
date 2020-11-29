using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public GameObject rock;
    public UIController uiController;
    public GameObject rocksParentNode;
    
    private PlayerController playerController;
    private Rock[] rocks;

    public GameObject winUI;
    public GameObject loseUI;

    public void FixedUpdate()
    {
        loseGameCheck();
        winGameCheck();
    }

    public void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        rocks = rocksParentNode.GetComponentsInChildren<Rock>();
    }


    public void Update()
    {
        escKeyPressed(); 
        TimeTravelPressed();
    }


    public void loseGameCheck()
    {
        if (playerController.gameLost == true)
        {
            //UnityEngine.Debug.Log("EnvironmentController FixedUpdate loseGameCheck");
            playerController.enabled = false; //stops player moving
            loseUI.SetActive(true); //Show UI
        }
    }


    public void winGameCheck()
    {
        if (playerController.gameWon == true)
        {
            //UnityEngine.Debug.Log("EnvironmentController FixedUpdateWinGameCheck");
            playerController.enabled = false;//stops player moving
            winUI.SetActive(true); //Show UI
        }
    }


    public void escKeyPressed()
    {
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("EnvironmentController escKeyPressed");
        }
    }

    public void TimeTravelPressed()
    {
        if (Input.GetKeyDown("e"))
        {
            TriggerTimeTravel(Int32.MaxValue);
        }
    }

    private void TriggerTimeTravel(int duration)
    {
        playerController.TriggerTimeTravel(duration);
        foreach (var r in rocks) {
            r.TriggerTimeTravel(duration);
        }
    }
    
}
