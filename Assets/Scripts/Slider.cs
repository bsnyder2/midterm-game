using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should inherit from a class called Minigame for any minigame, and override some methods
public class Slider : MonoBehaviour
{
    public GameObject oscillator;
    public KeyCode up, down;

    private Oscillator oscillatorControl;

    // textures etc.

    // Start is called before the first frame update
    void Start()
    {
        // pull osc control script- no other components on Oscillator prefab
        oscillatorControl = Instantiate(oscillator).GetComponent<Oscillator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(up))
        {
            oscillatorControl.PitchUp();
        }
        else if (Input.GetKey(down))
        {
            oscillatorControl.PitchDown();
        }
    }
}

// first idea
// bar with meter that slides back and forth
// qw keys, op keys, etc. for up and down

// public variable: target pitch for each
// should be actual notes, intervals that represent something
// start with stacked perfect 4ths
// maybe could match up with music
