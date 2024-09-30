using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameRunner : MonoBehaviour
{
    // better place to define constants?
    private readonly int[,] barTargets = { { 1, 3 }, { 4, 1 }, { 2, 5 } };

    private Player playerControl;
    private Slider[] sliderControl;
    private SliderBar[] sliderBarControl;
    private Enemy currentEnemyControl;

    private int barTargetsIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // should set pointer with different method
        currentEnemyControl = FindObjectOfType<Enemy>();
        playerControl = FindObjectOfType<Player>();

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

        int sliderHits = 0;
        foreach (var bar in sliderBarControl)
        {
            if (bar.isHit) sliderHits++;
        }
        // if all slides have a hit
        if (sliderHits >= sliderBarControl.Length)
        {
            HitEnemy();
            // end hits
            foreach (var bar in sliderBarControl) bar.isHit = false;
        }
    }

    private void HitEnemy()
    {
        // draw ray
        Vector3 enemyPosition = currentEnemyControl.transform.position;
        foreach (var bar in sliderBarControl)
        {
            bar.DrawLine(enemyPosition);
            Debug.Log("draw line");
        }

        playerControl.Attack();
        currentEnemyControl.Die();

        // reset bar targets for each slider
        sliderControl[0].ResetBarTarget(barTargets[barTargetsIndex,0]);
        sliderControl[1].ResetBarTarget(barTargets[barTargetsIndex,1]);

        barTargetsIndex++;
        //sliderControls[0].badPracticeVariable++;
        //sliderControls[1].badPracticeVariable++;
    }
}
