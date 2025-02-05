using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Scriptable Objects/GameManager")]
public class GameManagerSO : ScriptableObject
{
    private DialogSystem dialogSystem;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += NewSceneLoaded;
    }

    private void NewSceneLoaded(Scene arg0, LoadSceneMode arg1)
        => dialogSystem = FindObjectOfType<DialogSystem>();

    public void NpcInteraction(bool isInteracting)
        => dialogSystem.ChangeFrameStatus(isInteracting);

    public void NpcTalk(string phrase)
        => dialogSystem.SetFrameText(phrase);

    public void ChangePlayerStatus(bool v)
    {
        throw new NotImplementedException();
    }
}
