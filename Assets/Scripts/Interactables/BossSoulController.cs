using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Models;
public class BossSoulController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject particles;
    public Powers CurrentPowerType;
    public string BossName;
    public string PowerName;
    BossSoul BossSoul;

    void Start()
    {
        BossSoul = new BossSoul(BossName, PowerName, CurrentPowerType);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Instantiate(particles, transform.position, transform.rotation);
            var seq = DOTween.Sequence();
            Color newColor = UIManager.Instance.Background.color;
            newColor.a = .8f;
            seq.Append(UIManager.Instance.Background.DOColor(newColor, 3f))
            .AppendInterval(2)
            .AppendCallback(() =>
            {
                newColor.a = 0;
                UIManager.Instance.Background.DOColor(newColor, 3f);
            });

            UIManager.Instance.UpdateBossSouls(BossSoul);
            PlayerDataController.Instance.EnablePower(CurrentPowerType);
            SaveManager.Instance.SaveModel<SaveData>(SaveManager.Instance.SaveData, $"SaveData{SaveManager.Instance.SaveData.Slot}.dat");
            Destroy(gameObject);
        }
    }
}
