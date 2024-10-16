using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinigameRunner : MonoBehaviour
{
    // better place to define constants?
    private readonly int[,] barTargets = { { 0, 0 }, { 4, 4 }, { 2, 6 }, { 4, 2 }, { 0, 5 }, {1,4}, {2, 3}, {5, 6}, {0, 0}, { 3, 3 },
    { 2, 2 }, { 5, 5 }, { 4, 4 }, { 3, 3 }, { 4, 2 }, { 5, 1 } , {6, 0}, {3, 3}, {4,5}, {5,4}, {4,5}, {0,0}};

    private Player playerControl;
    private Slider[] sliderControl;
    private SliderBar[] sliderBarControl;

    private List<Enemy> enemies;
    private int currentEnemyIndex;
    private Enemy currentEnemyControl;

    private int barTargetsIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // should set pointer with different method
        playerControl = FindObjectOfType<Player>();

        // get all enemy objects placed
        Enemy[] enemyControls = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        enemies = enemyControls.OrderBy((Enemy c) => c.gameObject.transform.position.x).ToList();
        currentEnemyIndex = 0;

        currentEnemyControl = enemies[currentEnemyIndex];
        playerControl.moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSliderHits();
    }

    public void NextEnemy()
    {
        sliderControl[0].ResetBarTarget(barTargets[barTargetsIndex, 0]);
        sliderControl[1].ResetBarTarget(barTargets[barTargetsIndex, 1]);
        barTargetsIndex++;

        currentEnemyIndex++;
        if (currentEnemyIndex > (enemies.Count - 1)) return;
        currentEnemyControl = enemies[currentEnemyIndex];

    }

    public IEnumerator EnemyFade()
    {
        SpriteRenderer enemySprite = currentEnemyControl.GetComponent<SpriteRenderer>();
        yield return StartCoroutine(FadeAnimator.FadeIn(enemySprite, 1f, 0f, 1f));
    }

    private void CheckSliderHits()
    {
        // TODO this is a hack. Because can't call this on Start because SliderBars haven't initialized yet
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
        if (playerControl.currentAnimationState != Player.AnimationState.Dying)
        {
            playerControl.Attack();
            StartCoroutine(ZapAfter());
        }

    }

    private IEnumerator ZapAfter()
    {
        yield return new WaitForSeconds(0.6f);
        // draw ray
        Vector3 enemyPosition = currentEnemyControl.transform.position;

        foreach (var bar in sliderBarControl)
        {
            bar.DrawLine(enemyPosition);
        }

        currentEnemyControl.Die();

        if (barTargetsIndex > (barTargets.GetLength(0) - 1))
        {
            yield break;
        }

        NextEnemy();
    }
}
