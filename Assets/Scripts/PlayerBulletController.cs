using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour {

    /// <summary>
    /// Max speed of bullet
    /// </summary>
    public float MaxSpeed;

    /// <summary>
    /// Attack power of bullet
    /// </summary>
    public int AttackPower;

    PlayerController player;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();

        if (player.transform.localScale.x < 0)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.left * MaxSpeed;
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.right * MaxSpeed;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag.Equals("Enemy") && !collision.isTrigger)
        {
            Destroy(this.gameObject);
            collision.GetComponent<EnemyHealthManager>().DecreaseHealth(AttackPower);
        }

    }
}
