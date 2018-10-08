using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObjectReceiver : MonoBehaviour {

    public void Break()
    {
        Destroy(this.gameObject);
    }
}
