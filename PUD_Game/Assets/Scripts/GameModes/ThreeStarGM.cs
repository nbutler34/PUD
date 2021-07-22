using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThreeStarGM : MonoBehaviour
{
    //dictionary 
    //key = level selcted 
    //value = time to beat
    Dictionary<int, float> levels = new Dictionary<int, float>()
    {
        {1, 4f},
        {2, 5f}
    };

    public int levelSelected;
    public float timeToBeat;

    private void Start()
    {
        SetLevel();
    }
    public void SetLevel()
    {
        levelSelected = levels.ElementAt(1).Key;
        timeToBeat = levels.ElementAt(1).Value;

        Debug.Log(levelSelected + "  " + timeToBeat);
    }

    
}
