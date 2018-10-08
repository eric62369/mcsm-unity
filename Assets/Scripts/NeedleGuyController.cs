using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleGuyController : EnemyController {

    /// <summary>
    /// How long to wait after each attack.
    /// </summary>
    public float CycleCooldownTime;
    private float cycleTimer;
    /// <summary>
    /// Whether or not NeedleGuy is rising
    /// </summary>
    private bool isRising;
    public float RiseTime;
    private float riseTimer;
    /// <summary>
    /// Whether or not NeedleGuy is attacking or not.
    /// </summary>
    private bool isAttacking;
    public float AttackTime;
    private float attackTimer;

    /// <summary>
    /// Where's the player?
    /// </summary>
    private PlayerController player;
    private Vector3 startingPosition;
    private Vector3 targetPosition;
    private float groundLevel;

    /// <summary>
    /// How high NeedleGuy jumps up to attack
    /// </summary>
    public float DropHeight;

    /// <summary>
    /// Reference to the shockwaves that are sent out.
    /// </summary>
    public GameObject shockWave;

    /// <summary>
    /// How much should the pattern speed up?
    /// 0.5 for double speed (half time)
    /// 0.8 for (80% of original time)
    /// </summary>
    public float DesperationSpeedModifier;

    public Animator animator;
    public EnemyHealthManager enemyHealthManager;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerController>();
        cycleTimer = CycleCooldownTime;
        riseTimer = 0f;
        groundLevel = GetComponent<Transform>().position.y;
        animator = GetComponent<Animator>();
        enemyHealthManager = GetComponent<EnemyHealthManager>();
    }
	
	// Update is called once per frame
	void Update () {
        animator.SetBool("IsRising", isRising);
        animator.SetBool("IsAttacking", isAttacking);
        if (!isAttacking && !isRising) // Cycle Timer can run 
        {
            bool isDone = CycleCoolDown();
            if (isDone)
            {
                isRising = true;
                UpdateRisePositions();
                if (base.IsInDesperation())
                {
                    DropAttack();
                }
            }
        }
        else if (isRising) // Rise Lerp
        {
            bool isDone = RiseLerp();
            if (isDone)
            {
                isRising = false;
                isAttacking = true;
                UpdateAttackPositions();
            }
        }
        else // Attack Lerp
        {
            bool isDone = AttackLerp();
            if (isDone)
            {
                isAttacking = false;
                DropAttack();
            }
        }
	}

    private bool CycleCoolDown()
    {
        cycleTimer -= Time.deltaTime;
        if (cycleTimer <= 0f)
        {
            cycleTimer = CycleCooldownTime;
            return true;
        }
        return false;
    }
    private bool RiseLerp()
    {
        riseTimer += Time.deltaTime;
        if (riseTimer >= RiseTime)
        {
            riseTimer = 0f;
            return true;
        }
        Lerp(riseTimer, RiseTime, startingPosition, targetPosition);
        return false;
    }
    private bool AttackLerp()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= AttackTime)
        {
            attackTimer = 0f;
            return true;
        }
        Lerp(attackTimer, AttackTime, startingPosition, targetPosition);
        return false;
    }

    private void Lerp(float currentTime, float lerpTime, Vector3 start, Vector3 end)
    {
        float Perc = currentTime / lerpTime;
        GetComponent<Transform>().position = Vector3.Lerp(start, end, Perc);
    }
    private void UpdateRisePositions()
    {
        // TODO: Is Reference or Value?
        startingPosition = GetComponent<Transform>().position;
        targetPosition = player.GetComponent<Transform>().position;
        targetPosition.y = groundLevel + DropHeight;
    }
    private void UpdateAttackPositions()
    {
        startingPosition = GetComponent<Transform>().position;
        targetPosition = startingPosition;
        targetPosition.y -= DropHeight;
    }
    private void DropAttack()
    {
        Transform thisTransform = GetComponent<Transform>();
        Vector3 groundPosition = new Vector3(thisTransform.position.x, groundLevel, thisTransform.position.z);
        Instantiate(shockWave, groundPosition, thisTransform.transform.rotation).GetComponent<ShockWaveController>().StartGoLeft();
        Instantiate(shockWave, groundPosition, thisTransform.transform.rotation).GetComponent<ShockWaveController>().StartGoRight();
    }

    public override void DesperationMode()
    {
        base.DesperationMode();
        CycleCooldownTime *= DesperationSpeedModifier;
        RiseTime *= DesperationSpeedModifier;
        AttackTime *= DesperationSpeedModifier;
        GetComponent<SpriteRenderer>().color = new Color(1, 0.7f, 0.7f);
        animator.SetBool("IsInDesperation", true);
    }

    public override void DisableSelf()
    {
        GetComponent<NeedleGuyController>().enabled = false;
        BecomeInvulnerable();
    }
    public void BecomeInvulnerable()
    {
        GetComponent<EnemyHealthManager>().enabled = false;
    }
    public override void EnableSelf()
    {
        cycleTimer = CycleCooldownTime;
        riseTimer = 0f;
        isRising = false;
        attackTimer = 0f;
        isAttacking = false;
        GetComponent<NeedleGuyController>().enabled = true;
        BecomeVulnerable();
    }
    public void BecomeVulnerable()
    {
        GetComponent<EnemyHealthManager>().enabled = true;
    }
}
