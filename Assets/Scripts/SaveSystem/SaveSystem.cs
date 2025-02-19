using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem saveSystem;
    private BinaryFormatter formatter = new BinaryFormatter();
    string path = Application.persistentDataPath + "/" + "save001.txt";
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
    public void Save(GameManagerSO gameManager)
    {

        FileStream stream = new FileStream(path, FileMode.Create);

        PersistentData dataToSave = new PersistentData(gameManager);

        //Saving the progress
        formatter.Serialize(stream, dataToSave);
        stream.Close();
    }

    public PersistentData Load() 
    {
        FileStream stream = new FileStream(path, FileMode.Open);

        //Loading the progress
        PersistentData loadedData = formatter.Deserialize(stream) as PersistentData;

        stream.Close();

        return loadedData;
    }
}
