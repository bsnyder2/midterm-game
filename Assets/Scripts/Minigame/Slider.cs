using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : MonoBehaviour
{
    public GameObject sliderPiece;
    public GameObject sliderBar;

    public KeyCode up, down;
    public int nSliderPieces = 10;
    public float sliderSpeed = 0.01f;
    public float sliderStart = 4f;
    public float interPieceDistance = 2f;
    public int barStart = 0;

    private float distanceScalar;

    private List<GameObject> sliderPieces;
    private SliderBar sliderBarControl;

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

        // instantiate bar at
        sliderBarControl = GetComponentInChildren<SliderBar>();
        sliderBar = sliderBarControl.gameObject;

        int initBarTarget = 3;
        ResetBarTarget(initBarTarget);

        // fade in all sprites
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(up))
        {
            if (sliderBar.transform.position.y <= (transform.position.y + transform.localScale.y - (distanceScalar * 2)))
            {
                sliderBar.transform.localPosition += sliderSpeed * distanceScalar * Vector3.up * Time.deltaTime;
                sliderBarControl.movingUp = true;
            }

        }
        else if (Input.GetKey(down))
        {
            if (sliderBar.transform.position.y >= (transform.position.y - transform.localScale.y + (distanceScalar * 0.5f)))
            {
                sliderBar.transform.localPosition += sliderSpeed * distanceScalar * Vector3.down * Time.deltaTime;
                sliderBarControl.movingUp = false;
            }
        }
    }

    public void ResetBarTarget(int newBarTarget)
    {
        foreach (var sliderPiece in sliderPieces)
        {
            sliderPiece.GetComponent<SliderPiece>().isTarget = false;
        }
        sliderPieces[newBarTarget].GetComponent<SliderPiece>().isTarget = true;
    }
}
