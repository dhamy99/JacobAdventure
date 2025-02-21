using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Orb : MonoBehaviour, IInteractable
{
    public GameObject GameObject => throw new System.NotImplementedException();

    [SerializeField] private GameManagerSO gameManager;

    public virtual void Interact()
    {
        gameManager.NewPosition = new Vector3(-4.5f, -1.5f, 0f);
        AudioManager.instance.PlayBGM("Determination");
        SceneManager.LoadScene(5);
    }
}
