using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class SoulPickupController : MonoBehaviour
{
    // Start is called before the first frame update
    public int Souls;
    public GameObject particles;
    public SoulType CurrentSoulType;
    public string bossName;
    BossSoul BossSoul;
    public enum SoulType
    {
        Commun = 0,
        Rare = 1,
        Boss = 2
    }

    void Start()
    {
        BossSoul = new BossSoul("Skeleton King Soul", "POWER OF VOID");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            switch (CurrentSoulType)
            {
                case SoulType.Commun:
                    Instantiate(particles, transform.position, transform.rotation);
                    UIManager.Instance.UpdateSouls(Souls);
                    break;
                case SoulType.Boss:
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
                    break;
            }
            Destroy(gameObject);
        }
    }
}
