using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : Singleton<GameController>
{

    public Canvas startCanvas;
    public Canvas pauseCanvas;
    public Canvas gameCanvas;
    public Canvas winCanvas;
    public Canvas caughtCanvas;

    public GameObject player;
    public GameObject panCamera;
    public GameObject playerCamera;



    void Start()
    {
        ResetUI();  
    }
    public void StartGame()
    {
        startCanvas.gameObject.SetActive(false);
        panCamera.gameObject.SetActive(false);

        gameCanvas.gameObject.SetActive(true);
        player.gameObject.SetActive(true);
        playerCamera.gameObject.SetActive(true);

    }

    public void pauseGame(bool pause)
    {
        pauseCanvas.gameObject.SetActive(pause);  
        Time.timeScale = (pause) ? 0 : 1f;
    }

    private void ResetUI()
    {
        startCanvas.gameObject.SetActive(true);
        panCamera.gameObject.SetActive(true);

        pauseCanvas.gameObject.SetActive(false);
        gameCanvas.gameObject.SetActive(false);
        player.gameObject.SetActive(false);
        winCanvas.gameObject.SetActive(false);
        caughtCanvas.gameObject.SetActive(false);
        playerCamera.gameObject.SetActive(false);
        
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }



}
