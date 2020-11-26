using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject rock;
    public UIController uiController;

    public GameObject winUI;
    public GameObject loseUI;
    public Camera camera; 

    public void FixedUpdate()
    {
        loseGameCheck();
        winGameCheck();
    }

    public void Update()
    {
        if (rock.GetComponent<Rock>().rockTriggered == true)
        {
            rock.GetComponent<Rock>().rockMovement();
        }
    }


    public void loseGameCheck()
    {
        if (playerController.gameLost == true)
        {
            UnityEngine.Debug.Log("EnvironmentController FixedUpdate loseGameCheck");
            camera.GetComponent<Transform>().position = new Vector2(8.02f, -1.3f); //moves camera out
            playerController.enabled = false; //stops player moving
            loseUI.SetActive(true); //Show UI
        }
    }


    public void winGameCheck()
    {
        if (playerController.gameWon == true)
        {
            UnityEngine.Debug.Log("EnvironmentController FixedUpdateWinGameCheck");
            camera.GetComponent<Transform>().position = new Vector2(8.02f, -1.3f); //moves camera out
            playerController.enabled = false;//stops player moving
            winUI.SetActive(true); //Show UI
        }
    }
}
