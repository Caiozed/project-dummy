using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent OnTriggerEvent;
    public string TagToCheck;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag(TagToCheck))
        {
            OnTriggerEvent.Invoke();
        }
    }

    public void InstantiateObject(GameObject obj)
    {
        Instantiate(obj, transform.position, transform.rotation);
    }
}
