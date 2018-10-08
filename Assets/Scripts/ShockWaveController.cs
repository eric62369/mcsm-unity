using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveController : MonoBehaviour {

    /// <summary>
    /// Max speed of wave
    /// </summary>
    public float MaxSpeed;

    // Use this for initialization
    void Start()
    {
    }
    public void StartGoLeft()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.left * MaxSpeed;
    }
    public void StartGoRight()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.right * MaxSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.tag.Equals("Player"))
        {
            if (!collision.tag.Equals("Ground"))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
