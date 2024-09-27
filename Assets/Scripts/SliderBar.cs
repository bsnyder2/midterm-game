using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBar : MonoBehaviour
{
    public GameObject oscillator;
    public bool movingUp;
    public bool isHit;

    //private Rigidbody2D thisRigidbody;

    private SliderPiece currentSliderPieceControl;
    private SliderPiece previousSliderPieceControl;
    private Oscillator oscillatorControl;
    private LineRenderer lineRenderer;

    // *Awake is called when script instance is loaded
    void Awake()
    {
        oscillatorControl = Instantiate(oscillator).GetComponent<Oscillator>();
        lineRenderer = GetComponent<LineRenderer>();
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

        // if moving down, decrease pitch; if moving up increase
        //Debug.Log(thisRigidbody.velocity);
        //if (Input.GetKey(KeyCode.LeftShift))
        if (movingUp)
        {
            oscillatorControl.PitchNext();
        } else
        {
            oscillatorControl.PitchPrevious();
        }

        isHit = currentSliderPieceControl.isTarget;


        // check for target hit
        //sliderTargetControl = collision.GetComponent<SliderTarget>();
        //if (sliderTargetControl != null)
        //{
        //    Debug.Log("TARGET HIT");
        //}

        // TODO target hit on each slider (different color eye)
        // -> kill enemy by casting rays from each eye
        // slowing down time and music effect? enough time to create interval that matches melody
        // analog synth tones
        // guitar?
        // make it sound great in headphones
    }

    public void DrawLine(Vector3 enemyPosition)
    {
        lineRenderer.SetPosition(0, currentSliderPieceControl.transform.position);
        lineRenderer.SetPosition(1, enemyPosition);
    }

}
