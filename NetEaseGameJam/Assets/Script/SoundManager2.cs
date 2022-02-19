using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager2 : MonoBehaviour
{
    public AudioSource backGround;
    public AudioSource action;

    public static SoundManager2 instance = null;
    void Awake()
    {
        
        instance = this;
    }

    public void PlaySingle (AudioClip clip)
    {
        action.clip = clip;
        action.Play();
    }

    public void RandomizeSfx(params AudioClip [] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        action.clip = clips[randomIndex];
        action.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
