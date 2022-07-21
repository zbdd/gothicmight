using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMelee : MonoBehaviour
{
    Animator anim;
    new AudioSource audio;
    ArrayList hostileTo;
    GameObject canAttack;
    StatsController _stats;
    bool attackDelay = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
        hostileTo = GetComponent<NPCController>().hostileTo;
        _stats = GetComponent<StatsController>();
    }

    private void Update()
    {
        Attack();

        canAttack = null;
    }

    private void Attack()
    {
        if (!canAttack) return;
        if (attackDelay) return;

        attackDelay = true;

        StatsController stats = canAttack.GetComponent<StatsController>();
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" || other.tag == "NPC")
        {
            foreach (GameObject obj in hostileTo)
            {
                if (obj == other.gameObject) canAttack = other.gameObject;
            } 
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" || other.tag == "NPC")
        {
            foreach (GameObject obj in hostileTo)
            {
                if (obj == other.gameObject) canAttack = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        audio.Stop();
    }
}
