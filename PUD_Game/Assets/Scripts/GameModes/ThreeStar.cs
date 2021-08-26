using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class ThreeStar : MonoBehaviour
{
    public float timer;
    public bool startLevel;
    public bool endLevel;

    public ThreeStarGM GM;
    public Saver saver;

    public GameObject endScreen;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public Text timerText;
    public Text timerTextOnScreen;

    private void Start()
    {
        GM = FindObjectOfType<ThreeStarGM>();
        saver = FindObjectOfType<Saver>();
    }
    private void Update()
    {
        if (startLevel && !endLevel)
        {
            timer += Time.deltaTime;
            //rounds timer to 3 decimal places ex. 0.000
            timerTextOnScreen.text = (Mathf.Round(timer * 1000f) / 1000f).ToString();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Start"))
        {
            startLevel = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            endLevel = true;            
            endScreen.SetActive(true);
            timer = Mathf.Round(timer * 1000f) / 1000f;
            timerText.text = timer.ToString();
            SetEndScreen();
        }
    }

    public void SetEndScreen()
    {

        //system to set the amount of stars the player got on the level
        if (timer < GM.timeToBeat)
        {
            star1.SetActive(true);
            star2.SetActive(true);
            star3.SetActive(true);

        }
        else if(timer < GM.timeToBeat + 3f)
        {
            star1.SetActive(true);
            star2.SetActive(true);
        }
        else if(timer < GM.timeToBeat + 6)
        {
            star3.SetActive(true);
        }

        if(timer < GM.timeToBeat + 6)
        {            
            GM.levelsUnlocked[GM.levelSelected + 1] = true;
        }

        //set dictionary value for playertime for the level just played
        if(timer < GM.playerTime[GM.levelSelected])
        {
            GM.playerTime[GM.levelSelected] = timer;
        }
        

        saver.Save();
    }
}
