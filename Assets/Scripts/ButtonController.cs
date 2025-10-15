using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //main menu
    public void StartGame()
    {
        PlayerScript.lifes = 3;
        SceneManager.LoadScene("Stage0");
    }

    public void Rules()
    {
        SceneManager.LoadScene("RulesScreen");
    }

    public void Options()
    {
        SceneManager.LoadScene("OptionsScreen");
    }

    //options
    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
