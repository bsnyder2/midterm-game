using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBar : MonoBehaviour
{
    SliderPiece sliderPiece;
    SliderTarget sliderTarget;
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
        Debug.Log("open");
        sliderPiece = collision.GetComponent<SliderPiece>();
        if (sliderPiece != null)
        {
            sliderPiece.Open();
        }
        sliderTarget = collision.GetComponent<SliderTarget>();
        if (sliderTarget != null)
        {
            Debug.Log("TARGET HIT");
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("chose");
        // don't worry about initialization, since will always enter before exit
        sliderPiece.Close();
    }
}
