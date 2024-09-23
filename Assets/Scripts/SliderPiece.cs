using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPiece : MonoBehaviour
{
    public Sprite[] frames;
    public float animationFrameTime = 10;

    private SpriteRenderer spriteRenderer;
    private Queue<IEnumerator> animationQueue;

    // *Awake is called when script instance is loaded
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // animation queue is always active
        animationQueue = new Queue<IEnumerator>();
        StartCoroutine(AnimationCoordinator());
    }

    // Update is called once per frame
    void Update()
    {
    }

    // inspired by https://discussions.unity.com/t/how-to-stack-coroutines-and-call-each-one-till-all-are-executed/219063/5
    IEnumerator AnimationCoordinator()
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

    public void Open()
    {
        animationQueue.Enqueue(OpenRoutine());
    }

    public void Close()
    {
        animationQueue.Enqueue(CloseRoutine());
    }

    IEnumerator OpenRoutine()
    {
        // play all frames of animation
        int animationFrameIndex = frames.Length - 1;
        while (animationFrameIndex >= 0)
        {
            spriteRenderer.sprite = frames[animationFrameIndex];
            animationFrameIndex--;
            yield return new WaitForSeconds(Time.deltaTime * animationFrameTime);
        }
    }

    IEnumerator CloseRoutine()
    {
        // play all frames of animation
        int animationFrameIndex = 0;
        while (animationFrameIndex < frames.Length)
        {
            spriteRenderer.sprite = frames[animationFrameIndex];
            animationFrameIndex++;
            yield return new WaitForSeconds(Time.deltaTime * animationFrameTime);
        }
    }

}