using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private ItemSlot[] slots;
    [SerializeField] private ItemSlot[] usableSlots;
    [SerializeField] private GameObject inventoryCanvas;
    [SerializeField] private GameManagerSO gameManager;

    private List<ItemSO> myItems = new List<ItemSO>();
    private int collectedItems = 0;
    private ItemInfo[] itemInfoArray;

    private static InventorySystem instance;



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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitSlots();
        }
        else
        {
            Destroy(gameObject);
        }
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
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
}
