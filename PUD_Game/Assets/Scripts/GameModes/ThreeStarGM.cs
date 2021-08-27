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
        {5, 3f},
        {6, 4f},
        {7, 5f},
        {8, 5f},
        {9, 5f},
        {10, 3f},
        {11, 4f},
        {12, 5f},
        {13, 5f},
        {14, 5f},
        {15, 3f},
        {16, 4f},
        {17, 5f},
        {18, 5f},
        {19, 5f},
        {20, 3f}
    };

    public Dictionary<int, bool> levelsUnlocked = new Dictionary<int, bool>()
    {
        {1, true},
        {2, false},
        {3, false},
        {4, false},
        {5, false},
        {6, false},
        {7, false},
        {8, false},
        {9, false},
        {10, false},
        {11, false},
        {12, false},
        {13, false},
        {14, false},
        {15, false},
        {16, false},
        {17, false},
        {18, false},
        {19, false},
        {20, false}
    };

    public Dictionary<int, float> playerTime = new Dictionary<int, float>()
    {
        {1, 100f},
        {2, 100f},
        {3, 100f},
        {4, 100f},
        {5, 100f},
        {6, 100f},
        {7, 100f},
        {8, 100f},
        {9, 100f},
        {10, 100f},
        {11, 100f},
        {12, 100f},
        {13, 100f},
        {14, 100f},
        {15, 100f},
        {16, 100f},
        {17, 100f},
        {18, 100f},
        {19, 100f},
        {20, 100f}
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
