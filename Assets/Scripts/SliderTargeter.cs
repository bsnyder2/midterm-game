using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameRunner : MonoBehaviour
{
    // better place to define constants?
    private readonly int[,] barTargets = { { 1, 3 }, { 4, 1 }, { 2, 5 } };

    private Slider[] sliderControls;
    private SliderBar[] sliderBarControls;
    private Enemy currentEnemyControl;

    private int barTargetsIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // should set pointer with different method
        currentEnemyControl = FindObjectOfType<Enemy>();
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
        if (sliderBarControls == null)
        {
            sliderControls = FindObjectsByType<Slider>(FindObjectsSortMode.None);
            sliderBarControls = FindObjectsByType<SliderBar>(FindObjectsSortMode.None);
        }

        int sliderHits = 0;
        foreach (var bar in sliderBarControls)
        {
            if (bar.isHit) sliderHits++;
        }
        // if all slides have a hit
        if (sliderHits >= sliderBarControls.Length)
        {
            HitEnemy();
            // end hits
            foreach (var bar in sliderBarControls) bar.isHit = false;
        }
    }

    private void HitEnemy()
    {
        // draw ray
        Vector3 enemyPosition = currentEnemyControl.transform.position;
        foreach (var bar in sliderBarControls)
        {
            bar.DrawLine(enemyPosition);
        }
        currentEnemyControl.Die();

        // reset bar targets for each slider
        sliderControls[0].ResetBarTarget(barTargets[barTargetsIndex,0]);
        sliderControls[1].ResetBarTarget(barTargets[barTargetsIndex,1]);

        barTargetsIndex++;
    }
}
