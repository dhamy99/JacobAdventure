using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject saveGameUI;
    [SerializeField] private GameObject loadGameUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject controlsUI;
    [SerializeField] private GameObject generalFrame;
    [SerializeField] private GameManagerSO gameManager;
    private GameObject[] availableScreens;
    private GameObject activeUIElement;

    private void Start()
    {
        availableScreens = new GameObject[5];
        availableScreens[0] = pauseMenu;
        availableScreens[1] = saveGameUI;
        availableScreens[2] = loadGameUI;
        availableScreens[3] = optionsUI;
        availableScreens[4] = controlsUI;
    }


    public void SetActiveFalseAllBut(GameObject activeUI)
    {
        for (int i = 0; i < availableScreens.Length; i++)
        {
            availableScreens[i].SetActive(false);
        }
        activeUI.SetActive(true);
        activeUIElement = activeUI;
    }


    public void Pause()
    {
        AudioManager.instance.PlaySFX("Select");
        generalFrame.SetActive(true);
        SetActiveFalseAllBut(pauseMenu);
        Time.timeScale = 0.0f;
    }

    public void Back()
    {
        AudioManager.instance.PlaySFX("Select");
        SetActiveFalseAllBut(pauseMenu);
    }

    public void ReturnToGame()
    {
        AudioManager.instance.PlaySFX("Select");
        SetActiveFalseAllBut(pauseMenu);
        generalFrame.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Exit()
    {
        AudioManager.instance.PlaySFX("Select");
        SetActiveFalseAllBut(pauseMenu);
        generalFrame.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}