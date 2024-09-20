using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should inherit from a class called Minigame for any minigame, and override some methods
public class Slider : MonoBehaviour
{
    public GameObject oscillator;
    public GameObject sliderBar;
    public GameObject sliderTarget;
    public GameObject sliderPiece;

    public KeyCode up, down;

    private List<GameObject> sliderPieces;


    // target point on slider, between 0 and 1
    public float targetPoint = 0.5f;

    private Oscillator oscillatorControl;
    private float dist = 0.005f;


    // textures etc.

    // Start is called before the first frame update
    void Start()
    {
        // pull osc control script- no other components on Oscillator prefab
        oscillatorControl = Instantiate(oscillator).GetComponent<Oscillator>();
        sliderBar = Instantiate(sliderBar, transform.position, Quaternion.identity);
        sliderTarget = Instantiate(sliderTarget, transform.position + (Vector3.up * Random.Range(-4, 4)), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(up))
        {
            oscillatorControl.PitchUp();
            sliderBar.transform.position += Vector3.up * dist;

        }
        else if (Input.GetKey(down))
        {
            oscillatorControl.PitchDown();
            sliderBar.transform.position += Vector3.down * dist;

        }
        // should have velocity that slows down
    }
}

// first idea
// bar with meter that slides back and forth
// qw keys, op keys, etc. for up and down

// public variable: target pitch for each
// should be actual notes, intervals that represent something
// start with stacked perfect 4ths
// maybe could match up with music
