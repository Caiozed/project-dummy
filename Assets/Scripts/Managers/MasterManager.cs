using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class MasterManager : MonoBehaviour
{
    public static MasterManager Instance;
    public Animator anim;

    void Awake()
    {
        if (Instance != null)
            GameObject.Destroy(Instance);
        else
            Instance = this;

        DontDestroyOnLoad(this);
    }

    public void Update()
    {

    }

    public void TriggerAnim(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

}
