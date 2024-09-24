using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public int shiftsPerSecond = 4;
    public float pitchTick = 0.002f;

    private AudioSource sample;
    //private float pitchFrameTimer;

    public List<int> pitches;
    private int currentPitchIndex;
    //private float lerpTimer = 4;
    // other controls for tremolo etc.

    // Start is called before the first frame update
    void Start()
    {
        sample = GetComponent<AudioSource>();
        currentPitchIndex = 0;
    }

    public void PitchNext()
    {
        // if last pitch...
        if (currentPitchIndex >= (pitches.Count - 1)) return;
        currentPitchIndex++;
        sample.pitch = SemitonesToPitch(pitches[currentPitchIndex]);
    }

    public void PitchPrevious()
    {
        // if first pitch...
        if (currentPitchIndex <= 0) return;
        currentPitchIndex--;
        sample.pitch = SemitonesToPitch(pitches[currentPitchIndex]);
    }

    //public void PitchUp()
    //{
    //    if (sample.pitch < 0)
    //    {
    //        sample.pitch = 0;
    //    }
    //    // (4 shifts per second)
    //    // every 1/4 second,
    //    // add to current pitch: 0.02 / 4 = 0.005
    //    // (8 shifts per second)
    //    // every 1/8 second,
    //    // add to current pitch: 0.02 / 8 = 0.0025

    //    // next pitch shift here
    //    if (pitchFrameTimer <= 0)
    //    {
    //        sample.pitch += (pitchTick / shiftsPerSecond);
    //        pitchFrameTimer = (1 / shiftsPerSecond);
    //    }
    //    pitchFrameTimer -= Time.deltaTime;
    //}

    //public void PitchDown()
    //{
    //    if (sample.pitch < 0)
    //    {
    //        sample.pitch = 0;
    //    }
    //    // next pitch shift here
    //    if (pitchFrameTimer <= 0)
    //    {
    //        sample.pitch -= (pitchTick / shiftsPerSecond);
    //        pitchFrameTimer = (1 / shiftsPerSecond);
    //    }
    //    pitchFrameTimer -= Time.deltaTime;
    //}

    private static float SemitonesToPitch(int semitones)
    {
        return Mathf.Pow(2, semitones / 12f);
    }
}
