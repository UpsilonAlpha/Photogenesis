using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DentedPixel;

public enum Dir
{
    Right, Left, Up, Down
}

public class InputManager : MonoBehaviour
{
    public GameObject Circle;
    public AudioManager speaker;
    public Photon photon = Photon.instance;
    public CanvasGroup gamePanel;
    public CanvasGroup gameOver;
    public Text encouragement;
    public static bool paused;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if(photon.photonReleased && gamePanel.alpha == 0 && gameOver.alpha == 0 && !paused)
        {
            Enable(gamePanel);
            if (timer < 1000)
            {
                FadeOut();
            }
        }
    }

    public void FadeOut()
    {
        encouragement.CrossFadeAlpha(0, 2, false);
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ProtonGenerator.ClearObjects();
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void Enable(CanvasGroup panel)
    {
        panel.blocksRaycasts = true;
        foreach(CanvasGroup group in GetComponentsInChildren<CanvasGroup>())
        {
            group.alpha = 0;
            group.blocksRaycasts = false;
        }
        LeanTween.alphaCanvas(panel, 1f, 0.1f).setIgnoreTimeScale(true);
        panel.blocksRaycasts = true;
        speaker.Play("Bloop");
    }

    public void Pause()
    {
        Time.timeScale = 0;
        paused = true;
        Drag.isSwipe = false;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        paused = false;
        Drag.isSwipe = false;
    }

    public void Disable(CanvasGroup panel)
    {
        panel.alpha = 0;
        panel.blocksRaycasts = false;
        LeanTween.alphaCanvas(GetComponentsInChildren<CanvasGroup>()[0], 1f, 0.5f);
        GetComponentsInChildren<CanvasGroup>()[0].blocksRaycasts = true;
    }
    public void ExplodeTransition(CanvasGroup panel)
    {
        speaker.Play("Supernova");
        GameObject c = Instantiate(Circle);
        LeanTween.scale(c, new Vector3(1,1,1), 0.5f);
        LeanTween.scale(c, new Vector3(3,3,1), 0.2f).setDelay(0.5f);
        LeanTween.alpha(c, 0, 0.3f).setDelay(0.7f).setDestroyOnComplete(true);
        Enable(panel);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
}
