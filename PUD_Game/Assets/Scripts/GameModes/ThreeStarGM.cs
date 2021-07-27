using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThreeStarGM : MonoBehaviour
{
    //dictionary 
    //key = level selcted 
    //value = time to beat
    public Dictionary<int, float> levels = new Dictionary<int, float>()
    {
        {1, 4f},
        {2, 5f},
        {3, 5f}
    };

    public Dictionary<int, bool> levelsUnlocked = new Dictionary<int, bool>()
    {
        {1, true},
        {2, false},
        {3, false}
    };

    public int levelSelected;
    public float timeToBeat;

    public GameObject Pud;
    public GameObject[] Levels;

    public GameObject levelSpawn;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SetLevel()
    {
        
        timeToBeat = levels.ElementAt(levelSelected - 1).Value;

        Debug.Log(levelSelected + "  " + timeToBeat);
        Delay();
        SceneManager.LoadScene("Level");     
        
    }

   
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
    }

    public void SetGameUp()
    {
        levelSpawn = GameObject.FindGameObjectWithTag("LevelSpawn");
        Instantiate(Levels[levelSelected - 1], levelSpawn.transform);
        Instantiate(Pud, GameObject.FindGameObjectWithTag("PudSpawn").transform, false);
    }

    
    
}
