using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {
    /// <summary>
    /// Attack power of this Attack
    /// </summary>
    public int AttackPower;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.GetComponent<PlayerHealthManager>().decreaseHealth(AttackPower);
        }
    }
}
