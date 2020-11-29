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

    public GameObject pauseUI; 

    private bool gamePaused; 

    public void FixedUpdate()
    {
        loseGameCheck();
        winGameCheck();
    }

    public void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        rocks = rocksParentNode.GetComponentsInChildren<Rock>();
        gamePaused = false; 
    }


    public void Update()
    {
        escKeyPressed(); 
        TimeTravelPressed();
        pauseGameCheck(); 
    }


    public void loseGameCheck()
    {
        if (playerController.gameLost == true)
        {
            //UnityEngine.Debug.Log("EnvironmentController FixedUpdate loseGameCheck");
            playerController.enabled = false; //stops player moving
            loseUI.SetActive(true); //Show UI
            uiController.RemoveTimeTravelUI();
        }
    }


    public void winGameCheck()
    {
        if (playerController.gameWon == true)
        {
            //UnityEngine.Debug.Log("EnvironmentController FixedUpdateWinGameCheck");
            playerController.enabled = false;//stops player moving
            winUI.SetActive(true); //Show UI
            uiController.RemoveTimeTravelUI(); 
        }
    }


    public void pauseGameCheck()
    {
        if (pauseUI.active)
        {
            playerController.enabled = false;
        }
        else
        {
            playerController.enabled = true;
        }
    }

    public void escKeyPressed()
    {
        if((playerController.gameLost == true) || (playerController.gameWon == true))
        {
            return; 
        }
        if (Input.GetKeyDown("escape"))
        {
            gamePaused = true; 
            Debug.Log("EnvironmentController escKeyPressed");
            pauseUI.SetActive(true);
            uiController.RemoveTimeTravelUI(); 
        }
    }

    public void TimeTravelPressed()
    {
        if ((playerController.gameLost == true) || (playerController.gameWon == true))
        {
            return;
        }

        if (Input.GetKeyDown("e"))
        {
            uiController.TimeTravelTriggered();
            TriggerTimeTravel(Int32.MaxValue);
        }
        if (Input.GetKeyUp("e"))
        {
            uiController.TimeTravelNeutral();
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
