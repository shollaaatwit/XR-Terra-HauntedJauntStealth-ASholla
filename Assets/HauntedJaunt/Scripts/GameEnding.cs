using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{

    public GameObject player;
    public CanvasGroup winCanvas;
    public CanvasGroup caughtCanvas;

    private bool _isAtExit;
    private bool _isCaught;

    private float _timer;
    public float fadeDuration;
    public float displayDuration;
    

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            _isAtExit = true;

        }
    }

    void Update()
    {
        if(_isAtExit)
        {
            EndLevel(winCanvas, false);

        }
        else if(_isCaught)
        {
            EndLevel(caughtCanvas, true);
        }
    }


    public void CaughtPlayer()
    {
        _isCaught = true;
    }


    private void EndLevel(CanvasGroup canvas, bool doRestart)
    {
        canvas.gameObject.SetActive(true);
        _timer += Time.deltaTime;
        canvas.alpha = _timer/fadeDuration;

        if(_timer > fadeDuration + displayDuration)
        {
            if(doRestart)
            {
                SceneManager.LoadScene(0);

            }
            else
            {
                Application.Quit();
            }
        }       
    }




}
