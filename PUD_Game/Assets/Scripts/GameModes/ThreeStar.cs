using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThreeStar : MonoBehaviour
{
    public float timer;
    public bool startLevel;
    public bool endLevel;

    public ThreeStarGM GM;

    public GameObject endScreen;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public Text timerText;

    private void Start()
    {
        GM = FindObjectOfType<ThreeStarGM>();
    }
    private void Update()
    {
        if (startLevel && !endLevel)
        {
            timer += Time.deltaTime;
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
            timerText.text = timer.ToString();
            SetEndScreen();
        }
    }

    public void SetEndScreen()
    {
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
    }
}
