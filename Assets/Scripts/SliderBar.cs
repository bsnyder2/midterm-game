using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBar : MonoBehaviour
{
    public GameObject oscillator;

    private Rigidbody2D thisRigidbody;

    private SliderPiece currentSliderPieceControl;
    private SliderPiece previousSliderPieceControl;
    private Oscillator oscillatorControl;
    // Start is called before the first frame update
    void Start()
    {
        oscillatorControl = Instantiate(oscillator).GetComponent<Oscillator>();
        thisRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // on new collision, set previous pointer to previous collision
        // set current pointer to current collision
        previousSliderPieceControl = currentSliderPieceControl;
        currentSliderPieceControl = collision.GetComponent<SliderPiece>();
        // no animation if reentered same trigger
        if (ReferenceEquals(currentSliderPieceControl, previousSliderPieceControl)) return;

        // if collided with slider piece, open and close previous
        if (currentSliderPieceControl != null)
        {
            currentSliderPieceControl.Open();
        }
        if (previousSliderPieceControl != null)
        {
            previousSliderPieceControl.Close();
        }

        // if moving down, decrease pitch; if moving up
        Debug.Log(thisRigidbody);

        // check for target hit
        //sliderTargetControl = collision.GetComponent<SliderTarget>();
        //if (sliderTargetControl != null)
        //{
        //    Debug.Log("TARGET HIT");
        //}

        // TODO oscillator control with list of pitches
    }

}
