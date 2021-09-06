using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPP : MonoBehaviour
{
    public GameObject PPVolume;

    // Start is called before the first frame update
    void Start()
    {
        GameObject postProcess = Instantiate(PPVolume, new Vector2(100, 100), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
