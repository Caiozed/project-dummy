using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class StoreController : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI CurrentSoulsText;
    public static StoreController Instance;
    public bool IsOpen;
    void Start()
    {
        Instance = this;
        StoreController.Instance.CurrentSoulsText.text = PlayerDataController.Instance.PlayerModel.SmallSouls.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuyUpgrade(int cost)
    {
        if (PlayerDataController.Instance.PlayerModel.SmallSouls - cost >= 0)
        {
            var oldValue = PlayerDataController.Instance.PlayerModel.SmallSouls;
            PlayerDataController.Instance.PlayerModel.SmallSouls -= cost;
            UIManager.Instance.SmallSoulsText.text = PlayerDataController.Instance.PlayerModel.SmallSouls.ToString();

            DOTween.To(() => oldValue,
        x => { CurrentSoulsText.text = x.ToString(); },
        PlayerDataController.Instance.PlayerModel.SmallSouls, 1f);
        }
    }
}
