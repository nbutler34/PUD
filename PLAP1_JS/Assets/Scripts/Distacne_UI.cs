using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distacne_UI : MonoBehaviour
{

    public Text distance;
    public Transform player;
    public int dis;
    public float disX;
    public float disY;
    public Transform winZone;
    // Start is called before the first frame update
    void Awake()
    {
        dis = (int)(player.position.x);
    }

    // Update is called once per frame
    void Update()
    {
        distance_get();
    }

    void distance_get()
    {
        disX = winZone.position.x - player.position.x;
        disY = winZone.position.y - player.position.y;

        dis = (int)(Mathf.Sqrt((disX * disX) + (disY * disY)));

        distance.text = ("Distance To Go: " + dis.ToString());
    }
}
