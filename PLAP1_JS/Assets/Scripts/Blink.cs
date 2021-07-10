using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Blink : MonoBehaviour
{
    float lastToggle;
    public float interval = 1f;
    public GameObject text;

    void Start()
    {
        lastToggle = Time.time;
    }

    void Update()
    {
        Blinking();
    }

    void Blinking()
    {
        if (Time.time > (lastToggle + interval) )
        {
            if (text.activeInHierarchy) { text.SetActive(false); }
            else { text.SetActive(true); }

            lastToggle = Time.time;
        }
    }
}
