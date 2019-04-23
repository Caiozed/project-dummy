using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthManager : MonoBehaviour
{

    public static HealthManager Instance;
    public Transform HealthContainer;
    public Image FadeImage;
    public GameObject healthObj;
    string rootPath;
    int currentHealth;

    //Creates instance
    void Awake()
    {
        if (Instance != null)
            GameObject.Destroy(Instance);
        else
            Instance = this;

        DontDestroyOnLoad(this);
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void SetHealth(int health)
    {
        currentHealth = health;
        DrawHealth();
    }

    public void DrawHealth()
    {
        //Remove objects
        Transform[] objs = HealthContainer.GetComponentsInChildren<Transform>();
        for (int i = 1; i < objs.Length; i++)
        {
            Destroy(objs[i].gameObject);
        }

        //Reintantate objects
        for (int i = 1; i <= currentHealth; i++)
        {
            GameObject obj = Instantiate(healthObj, HealthContainer.position + new Vector3(35 * i, 0, 0), HealthContainer.rotation);
            obj.transform.SetParent(HealthContainer);
        }
    }

    public void FadeIn()
    {
        FadeImage.CrossFadeAlpha(0.1f, 2, true);
    }

    public void FadeOut()
    {
        FadeImage.CrossFadeAlpha(1, 2, true);
    }
}
