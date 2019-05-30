using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class BossSoul
{
    public string Name;
    public string Description;
    // public Vector3 CurrentHealth;

    public BossSoul(string name = "NAME HERE", string description = "DESC HERE")
    {
        Name = name;
        Description = description;
    }
}
