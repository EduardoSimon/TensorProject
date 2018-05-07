using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemySound : MonoBehaviour
{
    public AudioClip destroyedClip;
    public AudioClip alertClip;

    private AudioSource audioSource;

	// Use this for initialization
	void Awake () {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = destroyedClip;
	}
	
    public void PlayEnemyClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
