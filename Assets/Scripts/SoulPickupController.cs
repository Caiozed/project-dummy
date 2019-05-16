using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class SoulPickupController : MonoBehaviour
{
    // Start is called before the first frame update
    public int Souls;
    public GameObject particles;
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
            Instantiate(particles, transform.position, transform.rotation);
            UIManager.Instance.UpdateSouls(Souls);
            Destroy(gameObject);
        }
    }
}
