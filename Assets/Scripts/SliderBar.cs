using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBar : MonoBehaviour
{
    SliderPiece currentSliderPieceControl;
    SliderPiece previousSliderPieceControl;
    SliderTarget sliderTargetControl;
    // Start is called before the first frame update
    void Start()
    {
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

        // check for target hit
        //sliderTargetControl = collision.GetComponent<SliderTarget>();
        //if (sliderTargetControl != null)
        //{
        //    Debug.Log("TARGET HIT");
        //}
    }

}
