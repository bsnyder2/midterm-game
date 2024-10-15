using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    public AudioClip swordSlash;
    private AudioSource sample;

    // Start is called before the first frame update
    void Start()
    {
        sample = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Play()
    {
        Debug.Log("Slash");
        sample.PlayOneShot(swordSlash);
    }
}
