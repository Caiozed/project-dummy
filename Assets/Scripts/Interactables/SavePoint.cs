using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

public class SavePoint : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent OnTrigger;
    Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _anim.SetTrigger("IsActive");

            //Update playerModel position
            var player = other.transform.gameObject.GetComponent<PlayerController>();
            player.PlayerModel.CurrentPosition = transform.position;
            SaveManager.Instance.SaveData.PlayerData = player.PlayerModel;
            SaveManager.Instance.SaveData.UpdatedOn = Utils.TimeNow();

            //Save player data
            SaveManager.Instance.SaveModel<SaveData>(SaveManager.Instance.SaveData,
            $"SaveData{SaveManager.Instance.SaveData.Slot}.dat");
        }
    }
}
