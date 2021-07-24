using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeStarLevelSelector : MonoBehaviour
{
    public int level;
    public ThreeStarGM GM;
    public void SetLevel()
    {
        GM.levelSelected = level;
    }
}
