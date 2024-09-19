using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public int shiftsPerSecond = 4;
    public float pitchTick = 0.002f;
    public int semitones;

    private AudioSource sample;
    private float pitchFrameTimer;
    //private float lerpTimer = 4;
    // other controls for tremolo etc.

    // Start is called before the first frame update
    void Start()
    {
        sample = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PitchUp()
    {
        if (sample.pitch < 0)
        {
            sample.pitch = 0;
        }
        // (4 shifts per second)
        // every 1/4 second,
        // add to current pitch: 0.02 / 4 = 0.005
        // (8 shifts per second)
        // every 1/8 second,
        // add to current pitch: 0.02 / 8 = 0.0025

        // next pitch shift here
        if (pitchFrameTimer <= 0)
        {
            sample.pitch += (pitchTick / shiftsPerSecond);
            pitchFrameTimer = (1 / shiftsPerSecond);
        }
        pitchFrameTimer -= Time.deltaTime;
    }

    public void PitchDown()
    {
        if (sample.pitch < 0)
        {
            sample.pitch = 0;
        }
        // next pitch shift here
        if (pitchFrameTimer <= 0)
        {
            sample.pitch -= (pitchTick / shiftsPerSecond);
            pitchFrameTimer = (1 / shiftsPerSecond);
        }
        pitchFrameTimer -= Time.deltaTime;
    }
}
