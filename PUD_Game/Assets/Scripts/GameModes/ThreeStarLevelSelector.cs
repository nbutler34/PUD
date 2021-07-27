using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ThreeStarLevelSelector : MonoBehaviour
{
    public int level;
    public ThreeStarGM GM;
    public void SetLevel()
    {
        if (GM.levelsUnlocked.ElementAt(level - 1).Value)
        {
            GM.levelSelected = level;
            GM.SetLevel();
        }
        
    }
}
