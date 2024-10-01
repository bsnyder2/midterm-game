using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 1;
    public float distance = 1f;
    private PlayerAnimator playerAnimator;

    //public float animationFrameTime;
    public Sprite[] idleFrames;
    public Sprite[] runFrames;
    //private Sprite[] frames;

    private SpriteRenderer spriteRenderer;
    //private Queue<IEnumerator> animationQueue;

    public bool isMoving = false;
    private float animationSpeed = 0.2f;
    private int currentSpriteIndex = 0;
    private float animationTimer;

    //private Rigidbody2D rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // animation queue is always active
        //animationQueue = new Queue<IEnumerator>();
        //StartCoroutine(AnimationCoordinator());
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == true)
        {
            StartCoroutine(MoveRight());
        }

        else
        {
            StartCoroutine(Idle());
        }
        //StartCoroutine(MoveRight());
    }

    public void Attack()
    {
        // playerAnimator.PlayAnimation("attack");
        playerAnimator.Attack();
    }

    private IEnumerator Idle()
    {
        Animate(idleFrames);
        yield return null;
    }


    private IEnumerator MoveRight()
    {
        Vector3 start = transform.position;
        Vector3 target = new Vector3(start.x + distance, start.y, start.z);
        Animate(runFrames);

        float t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime * moveSpeed;
            if (t > 1) t = 1;
            transform.position = Vector3.Lerp(start, target, t);

            yield return null;
        }

    }

    private void Animate(Sprite[] frames)
    {

        animationTimer += Time.deltaTime;

        if (animationTimer >= animationSpeed)
        {

            currentSpriteIndex = (currentSpriteIndex + 1) % frames.Length;
            spriteRenderer.sprite = frames[currentSpriteIndex];

            animationTimer = 0f;
        }
    }

    /*public void Idle()
    {
        animationQueue.Enqueue(IdleRoutine());
    }

    public void Run()
    {
        isMoving = true;
        animationQueue.Enqueue(RunRoutine());
    }

    // inspired by https://discussions.unity.com/t/how-to-stack-coroutines-and-call-each-one-till-all-are-executed/219063/5
    private IEnumerator AnimationCoordinator()
    {
        // constantly check...
        while (true)
        {
            // while animations left in the queue
            while (animationQueue.Count > 0)
            {
                // run next animation
                yield return StartCoroutine(animationQueue.Dequeue());
            }
            yield return null;
        }
    }

    private IEnumerator IdleRoutine()
    {
        // play all frames of animation
        //Sprite[] frames = isTarget ? targetFrames : standardFrames;
        int animationFrameIndex = 0;
        while (animationFrameIndex < idleFrames.Length)
        {
            spriteRenderer.sprite = idleFrames[animationFrameIndex];
            animationFrameIndex++;
            yield return new WaitForSeconds(Time.deltaTime * animationFrameTime);
        }
    }

    private IEnumerator RunRoutine()
    {
        // play all frames of animation
        //Sprite[] frames = isTarget ? targetFrames : standardFrames;
        int animationFrameIndex = runFrames.Length - 1;
        while (animationFrameIndex >= 0)
        {
            spriteRenderer.sprite = runFrames[animationFrameIndex];
            animationFrameIndex--;
            yield return new WaitForSeconds(Time.deltaTime * animationFrameTime);
        }
    }*/

}
