using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    enum AnimationState {
        Running,
        Attacking,
        Dying,
    }

    public float moveSpeed = 1f;
    public float animationSpeed = 6f;

    public Sprite[] run;
    public Sprite[] attack;
    public Sprite[] die;
    public bool moving = false;

    private SpriteRenderer spriteRenderer;
    private MinigameManager minigameManager;
    private MinigameRunner minigameRunner;

    private AnimationState currentAnimationState;
    private Sprite[] currentFrames;
    private int currentSpriteIndex = 0;
    private float animationTimer;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        minigameManager = FindFirstObjectByType<MinigameManager>();
        minigameRunner = FindFirstObjectByType<MinigameRunner>();
        SwitchAnimation(AnimationState.Running);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving) MoveRight();

        // play current animation
        PlayCurrentAnimation();
    }

    private void MoveRight()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }


    // only executed on state switch
    private void SwitchAnimation(AnimationState animationState)
    {
        Debug.Log("Switched animation state to " + animationState);
        switch (animationState)
        {
            case AnimationState.Running:
                //Run(run);
                currentAnimationState = AnimationState.Running;
                currentFrames = run;
                break;
            case AnimationState.Attacking:
                //AttackRun(attack);
                currentAnimationState = AnimationState.Attacking;
                currentFrames = attack;
                break;
            case AnimationState.Dying:
                currentAnimationState = AnimationState.Dying;
                currentFrames = die;
                break;
        }
        currentSpriteIndex = 0;

    }

    private IEnumerator Die()
    {
        moving = false;
        minigameManager.LoseLife();
        SwitchAnimation(AnimationState.Dying);
        yield return StartCoroutine(minigameRunner.EnemyFade());
        yield return new WaitForSeconds(1f);
        minigameRunner.NextEnemy();
        moving = true;
    }

    private void PlayCurrentAnimation()
    {
        if (animationTimer <= 0)
        {
            if (currentSpriteIndex >= (currentFrames.Length - 1))
            {
                // if animation that completes, switch back to running and return
                if (currentAnimationState != AnimationState.Running)
                {
                    SwitchAnimation(AnimationState.Running);
                    return;
                }
                // if animation that repeats, loop
                currentSpriteIndex = 0;
            }
            spriteRenderer.sprite = currentFrames[currentSpriteIndex];
            currentSpriteIndex++;
            animationTimer = (1 / animationSpeed);

        }
        animationTimer -= Time.deltaTime;
    }

    // called externally
    public void Attack()
    {
        SwitchAnimation(AnimationState.Attacking);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy other = collision.gameObject.GetComponent<Enemy>();
        if (other != null) StartCoroutine(Die());
    }

}