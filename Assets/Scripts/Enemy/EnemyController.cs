using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public int Damage = 1;
    public int MaxHealth = 1;
    public int SoulsOnHold;
    public GameObject DeathEffect;
    public UnityEvent OnDeath;
    public bool IsDead;
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
        if (IsDead) return;
        anim.SetTrigger("Dead");
        OnDeath.Invoke();
        IsDead = true;
        this.enabled = false;
        if (SoulsOnHold > 0)
            UIManager.Instance.UpdateSouls(SoulsOnHold);

        Instantiate(DeathEffect, transform.position + new Vector3(0, 0.1f, 0), transform.rotation);
        _rb.isKinematic = true;
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("PlayerWeapon"))
        {
            PlayerController.Instance.BladeImpactEffect.Play();
            TakeDamage(other.transform.root.GetComponent<PlayerController>().GetDamage());
        }
    }
}
