using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    // better place to define constants?
    private readonly int[,] barTargets = { { 1, 2 } };

    //public bool running = false;

    private Slider[] sliderControl;
    private SliderBar[] sliderBarControl;

    public GameObject title;

    private int barTargetsIndex = 0;

    public bool Hit = false;

    private SpriteRenderer transitionSpriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        // should set pointer with different method
        GameObject[] transitions = GameObject.FindGameObjectsWithTag("Transition");
        GameObject transition = transitions[0];
        transitionSpriteRenderer = transition.GetComponent<SpriteRenderer>();
        sliderControl = FindObjectsByType<Slider>(FindObjectsSortMode.None);
            //sliderBarControl = FindObjectsByType<SliderBar>(FindObjectsSortMode.None);
        sliderControl[0].ResetBarTarget(barTargets[barTargetsIndex,0]);
        sliderControl[1].ResetBarTarget(barTargets[barTargetsIndex,1]);

    }

    // Update is called once per frame
    void Update()
    {
        CheckSliderHits();
        
    }

    private void CheckSliderHits()
    {
        // TODO this is a hack. Because can't call this on Start because SliderBars haven't initialized yet
        // actual init
        if (sliderBarControl == null)
        {
            sliderControl = FindObjectsByType<Slider>(FindObjectsSortMode.None);
            sliderBarControl = FindObjectsByType<SliderBar>(FindObjectsSortMode.None);
        }
        //Debug.Log("sliderbarcontrol is still null");

        int sliderHits = 0;
        foreach (var bar in sliderBarControl)
        {
            if (bar.isHit) sliderHits++;
        }
        // if all slides have a hit
        if (sliderHits >= sliderBarControl.Length)
        {
            Debug.Log("Title HIt");
            HitTitle();
            // end hits
            foreach (var bar in sliderBarControl){
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
            Debug.Log("draw line");
        }

        Hit = true;
        StartCoroutine(FadeAnimator.FadeIntoTransition(transitionSpriteRenderer, 0, 1, 2, "Opening1"));
    }
}
