using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour {

    public GameObject StandardBullet;
    public Transform FirePoint;
    public float FireCooldownTime;
    private float fireTimer;
    public bool IsShooting;
    public Animator animator;

	// Use this for initialization
	void Start () {
        fireTimer = 0f;
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.J))
        {
            IsShooting = true;

            if (fireTimer <= 0f) // Can Shoot again
            {
                Instantiate(StandardBullet, FirePoint.transform.position, FirePoint.transform.rotation);
                fireTimer = FireCooldownTime;
            }

        }
        else
        {
            IsShooting = false;
        }
        fireTimer -= Time.deltaTime;
        animator.SetBool("IsShooting", IsShooting);
    }
}
