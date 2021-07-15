using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracking : MonoBehaviour
{
    public Transform player; //pud
    public bool strictMode = false; //whether or not to follow exact location
    bool swap = false;
    float padding;
    float screenHeight;
    float screenWidth;

    void Start()
    {
        StartCoroutine(DelaySetting(2f));
    }

    void Update()
    {
        Track(strictMode, swap); //activates camera tracking
    }

    void Track(bool strict, bool change)
    {
        if (strict) //sets the camera to follow the player's exact location
        {
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);

            if (swap)
            {
                strictMode = false;
                swap = false;
            }
        }
        else //scrolls when necessary
        {
            if (ScreenPosition(player).x < padding && ScreenPosition(player).x > 0)
            {
                MoveCamera(1, 1000f / ScreenPosition(player).x);
            }
            else if (ScreenPosition(player).x > screenWidth - padding && ScreenPosition(player).x > 0)
            {
                MoveCamera(-1, 1000f / (screenWidth - ScreenPosition(player).x));
            }
            
            if (ScreenPosition(player).y < (padding / 2) && ScreenPosition(player).y > 0)
            {
                MoveCamera(-2, 1000f / ScreenPosition(player).y);
            }
            else if(ScreenPosition(player).y > screenHeight - (padding / 2) && ScreenPosition(player).x > 0)
            {
                MoveCamera(2, 1000f / (screenHeight - ScreenPosition(player).y));
            }
        }

        if(ScreenPosition(player).x < 0 || ScreenPosition(player).y < 0)
        {
            strictMode = true;
            swap = true;
        }
    }

    IEnumerator DelaySetting(float time) //delays the setscreen so it sets to the mobile resolution
    {
        yield return new WaitForSeconds(time);
        SetScreenValues();
    }

    void SetScreenValues()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        padding = screenWidth / 5;
        Debug.Log(screenWidth + "p x " + screenHeight + "p, padding is " + padding + "p.");
    }

    Vector3 ScreenPosition(Transform obj)
    {
        Vector3 pos;

        pos = Camera.main.WorldToScreenPoint(obj.position);

        return pos;
    }

    void MoveCamera(float direction, float speed)
    {
        if (direction == -1)
        {
            Camera.main.transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else if (direction == 1)
        {
            Camera.main.transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else if (direction == -2)
        {
            Camera.main.transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
        else if (direction == 2)
        {
            Camera.main.transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
    }

}
