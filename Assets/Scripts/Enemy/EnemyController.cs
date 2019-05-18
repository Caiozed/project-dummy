using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public int Damage = 1;
    public int MaxHealth = 1;
    public int SoulsOnHold;
    public GameObject DeathEffect;
    int _currentHealth;
    Animator anim;
    Rigidbody2D _rb;
    Collider2D _collider;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        _rb = GetComponentInChildren<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth > 0)
            anim.SetTrigger("Hit");
        else
        {
            Die();
        }
    }

    public void Die()
    {
        anim.SetTrigger("Dead");
        this.enabled = false;
        Debug.Log(SoulsOnHold);
        UIManager.Instance.UpdateSouls(SoulsOnHold);
        Instantiate(DeathEffect, transform.position + new Vector3(0, 0.1f, 0), transform.rotation);
        _rb.isKinematic = true;
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("PlayerWeapon"))
        {
            TakeDamage(other.transform.root.GetComponent<PlayerController>().GetDamage());
        }
    }
}
