using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public enum AnimationState {
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

    public AnimationState currentAnimationState;
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
    void Update()
    {
        if (moving) MoveRight();
        PlayCurrentAnimation();
    }

    private void MoveRight()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }

    public void StartRunning()
    {
        SwitchAnimation(AnimationState.Running);
    }

    // only executed on state switch
    private void SwitchAnimation(AnimationState animationState)
    {
        switch (animationState)
        {
            case AnimationState.Running:
                currentAnimationState = AnimationState.Running;
                currentFrames = run;
                break;
            case AnimationState.Attacking:
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
        minigameRunner.NextEnemy();
    }

    // called on update
    private void PlayCurrentAnimation()
    {
        if (animationTimer <= 0)
        {
            if (currentAnimationState == AnimationState.Attacking)
            {
                // this is not pretty, tried to use an array but it broke everything
                switch (currentSpriteIndex)
                {
                    case 0:
                        moveSpeed = 4f;
                        invincible = true;
                        break;
                    case 1:
                        moveSpeed = 4f; break;
                    case 2:
                        moveSpeed = 5f; break;
                    case 4:
                        moveSpeed = 10f; break;
                    case 5:
                        moveSpeed = 4f; break;
                    case 6:
                        moveSpeed = 1f; break;
                    case 8:
                        moveSpeed = 4f;
                        invincible = false;
                        break;
                }
            }

                if (currentSpriteIndex >= (currentFrames.Length - 1)) {
                // if animation that completes, switch back to running and return
                if ((currentAnimationState == AnimationState.Attacking) || (currentAnimationState == AnimationState.Dying))
                {
                    SwitchAnimation(AnimationState.Running);
                    moving = true;
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
        if (other != null && !invincible) StartCoroutine(Die());
    }

}