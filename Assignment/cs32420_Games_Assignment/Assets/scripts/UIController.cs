using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject Instruction_Screen;
    public GameObject Level_Select;
    public GameObject Title_Screen;




    public void Quit()
    {
        Debug.Log("UIController Quit");
        //Quit Game
    }


    public void Play()
    {
        Debug.Log("UIController Play");
        SceneManager.LoadScene("levelOne");
    }

    public void mainMenu()
    {
        Debug.Log("UIController mainMenu"); 
        SceneManager.LoadScene("StartScreen"); 
    }

    public void Restart()
    {
        Debug.Log("UIController Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Instruction()
    {
        Instruction_Screen.SetActive(true);
        Level_Select.SetActive(false);
        Title_Screen.SetActive(false); 
    }

    public void levelSelect()
    {
        Debug.Log("UIController levelSelect");
        Instruction_Screen.SetActive(false);
        Level_Select.SetActive(true);
        Title_Screen.SetActive(false);
    }

    public void titleScreen()
    {
        Debug.Log("UIController titleScreen");
        Instruction_Screen.SetActive(false);
        Level_Select.SetActive(false);
        Title_Screen.SetActive(true);
    }

    public void levelOne()
    {
        Debug.Log("UIController levelOne");
        SceneManager.LoadScene("levelOne");
    }

    public void levelTwo()
    {
        Debug.Log("UIController levelTwo");
        SceneManager.LoadScene("levelTwo");
    }

    public void nextLevel()
    {
        if (SceneManager.GetActiveScene().name == "levelOne")
        {
            Debug.Log("UIController nextLevel");
            levelTwo();
        }
    }
}
