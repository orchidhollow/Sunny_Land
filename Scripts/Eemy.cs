using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eemy : MonoBehaviour
{
    protected Animator Anim;
    protected AudioSource DeathAudio;
    protected virtual void Start()
    {
        Anim=GetComponent<Animator>();
        DeathAudio = GetComponent<AudioSource>();
    }
    public void Death()
    {
        GetComponent<Collider2D>().enabled=false;
        Destroy(gameObject);
    }
    
    public void JumpOn()
    {
        Anim.SetTrigger("death");
        DeathAudio.Play();
    }
}
