using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MasterManager : MonoBehaviour
{
    public static MasterManager Instance;
    public Animator anim;
    public Text LevelText;
    void Awake()
    {
        if (Instance != null)
            GameObject.Destroy(Instance);
        else
            Instance = this;

        DontDestroyOnLoad(this);
    }

    public void TriggerAnim(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public void SetText(string text)
    {
        LevelText.text = text;
    }
}
