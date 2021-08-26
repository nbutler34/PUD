using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ThreeStarLevelSelector : MonoBehaviour
{
    public int level;
    public ThreeStarGM GM;
    public Text playerTime;
    public GameObject[] stars;
    private void Start()
    {
        

        //system to set the amount of stars the player got on the level
        //unless no time is recorded aka -1 time;
        if (GM.playerTime.ElementAt(level - 1).Value != 100)
        {
            playerTime.text = GM.playerTime[level].ToString();
            if (GM.playerTime.ElementAt(level - 1).Value < GM.levels.ElementAt(level - 1).Value)
            {
                stars[0].SetActive(true);
                stars[1].SetActive(true);
                stars[2].SetActive(true);

            }
            else if (GM.playerTime.ElementAt(level - 1).Value < GM.levels.ElementAt(level - 1).Value + 3f)
            {
                stars[0].SetActive(true);
                stars[1].SetActive(true);
            }
            else if (GM.playerTime.ElementAt(level - 1).Value < GM.levels.ElementAt(level - 1).Value + 6f)
            {
                stars[2].SetActive(true);
            }
        }
        
    }
    public void SetLevel()
    {
        if (GM.levelsUnlocked.ElementAt(level - 1).Value)
        {
            GM.levelSelected = level;
            GM.SetLevel();
        }
        
    }
}
