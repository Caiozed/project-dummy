using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Models
{
    [Serializable]
    public class BossSoul
    {
        public string Name;
        public string Description;
        public Powers AvailablePower = Powers.DoubleJump;
        // public Vector3 CurrentHealth;

        public BossSoul(string name = "NAME HERE", string description = "DESC HERE", Powers availablePower = Powers.DoubleJump)
        {
            Name = name;
            Description = description;
            AvailablePower = availablePower;
        }
    }


    public enum Powers
    {
        DoubleJump = 1,
        Dash = 2,
        ChargedAttach = 3,
        SuperJump = 4,
        WallJump = 5
    }
}
