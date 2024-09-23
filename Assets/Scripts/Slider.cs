using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should inherit from a class called Minigame for any minigame, and override some methods
public class Slider : MonoBehaviour
{
    public GameObject sliderPiece;
    public GameObject sliderBar;
    public GameObject sliderTarget;
    public GameObject oscillator;

    public KeyCode up, down;
    // target point on slider, between 0 and 1
    public int nSliderPieces = 10;
    public float sliderSpeed = 0.01f;
    public float pieceDistance;
    public float targetPoint = 0.5f;

    [HideInInspector]
    public List<GameObject> sliderPieces;
    private Oscillator oscillatorControl;

    // textures etc.

    // Start is called before the first frame update
    void Start()
    {
        // pull osc control script- no other components on Oscillator prefab
        oscillatorControl = Instantiate(oscillator).GetComponent<Oscillator>();
        sliderBar = Instantiate(sliderBar);
        sliderTarget = Instantiate(sliderTarget, transform.position + (Vector3.up * Random.Range(-4, 4)), Quaternion.identity);

        sliderPieces = new List<GameObject>();
        for (int pieceI = 0; pieceI < nSliderPieces; pieceI++)
        {
            GameObject piece = Instantiate(sliderPiece, transform.position + (pieceDistance * pieceI * Vector3.down), Quaternion.identity);
            sliderPieces.Add(piece);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(up))
        {
            oscillatorControl.PitchUp();
            // go to next index in list of pitches
            //oscillatorControl.PitchNext();
            sliderBar.transform.position += Vector3.up * sliderSpeed;

        }
        else if (Input.GetKey(down))
        {
            //oscillatorControl.PitchPrevious();
            oscillatorControl.PitchDown();
            sliderBar.transform.position += Vector3.down * sliderSpeed;
        }
        // should have velocity that slows down
    }

    public void Foo()
    {
        // close other eyes
        foreach (var sliderPiece in sliderPieces)
        {
            sliderPiece.GetComponent<SliderPiece>().Close();
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
