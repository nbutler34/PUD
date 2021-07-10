using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{

    public float velocity;
    Rigidbody2D player;
    public AudioMixer mixer;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        velocity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = player.velocity.magnitude / 1.5f;

        mixer.SetFloat("Volume", velocity);

    }
}
