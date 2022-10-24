using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource;
    [SerializeField]//序列化
    private AudioClip JumpAudio,HurtAudio,CherryAudio,GemAudio;

    private void Awake() {
        instance=this;
    }

    public void Jump_Audio()
    {
        audioSource.clip=JumpAudio;
        audioSource.Play();
    }

    public void Hurt_Audio()
    {
        audioSource.clip=HurtAudio;
        audioSource.Play();
    }
    public void Cherry_Audio()
    {
        audioSource.clip=CherryAudio;
        audioSource.Play();
    }
    public void Gem_Audio()
    {
        audioSource.clip=GemAudio;
        audioSource.Play();
    }
}
