using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
namespace Models
{
    [Serializable]
    public class Player
    {
        public Vector3 CurrentPosition;
        public int Scene = 1;
        public int MaxHealth = 3;
        public float MaxMagic = 100;
        public int Damage = 1;
        public int SmallSouls = 0;
        public List<BossSoul> CollectedBossSouls = new List<BossSoul>();
        public List<Powers> CollectedPowers = new List<Powers>();

        // public Vector3 CurrentHealth;
        public bool HavePower(Powers power)
        {
            return CollectedPowers.Contains(power);
        }
    }
}
