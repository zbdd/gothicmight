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
    public bool showHealthbar = false;
    public float healthBarfade = 1f;

    private Animator _anim;
    private static readonly int Hit = Animator.StringToHash("Hit");

    public void SetHealth(int newHealth)
    {
        if (newHealth < health) OnDamageTaken(newHealth);
        else OnHealthGiven(newHealth);
    }
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDamageTaken(int newHealth)
    {
        health = newHealth;
        
        if (healthbar)
        {
            healthbar.value = health;
        }

        if (!_anim) return;
        
        _anim.SetTrigger(Hit);
        if (showHealthbar) return;
        
        healthbar.gameObject.SetActive(true);
        StartCoroutine(HideHealthbar());
    }

    private IEnumerator HideHealthbar()
    {
        yield return new WaitForSeconds(healthBarfade);
        healthbar.gameObject.SetActive(false);
    }

    public void OnHealthGiven(int newHealth)
    {
        health = newHealth;
        
        if (healthbar)
        {
            healthbar.value = health;
        }
    }
}
