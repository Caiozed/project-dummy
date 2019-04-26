using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChangeTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public string AreaName;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            MasterManager.Instance.SetText(AreaName);
            MasterManager.Instance.TriggerAnim("LevelTransition");
        }
    }
}
