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
        Idle,
    }

    public float moveSpeed = 1f;
    public float dashSpeed = 2f;
    public float animationSpeed = 6f;

    public Sprite[] run;
    public Sprite[] attack;
    public Sprite[] die;
    public Sprite[] idle;
    public bool moving = false;

    private SpriteRenderer spriteRenderer;
    private MinigameManager minigameManager;
    private MinigameRunner minigameRunner;

    private AnimationState currentAnimationState;
    private Sprite[] currentFrames;
    private int currentSpriteIndex = 0;
    private float animationTimer;

    private bool invincible = false;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        minigameManager = FindFirstObjectByType<MinigameManager>();
        minigameRunner = FindFirstObjectByType<MinigameRunner>();
        SwitchAnimation(AnimationState.Idle);
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

    public void StartRunningInIntro()
    {
        SwitchAnimation(AnimationState.Running);
    }

    public IEnumerator Dash()
    {
        invincible = true;
        //float originalMoveSpeed = moveSpeed;
        //moveSpeed = dashSpeed;
        yield return new WaitForSeconds(0.7f);
        //moveSpeed = 0.1f;
        //yield return new WaitForSeconds(0.1f);
        //moveSpeed = originalMoveSpeed;
        invincible = false;
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
            case AnimationState.Idle:
                currentAnimationState = AnimationState.Idle;
                currentFrames = idle;
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
        yield return new WaitForSeconds(2f);
        minigameRunner.NextEnemy();
        moving = true;
    }

    // called on fixedupdate
    private void PlayCurrentAnimation()
    {
        //animationSpeed = (currentAnimationState == AnimationState.Running) ? 6f : 12f;
        if (animationTimer <= 0)
        {
            if (currentAnimationState == AnimationState.Attacking)
            {
                // 4, 5, ., 9, 2, 1, ., 3, ...
                if (currentSpriteIndex == 1)
                {
                    moveSpeed = 4f;
                }
                if (currentSpriteIndex == 2)
                {
                    moveSpeed = 5f;
                }
                if (currentSpriteIndex == 4)
                {
                    moveSpeed = 10f;
                }
                else if (currentSpriteIndex == 5)
                {
                    Debug.Log("set to 0.7");
                    moveSpeed = 4f;
                }
                else if (currentSpriteIndex == 6)
                {
                    Debug.Log("set to 0.7");
                    moveSpeed = 1f;
                }
                else if (currentSpriteIndex == 8)
                {
                    moveSpeed = 3f;
                }
            }

            if (currentSpriteIndex >= (currentFrames.Length - 1))
            {
                // if animation that completes, switch back to running and return
                if ((currentAnimationState == AnimationState.Attacking) || (currentAnimationState == AnimationState.Dying))
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
        StartCoroutine(Dash());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy other = collision.gameObject.GetComponent<Enemy>();
        if (other != null && !invincible) StartCoroutine(Die());
    }

}