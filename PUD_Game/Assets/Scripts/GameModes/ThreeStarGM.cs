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
        {3, 5f},
        {4, 5f},
        {5, 3f}
    };

    public Dictionary<int, bool> levelsUnlocked = new Dictionary<int, bool>()
    {
        {1, true},
        {2, false},
        {3, false},
        {4, false},
        {5, false}
    };

    public Dictionary<int, float> playerTime = new Dictionary<int, float>()
    {
        {1, 100f},
        {2, 100f},
        {3, 100f},
        {4, 100f},
        {5, 100f}
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
