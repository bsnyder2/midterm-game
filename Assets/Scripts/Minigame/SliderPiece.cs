using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderPiece : MonoBehaviour
{
    public bool isTarget;
    private bool lastTarget;
    public Sprite[] standardFrames;
    public Sprite[] targetFrames;
    public float animationFrameTime;
    private Sprite[] frames;

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

    //public void BadFunction(bool isTarget)
    //{
    //    frames = isTarget ? targetFrames : standardFrames;
    //}

    private void Update()
    {
        // frames for this should be target if it's a target, not if otherwise
        frames = isTarget ? targetFrames : standardFrames;
        // if eye becomes a target
        if (!lastTarget && isTarget)
        {
            // set sprite to first frame
            spriteRenderer.sprite = frames[0];
            //Close();
        }
        // if eye stops being a target
        if (lastTarget && !isTarget)
        {
            spriteRenderer.sprite = frames[frames.Length - 1];
        }
        lastTarget = isTarget;
    }

    public void Open()
    {
        animationQueue.Enqueue(OpenRoutine());
    }

    public void Close()
    {
        animationQueue.Enqueue(CloseRoutine());
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

    private IEnumerator OpenRoutine()
    {
        // play all frames of animation
        //Sprite[] frames = isTarget ? targetFrames : standardFrames;
        int animationFrameIndex = 0;
        while (animationFrameIndex < frames.Length)
        {
            spriteRenderer.sprite = frames[animationFrameIndex];
            animationFrameIndex++;
            yield return new WaitForSeconds(Time.deltaTime * animationFrameTime);
        }
    }

    private IEnumerator CloseRoutine()
    {
        // play all frames of animation
        //Sprite[] frames = isTarget ? targetFrames : standardFrames;
        int animationFrameIndex = frames.Length - 1;
        while (animationFrameIndex >= 0)
        {
            spriteRenderer.sprite = frames[animationFrameIndex];
            animationFrameIndex--;
            yield return new WaitForSeconds(Time.deltaTime * animationFrameTime);
        }
    }

}
