using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    // Start is called before the first frame update
    AsyncOperation async;
    static int levelNum;
    void Start()
    {
        async = SceneManager.LoadSceneAsync(levelNum);
        async.allowSceneActivation = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (async.progress == 0.9f)
        {
            async.allowSceneActivation = true;
        }
    }

    public static void LoadLevel(int levelNum)
    {
        LoadingManager.levelNum = levelNum;
        SceneManager.LoadSceneAsync("LoadingScene");
    }
}
