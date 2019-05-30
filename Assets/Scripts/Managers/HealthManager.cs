using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class HealthManager : MonoBehaviour
{

    public static HealthManager Instance;
    public Transform HealthContainer;
    public GameObject healthObj;
    string rootPath;
    int currentHealth;

    //Creates instance
    void Awake()
    {
        Instance = this;
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
}
