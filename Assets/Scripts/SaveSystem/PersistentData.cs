using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class PersistentData
{
    private float lastPlayerposX, lastPlayerposY, lastPlayerRotX, lastPlayerRotY;
    private Dictionary<int,bool> items = new Dictionary<int,bool>();
    private int sceneId;

    public PersistentData(GameManagerSO gameManager) 
    {
        lastPlayerposX = gameManager.Player.transform.position.x;
        lastPlayerposY = gameManager.Player.transform.position.y;
        lastPlayerRotX = gameManager.Player.transform.rotation.x;
        lastPlayerRotY = gameManager.Player.transform.rotation.y;
        sceneId = SceneManager.GetActiveScene().buildIndex;
        
    }

    public float LastPlayerposX { get => lastPlayerposX;}
    public float LastPlayerposY { get => lastPlayerposY;}
    public float LastPlayerRotX { get => lastPlayerRotX;}
    public float LastPlayerRotY { get => lastPlayerRotY;}
    public int SceneId { get => sceneId; }
}
