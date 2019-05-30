using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossAreaController : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent OnInitialize;
    EnemyController EnemyController;
    void Start()
    {
        EnemyController = GetComponentInChildren<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            OnInitialize.Invoke();
            transform.GetComponent<Collider2D>().enabled = false;
        }
    }
}
