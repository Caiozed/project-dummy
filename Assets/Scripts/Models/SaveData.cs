using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class SaveData
{
    public int Slot;
    public string UpdatedOn;
    public Player PlayerData;

    public SaveData(int slot = 0)
    {
        UpdatedOn = Utils.TimeNow();
        this.Slot = slot;
        PlayerData = new Player();
    }
}
