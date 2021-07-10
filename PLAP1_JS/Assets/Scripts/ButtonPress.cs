using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPress : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartScene()
    {
        if (FigmentInput.GetButtonUp(FigmentInput.FigmentButton.ActionButton))
        {
            SceneManager.LoadScene(0);
        }
    }
}
