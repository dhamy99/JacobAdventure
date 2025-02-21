using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class PersistentData
{
    private float lastPlayerposX, lastPlayerposY, lastPlayerRotX, lastPlayerRotY;
    private Dictionary<string,bool> items = new Dictionary<string, bool>();
    private int sceneId, missionCount;
    private string currentQuestName;

    public PersistentData(GameManagerSO gameManager) 
    {
        lastPlayerposX = gameManager.Player.transform.position.x;
        lastPlayerposY = gameManager.Player.transform.position.y;
        lastPlayerRotX = gameManager.Player.transform.rotation.x;
        lastPlayerRotY = gameManager.Player.transform.rotation.y;
        sceneId = SceneManager.GetActiveScene().buildIndex;
        missionCount = gameManager.MissionCount;
        currentQuestName = gameManager.CurrentQuestName;
        List<ItemSO> itemsPlayer = gameManager.Inventory;

        if (itemsPlayer != null)
        {
            foreach (ItemSO item in itemsPlayer)
            {
                if (item != null)
                {
                    items.Add(item.itemName, true);
                }
            }
        }

        
    }

    public float LastPlayerposX { get => lastPlayerposX;}
    public float LastPlayerposY { get => lastPlayerposY;}
    public float LastPlayerRotX { get => lastPlayerRotX;}
    public float LastPlayerRotY { get => lastPlayerRotY;}
    public int SceneId { get => sceneId; }
    public Dictionary<string, bool> Items { get => items; set => items = value; }
    public int MissionCount { get => missionCount; set => missionCount = value; }
    public string CurrentQuestName { get => currentQuestName; set => currentQuestName = value; }
}
