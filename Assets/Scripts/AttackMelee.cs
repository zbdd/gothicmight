using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMelee : MonoBehaviour
{
    public Animator anim;
    public new AudioSource audio;
    public StatsController stats;
    private bool _attackDelay = false;

    public void Attack(GameObject other)
    {
        if (_attackDelay) return;
        
        _attackDelay = true;
        if (anim) anim.SetTrigger("Proximity");
        if (!audio.isPlaying) audio.PlayDelayed(1f);

        if (other)
        {
            StatsController otherStats = other.GetComponent<StatsController>();
            if (otherStats)
            {
                otherStats.SetHealth(otherStats.health - stats.attack);
            }
        }

        var delay = 1f;
        if (stats) delay = stats.attackSpeed; 
        
        StartCoroutine(AttackWait(delay));
    }

    IEnumerator AttackWait(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        _attackDelay = false;
        audio.Stop();
    }
}
