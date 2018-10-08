using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnBossDefeat : MonoBehaviour {

    public EnemyHealthManager BossHealthReference;
    
	// Use this for initialization
	void Start () {
        BossHealthReference = FindObjectOfType<EnemyHealthManager>();
        SetInvisibleAndInactive();
    }
	
	// Update is called once per frame
	void Update () {
        if (BossHealthReference.IsDead())
        {
            SetVisibleAndActive();
        }
	}

    private void SetInvisibleAndInactive()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<DoorEnterController>().enabled = false;
    }
    private void SetVisibleAndActive()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<DoorEnterController>().enabled = true;
    }
}
