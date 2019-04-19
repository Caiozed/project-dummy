using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationNotifier : MonoBehaviour
{
    public PlayerController _playerController;
    // Start is called before the first frame update
    void Start()
    {
        _playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void StopedAttacking()
    {
        _playerController.IsAttacking = false;
        _playerController.ResetDirectionAfterAttack();
    }
}
