using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    private PlayerController playerController;
    public GameObject rock;
    public UIController uiController;

    private GameObject winUI;
    private GameObject loseUI;

    public void FixedUpdate()
    {
        loseGameCheck();
        winGameCheck();
    }

    public void Start()
    {
        winUI = GameObject.Find("WinUI");
        loseUI = GameObject.Find("Los" +
            "" +
            "" +
            "" +
            "eUI");
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }


    public void Update()
    {
        escKeyPressed(); 
        if (rock.GetComponent<Rock>().rockTriggered == true)
        {
            rock.GetComponent<Rock>().rockMovement();
        }
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
}
