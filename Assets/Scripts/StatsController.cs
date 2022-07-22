using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsController : MonoBehaviour
{
    public Slider healthbar;
    public int health { get; private set; } = 100;
    public int attack = 10;
    public int attackSpeed = 2;
   
    public void SetHealth(int newHealth)
    {
        if (newHealth < health) OnDamageTaken();
        else OnHealthGiven();

        health = newHealth;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDamageTaken()
    {
        if (healthbar)
        {
            healthbar.value = health;
        }
    }

    public void OnHealthGiven()
    {
        if (healthbar)
        {
            healthbar.value = health;
        }
    }
}
