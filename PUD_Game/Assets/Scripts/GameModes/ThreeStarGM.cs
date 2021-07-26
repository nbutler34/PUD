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
    Dictionary<int, float> levels = new Dictionary<int, float>()
    {
        {1, 4f},
        {2, 5f},
        {3, 5f}
    };

    public int levelSelected;
    public float timeToBeat;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SetLevel()
    {
        
        timeToBeat = levels.ElementAt(levelSelected).Value;

        Debug.Log(levelSelected + "  " + timeToBeat);
        Delay();
        SceneManager.LoadScene("Level");

    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
    }
    
}
