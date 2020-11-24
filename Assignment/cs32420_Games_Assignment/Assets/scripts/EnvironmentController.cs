using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public PlayerController playerController;
    public GameObject rock;
    public UIController uiController;


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
            SceneManager.LoadScene("GameOver"); //change scene
            //newLevel or win screen logic
        }
    }


    public void winGameCheck()
    {
        if (playerController.gameWon == true)
        {
            UnityEngine.Debug.Log("EnvironmentController FixedUpdatewinGameCheck");
            SceneManager.LoadScene("Win"); //change scene
            //newLevel or win screen logic
        }
    }


    public void nextLevel()
    {
        //next level
    }

}
