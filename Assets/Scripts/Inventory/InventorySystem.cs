using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private ItemSlot[] slots;
    [SerializeField] private ItemSlot[] usableSlots;
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private GameManagerSO gameManager;

    [Header("Data slots")]
    [SerializeField] private TMP_Text hpNumber;
    [SerializeField] private TMP_Text atkNumber;
    [SerializeField] private TMP_Text completedQuestsNumber;
    [SerializeField] private TMP_Text currentQuestName;
    [SerializeField] private TMP_Text equipedItemName;

    private List<ItemSO> myItems = new List<ItemSO>();
    private int collectedItems = 0;
    private ItemInfo[] itemInfoArray;

    private static InventorySystem instance;

    public List<ItemSO> MyItems { get => myItems; set => myItems = value; }

    private void OnEnable()
    {
        gameManager.OnNewItem += AddNewItem;
    }

    private void OnDisable()
    {
        gameManager.OnNewItem -= AddNewItem;
    }

    private void Awake()
    {

        InitSlots();

        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //    InitSlots();
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    private void InitSlots()
    {
        //generates array with item info slots. 
        itemInfoArray = new ItemInfo[slots.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            itemInfoArray[i] = slots[i].GetComponentInChildren<ItemInfo>();
        }
    }

    void Update()
    {
        InputReading();
    }

    private void InputReading()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameManager.IsPaused)
        {
            UpdateInventoryData();
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);

            if (inventoryCanvas.activeSelf)
            {
                Time.timeScale = 0f;
            } else
            {
                Time.timeScale = 1f;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            usableSlots[0].GetComponentInChildren<ItemInfo>().UseItem();
        }
    }


    private void AddNewItem(ItemSO newItem)
    {
        if (myItems.Contains(newItem))
        {
            int stackIndex = myItems.FindIndex(foundItem => foundItem.Equals(newItem)); //also possible with .IndexOf
            itemInfoArray[stackIndex].UpdateStackItem();
        }
        else
        {
            myItems.Add(newItem);
            slots[collectedItems].gameObject.SetActive(true);
            itemInfoArray[collectedItems].FeedData(newItem);
            collectedItems++;
        }
    }

    public void UpdateInventoryData()
    {

        //hpNumber.text = gameManager.GetPlayerLife().ToString();
        if (usableSlots[0].TryGetComponent(out ItemInfo itemInfo))
        {
            atkNumber.text = itemInfo.CurrentData.damage.ToString();
            //usableSlots[0].GetComponent<ItemSO>().damage.ToString();
        } else
        {
            atkNumber.text = "0";
        }

        //atkNumber.text = usableSlots[0].GetComponent<ItemInfo>().CurrentData.damage.ToString();
        completedQuestsNumber.text = gameManager.MissionCount.ToString();

        
        currentQuestName.text = gameManager.CurrentQuestName;
        //ADD update current quest name
        //ADD update equiped item
    }
}
