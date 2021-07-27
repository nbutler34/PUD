using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saver : MonoBehaviour
{
    public ThreeStarGM GM;
    private void Awake()
    {
        SaveSystem.Init();

    }

    public void Save()
    {
        GM = FindObjectOfType<ThreeStarGM>();

        string json = JsonConvert.SerializeObject(GM.levels, Formatting.Indented) + JsonConvert.SerializeObject(GM.levelsUnlocked, Formatting.Indented);

        SaveSystem.Save(json);
    }

    public void LoadSave()
    {
        string saveString = SaveSystem.Load();

        Dictionary<int, float> levelsLoad  = JsonConvert.DeserializeObject<Dictionary<int, float>>(saveString);
        Dictionary<int, bool> levelsUnlockLoad = JsonConvert.DeserializeObject<Dictionary<int, bool>>(saveString);

        GM.levels = levelsLoad;
        GM.levelsUnlocked = levelsUnlockLoad;

    }

    public void DeleteSaves()
    {
        SaveSystem.DeleteSaves();
    }

}
