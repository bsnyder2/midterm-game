using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should inherit from a class called Minigame for any minigame, and override some methods
public class Slider : MonoBehaviour
{
    public GameObject sliderPiece;
    public GameObject sliderBar;

    public KeyCode up, down;
    // target point on slider, between 0 and 1
    public int nSliderPieces = 10;
    public float sliderSpeed = 0.01f;
    public float sliderStart = 4f;
    public float interPieceDistance = 2f;
    public int barStart = 0;
    public int initBarTarget;

    private float sliderEnd;
    private float distanceScalar;

    private List<GameObject> sliderPieces;
    private SliderBar sliderBarControl;

    // textures etc.

    // Start is called before the first frame update
    void Start()
    {
        distanceScalar = transform.localScale.y / 8f;
        //Debug.Log(distanceScalar);
        sliderPieces = new List<GameObject>();
        for (int pieceI = 0; pieceI < nSliderPieces; pieceI++)
        {
            // instantiate piece
            GameObject piece = Instantiate(sliderPiece, transform.position + (interPieceDistance * pieceI * distanceScalar * Vector3.down) + (sliderStart * distanceScalar * Vector3.up), Quaternion.identity);
            piece.transform.localScale *= distanceScalar;
            sliderPieces.Add(piece);
        }
        sliderEnd = sliderStart - (interPieceDistance * nSliderPieces);
        //float top = 0.75f;
        //float interPieceDistance = 0.2f;
        //sliderPieces = new List<GameObject>();
        //for (int pieceI = 0; pieceI < nSliderPieces; pieceI++)
        //{
        //    GameObject piece = Instantiate(sliderPiece, transform.position + (Vector3.up * transform.localScale.y * top) + (interPieceDistance * pieceI * transform.localScale.y * Vector3.down), Quaternion.identity);
        //    piece.transform.localScale *= transform.localScale.y;
        //}

        // instantiate bar at 
        sliderBar = Instantiate(sliderBar, sliderPieces[barStart].transform.position, Quaternion.identity);
        sliderBar.transform.localScale *= distanceScalar;
        sliderBarControl = sliderBar.GetComponent<SliderBar>();

        ResetBarTarget(initBarTarget);
    }

    // Update is called once per frame
    void Update()
    {
        // Some really not pretty equations here
        Debug.Log(sliderBar.transform.position.y);
        if (Input.GetKey(up))
        {
            if (sliderBar.transform.position.y <= (transform.position.y + transform.localScale.y - (distanceScalar * 3)))
            {
                // go to next index in list of discrete pitches
                //oscillatorControl.PitchNext();
                // or continuous increase
                //oscillatorControl.PitchUp();
                sliderBar.transform.position += sliderSpeed * distanceScalar * Vector3.up;
                sliderBarControl.movingUp = true;
            }

        }
        else if (Input.GetKey(down))
        {
            if (sliderBar.transform.position.y >= (transform.position.y - transform.localScale.y - (distanceScalar)))
            {
                //oscillatorControl.PitchPrevious();
                //oscillatorControl.PitchDown();
                sliderBar.transform.position += sliderSpeed * distanceScalar * Vector3.down;
                sliderBarControl.movingUp = false;
            }
        }
        // should have velocity that slows down- physics/vector movement, not position
    }

    public void ResetBarTarget(int newBarTarget)
    {
        foreach (var sliderPiece in sliderPieces)
        {
            sliderPiece.GetComponent<SliderPiece>().isTarget = false;
        }
        sliderPieces[newBarTarget].GetComponent<SliderPiece>().isTarget = true;
        //Debug.Log("this is beign rn");
    }
}

// first idea
// bar with meter that slides back and forth
// qw keys, op keys, etc. for up and down

// public variable: target pitch for each
// should be actual notes, intervals that represent something
// start with stacked perfect 4ths
// maybe could match up with music
