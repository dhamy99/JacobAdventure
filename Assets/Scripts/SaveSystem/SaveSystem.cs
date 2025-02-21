using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem saveSystem;
    private BinaryFormatter formatter = new BinaryFormatter();

    [SerializeField]
    private GameObject saveMenu;
    [SerializeField]
    private GameManagerSO gameManager;

    public void Awake()
    {
        if (saveSystem == null)
        {
            saveSystem = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Save()
    {

        string path = Application.persistentDataPath + "/" + "save001.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        PersistentData dataToSave = new PersistentData(gameManager);

        //Saving the progress
        formatter.Serialize(stream, dataToSave);
        stream.Close();

        
    }

    public PersistentData Load() 
    {
        string path = Application.persistentDataPath + "/" + "save001.txt";
        FileStream stream = new FileStream(path, FileMode.Open);

        //Loading the progress
        PersistentData loadedData = formatter.Deserialize(stream) as PersistentData;

        stream.Close();

        return loadedData;
    }

    public void ReturnToGame()
    {
        saveMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OnButtonClickLoad()
    {
        StartCoroutine(LoadSceneAndData());
    }

   private IEnumerator LoadSceneAndData()
    {
        PersistentData data = Load();

        //Setting the values of the save file
        gameManager.NewPosition = new Vector3(data.LastPlayerposX, data.LastPlayerposY);
        gameManager.NewOrientation = new Vector2(data.LastPlayerRotX, data.LastPlayerRotY);
        //gameManager.Inventory = data.ItemsPlayer;

        AsyncOperation operation = SceneManager.LoadSceneAsync(data.SceneId);
        Time.timeScale = 1.0f;
        yield return new WaitUntil( () => operation.isDone);
        


    }
}
