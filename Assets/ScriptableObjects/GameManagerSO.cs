using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Scriptable Objects/GameManager")]
public class GameManagerSO : ScriptableObject
{
    [SerializeField] private QuestSystem questSystem;

    private Player player;
    private DialogSystem dialogSystem;
    private InventorySystem inventorySystem;
    private AudioManager audioManager;
    private PlayerLife playerLife;
    private PauseMenu pauseMenu;
    private bool isPaused = false;
    private int missionCount;
    [NonSerialized] private string currentQuestName;
    private ItemSO equipedItem;
    private bool isInventoryOpen;


    public event Action<ItemSO> OnNewItem;

    [NonSerialized]
    private Vector3 newPosition = new Vector3(-4.5f, -1.5f, 0f); // Default Position

    [NonSerialized]
    private Vector2 newOrientation = new Vector2(0, -1); // Default Orientation

    public Vector3 NewPosition { get => newPosition; set => newPosition = value; }
    public Vector2 NewOrientation { get => newOrientation; set => newOrientation = value; }
    public List<ItemSO> Inventory { get => inventorySystem.MyItems; set => inventorySystem.MyItems = value; }
    public Player Player { get => player; set => player = value; }
    public AudioManager AudioManager { get => audioManager; }
    public int MissionCount { get => missionCount; set => missionCount = value; }
    public bool IsPaused { get => isPaused; set => isPaused = value; }
    public string CurrentQuestName { get => currentQuestName; set => currentQuestName = value; }
    public bool IsInventoryOpen { get => isInventoryOpen; set => isInventoryOpen = value; }
    public InventorySystem InventorySystem { get => inventorySystem; set => inventorySystem = value; }
    public ItemSO EquipedItem { get => equipedItem; set => equipedItem = value; }
    public PlayerLife PlayerLife { get => playerLife; set => playerLife = value; }

    private void OnEnable()
    {
        missionCount = 0;
        SceneManager.sceneLoaded += NewSceneLoaded;
    }

    private void NewSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        player = FindObjectOfType<Player>();
        dialogSystem = FindObjectOfType<DialogSystem>();
        inventorySystem = FindObjectOfType<InventorySystem>();
        audioManager = FindObjectOfType<AudioManager>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        if(arg0 != SceneManager.GetSceneByBuildIndex(0))
        {
            playerLife = FindObjectOfType<PlayerLife>();
            playerLife.UpdateLifeBar(playerLife.Health, 100.0f);
        }
        
        
    }

    public void ChangePlayerStatus(bool status)
        => player.IsInteracting = !status;

    public void EndInteraction(IInteractable interactable)
        => questSystem.CheckQuestUpdates(interactable);

    #region Dialog System
    public void NpcInteraction(bool isInteracting, IInteractable interactable)
    { 
        dialogSystem.ChangeFrameStatus(isInteracting);
        questSystem.CheckQuestUpdates(interactable);
    }


    public void NpcTalk(string phrase)
        => dialogSystem.SetFrameText(phrase);
    #endregion

    public void LoadNewScene(Vector3 newPosition, Vector2 newOrientation, int newSceneIndex)
    {
        this.newPosition = newPosition;
        this.newOrientation = newOrientation;
        SceneManager.LoadScene(newSceneIndex);

        if (newSceneIndex == 1)
        {
            AudioManager.instance.PlayBGM("Sun");
        }
        else if (newSceneIndex == 2 || newSceneIndex == 3)
        {
            AudioManager.instance.PlayBGM("Interior");
        }
        else if (newSceneIndex == 4)
        {
            AudioManager.instance.PlayBGM("Ghost");
        }
        else if (newSceneIndex == 0)
        {
            AudioManager.instance.PlayBGM("Hero");
        }
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

    public void StartQuest(QuestSO quest)
    {
        questSystem.StartQuest(quest);
        currentQuestName = quest.Name;
    }

    public bool IsMyquest(int questId)
        => questSystem.CurrentQuest.QuestId.Equals(questId);

    #endregion
}
