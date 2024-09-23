using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should inherit from a class called Minigame for any minigame, and override some methods
public class Slider : MonoBehaviour
{
    public GameObject sliderPiece;
    public GameObject sliderBar;
    //public GameObject sliderTarget;

    public KeyCode up, down;
    // target point on slider, between 0 and 1
    public int nSliderPieces = 10;
    public float sliderSpeed = 0.01f;
    public float pieceStart = 4f;
    public float pieceDistance = 2f;
    //public float targetPoint = 0.5f;
    private float pieceEnd;

    private List<GameObject> sliderPieces;

    // textures etc.

    // Start is called before the first frame update
    void Start()
    {
        // pull osc control script- no other components on Oscillator prefab
        sliderBar = Instantiate(sliderBar, transform.position, Quaternion.identity);
        //sliderTarget = Instantiate(sliderTarget, transform.position + (Vector3.up * Random.Range(-4, 4)), Quaternion.identity);

        sliderPieces = new List<GameObject>();
        for (int pieceI = 0; pieceI < nSliderPieces; pieceI++)
        {
            GameObject piece = Instantiate(sliderPiece, transform.position + (pieceDistance * pieceI * Vector3.down) + (pieceStart * Vector3.up), Quaternion.identity);
            sliderPieces.Add(piece);
        }
        pieceEnd = pieceStart - (pieceDistance * nSliderPieces);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(up))
        {
            if (sliderBar.transform.position.y <= pieceStart)
            {
                // go to next index in list of discrete pitches
                //oscillatorControl.PitchNext();
                // or continuous increase
                //oscillatorControl.PitchUp();
                sliderBar.transform.position += Vector3.up * sliderSpeed;
            }

        }
        else if (Input.GetKey(down))
        {
            if (sliderBar.transform.position.y >= pieceEnd)
            {
                //oscillatorControl.PitchPrevious();
                //oscillatorControl.PitchDown();
                sliderBar.transform.position += Vector3.down * sliderSpeed;
            }
        }
        // should have velocity that slows down- physics/vector movement, not position
    }
}

// first idea
// bar with meter that slides back and forth
// qw keys, op keys, etc. for up and down

// public variable: target pitch for each
// should be actual notes, intervals that represent something
// start with stacked perfect 4ths
// maybe could match up with music
