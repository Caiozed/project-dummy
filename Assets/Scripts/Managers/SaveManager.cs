using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    string rootPath;

    //Creates instance
    void Awake()
    {
        if (Instance != null)
            GameObject.Destroy(Instance);
        else
            Instance = this;

        rootPath = Application.persistentDataPath;

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Save Model
    public void SaveModel<T>(T data, string path)
    {
        string jsonString = JsonUtility.ToJson(data);

        using (StreamWriter streamWriter = File.CreateText(Path.Combine(rootPath, path)))
        {
            streamWriter.Write(jsonString);
        }
    }

    //Load Model
    public T LoadModel<T>(string path)
    {
        // // if (!File.Exists(rootPath)) { Debug.Log(default(T)); return default(T); }
        using (StreamReader streamReader = File.OpenText(Path.Combine(rootPath, path)))
        {
            string jsonString = streamReader.ReadToEnd();
            return JsonUtility.FromJson<T>(jsonString);
        }
    }
}