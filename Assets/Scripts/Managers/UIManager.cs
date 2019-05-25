using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [Space]
    public Image PlayerMapPosition;
    public Image[] Map;
    public Text LevelText;
    [Space]
    [Header("Small Souls")]
    public Image SoulsImage;
    public TextMeshProUGUI SmallSoulsText;
    public Image MagicFill;
    [Space]
    [Header("Store")]
    public TextMeshProUGUI ItemDescription;
    public GameObject Store;
    bool _isMapOpen;
    void Awake()
    {
        if (Instance != null)
            GameObject.Destroy(Instance);
        else
            Instance = this;

        DontDestroyOnLoad(this);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Select"))
        {
            _isMapOpen = !_isMapOpen;
            for (int i = 0; i < Map.Length; i++)
            {
                Map[i].DOColor(_isMapOpen ? new Color(Map[i].color.r, Map[i].color.g, Map[i].color.b, .7f) : new Color(Map[i].color.r, Map[i].color.g, Map[i].color.b, 0), 0.5f);
            }
        }
        // PlayerMapPosition.transform.position = Camera.main.WorldToScreenPoint(PlayerController.PlayerCTRL.transform.position);
    }

    public void UpdateSouls(int value)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(SoulsImage.DOColor(new Color(SoulsImage.color.r, SoulsImage.color.g, SoulsImage.color.b, 1), 0.5f));
        seq.Append(SmallSoulsText.DOColor(new Color(SmallSoulsText.color.r, SmallSoulsText.color.g, SmallSoulsText.color.b, 1), 0.5f));
        seq.AppendInterval(20);
        seq.AppendCallback(() => SoulsImage.DOColor(new Color(SoulsImage.color.r, SoulsImage.color.g, SoulsImage.color.b, 0), 0.5f));
        seq.AppendCallback(() => SmallSoulsText.DOColor(new Color(SmallSoulsText.color.r, SmallSoulsText.color.g, SmallSoulsText.color.b, 0), 0.5f));

        var oldValue = PlayerDataController.Instance.PlayerModel.SmallSouls;
        PlayerDataController.Instance.PlayerModel.SmallSouls += value;

        DOTween.To(() => oldValue,
        x => { SmallSoulsText.text = x.ToString(); },
        PlayerDataController.Instance.PlayerModel.SmallSouls, 1f);

        StoreController.Instance.CurrentSoulsText.text = PlayerDataController.Instance.PlayerModel.SmallSouls.ToString();
    }

    public void UpdateMagicUI()
    {
        var currentValue = (PlayerDataController.Instance.CurrentMagic / PlayerDataController.Instance.PlayerModel.MaxMagic);
        Debug.Log(currentValue);
        MagicFill.transform.DOScaleX(currentValue, 0.5f);
    }

    public void SetText(string text)
    {
        LevelText.text = text;
    }
}
