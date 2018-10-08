using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour {

    public int MaxHealth;
    public int currentHealth;

    public PlayerController playerReference;
    //TODO: Don't forget other scripts

    public Slider HealthSlider;

    // Use this for initialization
    void Start()
    {
        HealthSlider = GameObject.FindGameObjectWithTag("PlayerHealthBar").GetComponent<Slider>(); // Find the PlayerHealthBar 
        HealthSlider.maxValue = MaxHealth;
        HealthSlider.minValue = 0f;
        currentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthSlider.value = currentHealth;
    }
    public void increaseHealth(int healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }
    }
    public void decreaseHealth(int damage)
    {
        currentHealth -= damage;
        if (IsDead())
        {
            DeathProcedure(); //TODO: Death Procedure
        }
    }
    private bool IsDead()
    {
        return currentHealth <= 0;
    }

    private void DeathProcedure()
    {
        Time.timeScale = 0.0f;
        Destroy(this.gameObject);
        Destroy(playerReference);
    }

}
