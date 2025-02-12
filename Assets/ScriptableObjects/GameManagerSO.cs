using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Scriptable Objects/GameManager")]
public class GameManagerSO : ScriptableObject
{
    private Player player;
    private DialogSystem dialogSystem;
    private InventorySystem inventorySystem;
    private QuestSystem questSystem;

    public event Action<ItemSO> OnNewItem;

    [NonSerialized]
    private Vector3 newPosition = new Vector3(-4.5f, -1.5f, 0f); // Default Position

    [NonSerialized]
    private Vector2 newOrientation = new Vector2(0, -1); // Default Orientation

    public Vector3 NewPosition { get => newPosition; }
    public Vector2 NewOrientation { get => newOrientation; }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += NewSceneLoaded;
    }

    private void NewSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        player = FindObjectOfType<Player>();
        dialogSystem = FindObjectOfType<DialogSystem>();
        inventorySystem = FindObjectOfType<InventorySystem>();
        questSystem = FindObjectOfType<QuestSystem>();
    }

    public void ChangePlayerStatus(bool status)
        => player.IsInteracting = !status;

    public void EndInteraction(IInteractable interactable)
        => questSystem.CheckQuestUpdates(interactable);

    #region Dialog System
    public void NpcInteraction(bool isInteracting)
        => dialogSystem.ChangeFrameStatus(isInteracting);

    public void NpcTalk(string phrase)
        => dialogSystem.SetFrameText(phrase);
    #endregion

    public void LoadNewScene(Vector3 newPosition, Vector2 newOrientation, int newSceneIndex)
    {
        this.newPosition = newPosition;
        this.newOrientation = newOrientation;
        SceneManager.LoadScene(newSceneIndex);
    }

    #region Item System
    public void NewItem(ItemSO itemData)
    {
        OnNewItem?.Invoke(itemData);
    }
    #endregion

    #region Quest System
    public bool ExistsCurrentQuest()
        => questSystem.CurrentQuest != null;

    public void StartQuest(int questId)
        => questSystem.StartQuest(questId);

    public bool IsMyquest(int questId)
        => questSystem.CurrentQuest.QuestId.Equals(questId);

    #endregion
}
