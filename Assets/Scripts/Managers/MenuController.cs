using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    List<SaveData> SaveSlots;
    public Transform LoadSlotsContainer;
    public GameObject LoadBtnPrefab;
    // Start is called before the first frame update
    void Start()
    {
        SaveSlots = new List<SaveData>();
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists($"{Application.persistentDataPath}/SaveData{i}.dat"))
                SaveSlots.Add(SaveManager.Instance.LoadModel<SaveData>($"SaveData{i}.dat"));
        }

        for (int i = 0; i < SaveSlots.Count; i++)
        {
            int x = i;
            var gameObj = Instantiate(LoadBtnPrefab, LoadSlotsContainer.position + new Vector3(0, 100 * -i, 0), LoadSlotsContainer.rotation);
            gameObj.GetComponentInChildren<Text>().text = SaveSlots[i].UpdatedOn;
            gameObj.GetComponentInChildren<Button>().onClick.AddListener(delegate { LoadGame(x); });
            gameObj.transform.SetParent(LoadSlotsContainer);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewGame(int slot)
    {
        var savedata = new SaveData(slot);
        SaveManager.Instance.SaveModel<SaveData>(savedata, $"SaveData{slot}.dat");
    }

    public void LoadGame(int slot)
    {
        SaveManager.Instance.SaveData = SaveManager.Instance.LoadModel<SaveData>($"SaveData{slot}.dat");
        Debug.Log(SaveManager.Instance.SaveData.Slot);
        SceneManager.LoadScene(1);
    }
}
