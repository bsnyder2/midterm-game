using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    // make this more like PlayerController script
    public Sprite[] idleFrames;
    public Sprite[] dieFrames;
    public float animationFrameTime;

    private SpriteRenderer spriteRenderer;
    private Queue<IEnumerator> animationQueue;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animationQueue = new Queue<IEnumerator>();
        StartCoroutine(AnimationCoordinator());
    }

    public void Die()
    {
        animationQueue.Enqueue(DieRoutine());
        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        collider.enabled = false;
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
