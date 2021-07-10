using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSwing : MonoBehaviour
{
    public LineRenderer line;
    DistanceJoint2D joint;
    public float fixedRotation = 0;
    Rigidbody2D playerRB;
    Vector3 targetPos;
    RaycastHit2D hit;
    Transform player;

    void Start()
    {
        //Initialize variables
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = true;
        line.enabled = true;
        player = transform;
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //MAINTENANCE

        //Locks player rotation
        LockRotation();

        if (line.enabled)
        {
            //Updates the location of the end of the rope to the player's location if the line is enabled
            UpdateLineEnd();
        }

        void LockRotation()
        {
            //player.eulerAngles = new Vector3(player.eulerAngles.x, player.eulerAngles.y, player.eulerAngles.z);
            //example of unlocking rotation axes

            player.eulerAngles = new Vector3(fixedRotation, fixedRotation, fixedRotation);
            //locks all player rotation axes
        }

        void UpdateLineEnd()
        {
            line.SetPosition(0, transform.position - line.transform.position);
        }
    }
}
