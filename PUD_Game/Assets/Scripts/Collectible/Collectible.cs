using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public string collectibleType;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (collectibleType)
            {
                case "coin":
                    Destroy(gameObject);
                    break;
                case "boost":
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new UnityEngine.Vector2(300f,300f));
                    Destroy(gameObject);
                    break;

            }
        }
    }
}
