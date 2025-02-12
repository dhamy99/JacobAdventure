using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenu;

    public void Pause()
    {
        Time.timeScale = 0.0f;
    }

    public void ReturnToGame()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);

    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}