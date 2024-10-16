using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBar : MonoBehaviour
{
    public GameObject oscillator;
    public bool movingUp;
    public bool isHit;

    private SliderPiece currentSliderPieceControl;
    private SliderPiece previousSliderPieceControl;
    private Oscillator oscillatorControl;
    private LineRenderer lineRenderer;

    // *Awake is called when script instance is loaded
    void Awake()
    {
        oscillatorControl = Instantiate(oscillator).GetComponent<Oscillator>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (currentSliderPieceControl != null)
        {
            lineRenderer.SetPosition(0, currentSliderPieceControl.transform.position);
        }
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
            isHit = currentSliderPieceControl.isTarget;
        }
        if (previousSliderPieceControl != null)
        {
            previousSliderPieceControl.Close();
        }

        if (movingUp)
        {
            oscillatorControl.PitchNext();
        } else
        {
            oscillatorControl.PitchPrevious();
        }
    }

    public void DrawLine(Vector3 enemyPosition)
    {
        if (currentSliderPieceControl == null) return;
        StartCoroutine(DrawLineRoutine(enemyPosition));
    }

    private IEnumerator DrawLineRoutine(Vector3 enemyPosition)
    {
        lineRenderer.SetPosition(1, enemyPosition);
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.5f);
        lineRenderer.enabled = false;
    }

}
