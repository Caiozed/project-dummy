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
    [Space]
    [Header("Messages")]
    public Image MessageImage;
    public TextMeshProUGUI Messages;
    [Space]
    [Header("Small Souls")]
    public Image SoulsImage;
    public TextMeshProUGUI SmallSoulsText;
    public Image MagicFill;
    [Space]
    [Header("Store")]
    public TextMeshProUGUI ItemDescription;
    public Image Background;
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

    public void UpdateBossSouls(BossSoul soul)
    {
        PlayerDataController.Instance.PlayerModel.CollectedBossSouls.Add(soul);

        var seq = DOTween.Sequence();
        Color newColor = UIManager.Instance.MessageImage.color;
        newColor.a = .8f;
        seq.Append(MessageImage.DOColor(newColor, 3f))
                    .AppendInterval(2)
                    .AppendCallback(() =>
                    {
                        newColor.a = 0;
                        MessageImage.DOColor(newColor, 3f);
                    });
        SetText(soul.Name);
    }


    public void UpdateMagicUI()
    {
        var currentValue = (PlayerDataController.Instance.CurrentMagic / PlayerDataController.Instance.PlayerModel.MaxMagic);
        MagicFill.transform.DOScaleX(currentValue, 0.5f);
    }

    public void SetText(string text)
    {
        Messages.text = text;
        Sequence seq = DOTween.Sequence();
        seq.Append(Messages.DOColor(new Color(1, 1, 1, 1), 3f))
        .AppendInterval(2)
        .AppendCallback(() => Messages.DOColor(new Color(1, 1, 1, 0), 3f));
    }

    public void FadeIn()
    {
        Background.DOColor(new Color(Background.color.r, Background.color.g, Background.color.b, 0f), 2f);
    }

    public void FadeOut()
    {
        Background.DOColor(new Color(Background.color.r, Background.color.g, Background.color.b, 1f), 2f);
    }

    public void FadeBackground(float alpha, float time)
    {
        Background.DOColor(new Color(Background.color.r, Background.color.g, Background.color.b, alpha), time);
    }
}
