using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public Image FadeImage;
    public float FadeSpeed;
    List<SaveData> SaveSlots;
    public Transform LoadSlotsContainer;
    public GameObject LoadBtnPrefab;
    public List<GameObject> Slots;
    bool _started = false;

    // Start is called before the first frame update
    void Start()
    {
        SaveSlots = new List<SaveData>();
        // for (int i = 0; i < 3; i++)
        // {
        //     if (File.Exists($"{Application.persistentDataPath}/SaveData{i}.dat"))
        //         SaveSlots.Add(SaveManager.Instance.LoadModel<SaveData>($"SaveData{i}.dat"));
        // }


        //Check for existing savefiles
        for (int i = 0; i < Slots.Count; i++)
        {
            int x = i;
            if (File.Exists($"{Application.persistentDataPath}/SaveData{x}.dat"))
            {
                var file = SaveManager.Instance.LoadModel<SaveData>($"SaveData{x}.dat");
                Slots[x].GetComponentInChildren<Text>().text = $"Slot {x + 1}\n {file.UpdatedOn}";
                Slots[x].GetComponentInChildren<Button>().onClick.AddListener(delegate { StartCoroutine(LoadGame(x)); });
            }
            else
            {
                Slots[x].GetComponentInChildren<Text>().text = $"Slot {x + 1}\n New Game";
                Slots[x].GetComponentInChildren<Button>().onClick.AddListener(delegate { StartCoroutine(NewGame(x)); });
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Fade to black
        if (_started)
        {
            FadeImage.color = new Color(0, 0, 0, FadeImage.color.a + FadeSpeed);
        }
    }

    IEnumerator NewGame(int slot)
    {
        var savedata = new SaveData(slot);
        SaveManager.Instance.SaveModel<SaveData>(savedata, $"SaveData{slot}.dat");
        _started = true;
        yield return new WaitForSeconds(2);
        LoadingManager.LoadLevel(1);
    }

    IEnumerator LoadGame(int slot)
    {
        SaveManager.Instance.SaveData = SaveManager.Instance.LoadModel<SaveData>($"SaveData{slot}.dat");
        _started = true;
        yield return new WaitForSeconds(2);
        LoadingManager.LoadLevel(1);
    }
}
