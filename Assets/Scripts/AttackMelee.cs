using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMelee : MonoBehaviour
{
    Animator anim;
    new AudioSource audio;
    StatsController _stats;
    bool attackDelay = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        _stats = GetComponent<StatsController>();
    }

    public void Attack(GameObject attackObject)
    {
        if (!attackObject) return;
        if (attackDelay) return;

        attackDelay = true;

        StatsController stats = attackObject.GetComponent<StatsController>();
        if (stats)
        {
            anim.SetTrigger("Proximity");
            if (!audio.isPlaying) audio.PlayDelayed(1f);
            stats.SetHealth(stats.health - _stats.attack);
        }

        StartCoroutine(AttackWait());
    }

    IEnumerator AttackWait()
    {
        yield return new WaitForSeconds(_stats.attackSpeed);
        attackDelay = false;
        audio.Stop();
    }
}
