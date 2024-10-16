using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public int shiftsPerSecond = 4;
    public float pitchTick = 0.002f;

    private AudioSource sample;

    public List<int> pitches;
    private int currentPitchIndex;

    // Start is called before the first frame update
    void Awake()
    {
        sample = GetComponent<AudioSource>();
        currentPitchIndex = 0;
        sample.pitch = SemitonesToPitch(pitches[currentPitchIndex]);
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

    private static float SemitonesToPitch(int semitones)
    {
        return Mathf.Pow(2, semitones / 12f);
    }
}
