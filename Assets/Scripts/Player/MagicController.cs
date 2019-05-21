using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    // Start is called before the first frame update
    public int Damage;
    public float Speed;
    public GameObject ImpactEffect;
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * Speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            other.transform.GetComponent<EnemyController>().TakeDamage(Damage);
            Instantiate(ImpactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
