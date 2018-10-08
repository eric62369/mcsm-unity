using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Breakable"))
        {
            BreakGameObject(collision);
        }
    }
    private void BreakGameObject(Collider2D collision)
    {
        collision.gameObject.SendMessage("Break"); // Tell the breakable object to break itself.
    }
}
