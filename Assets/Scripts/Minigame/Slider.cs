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

    private float distanceScalar;
    //private float sliderEnd;

    private List<GameObject> sliderPieces;
    private SliderBar sliderBarControl;

    // textures etc.

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        distanceScalar = transform.localScale.y / 8f;
        sliderPieces = new List<GameObject>();

        SliderPiece[] sliderPieceControls = GetComponentsInChildren<SliderPiece>();
        foreach (var sliderPieceControl in sliderPieceControls)
        {
            sliderPieces.Add(sliderPieceControl.gameObject);
        }
        //for (int pieceI = 0; pieceI < nSliderPieces; pieceI++)
        //{
        //    // instantiate piece
        //    GameObject piece = Instantiate(sliderPiece, transform.position + (interPieceDistance * pieceI * distanceScalar * Vector3.down) + (sliderStart * distanceScalar * Vector3.up), Quaternion.identity);
        //    piece.transform.localScale *= distanceScalar;
        //    sliderPieces.Add(piece);
        //}

        // instantiate bar at
        sliderBarControl = GetComponentInChildren<SliderBar>();
        sliderBar = sliderBarControl.gameObject;
        //sliderBar = Instantiate(sliderBar, sliderPieces[barStart].transform.position, Quaternion.identity);
        //sliderBar.transform.localScale *= distanceScalar;
        //sliderBarControl = sliderBar.GetComponent<SliderBar>();

        // TODO bad
        int initBarTarget = 5;
        ResetBarTarget(initBarTarget);

        // Fade in... maybe a better way to do this. If all the other objects are children of Slider I could just iterate through all of those, and also do that in MinigameRunner which I should do
        StartCoroutine(FadeAnimator.FadeIn(spriteRenderer, 0, 1f, 2));

        foreach (var sliderPiece in sliderPieces)
        {
            StartCoroutine(FadeAnimator.FadeIn(sliderPiece.GetComponent<SpriteRenderer>(), 0, 1f, 2));
        }
        SpriteRenderer[] overlays = GetComponentsInChildren<SpriteRenderer>();
        foreach (var overlay in overlays)
        {
            StartCoroutine(FadeAnimator.FadeIn(overlay.GetComponent<SpriteRenderer>(), 0, 1f, 2));
        }
    }

    // Update is called once per fixed framerate frame
    void FixedUpdate()
    {
        // Some really not pretty equations here
        if (Input.GetKey(up))
        {
            if (sliderBar.transform.position.y <= (transform.position.y + transform.localScale.y - (distanceScalar * 2)))
            {
                // go to next index in list of discrete pitches
                //oscillatorControl.PitchNext();
                // or continuous increase
                //oscillatorControl.PitchUp();
                Debug.Log("MOVING UP");
                sliderBar.transform.position += sliderSpeed * distanceScalar * Vector3.up;
                sliderBarControl.movingUp = true;
            }

        }
        else if (Input.GetKey(down))
        {
            if (sliderBar.transform.position.y >= (transform.position.y - transform.localScale.y + (distanceScalar * 0.5f)))
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
        //Debug.Log(sliderPieces.Count);

        foreach (var sliderPiece in sliderPieces)
        {
            sliderPiece.GetComponent<SliderPiece>().isTarget = false;
        }
        sliderPieces[newBarTarget].GetComponent<SliderPiece>().isTarget = true;

        //sliderPieces[newBarTarget].GetComponent<SpriteRenderer>().sprite = targetSprite;
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
