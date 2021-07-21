using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeStar : MonoBehaviour
{
    public float timer;
    public bool startLevel;
    public bool endLevel;


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
        }
    }
}
