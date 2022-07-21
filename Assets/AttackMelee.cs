using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMelee : MonoBehaviour
{
    Animator anim;
    new AudioSource audio;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger("Proximity");
          
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetTrigger("Proximity");
            if (!audio.isPlaying) audio.PlayDelayed(1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        audio.Stop();
    }
}
