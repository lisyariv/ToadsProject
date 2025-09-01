using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void Backstory()
    {
        SceneManager.LoadScene("BackstoryScene");
    }

    public void Settings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");

    }
    /*
    public void retryButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    */

    public void WinScreen()
    {
        SceneManager.LoadScene("WinScreen");
    }

    public void LoseScene()
    {
        SceneManager.LoadScene("LoseScene");
    }

}
