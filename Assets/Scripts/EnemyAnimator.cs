using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public Sprite[] spritesheet;
    public float animationFrameTime;

    private SpriteRenderer spriteRenderer;
    private Queue<IEnumerator> animationQueue;

    private Sprite[] idleFrames;
    private Sprite[] dieFrames;


    // Start is called before the first frame update
    void Start()
    {
        idleFrames = spritesheet[0..7];
        dieFrames = spritesheet[51..60];

        spriteRenderer = GetComponent<SpriteRenderer>();
        animationQueue = new Queue<IEnumerator>();
        StartCoroutine(AnimationCoordinator());
        for (int i = 0; i < 5; i++)
        {
            animationQueue.Enqueue(IdleRoutine());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        animationQueue.Enqueue(DieRoutine());
    }

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
        int animationFrameIndex = 0;
        while (animationFrameIndex < idleFrames.Length)
        {
            spriteRenderer.sprite = idleFrames[animationFrameIndex];
            animationFrameIndex++;
            yield return new WaitForSeconds(Time.deltaTime * animationFrameTime);
        }
    }


    // refactor to stop writing this method over and over
    private IEnumerator DieRoutine()
    {
        int animationFrameIndex = 0;
        while (animationFrameIndex < dieFrames.Length)
        {
            spriteRenderer.sprite = dieFrames[animationFrameIndex];
            animationFrameIndex++;
            yield return new WaitForSeconds(Time.deltaTime * animationFrameTime);
        }
    }

}
