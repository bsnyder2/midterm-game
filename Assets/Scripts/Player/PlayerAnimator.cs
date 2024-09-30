using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Sprite[] idleFrames;
    public Sprite[] attackFrames;
    public float animationFrameTime;

    private SpriteRenderer spriteRenderer;
    private Queue<IEnumerator> animationQueue;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animationQueue = new Queue<IEnumerator>();
        StartCoroutine(AnimationCoordinator());
        for (int i = 0; i < 5; i++)
        {
            animationQueue.Enqueue(IdleRoutine());
        }
    }

    private void Update()
    {
       
    }

    public void Attack()
    {
        Start();
        gameObject.SetActive(true);
        animationQueue.Enqueue(AttackRoutine());
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

    // TODO fix this repeating pattern asap
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

    private IEnumerator AttackRoutine()
    {
        int animationFrameIndex = 0;
        while (animationFrameIndex < attackFrames.Length)
        {
            spriteRenderer.sprite = attackFrames[animationFrameIndex];
            animationFrameIndex++;
            yield return new WaitForSeconds(Time.deltaTime * animationFrameTime);
        }
    }
}
