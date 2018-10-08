using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour {

    public int MaxHealth;
    public int currentHealth;
    public Slider HealthSlider;

    /// <summary>
    /// When does the enemy go to Desperation?
    /// Ex) 0.5 for half health, 0.0 for on death (never)
    /// </summary>
    public float DesperationPercent;
    /// <summary>
    /// Has Desperation been Triggered yet?
    /// </summary>
    public bool HasDesperationTriggered;

    public EnemyController enemyReference;

	// Use this for initialization
	void Start () { 
        HealthSlider = GameObject.FindGameObjectWithTag("BossHealthBar").GetComponent<Slider>(); // Find the PlayerHealthBar 
        HealthSlider.maxValue = MaxHealth;
        HealthSlider.minValue = 0f;

        currentHealth = MaxHealth;
        HasDesperationTriggered = false;
    }
	
	// Update is called once per frame
	void Update () {
        HealthSlider.value = currentHealth;
	}

    public void DecreaseHealth(int damage)
    {
        currentHealth -= damage;
        GoToDesparationMode();
        if (IsDead())
        {
            DeathProcedure();
        }
    }
    public bool IsDead()
    {
        return currentHealth <= 0;
    }

    private void DeathProcedure()
    {
        GetComponent<Animator>().SetBool("IsDead", IsDead());
        enemyReference.DisableSelf();
        //Destroy(enemyReference);
    }

    public void GoToDesparationMode()
    {
        bool isInCriticalCondition = currentHealth <= (MaxHealth * DesperationPercent);
        if (isInCriticalCondition && !HasDesperationTriggered)
        {
            enemyReference.SendMessage("DesperationMode");
            HasDesperationTriggered = true;
        }
    }
}
