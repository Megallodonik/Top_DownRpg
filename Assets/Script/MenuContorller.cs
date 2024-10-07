using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuContorller : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject WinMenu;
    [SerializeField] GameObject DieMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void QuitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
    public void StartGame()
    {
        
        SceneManager.LoadScene("Main");
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }
    public void CountinueGame()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void BeatGame()
    {
        Time.timeScale = 0;
        WinMenu.SetActive(true);
    }
    public void NextBoss()
    {
        
        WinMenu.SetActive(true);
    }
    public void OnDead()
    {
        
        DieMenu.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
