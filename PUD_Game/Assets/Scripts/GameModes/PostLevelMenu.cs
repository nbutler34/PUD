using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostLevelMenu : MonoBehaviour
{
    public GameObject ThreeStarGM;

    private void Awake()
    {
        ThreeStarGM = FindObjectOfType<ThreeStarGM>().gameObject;
    }
    public void BackToLevelSelect()
    {
        ThreeStarGM = FindObjectOfType<ThreeStarGM>().gameObject;
        Destroy(ThreeStarGM);
        SceneManager.LoadScene("LevelSelect");
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene("Level");
    }

    public void NextLevel()
    {
        ThreeStarGM.GetComponent<ThreeStarGM>().levelSelected += 1;
        SceneManager.LoadScene("Level");
    }
}
