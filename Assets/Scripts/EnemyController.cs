using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private bool isInDesperation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool IsInDesperation()
    {
        return isInDesperation;
    }

    public virtual void DesperationMode()
    {
        isInDesperation = true;
    }
    public virtual void DisableSelf()
    {
        return;
    }
    public virtual void EnableSelf()
    {
        return;
    }
}
