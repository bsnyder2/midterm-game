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
    public float sliderStart = 4f;
    public float interPieceDistance = 2f;
    public int barStart = 0;
    //public float targetPoint = 0.5f;
    private float sliderEnd;

    private List<GameObject> sliderPieces;
    private SliderBar sliderBarControl;

    // textures etc.

    // Start is called before the first frame update
    void Start()
    {
        sliderPieces = new List<GameObject>();
        for (int pieceI = 0; pieceI < nSliderPieces; pieceI++)
        {
            GameObject piece = Instantiate(sliderPiece, transform.position + (interPieceDistance * pieceI * Vector3.down) + (sliderStart * Vector3.up), Quaternion.identity);
            sliderPieces.Add(piece);
        }
        sliderEnd = sliderStart - (interPieceDistance * nSliderPieces);

        sliderBar = Instantiate(sliderBar, sliderPieces[barStart].transform.position, Quaternion.identity);
        //sliderBar = Instantiate(sliderBar, transform.position + (interPieceDistance * barStart * Vector3.down) + (sliderStart * Vector3.up), Quaternion.identity);
        sliderBarControl = sliderBar.GetComponent<SliderBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(up))
        {
            if (sliderBar.transform.position.y <= sliderStart)
            {
                // go to next index in list of discrete pitches
                //oscillatorControl.PitchNext();
                // or continuous increase
                //oscillatorControl.PitchUp();
                sliderBar.transform.position += Vector3.up * sliderSpeed;
                sliderBarControl.movingUp = true;
            }

        }
        else if (Input.GetKey(down))
        {
            if (sliderBar.transform.position.y >= sliderEnd)
            {
                //oscillatorControl.PitchPrevious();
                //oscillatorControl.PitchDown();
                sliderBar.transform.position += Vector3.down * sliderSpeed;
                sliderBarControl.movingUp = false;
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
