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

        //converts dictionarys into json file seperated by "n" so that it can be split later
        string json = JsonConvert.SerializeObject(GM.levels, Formatting.Indented) + 'n' + JsonConvert.SerializeObject(GM.levelsUnlocked, Formatting.Indented) + 'n' +
            JsonConvert.SerializeObject(GM.playerTime, Formatting.Indented);

        SaveSystem.Save(json);
        //debug line if you need to view saved json file
        //Debug.Log(json);
    }

    public void LoadSave()
    {
        string saveString = SaveSystem.Load();
        string[] splits = saveString.Split('n');

        //seperate dictionary strings and convert them back into dictionaries
        Dictionary<int, float> levelsLoad  = JsonConvert.DeserializeObject<Dictionary<int, float>>(splits[0]);
        Dictionary<int, bool> levelsUnlockLoad = JsonConvert.DeserializeObject<Dictionary<int, bool>>(splits[1]);
        Dictionary<int, float> playerTime = JsonConvert.DeserializeObject<Dictionary<int, float>>(splits[2]);

        GM = FindObjectOfType<ThreeStarGM>();

        //take dictionaries from save data and put them into game data
        GM.levels = levelsLoad;
        GM.levelsUnlocked = levelsUnlockLoad;
        GM.playerTime = playerTime;

    }

    public void DeleteSaves()
    {
        SaveSystem.DeleteSaves();
    }

}
