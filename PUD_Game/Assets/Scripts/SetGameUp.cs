using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameUp : MonoBehaviour
{
    public ThreeStarGM GM;

    private void Awake()
    {
        GM = FindObjectOfType<ThreeStarGM>();
    }

    private void Start()
    {
        GM.SetGameUp();
    }
}
