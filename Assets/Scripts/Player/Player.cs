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
    public float moveSpeed = 1;
    public float distance = 1f;

    private PlayerAnimator playerAnimator;
    private bool attacking = false;

    private AnimationState currentAnimationState;


    // try just doing flipbook animation like bomberman
    public Sprite[] run;
    public Sprite[] attack;
    public Sprite[] die;

    //private AnimationState animationState;

    //public float animationFrameTime;
    public Sprite[] idleFrames;
    //public Sprite[] runFrames;
    //private Sprite[] frames;

    private Sprite[] currentFrames;

    private SpriteRenderer spriteRenderer;
    //private Rigidbody2D thisRigidbody;
    //private Queue<IEnumerator> animationQueue;

    public bool isMoving = false;
    private float animationSpeed = 6f;
    private int currentSpriteIndex = 0;
    private float animationTimer;

    private MinigameManager minigameManager; 

    //private Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //thisRigidbody = GetComponent<Rigidbody2D>();
        // animation queue is always active
        //animationQueue = new Queue<IEnumerator>();
        //StartCoroutine(AnimationCoordinator());
        //thisRigidbody.MovePosition(Vector3.right * moveSpeed * Time.deltaTime);
        SwitchAnimation(AnimationState.Running);
        minigameManager = FindFirstObjectByType<MinigameManager>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isMoving) MoveRight();

        // play current animation
        PlayCurrentAnimation();
    }

    //private void Idle()
    //{
    //    Animate(idleFrames);
    //}

    private void MoveRight()
    {
        //Debug.Log(attacking);
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        //if (attacking) {
        //    AnimateAttack(attack);
        //} else {
        //    Animate(run);
        //}
        //Animate(attacking ? attack : run);
    }

    //private void AnimateAttack(Sprite[] frames)
    //{
    //    animationTimer += Time.deltaTime;

    //    int i = 0;
    //    while (i < frames.Length)
    //    {
    //        if (animationTimer >= animationSpeed)
    //        {

    //            currentSpriteIndex = (currentSpriteIndex + 1) % frames.Length;
    //            spriteRenderer.sprite = frames[currentSpriteIndex];

    //            animationTimer = 0f;
    //        }
    //        i++;
    //    }
    //    attacking = false;
    //}

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
        }

    }

    private void PlayCurrentAnimation()
    {
        if (animationTimer <= 0)
        {
            if (currentSpriteIndex >= (currentFrames.Length - 1))
            {
                // if animation that completes, return
                //return;
                if (currentAnimationState == AnimationState.Attacking)
                {
                    SwitchAnimation(AnimationState.Running);
                    return;
                }
                // if animation that repeats, loop
                currentSpriteIndex = 0;
            }
            spriteRenderer.sprite = currentFrames[currentSpriteIndex];
            //Debug.Log("sprite " + currentSpriteIndex);
            //Debug.Log("played frame " + currentSpriteIndex);

            currentSpriteIndex++;
            animationTimer = (1 / animationSpeed);

        }
        animationTimer -= Time.deltaTime;
    }

    //private void Run(Sprite[] frames)
    //{
    //    //if (animationTimer >= animationSpeed)
    //    //{

    //    //    currentSpriteIndex = (currentSpriteIndex + 1) % frames.Length;
    //    //    spriteRenderer.sprite = frames[currentSpriteIndex];

    //    //    animationTimer = 0f;
    //    //}

    //    // on next frame
    //    if (animationTimer <= 0)
    //    {
    //        if (animationTimer >= (frames.Length - 1))
    //        {
    //            currentSpriteIndex = 0;
    //        }
    //        spriteRenderer.sprite = frames[currentSpriteIndex];
    //        currentSpriteIndex++;
    //        animationTimer = (1 / animationSpeed);
    //    }
    //    animationTimer -= Time.deltaTime;
    //}

    // called externally
    public void Attack()
    {
        SwitchAnimation(AnimationState.Attacking);
    }

    //public void AttackRun(Sprite[] frames)
    //{
    //    // playerAnimator.PlayAnimation("attack");
    //    //playerAnimator.Attack();
    //    Debug.Log("animating attack");

    //    // I wrote a good explanation of how flipbook animation works somewhere so find that
    //    while (true)
    //    {
    //        // next frame
    //        if (animationTimer <= 0)
    //        {
    //            if (currentSpriteIndex >= (frames.Length - 1))
    //            {
    //                return;
    //            }
    //            spriteRenderer.sprite = frames[currentSpriteIndex];
    //            currentSpriteIndex++;
    //            animationTimer = (1 / animationSpeed);
    //        }
    //        animationTimer -= Time.deltaTime;
    //    }
    //}


    // attack animation should loop thru sprites once and set state back to idle at end

    public void Idle()
{
    //animationQueue.Enqueue(IdleRoutine());
}

// inspired by https://discussions.unity.com/t/how-to-stack-coroutines-and-call-each-one-till-all-are-executed/219063/5
//private IEnumerator AnimationCoordinator()
//{
//    // constantly check...
//    while (true)
//    {
//        // while animations left in the queue
//        while (animationQueue.Count > 0)
//        {
//            // run next animation
//            yield return StartCoroutine(animationQueue.Dequeue());
//        }
//        yield return null;
//    }
//}

//private IEnumerator IdleRoutine()
//{
//    // play all frames of animation
//    //Sprite[] frames = isTarget ? targetFrames : standardFrames;
//    int animationFrameIndex = 0;
//    while (animationFrameIndex < idleFrames.Length)
//    {
//        spriteRenderer.sprite = idleFrames[animationFrameIndex];
//        animationFrameIndex++;
//        yield return new WaitForSeconds(Time.deltaTime * animationFrameTime);
//    }
//}

//private IEnumerator RunRoutine()
//{
//    // play all frames of animation
//    //Sprite[] frames = isTarget ? targetFrames : standardFrames;
//    int animationFrameIndex = runFrames.Length - 1;
//    while (animationFrameIndex >= 0)
//    {
//        spriteRenderer.sprite = runFrames[animationFrameIndex];
//        animationFrameIndex--;
//        yield return new WaitForSeconds(Time.deltaTime * animationFrameTime);
//    }
//}

private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy other = collision.gameObject.GetComponent<Enemy>();
        if (other != null)
        {
            //Debug.Log("Enemy hit... calling LoseLife");
            Debug.Log("minigame manager is " + minigameManager);
            minigameManager.LoseLife();
        }
        //SceneManager.LoadScene("Main");
    }
}