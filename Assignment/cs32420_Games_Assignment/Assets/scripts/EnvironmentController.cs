using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void FixedUpdate()
    {
        if (playerController.gameWon == true)
        {
            UnityEngine.Debug.Log("EnvironmentController FixedUpdate");
            SceneManager.LoadScene("Win"); //change scene
            //newLevel or win screen logic
        }
    }


}
