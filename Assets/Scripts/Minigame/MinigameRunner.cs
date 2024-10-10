using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinigameRunner : MonoBehaviour
{
    // better place to define constants?
    private readonly int[,] barTargets = { { 1, 3 }, { 4, 1 }, { 2, 5 }, { 3, 6 }, { 1, 3 } };

    public bool running = false;

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
        //List<GameOb> foo = new List<Object>(enemies);
        enemies = enemyControls.OrderBy((Enemy c) => c.gameObject.transform.position.x).ToList();
        //foreach (Enemy e in enemies)
        //{
        //    Debug.Log(e.gameObject.transform.position.x);
        //}
        currentEnemyIndex = 0;

        currentEnemyControl = enemies[currentEnemyIndex];
        playerControl.moving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (running) CheckSliderHits();
    }

    public void NextEnemy() {
        currentEnemyIndex++;
        if (currentEnemyIndex > (enemies.Count - 1))
        {
            Debug.Log("out of enemies");
            return;
        }
        currentEnemyControl = enemies[currentEnemyIndex];
    }

    public IEnumerator EnemyFade()
    {
        Debug.Log("enemy fading");
        SpriteRenderer enemySprite = currentEnemyControl.GetComponent<SpriteRenderer>();
        Debug.Log(enemySprite);
        yield return StartCoroutine(FadeAnimator.FadeIn(enemySprite, 1f, 0f, 1f));
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

        // but this should also draw line here
        playerControl.Attack();
        StartCoroutine(ZapAfter());

    }

    private IEnumerator ZapAfter()
    {
        yield return new WaitForSeconds(0.6f);
        // draw ray
        Vector3 enemyPosition = currentEnemyControl.transform.position;

        foreach (var bar in sliderBarControl)
        {
            bar.DrawLine(enemyPosition);
            Debug.Log("draw line");
        }

        currentEnemyControl.Die();

        // temp
        if (barTargetsIndex > (barTargets.GetLength(0) - 1))
        {
            Debug.Log("(out of targets)");
            yield break;
        }

        //Debug.Log(barTargetsIndex + " " + barTargets.GetLength(0));
        // reset bar targets for each slider
        sliderControl[0].ResetBarTarget(barTargets[barTargetsIndex, 0]);
        sliderControl[1].ResetBarTarget(barTargets[barTargetsIndex, 1]);

        barTargetsIndex++;
        NextEnemy();
    }
}
