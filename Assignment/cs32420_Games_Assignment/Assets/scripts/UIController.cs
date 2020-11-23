using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public string currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void getCurrentLevel()
    {
        currentLevel = SceneManager.GetActiveScene().name; 
    }

    public void quit()
    {
        Debug.Log("quit");
    }

    public void mainMenu()
    {
        Debug.Log("Main Menu"); 
    }

    public void restart()
    {
        Debug.Log("restart");
        SceneManager.LoadScene(currentLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
