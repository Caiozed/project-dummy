using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Player
{
    public Vector3 CurrentPosition;
    public int Scene = 1;
    public bool HaveChargedJump = false;
    public bool HaveWallJump = false;
    public int MaxHealth = 3;
    public int Damage = 1;
    public int SmallSouls = 0;
    // public Vector3 CurrentHealth;

}
