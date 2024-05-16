using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSound : MonoBehaviour
{
    public AudioSource Sound;
    // Start is called before the first frame update
    public float SoundDuration;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Sound.Play();
        Destroy(gameObject, SoundDuration);
        }
    }
