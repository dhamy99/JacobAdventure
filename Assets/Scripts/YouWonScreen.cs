using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWonScreen : MonoBehaviour
{
    public void GoBack()
    {
        AudioManager.instance.PlaySFX("Select");
        AudioManager.instance.PlayBGM("Hero");
        SceneManager.LoadScene(0);
    }
}
