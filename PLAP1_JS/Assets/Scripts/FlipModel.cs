using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipModel : MonoBehaviour
{
    public Transform player;

    public float fixedRotation = 0;
    

    private float preLoc;
    private float curLoc;

    // Start is called before the first frame update
    void Start()
    {
        preLoc = 0f;
        curLoc = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        curLoc = player.transform.position.x;
        //Debug.Log("curloc" + curLoc);

        if ((curLoc - preLoc) > 0)
        {
            transform.eulerAngles = new Vector3(fixedRotation - 90, fixedRotation, 90f);
        }
        if((curLoc - preLoc) < 0)
        {
            transform.eulerAngles = new Vector3(fixedRotation - 90, fixedRotation, 90f + 180f);
        }

        preLoc = curLoc;
        //Debug.Log(preLoc);

    }


}
