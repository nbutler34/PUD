using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject killBox;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            SceneManager.LoadScene(1);
        }
    }
}
