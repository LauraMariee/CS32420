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

    public void mainMenu()
    {
        Debug.Log("UIController mainMenu"); 
        SceneManager.LoadScene("StartScreen"); 
    }

    public void Restart()
    {
        Debug.Log("UIController Restart");
        //SceneManager.LoadScene(currentLevel);
    }

    public void Instruction()
    {
        //enable components for instructions
        Instruction_Screen.SetActive(true);
        Level_Select.SetActive(false);
        Title_Screen.SetActive(false); 
    }

    public void levelSelect()
    {
        Debug.Log("UIController levelSelect");
        //enable components for levelSelect
    }
}
