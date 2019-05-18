using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using ActionCode2D.Renderers;
using System;
public class PlayerDataController : MonoBehaviour
{
    [HideInInspector]
    public Player PlayerModel;
    [HideInInspector]
    public int _currentHealth, _currentDamage;
    public static PlayerDataController Instance;
    public enum PlayerPower
    {
        ChargedJump,
        WallJump
    }

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //Tries to load player data
        LoadData();
        UpdateHealth();
        HealthManager.Instance.FadeIn();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {

    }


    //Load Player Data
    public void LoadData()
    {
        try
        {
            PlayerModel = SaveManager.Instance.SaveData.PlayerData;
            transform.position = PlayerModel.CurrentPosition;

        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            PlayerModel = new Player();
        }
        ResetHealth();
        ResetDamage();
    }

    public void EnablePower(PlayerPower powerToEnable)
    {
        switch (powerToEnable)
        {
            case PlayerPower.ChargedJump:
                PlayerModel.HaveChargedJump = true;
                break;
            case PlayerPower.WallJump:
                PlayerModel.HaveWallJump = true;
                break;
        }
    }

    //Update heath
    public void UpdateHealth()
    {
        HealthManager.Instance.SetHealth(_currentHealth);
    }

    public void ResetHealth()
    {
        _currentHealth = PlayerModel.MaxHealth;
        UpdateHealth();
    }

    public void ResetDamage()
    {
        _currentDamage = PlayerModel.Damage;
    }
}

