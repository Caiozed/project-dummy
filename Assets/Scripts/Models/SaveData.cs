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
        UpdatedOn = $"{DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}-{DateTime.Now.Hour}:{DateTime.Now.Minute}";
        this.Slot = slot;
        PlayerData = new Player();
    }
}
