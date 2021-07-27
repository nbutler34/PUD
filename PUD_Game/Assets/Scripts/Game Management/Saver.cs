using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Saver : MonoBehaviour
{
    public ThreeStarGM GM;

    public GameObject saveButton;
    public GameObject loadButton;
    private void Awake()
    {
        SaveSystem.Init();
        DontDestroyOnLoad(gameObject);
        saveButton = GameObject.FindGameObjectWithTag("saveButton");
        loadButton = GameObject.FindGameObjectWithTag("loadButton");

        saveButton.GetComponent<Button>().onClick.AddListener(Save);
        loadButton.GetComponent<Button>().onClick.AddListener(LoadSave);
        GM = FindObjectOfType<ThreeStarGM>();
        LoadSave();
    }

    
    public void Save()
    {
        GM = FindObjectOfType<ThreeStarGM>();

        string json = JsonConvert.SerializeObject(GM.levels, Formatting.Indented) + 'n' + JsonConvert.SerializeObject(GM.levelsUnlocked, Formatting.Indented);

        SaveSystem.Save(json);
        Debug.Log(json);
    }

    public void LoadSave()
    {
        string saveString = SaveSystem.Load();
        string[] splits = saveString.Split('n');

        Debug.Log(splits[0]);
        Debug.Log(splits[1]);

        Dictionary<int, float> levelsLoad  = JsonConvert.DeserializeObject<Dictionary<int, float>>(splits[0]);
        Dictionary<int, bool> levelsUnlockLoad = JsonConvert.DeserializeObject<Dictionary<int, bool>>(splits[1]);

        GM = FindObjectOfType<ThreeStarGM>();

        GM.levels = levelsLoad;
        GM.levelsUnlocked = levelsUnlockLoad;

    }

    public void DeleteSaves()
    {
        SaveSystem.DeleteSaves();
    }

}
