using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Orb : MonoBehaviour, IInteractable
{
    public GameObject GameObject => throw new System.NotImplementedException();

    public virtual void Interact()
    {
        AudioManager.instance.PlaySFX("Complete");
        AudioManager.instance.PlayBGM("Determination");
        SceneManager.LoadScene(5);
    }
}
