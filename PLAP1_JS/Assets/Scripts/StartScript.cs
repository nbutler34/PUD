using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (FigmentInput.GetButton(FigmentInput.FigmentButton.ActionButton))
        {
            SceneManager.LoadScene(1);
        }
        else if (FigmentInput.GetButton(FigmentInput.FigmentButton.LeftButton))
        {
            SceneManager.LoadScene(1);
        }
        else if (FigmentInput.GetButton(FigmentInput.FigmentButton.RightButton))
        {
            SceneManager.LoadScene(1);
        }
    }
}
