using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject title;
    public bool hit = false;

    private SliderBar[] sliderBarControl;
    private SpriteRenderer transitionSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // should set pointer with different method
        GameObject[] transitions = GameObject.FindGameObjectsWithTag("Transition");
        GameObject transition = transitions[0];
        transitionSpriteRenderer = transition.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSliderHits();
    }

    private void CheckSliderHits()
    {
        // TODO this is a hack. Because can't call this on Start because SliderBars haven't initialized yet
        if (sliderBarControl == null)
        {
            sliderBarControl = FindObjectsByType<SliderBar>(FindObjectsSortMode.None);
        }

        int sliderHits = 0;
        foreach (var bar in sliderBarControl)
        {
            if (bar.isHit) sliderHits++;
        }
        // if all slides have a hit
        if (sliderHits >= sliderBarControl.Length)
        {
            HitTitle();
            // end hits
            foreach (var bar in sliderBarControl)
            {
                bar.isHit = false;
            }
        }
    }

    private void HitTitle()
    {
        // draw ray
        Vector3 titlePosition = title.transform.position;
        foreach (var bar in sliderBarControl)
        {
            bar.DrawLine(titlePosition);
        }

        hit = true;
        StartCoroutine(FadeAnimator.FadeIntoTransition(transitionSpriteRenderer, 0, 1, 2, "Opening"));
    }
}
