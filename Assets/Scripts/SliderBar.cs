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
        previousSliderPieceControl = currentSliderPieceControl;
        //Debug.Log("open");
        // set current pointer to current collision
        currentSliderPieceControl = collision.GetComponent<SliderPiece>();
        // if collided with slider piece, open
        //Debug.Log(currentSliderPieceControl == previousSliderPieceControl);
        if (ReferenceEquals(currentSliderPieceControl, previousSliderPieceControl))
        {
            return;
        }
        if (currentSliderPieceControl != null)
        {
            currentSliderPieceControl.Open();
            // close all other eyes
            //transform.parent.GetComponent<Slider>().Foo();
        }
        if (previousSliderPieceControl != null)
        {
            previousSliderPieceControl.Close();
        }

        sliderTargetControl = collision.GetComponent<SliderTarget>();
        if (sliderTargetControl != null)
        {
            Debug.Log("TARGET HIT");
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("close");
    //    sliderPiece = collision.GetComponent<SliderPiece>();
    //    if (sliderPiece != null)
    //    {
    //        sliderPiece.Close();
    //    }
    //}
}
