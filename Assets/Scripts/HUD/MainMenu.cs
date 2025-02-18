using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsTab;
    [SerializeField] private GameObject mainTab;
    [SerializeField] private GameObject controlsTab;

    public void Play()
    {
        AudioManager.instance.PlaySFX("Select");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        AudioManager.instance.PlayBGM("Sun");
        Time.timeScale = 1.0f;
    }

    public void ShowOptions()
    {
        AudioManager.instance.PlaySFX("Select");
        mainTab.SetActive(false);
        optionsTab.SetActive(true);
    }

    public void Back()
    {
        AudioManager.instance.PlaySFX("Select");
        optionsTab.SetActive(false);
        controlsTab.SetActive(false);
        mainTab.SetActive(true);
    }

    public void ShowControls()
    {
        AudioManager.instance.PlaySFX("Select");
        mainTab.SetActive(false);
        controlsTab.SetActive(true);
    }

    public void Exit()
    {
        AudioManager.instance.PlaySFX("Select");
        Application.Quit();
    }
}